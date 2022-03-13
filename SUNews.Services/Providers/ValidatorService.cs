namespace SUNews.Services.Providers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ValidatorService : IValidatorService
    {
        public void NullOrEmptyCollection(ICollection<string> collection)
        {
            if (collection == null || collection.Count() == 0 || collection.Contains(String.Empty))
            {
                throw new ArgumentNullException("Nullable values are not supported.");
            }

            return;
        }

        public void NullOrWhiteSpacesCheck(string parameter)
        {
            if (String.IsNullOrEmpty(parameter.ToString()) ||
                String.IsNullOrWhiteSpace(parameter.ToString()) ||
                parameter.ToString().Contains(' '))
            {
                throw new ArgumentNullException("Nullable or white spaces values are not supported.");
            }

            return;
        }

        public void ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var errorResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, errorResult, true);

            if (isValid)
            {
                return;
            }

            string error = String.Join(", ", errorResult.Select(e => e.ErrorMessage));

            throw new ArgumentException(error);
        }
    }
}
