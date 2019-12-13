// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAccountTesting.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundoTestProject.Account
{
    using BusinessLayer.Services;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using Xunit;

    /// <summary>
    /// UserAccountTesting
    /// </summary>
    public class UserAccountTesting
    {        
        [Fact]
        public void RegistrationTesting()
        {
            //// Using Mock create the instance of IUserRegistrationRepository
            var repositoryLayer = new Mock<IUserRegistraionRepository>();
            var businessLayer = new UserRegistrationService(repositoryLayer.Object);
            var model = new UserDetails()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName",
                Email = "Email",
                Password = "Password",
                ProfilePicture = "ProfilePicuture"
            };

            ////Act
            var data = businessLayer.AddUserDetails(model);

            //// Assert
            Assert.NotNull(data);           
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        [Fact]
        public void Login()
        {
            var Repository = new Mock<IUserRegistraionRepository>();
            var businessLayer = new UserRegistrationService(Repository.Object);
            var model = new LoginModel()
            {
                Email = "userName",
                Password = "Password"
            };

            var data = businessLayer.Login(model);

            Assert.NotNull(data);
        }

        /// <summary>
        /// Forgot the password.
        /// </summary>
        [Fact]
        public void ForgotPassword()
        {
            var Repository = new Mock<IUserRegistraionRepository>();
            var businessLayer = new UserRegistrationService(Repository.Object);
            var model = new ForgotPasswordModel()
            {
                Email = "Email"
            };

            var data = businessLayer.ForgotPassword(model);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        [Fact]
        public void ResetPassword()
        {
            var Repository = new Mock<IUserRegistraionRepository>();
            var Business = new UserRegistrationService(Repository.Object);
            var model = new ResetPasswordModel()
            {
                Password = "Password"
            };

            var data = Business.ResetPassword(model, "Reset Password");
            Assert.NotNull(data);
        }

        [Fact]
        public void AdminRegistration()
        {
            var Repository = new Mock<IUserRegistraionRepository>();
            var Business = new UserRegistrationService(Repository.Object);
            var model = new UserDetails()
            {
                FirstName = "Manoj",
                LastName = "Pawane",
                UserName = "manoj123",
                Email = "manoj123@gmail.com",
                Password = "manoj123",
                ProfilePicuture = "assassinscredd.jpeg"
            };

            var data = Business.AdminRegistration(model);
            Assert.NotNull(data);            
        }

        [Fact]
        public void AdminLogin()
        {
            var Repository = new Mock<IUserRegistraionRepository>();
            var Business = new UserRegistrationService(Repository.Object);
            var model = new LoginModel()
            {                
                Email = "manoj123",                
                Password = "manoj123",
             };

            var data = Business.AdminLogin(model);
            Assert.NotNull(data);            
        } 
    }
}