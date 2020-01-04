// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRegistrationRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using CommanLayer.Model;
    using CommanLayer.MSMQ;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;   

    /// <summary>
    /// RegistrationRL
    /// </summary>
    public class UserRegistrationRepository : IUserRegistraionRepository
    {
       private readonly IConfiguration _configuration;
        /// <summary>
        /// User Manager
        /// </summary>
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// AuthenticationContext
        /// </summary>
        AuthenticationContext _authenticationContext;
        private UserDetails userTypeData;

        /// <summary>
        /// Create the parameterized Constructor of class and pass the UserManager
        /// </summary>
        /// <param name="userManager">userManager</param>
        public UserRegistrationRepository(UserManager<ApplicationUser> userManager, AuthenticationContext authenticationContext,IConfiguration configuration)
        {
            this._userManager = userManager;
            this._authenticationContext = authenticationContext;
            _configuration = configuration;
        }
  
        /// <summary>
        /// AddUser Details 
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>Userdetails</returns>
        public async Task<UserDetails> AddUserDetails(UserDetails userDetail)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            try
            {              
                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand = new SqlCommand("InsertUsers", con);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@FirstName", userDetail.FirstName);
                sqlcommand.Parameters.AddWithValue("@LastName", userDetail.LastName);
                sqlcommand.Parameters.AddWithValue("@UserName", userDetail.UserName );
                sqlcommand.Parameters.AddWithValue("@Email", userDetail.UserName + "@gmail.com");
                sqlcommand.Parameters.AddWithValue("@Password", userDetail.Password);
                sqlcommand.Parameters.AddWithValue("@ProfilePicture", userDetail.ProfilePicture);
                sqlcommand.Parameters.AddWithValue("@UserType", userDetail.UserType);
                sqlcommand.Parameters.AddWithValue("@ServiceType", userDetail.ServiceId);
                con.Open();
                int row = await sqlcommand.ExecuteNonQueryAsync();
                if (row > 0)
                {
                    return userDetail;                   
                }
                else
                {
                    throw new Exception("Unable to register");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>loginModel</returns>
        public async Task<UserDetails> Login(LoginModel loginModel)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            //// it confirms that user is avaiable in database or not
            ///var user = await _userManager.FindByNameAsync(loginModel.UserName);

            SqlCommand sqlCommand = new SqlCommand("SelectUserDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Email", loginModel.Email);
            sqlCommand.Parameters.AddWithValue("@Password", loginModel.Password);

            sqlConnection.Open();

            SqlDataReader sdr =  sqlCommand.ExecuteReader();
            // RegistrationModel user = new RegistrationModel();

            UserDetails userModel= null;
            while (sdr.Read())
            {
                userModel = new UserDetails();
                userModel.FirstName = sdr["FirstName"].ToString();
                userModel.LastName = sdr["LastName"].ToString();
                userModel.UserName = sdr["UserName"].ToString();
                userModel.id = (int)sdr["Id"];
                userModel.ServiceId = sdr["ServiceType"].ToString();
                userModel.UserType = sdr["UserType"].ToString();
                userModel.Email = sdr["Email"].ToString();
                userModel.ProfilePicture = sdr["Profilepicture"].ToString();
            }
            sdr.Close();

            return userModel;

        }

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>passwordModel</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> ForgotPassword(ForgotPasswordModel passwordModel)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
               
                SqlCommand sqlCommand = new SqlCommand("ForgotPassword", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Email", passwordModel.Email);
                con.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                ForgotPasswordModel model = null;
                while (dataReader.Read())
                {
                    model = new ForgotPasswordModel();
                    model.Email = dataReader["Email"].ToString();                   
                }

                dataReader.Close();

                if (model != null)
                {
                    ////here we create object of MsmqTokenSender which is present in Common-Layer
                    MsmqTokenSender msmq = new MsmqTokenSender();
                    string key = "This is my SecretKey which is used for security purpose";

                    ////Here generate encrypted key and result store in security key
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                    //// here using securitykey and algorithm(security) the creadintails is generate(SigningCredentials present in Token)
                    var creadintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                    new Claim("Email", model.Email),
                };

                    var token = new JwtSecurityToken("Security token", "https://Test.com",
                        claims,
                        DateTime.UtcNow,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: creadintials);

                    var NewToken = new JwtSecurityTokenHandler().WriteToken(token);

                    //// Send the email and password to Method in MsmqTokenSender
                    msmq.SendMsmqToken(passwordModel.Email, NewToken.ToString());
                    return NewToken;
                }
                else
                {
                    return "Invalid user";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <param name="tokenString">tokenString</param>
        /// <returns>resetPasswordModel</returns>
        public async Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var token = new JwtSecurityToken(resetPasswordModel.token);

            //// Claims the email from token
            var Email = (token.Claims.First(c => c.Type == "Email").Value);
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);

            SqlCommand sqlCommand = new SqlCommand("UpdatePassword", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Email", Email);
            sqlCommand.Parameters.AddWithValue("@password", resetPasswordModel.Password);
            con.Open();

            if (Email != null)
            {
              var result = await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="file">The file.</param>
        /// <returns>url</returns>
        public async Task<string> ProfilePicture(int userid, IFormFile file)
        {
            try
            {
                CloudinaryImageUpload cloudinary = new CloudinaryImageUpload();
                var url = cloudinary.UploadImageOnCloud(file);
                SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SPAddImageToProfile", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", userid);
                sqlCommand.Parameters.AddWithValue("@Image", url);
                con.Open();
              
                 await sqlCommand.ExecuteNonQueryAsync();
                
                    return url;
              
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Admins the registration.
        /// </summary>
        /// <param name="adminDetails">The admin details.</param>
        /// <returns>adminDetails</returns>
        public async Task<bool> AdminRegistration(UserDetails adminDetails)
        {
            //// create the instance of ApplicationUser
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            try
            {
                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand = new SqlCommand("InsertUsers", con);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@FirstName", adminDetails.FirstName);
                sqlcommand.Parameters.AddWithValue("@LastName", adminDetails.LastName);
                sqlcommand.Parameters.AddWithValue("@UserName", adminDetails.UserName);
                sqlcommand.Parameters.AddWithValue("@Email", adminDetails.Email);
                sqlcommand.Parameters.AddWithValue("@Password", adminDetails.Password);
                sqlcommand.Parameters.AddWithValue("@ProfilePicture", adminDetails.ProfilePicture);
                sqlcommand.Parameters.AddWithValue("@UserType", "Admin");
                sqlcommand.Parameters.AddWithValue("@ServiceType", "NULL");
                con.Open();
                int row = await sqlcommand.ExecuteNonQueryAsync();
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// Admins the login.
        /// </summary>
        /// <param name="adminloginModel">The login model.</param>
        /// <returns>login model</returns>
      
        public async Task<string> AdminLogin(AdminLoginModel adminloginModel)
        {

            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SpAdmin", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            con.Open();
            sqlCommand.Parameters.AddWithValue("@UserName", adminloginModel.UserName);
            sqlCommand.Parameters.AddWithValue("@password", adminloginModel.Password);

            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            AdminLoginModel adminlogin = null;
            while (dataReader.Read())
            {
                adminlogin = new AdminLoginModel();
                adminlogin.UserName = dataReader["UserName"].ToString();
                adminlogin.Password = dataReader["Password"].ToString();
                adminlogin.UserType = dataReader["UserType"].ToString();
            }

            if (adminloginModel.UserType == "Admin" || adminloginModel.UserType == "admin" && adminlogin.Password == adminloginModel.Password)
            {
                string key = "This is my SecretKey which is used for security purpose";

                ////Here generate encrypted key and result store in security key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                //// here using securitykey and algorithm(security) the creadintails is generate(SigningCredentials present in Token)
                var creadintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("UserName", adminlogin.UserName),
                };

                var token = new JwtSecurityToken("Security token", "https://Test.com",
                    claims,
                    DateTime.UtcNow,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creadintials);

                var NewToken = new JwtSecurityTokenHandler().WriteToken(token);
                return NewToken;
            }
            else
            {
                return "Token is not generated";
            }
        }

        /// <summary>
        /// Users the statistics.
        /// </summary>
        /// <returns>result in Key value pair</returns>
        public Dictionary<string, int> UserStaticstics()
        {
            //// Create the instance of Dictonary,Dictonary stores the data in key-value pair 
            Dictionary<string, int> map = new Dictionary<string, int>();

            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SpUserStatistics", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
           

             int advance = 0, basic = 0;
            //// foreach is usecd to check type of user
            
            while(dataReader.Read())
            { 
                if (dataReader["ServiceType"].ToString() == "Advance" || dataReader["ServiceType"].ToString() == "advance")
                {                 
                    advance++;
                }
                else if (dataReader["ServiceType"].ToString() == "Basic" || dataReader["ServiceType"].ToString() == "basic")
                {                  
                    basic++;
                }
            }

            map.Add("Advance", advance);
            map.Add("Basic", basic);
            return map;
        }

        /// <summary>
        /// Lists the of users.
        /// </summary>
        /// <returns>List</returns>
        public IList<UserDetails> ListOfUsers()
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SpUserStatistics", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            //// Create the instance of IList
            IList<UserDetails> list = new List<UserDetails>();
            UserDetails userDetails;
            while (dataReader.Read())
            {
                userDetails = new UserDetails()
                {
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    UserName = dataReader["UserName"].ToString(),
                    Email = dataReader["Email"].ToString(),
                    Password = dataReader["password"].ToString(),
                    ProfilePicture = dataReader["ProfilePicture"].ToString(),
                    UserType = dataReader["UserType"].ToString(),
                    ServiceId = dataReader["ServiceType"].ToString(),
                };

                list.Add(userDetails);
            }

            return list.ToList();
        }
    }
}