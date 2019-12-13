// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPasswordModel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.Model
{
    /// <summary>
    /// ResetPasswordModel
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        /// <value>
        /// The Password.
        /// </value>
        public string Password { get; set; }
        public string token { get; set; }
    }
}