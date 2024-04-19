using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoviesApp.validation
{
    public class StringNotEmptyValidationRule : ValidationRule
    {

        public string ErrorMessage { get; set; } = "Wert darf nicht leer sein";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return new ValidationResult(true, null);
        }
    }
}
