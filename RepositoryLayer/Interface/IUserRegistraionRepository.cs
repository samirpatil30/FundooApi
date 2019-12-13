// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRegistraionRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// IUserRegistraionRepository
    /// </summary>
    public interface IUserRegistraionRepository
    {   

        /// <summary>
        /// Adds the user details.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>user</returns>
      Task<bool> AddUserDetails(UserDetails user);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>loginModel</returns>
        Task<Tuple<string, string>> Login(LoginModel loginModel);

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>passwordModel</returns>
        Task<string> ForgotPassword(ForgotPasswordModel passwordModel);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <param name="tokenString">The token string.</param>
        /// <returns>resetPasswordModel</returns>
        Task<Tuple<bool,string>> ResetPassword(ResetPasswordModel resetPasswordModel);

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The user id.</param>
        /// <param name="file">The file.</param>
        /// <returns>url</returns>
        Task<string> ProfilePicture(int userid, IFormFile file);

        /// <summary>
        /// Admins the registration.
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
        /// Users the statistics.
        /// </summary>
        /// <returns>Key value pairs of UserType</returns>
        Dictionary<string, int> UserStaticstics();

        /// <summary>
        /// Lists the of users.
        /// </summary>
        /// <returns>UserDetails</returns>
        IList<UserDetails> ListOfUsers();
    }
}