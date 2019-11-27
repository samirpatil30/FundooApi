// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationContext.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Context
{
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// AuthenticationContext
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext" />
    public class AuthenticationContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.</param>
        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public DbSet<ApplicationUser> User { get; set; }

        /// <summary>
        /// Gets or sets the notes models.
        /// </summary>
        /// <value>
        /// The notes models.
        /// </value>
        public DbSet<NotesModel> notesModels { get; set; }

        /// <summary>
        /// Gets or sets the label models.
        /// </summary>
        /// <value>
        /// The label models.
        /// </value>
        public DbSet<LabelModel> labelModels { get; set; }

        /// <summary>
        /// Gets or sets the collabrations.
        /// </summary>
        /// <value>
        /// The collabrations.
        /// </value>
        public DbSet<CollabrationModel> Collabrations { get; set; }

        /// <summary>
        /// Gets or sets the searches.
        /// </summary>
        /// <value>
        /// The searches.
        /// </value>
        public DbSet<SearchModel> searches { get; set; }
    }
}