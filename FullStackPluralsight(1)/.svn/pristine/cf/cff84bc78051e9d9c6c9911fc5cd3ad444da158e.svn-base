using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FullStackPluralsight.Models
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)      //"value" would be the value of the date
        {
            string[] timeFormats = { "HH:mm", "H:mm" };
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                timeFormats,
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);

            return (isValid);
        }
    }
}