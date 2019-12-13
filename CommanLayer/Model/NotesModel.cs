// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CommanLayer.Enum;
   
    /// <summary>
    /// Notes Model
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id of the user.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId
        /// </value>
        [ForeignKey("UserDetails")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the NotesTitle.
        /// </summary>
        /// <value>
        /// The NotesTitle of the notes
        /// </value>
        public string NotesTitle { get; set; }

        /// <summary>
        /// Gets or sets the NotesDescription.
        /// </summary>
        /// <value>
        /// The NotesDescription of the notes.
        /// </value>
        public string NotesDescription { get; set; }

        /// <summary>
        /// Gets or sets the CreateDate.
        /// </summary>
        /// <value>
        /// The CreateDate of the notes.
        /// </value>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Modified.
        /// </summary>
        /// <value>
        /// The ModifiedDate of the notes.
        /// </value>
       public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the Color.
        /// </summary>
        /// <value>
        /// The Color of the Notes.
        /// </value>        
  
        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$")]
        public string color { get; set; }

        /// <summary>
        /// Gets or sets the NotesType.
        /// </summary>  
        /// <value>
        /// The NotesType of the notes.
        /// </value>
        public EnumNoteType NotesType { get; set; }

        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public DateTime Reminder { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NotesModel"/> is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if trash; otherwise, <c>false</c>.
        /// </value>
        public bool Trash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NotesModel"/> is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if archive; otherwise, <c>false</c>.
        /// </value>
        public bool Archive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NotesModel"/> is pin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if pin; otherwise, <c>false</c>.
        /// </value>
        public bool Pin { get; set; } 
    }
}