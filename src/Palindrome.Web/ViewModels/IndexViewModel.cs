using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Palindrome.Web.ViewModels
{
    public class IndexViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<PalindromeViewModel> Data { get; set; }
    }
}
