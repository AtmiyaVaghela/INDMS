using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace INDMS.WebUI.ViewModels {

    public class ChangePassword {

        [Required(ErrorMessage = "Current Password Required")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password Required")]
        [Display(Name = "New Password")]
        [NotEqual(PropName = "CurrentPassword", ErrorMessage = "The Current Password and New Password are equal!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Re-type Password Required")]
        [Display(Name = "Re-type Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords don't match.")]
        public string ReEnterPassword { get; set; }
    }

    public class NotEqualAttribute : ValidationAttribute {
        public string PropName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(PropName);

            var otherPropertyStringValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null).ToString();

            if (Equals(value.ToString(), otherPropertyStringValue)) {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}