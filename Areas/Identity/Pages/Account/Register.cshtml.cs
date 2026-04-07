#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using PruebaTecnicaRossmon.DTOs;

namespace PruebaTecnicaRossmon.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(
            ILogger<RegisterModel> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }  // Se usa como Username

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return Page();

            returnUrl ??= Url.Content("~/");

            var client = _httpClientFactory.CreateClient("Api");

            var dto = new RegisterUserDto
            {
                Username = Input.Email.Trim(),
                Password = Input.Password,
                Role = "User"
            };

            var response = await client.PostAsJsonAsync("Auth/register", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al registrar usuario");
                return Page();
            }

            _logger.LogInformation("Usuario registrado correctamente");

            return RedirectToPage("/Account/Login");
        }
    }
}