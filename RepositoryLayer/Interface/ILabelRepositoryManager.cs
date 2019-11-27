// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelRepositoryManager.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using CommanLayer.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface ILabelRepositoryManager
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns></returns>
        Task<bool> AddLabel(LabelModel labelModel);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        Task<bool> UpdateLabel(LabelModel labelModel, string label);

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>userId</returns>
        IList<LabelModel> GetLabel(string userId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>labelModel</returns>
        Task<bool> DeleteLabel(LabelModel labelModel, int id);
    }
}
