// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserNotesBusiness.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;

    /// <summary>
    /// User Notes Business
    /// </summary>
    public class UserNotesBusiness : IUserNotesBusiness
    {
        /// <summary>
        /// Create the instance variable of interface INotesRepository
        /// </summary>
        private readonly INotesRepository _notesRepository;

        /// <summary>
        /// User NotesBL
        /// </summary>
        /// <param name="notesRepository"></param>
        public UserNotesBusiness(INotesRepository notesRepository)
        {
            this._notesRepository = notesRepository;
        }

        /// <summary>
        /// Add Notes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> AddNotes(NotesModel model)
        {
            try
            {
                //// if checks the model is Null or not
                if (model != null)
                {
                    return this._notesRepository.AddNotes(model);
                }
                else
                {
                    throw new Exception("Empty notes");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Get Notes
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User id</returns>
        public IList<NotesModel> GetNotes(int userId,  int pageNumber, int NotePerPage )
        {
            try
            {
                //// if checks the model is Null or not
               // if (UserId != null)
                //{
                    var result = this._notesRepository.GetNotes(userId, pageNumber, NotePerPage);
                    return result;
               // }
               // else
                //{
                    throw new Exception("Invalid User id");
               // }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Update Notes
        /// </summary>
        /// <param name="notesModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateNotes(NotesModel notesModel, int id)
        {
            try
            {
                //// if checks the notesModel is Null or not
                if (notesModel != null)
                {
                    var result = await this._notesRepository.UpdateNotes(notesModel, id);
                    return result;
                }
                else
                {
                    throw new Exception("notes are not updated");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Delete Notes the notes from Database
        /// </summary>
        /// <param name="notesModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteNotes( int id)
        {
            try
            {
                //// if checks the notesModel is Null or not
                if (id != 0)
                {
                    var result = await  this._notesRepository.DeleteNotes(id);
                    return result;
                }
                else
                {
                    throw new Exception("notes are not deleted");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Add image.
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Image is not uploaded</exception>
        public async Task<string> AddImage(int userid, int id, IFormFile file)
        {
            try
            {
                //CloudinaryImageUpload cloudinary = new CloudinaryImageUpload();
               // var url = cloudinary.UploadImageOnCloud(file);
                if (userid != 0)
                {
                    var result = await this._notesRepository.AddImage(userid, id, file);
                    return result;
                }
                else
                {
                    throw new Exception("Image is not uploaded");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object AddReminder(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Archives the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">Note is not found select the correct note</exception>
        public async Task<bool> Archive(int id)
        {           
            try
            {
                var result = await this._notesRepository.Archive(id);
                if (id != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Note is not found select the correct note");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Un archive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// id
        /// </returns>
        /// <exception cref="Exception">Unable to Urachive note</exception>
        public async Task<bool> UnArchive(int id)
        {
            try
            {
                var result = await this._notesRepository.UnArchive(id);

                if (id != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Unable to Urachive note");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Trashes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">Unable to trash note</exception>
        public async Task<bool> Trash(int id)
        {
            try
            {
                var result = await this._notesRepository.Trash(id);
                if (id != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Unable to trash note");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Un trash.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">Unable to restore note</exception>
        public async Task<bool> UnTrash(int id)
        {
            try
            {
                var result = await this._notesRepository.UnTrash(id);
                if (id != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Unable to restore note");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Pins the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">Unable to pin note</exception>
        public async Task<bool> Pin(int id)
        {
            try
            {
                var result = await this._notesRepository.Pin(id);
                if (id != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Unable to pin note");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Un pin.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">Unable to pin note</exception>
        public async Task<bool> UnPin(int id)
        {
            try
            {
                var result = await this._notesRepository.UnPin(id);
                if (id != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Unable to pin note");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="id">identifier.</param>
        /// <param name="time">time.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">add reminder failed</exception>
        public async Task<bool> AddReminder(int id, DateTime time)
        {
            try
            {
                var result = await this._notesRepository.AddReminder(id, time);
                if (result != false)
                {
                    return result;
                }
                else
                {
                    throw new Exception("add reminder failed");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        /// <exception cref="Exception">Unable to remove reminder</exception>
        public async Task<bool> DeleteReminder(int id)
        {
            try
            {
                var result = await this._notesRepository.DeleteReminder(id);

                if (result != false)
                {
                    return result;
                }
                else
                {
                    throw new Exception("Unable to remove reminder");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Collabration the notes.
        /// </summary>
        /// <param name="Noteid"></param>
        /// <param name="senderId"></param>
        /// <returns>
        /// NoteId
        /// </returns>
        /// <exception cref="Exception">Unable to collabrate note</exception>
        public async Task<bool> Collabrate(int Noteid, IList<string> senderId, int CurrentUser)
        {
            try
            {             
                if(senderId != null)
                {
                    return await this._notesRepository.Collabrate(Noteid, senderId,CurrentUser);
                }
                else
                {
                    throw new Exception("Unable to collabrate note");
                }
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
        /// <returns>id</returns>
        /// <exception cref="Exception">Unable to delete bulk notes</exception>
        public async Task<bool> BulkTrash(IList<int> id)
        {
            try
            {
                var result = await this._notesRepository.BulkTrash(id);
                if (result != false)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Unable to delete bulk notes");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IList<NotesModel> Search(string word, int UserId)
        {
            try
            {
                if(UserId > 0)
                {
                    return _notesRepository.Search(word, UserId);
                }
                else
                {
                    throw new Exception("Unable to search notes");
                }                
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Searches the specified note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>note</returns>
        /// <exception cref="Exception"></exception>
        //public IList<NotesModel> Search(string note)
        //{
        //    try
        //    {
        //       return this._notesRepository.Search(note);

        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}
    }
}