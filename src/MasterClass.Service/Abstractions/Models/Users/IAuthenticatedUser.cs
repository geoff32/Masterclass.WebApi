namespace MasterClass.Service.Abstractions.Models.Users
{
    public interface IAuthenticatedUser
    {
        int Id { get; }
        string Token { get; }
    }
}