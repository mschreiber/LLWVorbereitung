using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoviesApp.validation
{
    public class DoubleValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; } = "Keine gültige Zahl";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool ok = double.TryParse(value.ToString(), out double result);
            if (!ok)
            {
                return new ValidationResult(false, ErrorMessage);
            }

            return new ValidationResult(true, null);
        }
    }
}
