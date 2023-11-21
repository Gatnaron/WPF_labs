using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace meeeh.ValidationRules
{
    internal class CodeRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
       System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                Page1.canSave = false;
                return new ValidationResult(false, "Код товара введён некорректно! ");
            }
            string code = value.ToString();
            Regex regex = new Regex(@"\D{3}-\d{3}-\d{3}-\D{3}-\d{3}");
            if (regex.IsMatch(code))
            {
                Page1.canSave = true;
                Console.WriteLine("1");
                return new ValidationResult(true, null);
            }
            else
            {
                Page1.canSave = false;
                return new ValidationResult(false,
                   "Код товара должен иметь формат XXX-000-000-XXX-000");
            }
        }
    }
}
