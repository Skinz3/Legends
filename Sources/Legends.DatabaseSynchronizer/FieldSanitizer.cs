using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer
{
    public class FieldSanitizer
    {
        public static object Sanitize(string value, Type fieldType)
        {
            if (value == "No")
            {
                return false;
            }
            else if (value.ToString() == "Yes")
            {
                return true;
            }
            else if (value.ToString() == true.ToString() && fieldType != typeof(Boolean))
            {
                return 1;
            }
            else if (value.ToString() == false.ToString() && fieldType != typeof(Boolean))
            {
                return 0;
            }
            if (fieldType == typeof(Boolean) && value == "0")
            {
                return false;
            }
            if (fieldType == typeof(Boolean) && value == "1")
            {
                return true;
            }
            else if (value.ToString().Split('.').Last() == "0")
            {
                return value.ToString().Split('.')[0];
            }
            else if (value.ToString().Contains('.') && !value.Contains("troybin"))
            {
                return value.ToString().Replace('.', ',');

            }
            else if (value.ToString().StartsWith("."))
            {
                return "0" + value.ToString().Replace('.', ',');
            }
            return value;
        }
    }
}
