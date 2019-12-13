// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelBussinessManager.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommanLayer.Model;
    
    /// <summary>
    /// Label Business Manager
    /// </summary>
    public interface ILabelBussinessManager
    {
        /// <summary>
        /// Add Label
        /// </summary>
        /// <param name="labelModel">label Model</param>
        /// <returns>result</returns>
        Task<bool> AddLabel(int NoteId,int UserId, List<string> labelName);

        /// <summary>
        /// Update Label
        /// </summary>
        /// <param name="labelModelDetails">label Model Details</param>
        /// <param name="labelName">labelName</param>
        /// <returns>result</returns>
        Task<bool> UpdateLabel(int noteId,int UserId, int labelId, string labelName);

        /// <summary>
        /// Get Label
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>result</returns>
        IList<LabelModel> GetLabel(int userId, int pageNumber, int LabelPerPage);

        /// <summary>
        /// Delete Label
        /// </summary>
        /// <param name="labelModel">labelModel</param>
        /// <param name="id">id</param>
        /// <returns>result</returns>
        Task<bool> DeleteLabel(int id);
    }
}
