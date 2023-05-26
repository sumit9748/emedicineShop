using Emedicine.DAL.model;


namespace EMedicine.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string userName, string password);
    }
}
