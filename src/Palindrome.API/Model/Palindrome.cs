using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Palindromes.API.Model
{
    public class Palindrome
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
