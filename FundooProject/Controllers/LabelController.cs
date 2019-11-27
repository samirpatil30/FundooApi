// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooProject.Controllers
{
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// LabelController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
   ////[ApiController]
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
        public async Task<IActionResult> AddLabel(LabelModel labelModel)
        {
            var result = await this._bussinessManager.AddLabel(labelModel);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>result</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLabel(LabelModel labelModel, string labelName)
        {
            var result = await this._bussinessManager.UpdateLabel(labelModel, labelName);
            return this.Ok(new { result });          
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result</returns>
        [HttpGet]
        public IActionResult GetLabel(string userId)
        {
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
        public async Task<IActionResult> DeleteLabel(LabelModel labelModel, int id)
        {
            var result = await this._bussinessManager.DeleteLabel(labelModel, id);
            return this.Ok(new { result });
        }
    }
}