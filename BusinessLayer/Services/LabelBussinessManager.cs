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
        public async Task<bool> AddLabel(LabelModel labelModel)
        {
            try
            {
                //// Here checked labelModel contains information or not 
                if (labelModel != null)
                {
                    return await this._repositoryManager.AddLabel(labelModel);
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
        public async Task<bool> UpdateLabel(LabelModel labelModelDetails, string labelName)
        {
            try
            {
                //// Here checked labelModelDetails contains information or not 
                if (labelModelDetails != null)
                {
                    return await this._repositoryManager.UpdateLabel(labelModelDetails, labelName);
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
        public IList<LabelModel> GetLabel(string userId)
        {
            try
            {
                //// Here checked userId contains information or not 
                if (userId != null)
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
        public async Task<bool> DeleteLabel(LabelModel labelModel, int id)
        {
            try
            {
                //// Here checked labelModel contains information or not 
                if (labelModel != null)
                {
                    //// variable result store the result of DeleteLabel()  
                    var result = await this._repositoryManager.DeleteLabel(labelModel, id);
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
    }
}