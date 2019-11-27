// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRegistrationService.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;

    /// <summary>
    /// AccountBL
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IUserRegistrationBusiness" />
    public class UserRegistrationService : IUserRegistrationBusiness
    {
        /// <summary>
        /// Create the reference variable of Repository layer interface i.e IRegistration
        /// </summary>
        private IUserRegistraionRepository _registration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegistrationService"/> class.
        /// </summary>
        /// <param name="registration">The registration.</param>
        public UserRegistrationService(IUserRegistraionRepository registration)
        {
            this._registration = registration;
        }

        /// <summary>
        /// Adds the user details.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">User is empty</exception>
        public async Task<bool> AddUserDetails(UserDetails user)
        {
            try
            {
                //// If user the user details is empty or not 
                if (user != null)
                {
                    var result = await _registration.AddUserDetails(user);
                    if (result != false)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception("User is empty");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">login model.</param>
        /// <returns>LoginResult</returns>
        /// <exception cref="NotImplementedException">exception</exception>
        public async Task<Tuple<string, string>> Login(LoginModel loginModel)
        {
            try
            {
                //// If checks login details is empty or not 
                if (loginModel != null)
                {
                    var loginResult = await this._registration.Login(loginModel);
                    return Tuple.Create(loginResult.Item1, "Login Successful");
                }
                else
                {
                    throw new Exception("User is empty");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>this._registration.ForgotPassword(passwordModel);</returns>
        /// <exception cref="Exception">User Email is not valid</exception>
        public async Task<string> ForgotPassword(ForgotPasswordModel passwordModel)
        {
            try
            {
                //// If checks passwordModel details is empty or not 
                if (passwordModel != null)
                {
                    return await this._registration.ForgotPassword(passwordModel);
                }
                else
                {
                    throw new Exception("User Email is not valid");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <param name="tokenString">token string.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">User Email is not valid</exception>
        public async Task<Tuple<bool, string>> ResetPassword(ResetPasswordModel resetPasswordModel, string tokenString)
        {
            try
            {
                //// If checks resetPasswordModel details is empty or not 
                if (resetPasswordModel != null)
                {
                    //// variable result stores the result of ResetPassword()
                   var result = await this._registration.ResetPassword(resetPasswordModel, tokenString);

                    //// If checks result is null or not
                    if (result != null)
                    {
                        return Tuple.Create(true, "Password Has Been change");
                    }
                    else
                    {
                        return Tuple.Create(true, "Password Has not Been change");
                    }
                }
                else
                {
                    throw new Exception("User Email is not valid");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="user id">user id.</param>
        /// <param name="file">file.</param>
        /// <returns> this._registration.ProfilePicture(profilePicUrl, user id, file);</returns>
        public string ProfilePicture(string userid, IFormFile file)
        {
            try
            {
                CloudinaryImageUpload cloudinary = new CloudinaryImageUpload();
                var profilePicUrl = cloudinary.UploadImageOnCloud(file);
                if (userid != null)
                {
                    return this._registration.ProfilePicture(profilePicUrl, userid, file);
                }
                else
                {
                    return "Profile Picture is not uploaded";                   
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<bool> AdminRegistration(UserDetails adminDetails)
        {
            try
            {
                //// If user the user details is empty or not 
                if (adminDetails != null)
                {
                    var result = await this._registration.AdminRegistration(adminDetails);
                    if (result != false)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception("User is empty");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<string> AdminLogin(AdminLoginModel loginModel)
        {
            try
            {
                //// If checks login details is empty or not 
                if (loginModel != null)
                {
                    var loginResult = await this._registration.AdminLogin(loginModel);
                    return loginResult;
                }
                else
                {
                    throw new Exception("admin is empty");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public  Dictionary<string, int> UserStaticstics()
        {
            try
            {
                return this._registration.UserStaticstics();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Lists the of users.
        /// </summary>
        /// <returns>
        /// List of Users
        /// </returns>
        /// <exception cref="Exception">Unable to retrive list of users</exception>
        public IList<UserDetails> ListOfUsers()
        {
            try
            {
                IList<UserDetails> result = new List<UserDetails>();
                result = this._registration.ListOfUsers();
               
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Unable to retrive list of users");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}