using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;
using Biz.Interfaces;
using Core.Domains;
using Rotativa;
using Web.ViewModels;
using System.IO;
using System.Configuration;
using System.Text;

namespace Web.Controllers
{
    public class FacilityController : Controller
    {

        #region Properties

        private readonly IFacilityService _facilityService;
        private const String USER_ACCESS_ERR_MSG = "Access to this page is Restricted.";
        private const String USER_LOGIN_ERR_MSG = "You are not logged-in. Please login.";

        #endregion

        #region Constructor

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        #endregion


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

        // GET: Student
        public ActionResult FacilityList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {

                    var facilities = _facilityService.GetAll();
                    var model = new StandardIndexViewModel(facilities);
                    return View("FacilityList",model);
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

        public ViewResult InactiveFacilityList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var facilities = _facilityService.GetAllInactive();
                    var model = new StandardIndexViewModel(facilities);
                    return View("FacilityList", model);
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
            return View("Error");

        }

        public ActionResult ViewReport()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var facilities = _facilityService.GetAll();
                    var model = new StandardIndexViewModel(facilities);
                    return View("ViewReport", model);
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ViewAsPdf(string GridHtml)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var facilities = _facilityService.GetAll();
                    var model = new StandardIndexViewModel(facilities);
                    var pdfResult = new Rotativa.PartialViewAsPdf("ViewReport", model);
                    return (pdfResult);
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });
        }


        // GET: Student/Create
        public ActionResult Create()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacilityViewModel model)
        {

            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            var facility = new Facility()
                            {
                                Id = model.Id,
                                Name = model.Name,
                                Landmark = model.Landmark,
                                Address = model.Address,
                                Address2 = model.Address2,
                                City = model.City,
                                State = model.State,
                                ZipCode = model.ZipCode,
                                IsActive = true
                            };

                            _facilityService.InsertOrUpdate(facility);
                            return RedirectToAction("FacilityList");
                        }
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }

        public ActionResult Edit(int? id)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    var facility = _facilityService.GetById(id??0);

                    if (facility == null)
                    {
                        return HttpNotFound();
                    }

                    var model = new FacilityViewModel(facility);
                    return View("Edit",model);
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(FacilityViewModel model)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            var facility = new Facility()
                            {
                                Id = model.Id,
                                Name = model.Name,
                                Landmark = model.Landmark,
                                Address = model.Address,
                                Address2 = model.Address2,
                                City = model.City,
                                State = model.State,
                                ZipCode = model.ZipCode,
                                IsActive = model.IsActive
                            };

                            _facilityService.InsertOrUpdate(facility);
                            return RedirectToAction("FacilityList");
                        }
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }

                    return RedirectToAction("Edit",model);
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", USER_LOGIN_ERR_MSG);
            }
            return RedirectToAction("Login", "Standard", new { area = "" });

        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    var facility = _facilityService.GetById(id ?? 0);

                    if (facility == null)
                    {
                        return HttpNotFound();
                    }

                    var model = new FacilityViewModel(facility);
                    return View("Details", model);
                }
                else
                {
                    ModelState.AddModelError("", USER_ACCESS_ERR_MSG);
                    return View();
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
