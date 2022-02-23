using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5.Service.Exp
{
    public class SomeArbClass
    {
        public string ReverseString(string someString)
        {
            return new string(someString.Reverse().ToArray());
        }
    }
}
