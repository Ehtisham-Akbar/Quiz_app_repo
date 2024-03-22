using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class BusinessManagerBase
    {
        public static string Truncate(string input, int length)
        {
            string retstr = "";
            if (!string.IsNullOrEmpty(input))
            {


                if (input.Length <= length)
                {
                    return retstr = input;
                }
                else
                {
                    retstr = input.Substring(0, length) + "...";
                    return retstr;
                }
            }

            return retstr;
        }

    }
}