using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Palindrome.Web.ViewModels
{
    public class PalindromeViewModel
    {
        public int Id { get; set; }

        [Required()]
        [MaxLength(1000)]
        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
