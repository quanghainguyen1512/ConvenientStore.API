using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Helpers
{
    public class Helpers
    {
        public static int DateOfBirthToAge(DateTime dob)
        {
            var ts = DateTime.Now.Subtract(dob);
            var tempDate = DateTime.MinValue.Add(ts);

            return tempDate.Year - 1;
        }

        public static string Base64Encode(string str)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string encodedStr)
        {
            var encodedBytes = System.Convert.FromBase64String(encodedStr);
            return System.Text.Encoding.UTF8.GetString(encodedBytes);
        }
    }
}