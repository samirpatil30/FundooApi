// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Account Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase"/>
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    //[EnableCors]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// The account
        /// </summary>
        private IUserRegistrationBusiness _account;

        private IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public AccountController(IUserRegistrationBusiness account, IConfiguration configuration)
        {
            this._account = account;
            _configuration = configuration;
        }

        /// <summary>
        /// Adds the user detail.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns>result</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Add")]
        //[EnableCors("CorsPolicy")]
        public async Task<IActionResult> AddUserDetail(UserDetails details)
        {
            try
            {
                //// the variable result stores the result of method AddUserDetails          
                UserDetails model = await _account.AddUserDetails(details);
                if (model != null)
                {
                    var firstName = model.FirstName;
                    var lastName = model.LastName;
                    return Ok(new { success = true, message = "User Registration successful", data= (model) });
                }
                else
                {
                    return Ok(new { success = false, message = "Registration failed" });
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
           
        }

        /// <summary>
        /// Logins the specified details.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns>ResultOfLogin</returns>
       
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                UserDetails model = await _account.Login(login);

                if (model != null)
                {
                  var token=  TokenGeneration(model);
                    var email = login.Email;
                    var password = login.Password;
                    return Ok(new { success = true, message = "Success",  token, model });
                }
                else
                {
                    var success = false;
                    var message = "Login failed";
                    return BadRequest(new { success, message });
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }


        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>passwordModel</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPasword(ForgotPasswordModel passwordModel)
        {
           //// the variable result stores the result of method Login
            var result= await this._account.ForgotPassword(passwordModel);
            return Ok(new { success = true, message = "Link sent to your mail id", data = (passwordModel) });
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <param name="tokenString">The token string.</param>
        /// <returns>result</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var result = await this._account.ResetPassword(resetPasswordModel);
            return this.Ok(new { success= true, message= "Password change successfully", data=(result) });
        }

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>UrlOfProfilePicture</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ProfilePicture")]
        public IActionResult ProfilePicture(int userId, IFormFile file)
        {
            var UrlOfProfilePicture = this._account.ProfilePicture(userId, file);
            return this.Ok(new { UrlOfProfilePicture });
        }

        /// <summary>
        /// Admin Registration
        /// </summary>
        /// <param name="adminDetails">adminDetails</param>
        /// <returns>result</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("adminRegistration")]
        public async Task<IActionResult> AdminRegistration(UserDetails adminDetails)
        {
            var result = await this._account.AdminRegistration(adminDetails);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="loginModel">loginModel</param>
        /// <returns>token</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("adminLogin")]
        public async Task<IActionResult> AdminLogin(AdminLoginModel loginModel)
        {
            var AdminToken = await this._account.AdminLogin(loginModel);
            return this.Ok(new { AdminToken });
        }

        /// <summary>
        /// User Staticstics
        /// </summary>
        /// <returns>return the result of UserStaticstics</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("statitics")]
        public Dictionary<string, int> UserStaticstics()
        {
            return this._account.UserStaticstics();
        }

        /// <summary>
        /// List Of Users
        /// </summary>
        /// <returns>retuen the list of users</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("list of Users")]
        public IList<UserDetails> ListOfUsers()
        {
           return this._account.ListOfUsers();        
        }


        [HttpGet]
        public string TokenGeneration(UserDetails model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            //// here using securitykey and algorithm(security) the creadintails is generate(SigningCredentials present in Token)
            var creadintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
               new Claim("Email",model.Email),
               new Claim("Id", (model.id).ToString()),
               new Claim("FirstName", model.FirstName),
               new Claim("LastName", model.LastName),
               new Claim("UserName", model.UserName),
                new Claim("ProfoilePicture", model.ProfilePicture)
                };

            var token = new JwtSecurityToken("Security token", "https://Test.com",
                claims,
                DateTime.UtcNow,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creadintials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}