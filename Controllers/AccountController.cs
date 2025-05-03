using System.Web.Security;
using System.Linq;
using System.Web.Mvc;
using EncuestasMVC.Models;

namespace EncuestasMVC.Controllers
{
    public class AccountController : Controller
    {
        private EncuestasDbContext db = new EncuestasDbContext();

        // GET: /Account/Login
        public ActionResult Login()
        {
           
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Debe llenar todos los campos.";
                return View();
            }

            var user = db.Usuarios.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Index", "Encuestas");
            }

            ViewBag.Error = "Credenciales incorrectas.";
            return View();
        }

        // GET: /Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");

        }
    }
}
