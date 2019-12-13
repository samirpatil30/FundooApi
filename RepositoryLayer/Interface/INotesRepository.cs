// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotesRepository.cs" company="Bridgelabz">
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
    /// Notes Repository
    /// </summary>
    public interface INotesRepository
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>noteModel</returns>
        Task<bool> AddNotes(NotesModel notesModel);

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        IList<NotesModel> GetNotes(int UserId, int pageNumber, int NotePerPage);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> UpdateNotes(NotesModel model, int id);

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> DeleteNotes( int id);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>id</returns>
        Task<string> AddImage( int userid, int id, IFormFile file);

        /// <summary>
        /// Archives the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> Archive(int id);

        /// <summary>
        /// Uns the archive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> UnArchive(int id);

        /// <summary>
        /// Trashes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> Trash(int id);

        /// <summary>
        /// Un trash.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> UnTrash(int id);

        /// <summary>
        /// Pins the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> Pin(int id);

        /// <summary>
        /// Un pin.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> UnPin(int id);

        /// <summary>
        /// Add reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="time">The time.</param>
        /// <returns>id</returns>
        Task<bool> AddReminder(int id, DateTime time);

        /// <summary>
        /// Delete reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> DeleteReminder(int id);

        /// <summary>
        /// Collabrates the specified noteid.
        /// </summary>
        /// <param name="Noteid">The noteid.</param>
        /// <param name="senderId">The sender identifier.</param>
        /// <returns>senderId</returns>
        Task<bool> Collabrate(int Noteid, IList<string> senderId, int CurrentUser);

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        Task<bool> BulkTrash(IList<int> id);

        IList<NotesModel> Search(string word, int UserId);
    }
}