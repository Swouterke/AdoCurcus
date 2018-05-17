using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using AdoLibrary;

namespace AdoCurcus
{
    public class IngevuldGroterDanNul : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal getal;
            NumberStyles style = NumberStyles.Currency;
            if (value == null || value.ToString() == string.Empty)
                return ValidationResult.ValidResult;
            if (!decimal.TryParse(value.ToString(), style, cultureInfo, out getal))
                return new ValidationResult(false, "Waarde moet een getal zijn");
            if (getal <= 0)
                return new ValidationResult(false, "Getal moet groter dan 0 zijn");
            return ValidationResult.ValidResult;
        }   
    }
}
