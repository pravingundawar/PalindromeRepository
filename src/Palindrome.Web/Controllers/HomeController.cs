using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Palindrome.Web.Extensions;
using Palindrome.Web.Models;
using Palindrome.Web.Services;
using Palindrome.Web.ViewModels;

namespace Palindrome.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPalindromesService _palindromeSvc;

        public HomeController(IPalindromesService palindromeSvc) => _palindromeSvc = palindromeSvc;

        public async Task<IActionResult> Index(int? page)
        {
            var itemsPage = 100;
            var viewModel = await _palindromeSvc.GetPalindromes(page ?? 0, itemsPage);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new PalindromeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(PalindromeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!viewModel.Text.IsPalindrome())
                {
                    ModelState.AddModelError("Text", "Sentence is not a Palindrome.");
                }

                if (ModelState.IsValid)
                {
                    var newPalindrome = await _palindromeSvc.AddPalindrome(viewModel);
                    return RedirectPermanent("Index");
                }
            }

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
