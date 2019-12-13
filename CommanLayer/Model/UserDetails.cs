// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDetails.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// User Details
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Key]

        public int id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        [RegularExpression("^[a-zA-Z ]*$")]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        public string ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public string UserType { get; set; }

        /// <summary>
        /// Gets or sets the service identifier.
        /// </summary>
        /// <value>
        /// The service identifier.
        /// </value>
        public string ServiceId { get; set; }
    }
}