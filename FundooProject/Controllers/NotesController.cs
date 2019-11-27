// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// NotesController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    ////[ApiController]
    ////Authorize attribute to the controller class, then any action methods on the controller will be only available to authenticated users. 
    [Authorize]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The user notes
        /// </summary>
        private readonly IUserNotesBusiness _userNotes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="userNotes">The user notes.</param>
        public NotesController(IUserNotesBusiness userNotes)
        {
            this._userNotes = userNotes;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>result</returns>
        [HttpPost]
        ////[//Route("AddNotes")]
        public async Task<IActionResult> AddNotes(NotesModel notesModel)
        {
            var result = await this._userNotes.AddNotes(notesModel);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("Get/{userId}")]
        public IActionResult getNotes(int userId, int pageNumber, int NotePerPage)
        {
            var result = this._userNotes.GetNotes(userId, pageNumber, NotePerPage);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPut]
        //// [Route("updateNotes")]
        public async Task<IActionResult> UpdateNotes(NotesModel notesModel, int id)
        {
            var result = await this._userNotes.UpdateNotes(notesModel, id);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpDelete]
        ////[Route("DeleteNotes")]
        public async Task<IActionResult> DeleteNotes( int id)
        {
            var result = await this._userNotes.DeleteNotes(id);
            var noteResult = "Note is Deleted";
            return this.Ok(new { result, noteResult });
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>urlOfImage</returns>
        [HttpPost]
        [Route("Image")]
        public IActionResult AddImage(int userId, int id, IFormFile file)
        {
            var urlOfImage = this._userNotes.AddImage(userId, id, file);
            //// return urlOfImage;
            return this.Ok(new { urlOfImage });
        }

        /// <summary>
        /// Archives the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        [HttpPost]
        [Route("Archive")]
        public Task<bool> Archive(int id)
        {
            return this._userNotes.Archive(id);
        }

        /// <summary>
        /// Un archive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("UnArchive")]
        public async Task<IActionResult> UnArchive(int id)
        {
            var result = await this._userNotes.UnArchive(id);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Trash specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Trash")]
        public async Task<IActionResult> Trash(int id)
        {
            var result = await this._userNotes.Trash(id);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Un trash.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("RestoreNotes")]
        public async Task<IActionResult> UnTrash(int id)
        {
            var result = await this._userNotes.UnTrash(id);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Pins the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Pin")]
        public async Task<IActionResult> Pin(int id)
        {
            var result = await this._userNotes.Pin(id);
            return this.Ok(new { result }); 
        }

        /// <summary>
        /// Unpins the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Unpin")]
        public async Task<IActionResult> Unpin(int id)
        {
            var result = await this._userNotes.UnPin(id);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="time">The time.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Reminder")]
        public async Task<IActionResult> AddReminder(int id,  DateTime time)
        {
            var result = await this._userNotes.AddReminder(id, time);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>id</returns>
        [HttpDelete]
        [Route("Reminder")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var result = await this._userNotes.DeleteReminder(id);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Collabrate the specified noteid.
        /// </summary>
        /// <param name="Noteid">The noteid.</param>
        /// <param name="senderId">The sender identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CollabrateNotes")]
        public async Task<IActionResult> Collabrate(int Noteid, IList<string> senderId, int CurrentUser)
        {
            var result = await this._userNotes.Collabrate(Noteid, senderId, CurrentUser);
            return this.Ok(new { result }); 
        }

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("BulkDelete")]
        public async Task<IActionResult> BulkTrash(IList<int> id)
        {
            var result = await this._userNotes.BulkTrash(id);
            return this.Ok(new {result} );
        }

        /// <summary>
        /// Searches the specified notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("search")]
        //public IList<NotesModel> Search(string notes)
        //{
        //    var result = this._userNotes.Search(notes);
        //    return result;
        //}
    }
}