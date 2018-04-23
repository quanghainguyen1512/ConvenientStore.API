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
    }
}
