using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class RoleController : Controller
    {
        RoleService _roleService = new RoleService();

        // GET: Role
        public ActionResult Index()
        {
            var roles = _roleService.GetAll();
            return View(roles);
        }

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            _roleService.CreateRole(Role);
            return RedirectToAction("Index");
        }
    }
}