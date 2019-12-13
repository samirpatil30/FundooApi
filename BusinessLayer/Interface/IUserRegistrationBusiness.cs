// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRegistrationBusiness.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;
  
    /// <summary>
    /// IAccountBL
    /// </summary>
    public interface IUserRegistrationBusiness
    {
        /// <summary>
        /// Adds the user details.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>result</returns>
        Task<bool> AddUserDetails(UserDetails user);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        Task<Tuple<string, string>> Login(LoginModel loginModel);

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>passwordModel</returns>
        Task<string> ForgotPassword(ForgotPasswordModel passwordModel);

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <param name="tokenString">The token string.</param>
        /// <returns>resetPasswordModel,tokenString </returns>
        Task<Tuple<bool, string>> ResetPassword(ResetPasswordModel resetPasswordModel);

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <param name="file">The file.</param>
        /// <returns>User id, file</returns>
        Task<string> ProfilePicture( int userid, IFormFile file);

        /// <summary>
        /// Admin registration.
        /// </summary>
        /// <param name="adminDetails">The admin details.</param>
        /// <returns>adminDetails</returns>
        Task<bool> AdminRegistration(UserDetails adminDetails);

        /// <summary>
        /// Admins the login.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>loginModel</returns>
        Task<string> AdminLogin(AdminLoginModel loginModel);

        /// <summary>
        /// Users the staticstics.
        /// </summary>
        /// <returns>Key value pair of UserType</returns>
        Dictionary<string, int> UserStaticstics();

        /// <summary>
        /// Lists the of users.
        /// </summary>
        /// <returns>List of Users</returns>
        IList<UserDetails> ListOfUsers();
    }
}
