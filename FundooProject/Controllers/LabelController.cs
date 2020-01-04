// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// LabelController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
     [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
   [ApiController]
    [Authorize]

    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The business manager
        /// </summary>
        private readonly ILabelBussinessManager _bussinessManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="bussinessManager">The bussiness manager.</param>
        public LabelController(ILabelBussinessManager bussinessManager)
        {
            this._bussinessManager = bussinessManager;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>result</returns>
        [HttpPost]
        public async Task<IActionResult> AddLabel(int NoteId,int UserId, List<string> labelName)
        {
            var result = await this._bussinessManager.AddLabel(NoteId,UserId,labelName);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>result</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLabel(int noteId,int UserId, int labelId, string labelName)
        {
            var result = await this._bussinessManager.UpdateLabel(noteId,UserId, labelId, labelName);
            return this.Ok(new { result });          
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result</returns>
        [HttpGet]
        public IActionResult GetLabel()
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
            var result = this._bussinessManager.GetLabel(userId);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteLabel(int id)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == "Id").Value);
            var result = await this._bussinessManager.DeleteLabel(id);
            return this.Ok(new { result });
        }

        [HttpPost]
        [Route("{label}/Add")]
        public async Task<IActionResult> AddLabel(string label)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "Id").Value;
                var results = await this._bussinessManager.AddLabelWithoutNoteId(label, userId);
                if (results != null)
                {
                    return Ok(new { status = true, message = "Added Successfully", data = results });
                }
                else
                {
                    return BadRequest(new { status = false, message = " Failed ", data = "" });
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            
        }
    }
}