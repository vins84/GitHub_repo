using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FullStackPluralsight.Models
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)      //"value" would be the value of the date
        {
            string[] dateFormats = { "d/M/yyyy", "d/MM/yyyy", "dd/MM/yyyy", "d MMM yyyy", "dd/mm/yyyy" };

            DateTime dateTime;
            
            var isValid = DateTime.TryParseExact(Convert.ToString(value), 
                dateFormats, 
                CultureInfo.CurrentCulture, 
                DateTimeStyles.None, 
                out dateTime);

            return (isValid && dateTime > DateTime.Now);        // Checking if the date is in the future.

            //return base.IsValid(value);       //Now this is obsolete
        }
    }


    
}