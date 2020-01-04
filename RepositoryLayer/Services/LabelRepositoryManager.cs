// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepositoryManager.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using CommanLayer.Model;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System.Data;
    using System.Data.SqlTypes;

    /// <summary>
    /// LabelRepositoryManager
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.ILabelRepositoryManager" />
    public class LabelRepositoryManager : ILabelRepositoryManager
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Create the Reference Variable of AuthenticationContext  
        /// </summary>
        private readonly AuthenticationContext _authenticationContext;

        /// <summary>
        /// LabelRepositoryManager
        /// </summary>
        /// <param name="authenticationContext"></param>
        public LabelRepositoryManager(AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            _authenticationContext = authenticationContext;
            _configuration = configuration;
        }

        /// <summary>
        /// Add Label
        /// </summary>
        /// <param name="labelModel">labelModel</param>
        /// <returns>result</returns>
        public async Task<bool> AddLabel(int noteId,int UserId, List<string> labelName)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            try
            {
                SqlCommand sqlcommand = new SqlCommand();
               
                //con.Open();
                int row = 0;
                foreach (var label in labelName)
                {
                    sqlcommand = new SqlCommand("SPAddLabel", con);
                    sqlcommand.CommandType = CommandType.StoredProcedure;  
                    sqlcommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlcommand.Parameters.AddWithValue("@noteId", noteId);
                    sqlcommand.Parameters.AddWithValue("@Label", label);
                    //sqlcommand.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                    //qlcommand.Parameters.AddWithValue("@ModifiedDate", DateTime.UtcNow);
                    con.Open();
                     row= await sqlcommand.ExecuteNonQueryAsync();
                    con.Close();
                }
                
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
        /// Update Label
        /// </summary>
        /// <param name="labelModelDetails"></param>
        /// <param name="labelName">labelName</param>
        /// <returns>result</returns>
        public async Task<bool> UpdateLabel(int noteId, int UserId, int labelId, string labelName)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            try
            {
                SqlCommand sqlcommand = new SqlCommand("UpdateLabel", con);
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.Parameters.AddWithValue("@UserId", UserId);
                sqlcommand.Parameters.AddWithValue("@noteId", noteId);
                sqlcommand.Parameters.AddWithValue("@Label", labelName);
                    sqlcommand.Parameters.AddWithValue("@LabelId", labelId);
                    con.Open();
                    int row = await sqlcommand.ExecuteNonQueryAsync();
                    con.Close();

                
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Get Label
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>list</returns>
        public IList<LabelModel> GetLabel(int userId)
        {
            try
            {
                IList<LabelModel> list = new List<LabelModel>();
                SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                SqlCommand sqlCommand = new SqlCommand("SPGetLabel", con);
                con.Open();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("UserId", userId);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    var labelModel = new LabelModel()
                    {
                        UserId = userId,
                        Label = Convert.ToString(dataReader["Label"]),
                        CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]),
                        ModifiedDate = Convert.ToDateTime(dataReader["ModifiedDate"]),
                    };
                    list.Add(labelModel);
                }

                //// Here the Linq querey return the Record match in Database

                //int count = list.Count();
                //int CurrentPage = pageNumber;
                //int PageSize = LabelPerPage;
                //int TotalCount = count;

                //// Calculating Totalpage by Dividing (No of Records / Pagesize)  
                //int TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
                //var items = list.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

                //var paginationMetadata = new
                //{
                //    totalCount = TotalCount,
                //    pageSize = PageSize,
                //    currentPage = CurrentPage,
                //    totalPages = TotalPages,
                //};

                return list;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Delete Label
        /// </summary>
        /// <param name="labelModel"></param>
        /// <param name="id">id</param>
        /// <returns>labelModel</returns>
        public async Task<bool> DeleteLabel(int id)
        {
            SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
            SqlCommand sqlCommand = new SqlCommand("SPDeleteLabel", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@LabelId",id);
            con.Open();
            
            int row = await sqlCommand.ExecuteNonQueryAsync();
            con.Close();
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LabelModel> AddLabelWithoutNoteId(string label, string UserId)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:connectionDb"]);
                LabelModel userList = new LabelModel();
              
                    SqlCommand sqlCommand = new SqlCommand("CreateLabelWithoutNoteId", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@NoteId", 0);
                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlCommand.Parameters.AddWithValue("@Label", label);

                    sqlConnection.Open();
                   // await sqlCommand.ExecuteNonQueryAsync();

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                   
                    while (reader.Read())
                    {
                        ////userList.Id = Convert.ToInt32(sdreader["Id"]);

                        userList.Id = Convert.ToInt32(reader["Id"]);
                        userList.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        userList.Label = reader["Label"].ToString();
                    }
                    sqlConnection.Close();

                

                return userList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}