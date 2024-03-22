using EatGoodNaija.Server.Model;
using EatGoodNaija.Server.Model.DTO;
using EatGoodNaija.Server.Model.DTO.authenticationDTO;
using EatGoodNaija.Server.Services.Interface;
using MailKit;
using Microsoft.AspNetCore.Identity;

namespace EatGoodNaija.Server.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenGeneratorService _tokenGeneratorService;
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<Vendor> userManager,
        IJwtTokenGeneratorService tokenGeneratorService,
        IAuthRepository authRepository,
        ILogger<AuthService> logger,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService)
        {
            _tokenGeneratorService = tokenGeneratorService;
            _authRepository = authRepository;
            _logger = logger;
            _roleManager = roleManager;
            _emailService = emailService;
        }
        public async Task<ResponseDTO<string>> ConfirmEmailAsync(int token, string email)
        {
            var response = new ResponseDTO<string>();
            try
            {
                var findUser = await _authRepository.FindUserByEmailAsync(email);
                if (findUser == null)
                {
                    response.ErrorMessage = new List<string>() { "There is no user with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var retrieveToken = await _authRepository.retrieveUserToken(findUser.Id);
                if (retrieveToken == null)
                {
                    response.ErrorMessage = new List<string>() { "Error user token token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                if (retrieveToken.Token != token)
                {
                    response.ErrorMessage = new List<string>() { "Invalid user token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var deleteToken = await _authRepository.DeleteUserToken(retrieveToken);
                if (deleteToken == false)
                {
                    response.ErrorMessage = new List<string>() { "Error removing user token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                findUser.EmailConfirmed = true;
                var updateUserConfirmState = await _authRepository.UpdateUserInfo(findUser);
                if (updateUserConfirmState == false)
                {
                    response.ErrorMessage = new List<string>() { "Error in confirming user token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully comfirm user token";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessage = new List<string>() { "Error in confirming user token" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDTO<string>> ForgotPasswordAsync(string userEmail)
        {
            var response = new ResponseDTO<string>();
            try
            {
                var checkUser = await _authRepository.FindUserByEmailAsync(userEmail);
                if (checkUser == null)
                {
                    response.ErrorMessage = new List<string>() { "Email is not available" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var result = await _authRepository.ForgotPassword(checkUser);
                if (result == null)
                {
                    response.ErrorMessage = new List<string>() { "Error in generating reset token for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var message = new Message(new string[] { checkUser.Email }, "Reset Password Code", $"<p>Your reset password code is below<p><br/><h6>{result}</h6><br/> <p>Please use it in your reset password endpoint</p>");
                _emailService.SendEmail(message);
                response.DisplayMessage = "Token generated Successfully";
                response.Result = result;
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessage = new List<string>() { "Error in generating reset token for user" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDTO<string>> LoginAsync(loginDTO loginDto)
        {
            var response = new ResponseDTO<string>();
            try
            {
                var checkUserExist = await _authRepository.FindUserByEmailAsync(loginDto.Email);
                if (checkUserExist == null)
                {
                    response.ErrorMessage = new List<string>() { "There is no user with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }

                var checkPassword = await _authRepository.CheckAccountPassword(checkUserExist, loginDto.Password);
                if (checkPassword == false)
                {
                    response.ErrorMessage = new List<string>() { "Invalid Password" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkEmailConfirm = await _authRepository.CheckEmailConfirmed(checkUserExist);
                if (checkEmailConfirm == false)
                {
                    response.ErrorMessage = new List<string>() { "Email not yet confirm" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var token = await _tokenGeneratorService.GenerateToken(checkUserExist);
                if (token == null)
                {
                    response.ErrorMessage = new List<string>() { "Error in generating jwt for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                //write the message for the return
                response.ErrorMessage = null;
                response.DisplayMessage = "Login Successful";
                response.StatusCode = 200;
                response.Result = token;

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessage = new List<string>() { "Error in login the user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDTO<string>> RegisterAsync(registerDTO registerDto, List<string> roles)
        {
            var response = new ResponseDTO<string>();
            try
            {
                var checkUserExist = await _authRepository.FindUserByEmailAsync(registerDto.Email);
                if (checkUserExist != null)
                {
                    response.ErrorMessage = new List<string>() { "User with the email already exist" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }


                foreach (var role in roles)
                {
                    var checkRole = await _authRepository.RoleExist(role);
                    if (checkRole == false)
                    {
                        response.ErrorMessage = new List<string>() { "Role is not available" };
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.DisplayMessage = "Error";
                        return response;
                    }
                }

                var mapAccount = new Vendor();
                mapAccount.Email = registerDto.Email;
                mapAccount.UserName = registerDto.Email;
                mapAccount.LastName = registerDto.LastName;
                mapAccount.FirstName = registerDto.FirstName;

                var createUser = await _authRepository.SignUpAsync(mapAccount, registerDto.Password);
                if (createUser == null)
                {
                    response.ErrorMessage = new List<string>() { "User not created successfully" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }

                //loop
                foreach (var role in roles)
                {
                    var addRole = await _authRepository.AddRoleAsync(createUser, role);
                    if (addRole == false)
                    {
                        response.ErrorMessage = new List<string>() { "Fail to add role to user" };
                        response.StatusCode = StatusCodes.Status501NotImplemented;
                        response.DisplayMessage = "Error";
                        return response;
                    }
                }
                var GenerateConfirmEmailToken = new ConfirmEmailToken()
                {
                    Token = _authRepository.GenerateConfirmEmailToken(),
                    VendorId = createUser.Id
                };
                var Generatetoken = await _authRepository.SaveGenerateConfirmEmailToken(GenerateConfirmEmailToken);
                if (Generatetoken == null)
                {
                    response.ErrorMessage = new List<string>() { "Fail to generate confirm email token for user" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var message = new Message(new string[] { createUser.Email }, "Confirm Email Token", $"<p>Your confirm email code is below<p><h6>{GenerateConfirmEmailToken.Token}</h6>");
                _emailService.SendEmail(message);
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "User successfully created";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessage = new List<string>() { "Error in resgistering the user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDTO<string>> ResetPasswordAsync(resetPasswordDTO newPassword)
        {
            var response = new ResponseDTO<string>();
            try
            {
                var findUser = await _authRepository.FindUserByEmailAsync(newPassword.Email);
                if (findUser == null)
                {
                    response.ErrorMessage = new List<string>() { "There is no user with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var resetPasswordAsync = await _authRepository.ResetPasswordAsync(findUser, newPassword);
                if (resetPasswordAsync == null)
                {
                    response.ErrorMessage = new List<string>() { "Invalid token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully reset user password";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessage = new List<string>() { "Error in reset user password" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}
