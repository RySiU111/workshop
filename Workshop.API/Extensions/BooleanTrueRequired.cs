using System.ComponentModel.DataAnnotations;

namespace Workshop.API.Extensions
{
    public class BooleanTrueRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (bool)value == true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Pole {name} musi mieć wartość równą true";
        }
    }
}