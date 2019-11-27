// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollabrationModel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Collabration Model
    /// </summary>
    public class CollabrationModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
        public string CurrentUserId { get; set; }


        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("UserDeatils")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }
    }
}