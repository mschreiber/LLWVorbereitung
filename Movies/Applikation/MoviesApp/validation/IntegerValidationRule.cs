using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoviesApp.validation
{
    public class IntegerValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; } = "Keine gültige Zahl";

        public int Max { get; set; } = int.MaxValue;
        public int Min { get; set; } = int.MinValue;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool ok = int.TryParse(value.ToString(), out int result);
            if (!ok || result < Min || result > Max)
            {
                return new ValidationResult(false, ErrorMessage);
            }
            return new ValidationResult(true, null);
        }
    }
}
