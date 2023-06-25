namespace NewsMVC.Models.DomainModels.CustomValidators
{
    public class FromNowToAfterOneWeek : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value ==null|| DateTime.Now.CompareTo(value) <= 0 && DateTime.Now.AddDays(7).CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Date must be between today and a week from today");
        }
    }
}
