// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteTests.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundoTestProject.NewFolder
{
    using System;
    using BusinessLayer.Services;
    using CommanLayer.Enum;
    using CommanLayer.Model;
    using Moq;
    using RepositoryLayer.Interface;
    using Xunit;

    /// <summary>
    /// NoteTests
    /// </summary>
    public class NoteTests
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        [Fact]
        public void AddNotes()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            { 
              UserId = "e3dea60d-21cc-4733-8b6c-363620d8ab0c",
              NotesTitle  = "Hello Bridgelabz",
              NotesDescription = "Come to office at 9.00 am",
              CreatedDate = DateTime.Now,
              ModifiedDate = DateTime.Now,
              color = "Yellow",
              NotesType = (EnumNoteType)0,
              Reminder = DateTime.Now,
              Image = "AssassinsCreed.jpeg",
              Trash = true,
              Archive = false,
              Pin = false
            };
          
            var data = businessLayer.AddNotes(model);
            Assert.NotNull(data);
           
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        [Fact]
       public void DeleteNotes()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            {
                Id = 1,
                UserId = "e3dea60d-21cc-4733-8b6c-363620d8ab0c",        
            };

            var data = businessLayer.AddNotes(model);
            Assert.NotNull(data);
       }

        /// <summary>
        /// Archives this instance.
        /// </summary>
        [Fact]
      public void Archive()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            {
                Id = 1,
                Archive = true,
            };

            var data = businessLayer.Archive(model.Id);
            Assert.NotNull(data);
      }

        /// <summary>
        /// Uns the archive.
        /// </summary>
        [Fact]
        public void UnArchive()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            {
                Id = 1,
                Archive = false
            };

            var data = businessLayer.UnArchive(model.Id);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Trashes this instance.
        /// </summary>
        [Fact]
        public void Trash()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            {
                Id = 1,
                Trash = true,
            };

            var data = businessLayer.Trash(model.Id);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Uns the trash.
        /// </summary>
        [Fact]
        public void UnTrash()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            {
                Id = 1,
                Trash = false,
            };

           var data = businessLayer.UnTrash(model.Id);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Pins this instance.
        /// </summary>
        [Fact]
        public void Pin()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            {
                Id = 1,
                Pin = true,
            };

            var data = businessLayer.Pin(model.Id);
            Assert.NotNull(data);
        }

        /// <summary>
        /// Un pin.
        /// </summary>
        [Fact]
        public void UnPin()
        {
            var Repository = new Mock<INotesRepository>();
            var businessLayer = new UserNotesBusiness(Repository.Object);
            var model = new NotesModel()
            {
                Id = 1,
                Pin = false,
            };

            var data = businessLayer.UnPin(model.Id);
            Assert.NotNull(data);
        }
    }
}