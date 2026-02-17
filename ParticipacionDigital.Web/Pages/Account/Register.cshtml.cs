using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ParticipacionDigital.Core.Entities;
using ParticipacionDigital.Core.Enums;
using ParticipacionDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ParticipacionDigital.Web.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly AppDbContext _context;

        public RegisterModel(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ILogger<RegisterModel> logger,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public List<Alcaldia> Alcaldias { get; set; } = new List<Alcaldia>();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Correo electrónico")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y máximo {1} caracteres de longitud.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }
            
            [Required(ErrorMessage = "El nombre es obligatorio")]
            [Display(Name = "Nombre Completo")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "La cédula es obligatoria")]
            [Display(Name = "Cédula de Identidad")]
            public string Cedula { get; set; }
            
            [Display(Name = "Dirección")]
            public string Direccion { get; set; }
            
            [Display(Name = "Teléfono")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Seleccione su alcaldía")]
            [Display(Name = "Alcaldía / Municipio")]
            public int AlcaldiaId { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Alcaldias = await _context.Alcaldias.OrderBy(a => a.Nombre).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
                var user = new Usuario { 
                    UserName = Input.Email, 
                    Email = Input.Email,
                    Nombre = Input.Nombre,
                    Cedula = Input.Cedula,
                    Direccion = Input.Direccion,
                    PhoneNumber = Input.PhoneNumber,
                    AlcaldiaId = Input.AlcaldiaId,
                    Rol = RolUsuario.Ciudadano,
                    FechaRegistro = DateTime.UtcNow,
                    Activo = true,
                    EmailConfirmed = true // Confirmado por defecto para facilitar pruebas
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, RolUsuario.Ciudadano.ToString());

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Redisplay form if something failed
            Alcaldias = await _context.Alcaldias.OrderBy(a => a.Nombre).ToListAsync();
            return Page();
        }
    }
}
