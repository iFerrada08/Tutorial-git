using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDeveloper.MvcIdentity.Model;

namespace WebDeveloper.MvcIdentity.Controllers
{
    public class HomeController : Controller
    {
        // La clase UserManager nos va a ayudar a registrar los usuarios bajo el modelo de Identity
        private readonly UserManager<ChinookUser> _userManager;
        private readonly SignInManager<ChinookUser> _signInManager;

        // Crear el IEmailService para el envio de correos con MailKit
        private readonly IEmailService _emailService;
        public HomeController(UserManager<ChinookUser> userManager, SignInManager<ChinookUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            // Guardar el returnUrl en el ViewData
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            // Tratar de encontrar el usuario en BD
            var user = await _userManager.FindByNameAsync(username);

            if(user != null)
            {
                // Iniciar sesion
                var resultadoInicio = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (resultadoInicio.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // Crear el usuario como IdentityUser
            var user = new ChinookUser
            {
                UserName = username,
                Email = "",
                Dni = "12345678"
            };

            // Realizar el registro en la BD
            var resultadoRegistro = await _userManager.CreateAsync(user, password);

            // Si se creo el usuario, enviar el correo para que verifique su mail
            if(resultadoRegistro.Succeeded)
            {
                // Generar el token para que el usuario confirme su cuenta
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var urlLink = Url.Action("VerificarCuenta", "Home", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());

                await _emailService.SendAsync("correo@mail.com", "Confirma tu cuenta", $"<a href=\"{urlLink}\">Verificar Email</a>", true);

            }
            return View();
        }

        public async Task<IActionResult> VerificarCuenta(string userId, string token)
        {
            // Obtener el usuario
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return BadRequest();
            }

            // Si el usuario existe, confirmarlo
            var resultadoConfirmar = await _userManager.ConfirmEmailAsync(user, token);

            if (resultadoConfirmar.Succeeded)
            {
                return View();
            }

            // En cualquier otro caso retornar un error
            return BadRequest();
        }


        public IActionResult RedirectToLocal(string returnUrl)
        {
            // Esta funcion va a tener la logica de redireccionar a otra URL
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
