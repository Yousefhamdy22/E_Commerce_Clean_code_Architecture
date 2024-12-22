namespace E_Commerce.Services.Abstraction.IEmailServices
{
    public interface IEmailServices
    {

        public Task<string> SendEmail(string email, string Message, string? reason);
    }
}
