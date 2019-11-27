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
                sqlcommand.Parameters.AddWithValue("@CreatedDate", notesModel.CreatedDate);
                sqlcommand.Parameters.AddWithValue("@ModifiedDate", notesModel.ModifiedDate);
                sqlcommand.Parameters.AddWithValue("@color", notesModel.color);
                sqlcommand.Parameters.AddWithValue("@Reminder", notesModel.Reminder);
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
        public IList<NotesModel> GetNotes(int userId,int pageNumber, int NotePerPage)
        {
            IList<NotesModel> list = new List<NotesModel>(); ;
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SpGetNotes", con);
            con.Open();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("UserId", userId);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            while(dataReader.Read())
            {
                var notesModel = new NotesModel()
                { 
                   UserId = userId.ToString(),
                   NotesTitle = dataReader["NotesTitle"].ToString(),
                   NotesDescription = dataReader["NotesDescription"].ToString(),
                   CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]),
                   ModifiedDate = Convert.ToDateTime(dataReader["ModifiedDate"]),
                   color = dataReader["color"].ToString(),
                   Reminder = Convert.ToDateTime(dataReader["Reminder"]),
                   Image = dataReader["Image"].ToString(),
                   Trash = Convert.ToBoolean(dataReader["Trash"]),
                   Archive = Convert.ToBoolean(dataReader["Archive"]),
                   Pin = Convert.ToBoolean(dataReader["Pin"])
                };
                list.Add(notesModel);
            }

            //// Here the Linq querey return the Record match in Database
            
            int count = list.Count();
            int CurrentPage = pageNumber;
            int PageSize = NotePerPage;
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            var items = list.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,   
            };
           
            return items.ToList();
        }

        /// <summary>
        /// Update Notes
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="Noteid">id</param>
        /// <returns>result</returns>
        public async Task<bool> UpdateNotes(NotesModel model, int Noteid)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SpGetNotes", con);
            con.Open();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            while(dataReader.Read())
            {
                NotesModel notesModel = new NotesModel()
                {

                };
            }

            ////if notes data have records then it will update the records
            //foreach (var updateNote in query)
            //{

            //    updateNote.NotesTitle = model.NotesTitle;
            //    updateNote.NotesDescription = model.NotesDescription;
            //    updateNote.color = model.color;
            //}
            ////save changes to the database
            var result = await this._authenticationContext.SaveChangesAsync();

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
        /// <returns>id</returns>
        public async Task<bool> Archive(int id)
        {     
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SpArchive", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);
           // sqlCommand.Parameters.AddWithValue("@Archive", archiveNote);
            con.Open();
            var result = await sqlCommand.ExecuteNonQueryAsync();

            if(result > 0)
            {
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
        public async Task<bool> Trash(int id)
        {
            //// Linq Query to select note id to Trash the note
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPTrash", con);
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
        /// <returns>id</returns>
        public async Task<bool> Pin(int id)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPPin", con);
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
        public async Task<bool> AddReminder(int id, DateTime time)
        {
           // FireBaseNotification fireBaseNotification = new FireBaseNotification();
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPReminder", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Reminder", time);
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
        public async Task<bool> Collabrate(int Noteid, IList<string> UserId,int CurrentUser)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                
                foreach(var user in UserId)
                {
                   SqlCommand sqlCommand = new SqlCommand("SPCollabration", con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    sqlCommand.Parameters.AddWithValue("@Id", Noteid);
                    sqlCommand.Parameters.AddWithValue("@UserId", CurrentUser);
                    sqlCommand.Parameters.AddWithValue("@ReciverId", user);
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

        /// <summary>
        /// Searches the specified anything.
        /// </summary>
        /// <param name="anything">Anything.</param>
        /// <returns>string</returns>
        /// <exception cref="Exception">Note not found</exception>
        //public IList<NotesModel> Search(string anything)
        //{
        //    IList<NotesModel> listResults = new List<NotesModel>();
        //    try
        //    {
        //        var resultsFromLabel = (from lable in this._authenticationContext.labelModels
        //                                where lable.Label == anything
        //                                select lable);
                
        //        if (resultsFromLabel != null)
        //        {
        //            foreach (LabelModel model in resultsFromLabel)
        //            {
        //                var result = (from note in _authenticationContext.notesModels
        //                              where note.UserId == model.UserId 
        //                              select note);

        //                ////  NotesModel notesModel =(NotesModel) res;
        //                foreach (NotesModel modelNote in result)
        //                {
        //                    if (listResults.Contains(modelNote))
        //                    {
        //                        break;
        //                    }
        //                    else
        //                    {
        //                       listResults.Add(modelNote);                              
        //                    }
        //                }
        //            }
        //            return listResults.ToList();
        //        }

        //        var resultsFromNotes = (from note in _authenticationContext.notesModels
        //                                select note);

        //        var lastItem = (from note in _authenticationContext.notesModels
        //                        select note).Last();
        //        //else
        //        //{
        //        //    throw new Exception("Note not found");
        //        //}
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //}
    }
}