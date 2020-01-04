// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserNotesBusiness.cs" company="Bridgelabz">
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
    /// User Notes
    /// </summary>
    public interface IUserNotesBusiness
    {
        /// <summary>
        /// Add Notes
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>models</returns>
        Task<bool> AddNotes(NotesModel model);

        /// <summary>
        /// Get Notes
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>user id</returns>
        IList<NotesModel> GetNotes(int userId );

        /// <summary>
        /// Update Notes
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="id">id</param>
        /// <returns>model, id</returns>
        Task<bool> UpdateNotes(NotesModel model, int id);

        /// <summary>
        /// Delete Notes
        /// </summary>
        /// <param name="notesModel">notes Model</param>
        /// <param name="id">id</param>
        /// <returns>notesModel, id</returns>
        Task<bool> DeleteNotes( int id);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>user id, file</returns>
        Task<string> AddImage(int userid, int id, IFormFile file);

        /// <summary>
        /// Archives the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> Archive(int id, int UserId);

        /// <summary>
        /// Un archive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> UnArchive(int id);

        /// <summary>
        /// Trashes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> Trash(int id,int UserId);

        /// <summary>
        /// Un trash.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> UnTrash(int id);

        /// <summary>
        /// Pins the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> Pin(int id);

        /// <summary>
        /// Un pin.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> UnPin(int id);

        /// <summary>
        /// Add reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="time">The time.</param>
        /// <returns>id, time</returns>
        Task<bool> AddReminder(int id, NotesModel time, int UserId);

        /// <summary>
        /// Delete reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> DeleteReminder(int id);

        /// <summary>
        /// Collabration the notes.
        /// </summary>
        /// <param name="collabrationModel">The collabration model.</param>
        /// <returns>NoteId</returns>
        Task<bool> Collabrate(int Noteid, IList<string> senderId, int CurrentUser);

        Task<bool> BulkTrash(IList<int> id);

        IList<NotesModel> Search(string word, int UserId);

        IList<NotesModel> GetArchiveNotes(int userId);

        IList<NotesModel> GetTrashNotes(int userId);

        bool ColorService(ColorModel data);
    }
}
