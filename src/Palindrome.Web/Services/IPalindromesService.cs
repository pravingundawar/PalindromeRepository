using Palindrome.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Palindrome.Web.Services
{
    public interface IPalindromesService
    {
        Task<IndexViewModel> GetPalindromes(int page, int take);
        Task<PalindromeViewModel> AddPalindrome(PalindromeViewModel viewModel);
    }
}
