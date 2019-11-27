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
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Account Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase"/>
    [Route("api/[controller]")]
    ////[ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// The account
        /// </summary>
        private IUserRegistrationBusiness _account;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public AccountController(IUserRegistrationBusiness account)
        {
            this._account = account;
        }

        /// <summary>
        /// Adds the user detail.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUserDetail(UserDetails details)
        {
            //// the variable result stores the result of method AddUserDetails          
            var result = await _account.AddUserDetails(details);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Logins the specified details.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns>ResultOfLogin</returns>
        [HttpPost]
        [Route("login")]
        public async Task<Tuple<string, string>> Login(LoginModel details)
        {
            //// the variable result stores the result of method Login
            var ResultOfLogin = await this._account.Login(details);
            return Tuple.Create(ResultOfLogin.Item1, "Login Successful");
        }

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>passwordModel</returns>
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<string> ForgotPasword(ForgotPasswordModel passwordModel)
        {
            //// the variable result stores the result of method Login
            return await this._account.ForgotPassword(passwordModel);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <param name="tokenString">The token string.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Reset/{tokenString}")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel, string tokenString)
        {
            var result = await this._account.ResetPassword(resetPasswordModel, tokenString);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>UrlOfProfilePicture</returns>
        [HttpPost]
        [Route("ProfilePicture")]
        public IActionResult ProfilePicture(string userId, IFormFile file)
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
        [Route("list of Users")]
        public IList<UserDetails> ListOfUsers()
        {
           return this._account.ListOfUsers();        
        }
    }
}