namespace SUNews.Services.Providers
{
    public interface IValidatorService
    {
        public void NullOrWhiteSpacesCheck(string parameter);

        public void NullOrEmptyCollection(ICollection<string> collection);

        public void ValidateModel(object model);

        public (bool, Guid) TryParseGuid(string guidId);
    }
}
