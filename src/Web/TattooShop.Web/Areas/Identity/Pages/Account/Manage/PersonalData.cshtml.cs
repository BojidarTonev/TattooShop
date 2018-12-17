using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Web.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<TattooShopUser> _userManager;
        private readonly IUsersService _usersService;
        private readonly IHomeService _homeService;

        public PersonalDataModel(
            UserManager<TattooShopUser> userManager,
            IUsersService usersService,
            IHomeService homeService)
        {
            _userManager = userManager;
            _usersService = usersService;
            _homeService = homeService;
        }

        public List<Book> Books { get; set; }

        public List<Order> Orders { get; set; }

        public List<ContactInfo> Feedback { get; set; }

        public string RegisteredEmail { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            this.RegisteredEmail = user.Email;
            this.Books = this._usersService.GetUserBooks(user.Id)?.ToList();
            this.Orders = this._usersService.GetUserOrders(user.Id)?.ToList();
            this.Feedback = this._homeService.AllFeedback().ToList();

            return Page();
        }
    }
}