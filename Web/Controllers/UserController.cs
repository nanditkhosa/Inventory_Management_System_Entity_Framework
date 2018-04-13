using System;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;
using Biz.Interfaces;
using Core.Domains;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserService _userService;
        public readonly IFacilityService _facilityService;
        private const String USER_ACCESS_ERR_MSG = "Access to this page is Restricted.";
        private const String USER_LOGIN_ERR_MSG = "You are not logged-in. Please login.";


        public UserController(IUserService userService, IFacilityService facilityService)
        {
            _userService = userService;
            _facilityService = facilityService;
        }

        public UserController(IUserService userService)
        {
            _userService = userService;
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

        // GET: User    
        public ActionResult UserList()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var users = _userService.GetAll();
                    var model = new StandardIndexViewModel(users);
                    return View("UserList", model);
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
            return RedirectToAction("Login","Standard", new { area = "" });

        }

        // GET: User    
        public ActionResult InactiveUserList()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var users = _userService.GetAllInactive();
                    var model = new StandardIndexViewModel(users);
                    return View("UserList", model);
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

        // GET: User/Details/5
        public ActionResult Details(int id)
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

        // GET: User/Create
        public ActionResult Create()
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var facilities = _facilityService.GetAll();
                    var model = new UserViewModel {
                    ListOfAllFacilities = facilities.ToList()
                    };
                    return View("Create",model);
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
            return RedirectToAction("Login","Standard", new { area = "" });
        }
       
        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {
                        var user_in_db = _userService.GetByUserName(userViewModel.UserName);
                        if (user_in_db == null)
                        {
                            var user = new User()
                            {
                                Id = userViewModel.Id,
                                UserName = userViewModel.UserName,
                                IsActive = userViewModel.IsActive,
                                FirstName = userViewModel.FirstName,
                                LastName = userViewModel.LastName,
                                Role = userViewModel.Role,
                                PasswordHash = Guid.NewGuid().ToString("d").Substring(1, 8)
                            };

                            _userService.Insert(user, userViewModel.ListOfFacilityIds);

                            return RedirectToAction("UserList");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Username exists in the system,please try with a different email or contact the admin");
                            return RedirectToAction("Create");
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                        return View();
                    }
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    var user = _userService.GetById(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    var model = new UserViewModel(user);
                    model.ListOfAllFacilities = _facilityService.GetAll().ToList();
                    return View("Edit", model);
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

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserViewModel model)
        {
            if (IsUserLoggedIn())
            {
                if (IsAdmin())
                {
                    try
                    {

                        var user = _userService.GetById(id);

                        user.Id = model.Id;
                        user.UserName = model.UserName;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Role = model.Role;
                        user.IsActive = model.IsActive;
                        // TODO: Add update logic here
                        _userService.Update(user, model.ListOfFacilityIds);

                        return RedirectToAction("UserList");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                        return View();
                    }
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
    }
}
