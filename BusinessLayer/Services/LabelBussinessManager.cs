// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelBussinessManager.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using BusinessLayer.Interface;
    using CommanLayer.Model;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// LabelBusinessManager
    /// </summary>
    public class LabelBussinessManager : ILabelBussinessManager
    {
        /// <summary>
        /// create the instance variable of ILabelRepositoryManager 
        /// </summary>
        private readonly ILabelRepositoryManager _repositoryManager;

        /// <summary>
        /// Create the constructor of class with parameter 
        /// </summary>
        /// <param name="repositoryManager">repository Manager</param>
        public LabelBussinessManager(ILabelRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        /// <summary>
        /// Add Label
        /// </summary>
        /// <param name="labelModel">label Model</param>
        /// <returns></returns>
        public async Task<bool> AddLabel(int NoteId, int UserId, List<string> labelName)
        {
            try
            {
                //// Here checked labelModel contains information or not 
                if (UserId != 0)
                {
                    return await this._repositoryManager.AddLabel(NoteId,UserId, labelName);
                }
                else
                {
                    throw new Exception("Label not added");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Update Label 
        /// </summary>
        /// <param name="labelModelDetails">label Model Details</param>
        /// <param name="labelName">label Name</param>
        /// <returns> this._repositoryManager.UpdateLabel(labelModelDetails, labelName);</returns>
        public async Task<bool> UpdateLabel(int noteId,int UserId, int labelId, string labelName)
        {
            try
            {
                //// Here checked labelModelDetails contains information or not 
                if (UserId > 0)
                {
                    return await this._repositoryManager.UpdateLabel(noteId,UserId, labelId, labelName);
                }
                else
                {
                    throw new Exception("Label is not updated");
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
        /// <param name="userId"></param>
        /// <returns>userId</returns>
        public IList<LabelModel> GetLabel(int userId)
        {
            try
            {
                //// Here checked userId contains information or not 
                if (userId > 0)
                {
                   var result = this._repositoryManager.GetLabel(userId);
                    return result; 
                }
                else
                {
                    throw new Exception("Invalid label");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Delete Label
        /// </summary>
        /// <param name="labelModel">labelModel</param>
        /// <param name="id">id</param>
        /// <returns>result</returns>
        public async Task<bool> DeleteLabel(int id)
        {
            try
            {
                //// Here checked labelModel contains information or not 
                if (id > 0)
                {
                    //// variable result store the result of DeleteLabel()  
                    var result = await this._repositoryManager.DeleteLabel(id);
                    return result;
                }
                else
                {
                    throw new Exception("label are not deleted");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Task<LabelModel> AddLabelWithoutNoteId(string label, string UserId)
        {
            try
            {
                if (label != null)
                {
                    return this._repositoryManager.AddLabelWithoutNoteId(label, UserId);
                }
                else
                {
                    throw new Exception("Empty");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}