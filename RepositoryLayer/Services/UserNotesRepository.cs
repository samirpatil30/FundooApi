// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserNotesRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Services
{   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using Newtonsoft.Json;
    using System.Data.SqlClient;
    using System.Data;
    using Microsoft.Extensions.Configuration;
    using System.Data.SqlTypes;

    /// <summary>
    /// User Notes Repository
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.INotesRepository" />
    public class UserNotesRepository : INotesRepository
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Create the Instance variable of AuthenticationContext
        /// </summary>
        private readonly AuthenticationContext _authenticationContext;

        /// <summary>
        /// UserNotesRepository
        /// </summary>
        /// <param name="authenticationContext"></param>
        public UserNotesRepository(AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            _authenticationContext = authenticationContext;
            _configuration = configuration;
        }

        /// <summary>
        /// Add Notes
        /// </summary>
        /// <param name="notesModel">notesModel</param>
        /// <returns>result</returns>
        public async Task<bool> AddNotes(NotesModel notesModel)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            try
            {
                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand = new SqlCommand("SpAddNotes", con);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@UserId", notesModel.UserId);
                sqlcommand.Parameters.AddWithValue("@NotesTitle", notesModel.NotesTitle);
                sqlcommand.Parameters.AddWithValue("@NotesDescription", notesModel.NotesDescription);
                // sqlcommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                //sqlcommand.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                sqlcommand.Parameters.AddWithValue("@color", notesModel.color);
                sqlcommand.Parameters.AddWithValue("@Reminder", SqlDateTime.MinValue);
                sqlcommand.Parameters.AddWithValue("@Image", notesModel.Image);
                sqlcommand.Parameters.AddWithValue("@Trash", notesModel.Trash);
                sqlcommand.Parameters.AddWithValue("@Archive", notesModel.Archive);
                sqlcommand.Parameters.AddWithValue("@Pin", notesModel.Pin);
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
        /// Get Notes
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>UserID</returns>
        public IList<NotesModel> GetNotes(int userId)
        {
            IList<NotesModel> notesModel = new List<NotesModel>();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SpGetNotes", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    NotesModel userList = new NotesModel();
                    userList.Id = Convert.ToInt32(reader["Id"]);
                    userList.UserId = (int)reader["UserId"];
                    userList.Image = reader["Image"].ToString();
                    userList.Archive = (bool)reader["Archive"];
                    userList.Pin = Convert.ToBoolean(reader["Pin"].ToString());
                    userList.Trash = Convert.ToBoolean(reader["Trash"].ToString());
                    userList.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    userList.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    userList.color = reader["Color"].ToString();
                    userList.NotesDescription = reader["NotesDescription"].ToString();
                    userList.Reminder = Convert.ToDateTime(reader["Reminder"].ToString());
                    userList.NotesTitle = reader["NotesTitle"].ToString();
                    notesModel.Add(userList);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            if (notesModel != null)
            {
                return notesModel;
            }
            else
            {
                return notesModel;
            }

        }

        public async Task<bool> UpdateNotes(NotesModel model, int noteId)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SPUpdateNote", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", noteId);
                sqlCommand.Parameters.AddWithValue("@UserId", model.UserId);
                sqlCommand.Parameters.AddWithValue("@Description", model.NotesDescription);
                sqlCommand.Parameters.AddWithValue("@Color", model.color);

                sqlCommand.Parameters.AddWithValue("@IsArchive", model.Archive);
                sqlCommand.Parameters.AddWithValue("@IsPin", model.Pin);
                sqlCommand.Parameters.AddWithValue("@IsTrash", model.Trash);
                sqlCommand.Parameters.AddWithValue("@Image", model.Image);
                /// sqlCommand.Parameters.AddWithValue("@Reminder", model.Reminder);
                sqlCommand.Parameters.AddWithValue("@Title", model.NotesTitle);

                sqlConnection.Open();
                var respone = await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Delete Notes
        /// </summary>
        /// <param name="notesModel"></param>
        /// <param name="id"></param>
        /// <returns>notesModel</returns>
        public async Task<bool> DeleteNotes(int id)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("DeleteNotes", con);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            con.Open();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            
            int row = await sqlCommand.ExecuteNonQueryAsync();
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }   

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>id url</returns>
        public async Task<string> AddImage( int userid, int id, IFormFile file)
        {
            CloudinaryImageUpload cloudinary = new CloudinaryImageUpload();
            var url = cloudinary.UploadImageOnCloud(file);
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPAddImage", con);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@UserId", userid);
            sqlCommand.Parameters.AddWithValue("@Image", url);
            con.Open();
            sqlCommand.CommandType = CommandType.StoredProcedure;
             var result = await sqlCommand.ExecuteNonQueryAsync(); 
        
            if (result > 0)
            {
                return url;
            }
            else
            {
                return "Image not uploaded";
            }
        }

        /// <summary>
        /// Archives the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Archive(int id, int UserId)
        {

            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("NoteId", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            sqlCommand.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            NotesModel userList = new NotesModel();
            while (reader.Read())
            {
                ////userList.Id = Convert.ToInt32(sdreader["Id"]);
                userList.UserId = (int)(reader["UserId"]);
                userList.Id = Convert.ToInt32(reader["Id"]);
                userList.Archive = Convert.ToBoolean(reader["Archive"].ToString());
            }
            sqlConnection.Close();
            /// SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCmd = new SqlCommand("SpArchive", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();


            ////if notes data have records then it will update the records
            if (id != 0)
            {

                if (userList.Archive == false)
                {
                    // userList.IsTrash = true;
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlCmd.Parameters.AddWithValue("@Archive", true);
                }
                else
                {
                    // userList.IsTrash = true;
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlCmd.Parameters.AddWithValue("@Archive", false);
                }
                await sqlCmd.ExecuteNonQueryAsync();
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Un archive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        public async Task<bool> UnArchive(int id)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPUnArchive", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            // sqlCommand.Parameters.AddWithValue("@Archive", archiveNote);
            con.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Trashes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        public async Task<bool> Trash(int id,int UserId)
        {

            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("NoteId", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            sqlCommand.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            NotesModel userList = new NotesModel();
            while (reader.Read())
            {
                ////userList.Id = Convert.ToInt32(sdreader["Id"]);
                userList.UserId = (int)(reader["UserId"]);
                userList.Id = Convert.ToInt32(reader["Id"]);
                userList.Trash = Convert.ToBoolean(reader["Trash"].ToString());
            }
            sqlConnection.Close();
            /// SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCmd = new SqlCommand("SpTrash", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();


            ////if notes data have records then it will update the records
            if (id != 0)
            {

                if (userList.Trash == false)
                {
                    // userList.IsTrash = true;
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.Parameters.AddWithValue("@Trash", true);
                    sqlCmd.Parameters.AddWithValue("@UserId", UserId);
                }
                else
                {
                    // userList.IsTrash = true;
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.Parameters.AddWithValue("@Trash", false);
                    sqlCmd.Parameters.AddWithValue("@UserId", UserId);
                }
                await sqlCmd.ExecuteNonQueryAsync();
                return true;
            }
            else
            {
                return false;
            }

        }


           /// <summary>
           /// Un trash.
           /// </summary>
           /// <param name="id">The identifier.</param>
           /// <returns>id</returns>
           public async Task<bool> UnTrash(int id)
           {
               SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
               SqlCommand sqlCommand = new SqlCommand("SPUnTrash", con);
               sqlCommand.CommandType = CommandType.StoredProcedure;
               sqlCommand.Parameters.AddWithValue("@Id", id);
               // sqlCommand.Parameters.AddWithValue("@Archive", archiveNote);
               con.Open();
               var result = await sqlCommand.ExecuteNonQueryAsync();

               if (result > 0)
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }  

        /// <summary>
        /// Pins the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Pin(int id)
        {

            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("NoteId", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            sqlCommand.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            NotesModel userList = new NotesModel();
            while (reader.Read())
            {
                ////userList.Id = Convert.ToInt32(sdreader["Id"]);

                userList.Id = Convert.ToInt32(reader["Id"]);
                userList.Pin = Convert.ToBoolean(reader["Pin"].ToString());
            }
            sqlConnection.Close();
            /// SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCmd = new SqlCommand("SpPin", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();


            ////if notes data have records then it will update the records
            if (id != 0)
            {

                if (userList.Pin == false)
                {
                    // userList.IsTrash = true;
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.Parameters.AddWithValue("@Pin", true);
                }
                else
                {
                    // userList.IsTrash = true;
                    sqlCmd.Parameters.AddWithValue("@Id", id);
                    sqlCmd.Parameters.AddWithValue("@Pin", false);
                }
                await sqlCmd.ExecuteNonQueryAsync();
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Un pin.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        public async Task<bool> UnPin(int id)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPUnPin", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            // sqlCommand.Parameters.AddWithValue("@Archive", archiveNote);
            con.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="time">The time.</param>
        /// <returns>id</returns>
        public async Task<bool> AddReminder(int id, NotesModel time, int UserId)
        {
           // FireBaseNotification fireBaseNotification = new FireBaseNotification();
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPReminder", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@UserId", UserId);
            sqlCommand.Parameters.AddWithValue("@Reminder", time.Reminder);

            con.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();
            //fireBaseNotification.Notification(Reminder);
            
            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        public async Task<bool> DeleteReminder(int id)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPReminder", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Reminder", SqlDateTime.MinValue);
            con.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();
            //fireBaseNotification.Notification(Reminder);

            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Collabrates the specified noteid.
        /// </summary>
        /// <param name="Noteid">The noteid.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>list of user id</returns>
        public async Task<bool> Collabrate(int Noteid, IList<string> email,int CurrentUser)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);

                List<int> ids = new List<int>();
                foreach (var user in email)
                {
                    SqlCommand sqlCommand = new SqlCommand("GetAllEmails", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@email", user);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        NotesModel userList = new NotesModel();
                        ////userList.Id = Convert.ToInt32(sdreader["Id"]);

                        userList.UserId = Convert.ToInt32( reader["Id"]);
                        ids.Add(userList.UserId);
                    }
                    sqlConnection.Close();

                }

                foreach (var users in ids)
                {
                   SqlCommand sqlCommand = new SqlCommand("SPCollabration", con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    sqlCommand.Parameters.AddWithValue("@Id", Noteid);
                    sqlCommand.Parameters.AddWithValue("@UserId", CurrentUser);
                    sqlCommand.Parameters.AddWithValue("@ReciverId", users);
                    await sqlCommand.ExecuteNonQueryAsync();
                    con.Close();
                }
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>list of id</returns>
        public async Task<bool> BulkTrash(IList<int> id)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
 
            foreach(var CurrentId in id)
            {
                SqlCommand sqlCommand = new SqlCommand("DeleteNotes", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                sqlCommand.Parameters.AddWithValue("@Id", id);          
               await sqlCommand.ExecuteNonQueryAsync();
                con.Close();
            }

            return true;
            
        }

        public IList<NotesModel> Search(string word, int UserId)
        {

            try
            {
                IList<NotesModel> notesModel = new List<NotesModel>();
                SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SPNoteSearch", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                sqlCommand.Parameters.AddWithValue("@word", word);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    ////userList.Id = Convert.ToInt32(sdreader["Id"]);
                    NotesModel userList = new NotesModel();
                    userList.Id = Convert.ToInt32(reader["Id"]);
                    userList.UserId = Convert.ToInt32(reader["UserId"].ToString());
                    userList.Image = reader["Image"].ToString();
                    userList.Archive = (bool)reader["Archive"];
                    userList.Pin = Convert.ToBoolean(reader["Pin"].ToString());
                    userList.Trash = Convert.ToBoolean(reader["Trash"].ToString());
                    userList.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    userList.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    userList.color = reader["color"].ToString();
                    userList.NotesDescription = reader["NotesDescription"].ToString();
                    userList.Reminder = Convert.ToDateTime(reader["Reminder"].ToString());
                    userList.NotesTitle = reader["NotesTitle"].ToString();
                    notesModel.Add(userList);
                }
                return notesModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IList<NotesModel> GetArchiveNotes(int userId)
        {
            IList<NotesModel> notesModel = new List<NotesModel>();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SPGetArchiveNotes", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    NotesModel userList = new NotesModel();
                    userList.Id = Convert.ToInt32(reader["Id"]);
                    userList.UserId = (int)reader["UserId"];
                    userList.Image = reader["Image"].ToString();
                    userList.Archive = (bool)reader["Archive"];
                    userList.Pin = Convert.ToBoolean(reader["Pin"].ToString());
                    userList.Trash = Convert.ToBoolean(reader["Trash"].ToString());
                    userList.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    userList.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    userList.color = reader["Color"].ToString();
                    userList.NotesDescription = reader["NotesDescription"].ToString();
                    userList.Reminder = Convert.ToDateTime(reader["Reminder"].ToString());
                    userList.NotesTitle = reader["NotesTitle"].ToString();
                    notesModel.Add(userList);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            if (notesModel != null)
            {
                return notesModel;
            }
            else
            {
                return notesModel;
            }

        }

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IList<NotesModel> GetTrashNotes(int userId)
        {
            IList<NotesModel> notesModel = new List<NotesModel>();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SPGetTrashNotes", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    NotesModel userList = new NotesModel();
                    userList.Id = Convert.ToInt32(reader["Id"]);
                    userList.UserId = (int)reader["UserId"];
                    userList.Image = reader["Image"].ToString();
                    userList.Archive = (bool)reader["Archive"];
                    userList.Pin = Convert.ToBoolean(reader["Pin"].ToString());
                    userList.Trash = Convert.ToBoolean(reader["Trash"].ToString());
                    userList.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    userList.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                    userList.color = reader["Color"].ToString();
                    userList.NotesDescription = reader["NotesDescription"].ToString();
                    userList.Reminder = Convert.ToDateTime(reader["Reminder"].ToString());
                    userList.NotesTitle = reader["NotesTitle"].ToString();
                    notesModel.Add(userList);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            if (notesModel != null)
            {
                return notesModel;
            }
            else
            {
                return notesModel;
            }

        }

        public bool ColorService(ColorModel data)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SPChangeColor", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", data.noteId);
                sqlCommand.Parameters.AddWithValue("@UserId", data.UserId);

                sqlCommand.Parameters.AddWithValue("@color", data.color);


                sqlConnection.Open();
                var response = sqlCommand.ExecuteNonQuery();
               
                if (response != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}