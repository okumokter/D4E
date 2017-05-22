using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4ECore.Common.Types
{
    public static class SignedIntegerExtension
    {
        public static string Signed(this int value)
        {
            return (value > -1 ? $"+{value}" : $"{value}");
        }
    }
}
