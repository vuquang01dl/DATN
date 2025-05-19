using ThucTapW1._1.Payloads.DataRequests;
using TT1.Entities;
using TT1.Payloads.DataRequests;
using TT1.Payloads.DataResponses;
using TT1.Payloads.Responses;

namespace TT1.Services.Interfaces
{
    public interface IUserService
    {
        ResponseObject<DataResponseUser> Register(Request_Register request);
        DataResponseToken GenerateAccessToken(Account account);
        DataResponseToken RenewAccessToken(Request_RenewAccessToken request);
        ResponseObject<DataResponseToken> Login(Request_Login request);
        IQueryable<DataResponseUser> GetAll();
        ResponseObject<DataResponseUser> ForgotPassword(string email);
        ResponseObject<DataResponseUser> ResetPassword(Request_ResetPassword request);
        DataResponseUser GetUserById(int userId);
        DataResponseUser UpdateUser(int userId, Request_UpdateUser request);

    }
}
