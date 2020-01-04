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
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// NotesController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
  
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
       //[Route("")]
        public async Task<IActionResult> AddNotes(NotesModel notesModel)
        {
            notesModel.UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
            var result = await this._userNotes.AddNotes(notesModel);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result</returns>
        [HttpGet]   
        [Route("Notes")]       
        public IActionResult GetNotes()
        {
            int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
            var result = this._userNotes.GetNotes(UserId);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("{noteId}")]
        public async Task<IActionResult> UpdateNotes(NotesModel details, int noteId)
        {
            try
            {
                int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
                details.UserId = UserId;
               
                var results = await _userNotes.UpdateNotes(details, noteId);
                if (results)
                {
                    return Ok(new { status = true, message = "Successfully Updated", results });
                }
                else
                {
                    return BadRequest(new { status = false, message = "Failed to Update ", results });
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            

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
        [Route("{noteId}/Image")]
        public IActionResult AddImage( int noteId, IFormFile file)
        {
            try
            {
                int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
                var urlOfImage = this._userNotes.AddImage(UserId, noteId, file);

                if(urlOfImage != null)
                {
                    return this.Ok(new {success= true, message="Image Uploaded Successfully",  urlOfImage });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Image is not uploaded" });
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
          
            //// return urlOfImage;
             

         
        }

        /// <summary>
        /// Archives the specified identifier.
        /// </summary>
        /// <param name="NoteId">The identifier.</param>
        /// <returns>id</returns>
        [HttpPost]
        [Route("{NoteId}/Archive")]
        public async Task<IActionResult> Archive(int NoteId)
        {
            int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
            var status = await _userNotes.Archive(NoteId,UserId);
                if (status == true)
                {
                    var message = "Note Archive successfully";
                    return Ok(new { status, message });
                }
                else
                {
                    var message = "unable to Archive note";
                    return Ok(new { status, message });
                }
            
            
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
        [Route("{NoteId}/Trash")]
   
        public async Task<IActionResult> Trash(int NoteId)
        {
            try
            {
                int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
                var status = await _userNotes.Trash(NoteId, UserId);
                if (status == true)
                {
                    var message = "Note trash successfully";
                    return Ok(new { status, message });
                }
                else
                {
                    var message = "unable to trash note";
                    return BadRequest(new { status, message });
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }

        /*
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
        } */

        /// <summary>
        /// Pins the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("Pin")]
        public async Task<IActionResult> Pin(int id)
        {
            var status = await _userNotes.Pin(id);
            if (status)
            {
                var results = " Success ";
                return Ok(new { status, results});
            }
            else
            {
                var results = "Failed ";
                return Ok(new { status, results });
            }
        }

      /*  /// <summary>
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
          */

        /// <summary>
        /// Adds the reminder.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="time">The time.</param>
        /// <returns>result</returns>
        [HttpPost]
        [Route("{Id}/Reminder")]
        public async Task<IActionResult> AddReminder(int id, NotesModel time)
        {
            try
            {
              
                int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
                var result = await this._userNotes.AddReminder(id, time, UserId);
                return this.Ok(new { result });
            }
            catch(Exception exception)
            {
                throw exception;
            }
         
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
        [Route("{EmailsList}/{id}/CollabrateNotes")]
        public async Task<IActionResult> Collabrate(int id, string EmailsList)
        {
            string[] ArrayOfemails = EmailsList.Split(',');
            List<string> emails = new List<string>(ArrayOfemails);
            int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);

            try
            {
                var result = await this._userNotes.Collabrate(id, emails, UserId);

                if(result == true)
                {
                    return this.Ok(new { success = true, message = "Collabrate Success", result });
                }
                else
                {
                    var message = "unable to Collabrate note";
                    return BadRequest(new { success=  false, message });
                }
              
            }
            catch(Exception exception)
            {
                throw exception;
            }
             
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
        [HttpGet]
        [Route("search")]
        public IList<NotesModel> Search(string word, int UserId)
        {
            var result = this._userNotes.Search(word,UserId);
            return result;
        }


        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result</returns>
        [HttpGet]
        [Route("GetArchiveNotes")]
        public IActionResult GetArchiveNotes()
        {
            int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
            var result = this._userNotes.GetArchiveNotes(UserId);
            return this.Ok(new { result });
        }

        [HttpGet]
        [Route("GetTrashNotes")]
        public IActionResult GetTrashNotes()
        {
            int UserId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
            var result = this._userNotes.GetTrashNotes(UserId);
            return this.Ok(new { result });
        }

        [HttpPut]
        [Route("{id}/{color}/color")]
        //[AllowAnonymous]
        public async Task<IActionResult> ColorService(int id, string color)
        {
            var userId = HttpContext.User.Claims.First(c => c.Type == "Id").Value;
            ColorModel colorObj = new ColorModel();
            colorObj.noteId = id;
            colorObj.UserId =  Convert.ToInt32(userId);
            colorObj.color = color;
            var results = _userNotes.ColorService(colorObj);
            if (results)
            {
                return Ok(new { status = true, message = "successfull", data = "" });
            }
            else
            {
                return Ok(new { results = "Failed to change " });
            }
        }
    }
}