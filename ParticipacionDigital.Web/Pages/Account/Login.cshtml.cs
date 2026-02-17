using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParticipacionDigital.Core.Entities;

namespace ParticipacionDigital.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ParticipacionDigital.Web.Services.SecurityService _securityService;
        private readonly ParticipacionDigital.Infrastructure.Data.AppDbContext _dbContext;

        public LoginModel(
            SignInManager<Usuario> signInManager, 
            UserManager<Usuario> userManager,
            ILogger<LoginModel> logger,
            ParticipacionDigital.Web.Services.SecurityService securityService,
            ParticipacionDigital.Infrastructure.Data.AppDbContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _securityService = securityService;
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var ipAddress = _securityService.GetClientIpAddress();
                var encryptedIp = _securityService.Encrypt(ipAddress);
                var userAgent = Request.Headers["User-Agent"].ToString();

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                
                var user = await _userManager.FindByEmailAsync(Input.Email);
                var userId = user?.Id.ToString();

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    
                    // Audit Log Success
                    _dbContext.AuditLogs.Add(new AuditLog
                    {
                        UsuarioId = userId,
                        Action = "Login",
                        Details = "Successful Login",
                        IpAddress = encryptedIp,
                        UserAgent = userAgent,
                        Timestamp = DateTime.UtcNow
                    });
                    await _dbContext.SaveChangesAsync();

                    if (user.MustChangePassword)
                    {
                        return Redirect("~/account/change-password");
                    }

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    
                    // Fetch Reason
                    var amonestacion = _dbContext.Amonestaciones
                        .Where(a => a.UsuarioId == int.Parse(userId) && a.FechaFin > DateTime.UtcNow)
                        .OrderByDescending(a => a.Fecha)
                        .FirstOrDefault();

                    if (amonestacion != null)
                    {
                        ModelState.AddModelError(string.Empty, $"Tu cuenta ha sido suspendida hasta el {amonestacion.FechaFin.ToLocalTime():g}. Razón: {amonestacion.Razon}");
                        return Page(); // Stay on page to show error
                    }

                     // Audit Log Lockout
                    _dbContext.AuditLogs.Add(new AuditLog
                    {
                        UsuarioId = userId,
                        Action = "Login LockedOut",
                        Details = "Account Locked Out",
                        IpAddress = encryptedIp,
                        UserAgent = userAgent,
                        Timestamp = DateTime.UtcNow
                    });
                    await _dbContext.SaveChangesAsync();

                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    
                    // Audit Log Failure
                    _dbContext.AuditLogs.Add(new AuditLog
                    {
                        UsuarioId = userId, // Might be null if user doesn't exist
                        Action = "Login Failed",
                        Details = "Invalid Password or User not found",
                        IpAddress = encryptedIp,
                        UserAgent = userAgent,
                        Timestamp = DateTime.UtcNow
                    });
                    await _dbContext.SaveChangesAsync();

                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}


