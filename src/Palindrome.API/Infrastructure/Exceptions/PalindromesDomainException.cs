using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Palindromes.API.Infrastructure.Exceptions
{
    public class PalindromesDomainException : Exception
    {
        public PalindromesDomainException()
        { }

        public PalindromesDomainException(string message)
            : base(message)
        { }

        public PalindromesDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

}
