using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using ThucTapW1._1.Payloads.DataRequests;
using ThucTapW1.Entities;
using TT1.AppDbContexts;
using TT1.Entities;
using TT1.Handle.Email;
using TT1.Payloads.Converters;
using TT1.Payloads.DataRequests;
using TT1.Payloads.DataResponses;
using TT1.Payloads.Responses;
using TT1.Services.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TT1.Services.Implements
{
    public class UserService : BaseService, IUserService
    {

        private readonly ResponseObject<DataResponseUser> _responseObject;
        private readonly UserConverter _converter;
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<DataResponseToken> _responseTokenObject;
        private readonly HttpContextAccessor _httpContextAccessor;

        public UserService(IConfiguration configuration)
        {
            _converter = new UserConverter();
            _responseObject = new ResponseObject<DataResponseUser>();
            _configuration = configuration;
            _responseTokenObject = new ResponseObject<DataResponseToken>();
            _httpContextAccessor = new HttpContextAccessor();
        }


        // service đăng ký tài khoản
        public ResponseObject<DataResponseUser> Register(Request_Register request)
        {
            if (string.IsNullOrWhiteSpace(request.user_name)
             || string.IsNullOrWhiteSpace(request.password)
             || string.IsNullOrWhiteSpace(request.email)
             || string.IsNullOrWhiteSpace(request.phone)
             || string.IsNullOrWhiteSpace(request.address)
             || string.IsNullOrWhiteSpace(request.avata)

             )  // nếu một trong các thông tin trên mà bị trống
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ", null);
            }
            if (_context.Users.Any(x => x.email.Equals(request.email)))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại trên hệ thống", null);
            }
            if (_context.Users.Any(x => x.user_name.Equals(request.user_name)))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "User name đã tồn tại trên hệ thống", null);
            }
            if (!Validate.IsValidEmail(request.email))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng email không hợp lệ", null);
            }

            Account account = new Account();
            account.user_name = request.user_name;
            account.avata = request.avata;
            account.password = BCryptNet.HashPassword(request.password);
            account.decentralizationId = request.decentralizationId;
            account.status = 1;
            account.created_at = DateTime.Now;
            account.updated_at = DateTime.Now;

            _context.Accounts.Add(account);
            _context.SaveChanges();

            User user = new User();
            user.user_name = request.user_name;
            user.phone = request.phone;
            user.email = request.email;
            user.address = request.address;
            user.accountId = account.Id;
            user.create_at = DateTime.Now;
            user.update_at = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();

            return _responseObject.ResponseSucess("Dang ky thanh cong", _converter.EntityToDTO(user));
        }
        // tạo token mới
        public DataResponseToken RenewAccessToken(Request_RenewAccessToken request)
        {
            throw new NotImplementedException();
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var item = RandomNumberGenerator.Create())
            {
                item.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }



        public DataResponseToken GenerateAccessToken(Account account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

            var decentralization = _context.Decentralizations.SingleOrDefault(x => x.Id == account.decentralizationId);
            // mô tả token
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", account.Id.ToString()),
                    new Claim("User_Name", account.user_name.ToString()),
                    new Claim(ClaimTypes.Role, decentralization.Code),

                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            // tạo token
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            // hiển thị token dưới dạng chuỗi
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpiredTime = DateTime.Now.AddDays(1),
                accountId = account.Id
            };
            _context.RefreshTokens.Add(rf);
            _context.SaveChanges();

            DataResponseToken resoult = new DataResponseToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken

            };
            return resoult;

        }

        public ResponseObject<DataResponseToken> Login(Request_Login request)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.user_name.Equals(request.user_name));
            if (string.IsNullOrWhiteSpace(request.user_name) || string.IsNullOrWhiteSpace(request.password))
            {
                return _responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin", null);
            }
            bool checkPass = BCryptNet.Verify(request.password, account.password);// so sánh mật khẩu vừa nhập và mật khẩu trong DB
            if ((!checkPass)) // nếu mật khẩu sai
            {
                return _responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không chính xác", null);
            }
            return _responseTokenObject.ResponseSucess("Dang nhap thanh cong", GenerateAccessToken(account));

        }

        public IQueryable<DataResponseUser> GetAll()
        {
            //if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    throw new UnauthorizedAccessException("Người dùng không được xác thực hoặc không được xác định");
            //}

            //if (!_httpContextAccessor.HttpContext.User.IsInRole("manager"))
            //{
            //    throw new UnauthorizedAccessException("Người dùng không có quyền sử dụng chức năng này");
            //}

            var result = _context.Users.Select(x => _converter.EntityToDTO(x));
            return result;
        }


        // xử lý quên mật khẩu

        public ResponseObject<DataResponseUser> ForgotPassword(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.email.Equals(email));
            if (user == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Email không tìm thấy trong DB", null);
            }


            var resetToken = GenerateResetToken();


            var refreshToken = new RefreshToken
            {
                Token = resetToken,
                ExpiredTime = DateTime.Now.AddHours(1),
                accountId = user.accountId
            };

            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();


            SendPasswordResetEmail(user.email, resetToken);

            return _responseObject.ResponseSucess("Password reset instructions sent to your email", null);
        }

        public ResponseObject<DataResponseUser> ResetPassword(Request_ResetPassword request)
        {
            var user = _context.Users.SingleOrDefault(usr => usr.email.Equals(request.email));
            if (user == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "User not found", null);
            }


            var refreshToken = _context.RefreshTokens.SingleOrDefault(rt => rt.accountId == user.accountId && rt.Token == request.resetToken);

            if (refreshToken == null || refreshToken.ExpiredTime < DateTime.Now)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Invalid or expired reset token", null);
            }


            var account = _context.Accounts.SingleOrDefault(acc => acc.Id == user.accountId);

            if (account == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Account not found", null);
            }


            account.password = BCryptNet.HashPassword(request.newPassword);


            _context.RefreshTokens.Remove(refreshToken);
            _context.SaveChanges();

            return _responseObject.ResponseSucess("Password reset successfully", null);
        }


        private string GenerateResetToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        private void SendPasswordResetEmail(string email, string resetToken)
        {
            var emailTo = new EmailTo
            {
                Mail = email,
                Subject = "Ma reset mat khau cua ban tao thanh cong",
                Content = $"ma resetToken cua ban la: {resetToken}"
            };

            SendEmail(emailTo);
        }

        // Sử dụng Mật khẩu Ứng dụng của bạn để xác thực Gmail
        private const string SmtpPassword = "kvyz gumz rimx kcdn";

        public string SendEmail(EmailTo emailTo)
        {
            if (!Validate.IsValidEmail(emailTo.Mail))
            {
                return "Định dạng email không hợp lệ";
            }
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vuquang01dl@gmail.com", "kvyz gumz rimx kcdn"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("vuquang01dl@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }


        public DataResponseUser GetUserById(int userId)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return null;
            }

            return _converter.EntityToDTO(user);
        }

        public DataResponseUser UpdateUser(int userId, Request_UpdateUser request)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return null;
            }


            user.phone = request.phone;
            user.email = request.email;
            user.address = request.address;
            _context.SaveChanges();
            return _converter.EntityToDTO(user);
        }
    }

}

