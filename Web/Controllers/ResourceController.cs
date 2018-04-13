using System;
using System.Web.Mvc;
using Web.ViewModels;
using Biz.Interfaces;
using Core.Domains;
using System.Linq;

namespace Web.Controllers
{
    public class ResourceController : Controller
    
    {
        public readonly IResourceService _resourceService;
        public readonly IFacilityService _facilityService;
        private const String USER_ACCESS_ERR_MSG = "Access to this page is Restricted.";
        private const String USER_LOGIN_ERR_MSG = "You are not logged-in. Please login.";

        public ResourceController(IResourceService resService, IFacilityService facilityService)
        {
            _resourceService = resService;
            _facilityService = facilityService;
        }

        public ResourceController(IResourceService resourceService)
        {
            
            _resourceService = resourceService;
        }

        public bool IsUserLoggedIn()
        {
            if (Session["userId"] == null)
            {
                return false;
            }
            return true;
        }

        public bool IsAdmin()
        {
            if (!Session["role"].Equals("admin"))
            {
                return false;
            }
            return true;
        }

        // GET: Resource/Details/5
        public ActionResult ResourceList()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {
                        var resourceList = _resourceService.GetAll();
                        var model = new StandardIndexViewModel(resourceList);
                        return View("ResourceList", model);
                    }
                    catch(Exception e)
                    {
                        ModelState.AddModelError("", "Unable to Retrive Resources. PLease try again later.");
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View("Error");
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }


        public ActionResult InactiveResourceList()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {
                        var resourceList = _resourceService.GetAllInactive();
                        var model = new StandardIndexViewModel(resourceList);
                        return View("ResourceList", model);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View("Error");
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }

        // GET: Resource/Create
        public ActionResult Create()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var facilities = _facilityService.GetAll();
                    var model = new ResourceViewModel
                    {
                        ListOfAllFacilities = facilities.ToList()
                    };
                    return View("Create", model);
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View("Error");
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }

        // POST: Resource/Create
        [HttpPost]
        public ActionResult Create(ResourceViewModel resourceViewModel)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            var resource = new Resource()
                            {
                                Name = resourceViewModel.Name,
                                Description = resourceViewModel.Description,
                                InitialCount = resourceViewModel.InitCount,
                                IsActive = resourceViewModel.IsActive,
                                FacilityId = resourceViewModel.FacilityId
                            };

                            _resourceService.InsertOrUpdate(resource);
                            return RedirectToAction("ResourceList");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.Write(e);
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return RedirectToAction("Error");
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }

        // GET: Resource/Edit/5
        public ActionResult Edit(int id)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var resource = _resourceService.GetById(id);
                    if (resource == null)
                    {
                        return HttpNotFound();
                    }

                    var model = new ResourceViewModel(resource);
                    model.ListOfAllFacilities = _facilityService.GetAll().ToList();
                    return View("Edit", model);
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return RedirectToAction("Error");
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }


        // POST: Resource/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ResourceViewModel model)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {
                        var resource = new Resource()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            InitialCount = model.InitCount,
                            CurrentCount = model.CurrentCount,
                            Comment = model.Comment,
                            Description = model.Description,
                            IsActive = model.IsActive,
                            FacilityId = model.FacilityId
                        };
                        _resourceService.InsertOrUpdate(resource);

                        return RedirectToAction("ResourceList");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Unable to save changes now. Please try again later.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return RedirectToAction("Error");
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });
        }
    }
}
