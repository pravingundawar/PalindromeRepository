using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Palindrome.Web.Extensions
{
    public static class StringExtensions
    {

        public static bool IsPalindrome(this string str)
        {
            var allChars = str.Where(c => Char.IsLetter(c)).Select(c => Char.ToLower(c)).ToArray();

            char[] allCharsReverse = allChars.Reverse().ToArray();
            return new string(allChars) == new string(allCharsReverse);
        }

    }
}
