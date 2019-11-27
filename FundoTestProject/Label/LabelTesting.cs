using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommanLayer.Model;
using Moq;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FundoTestProject.Label
{
   public class LabelTesting
    {
        /// <summary>
        /// Labels this instance.
        /// </summary>

        [Fact]
        public void Label()
        {
            var repository = new Mock<ILabelRepositoryManager>();
            var bussiness = new LabelBussinessManager(repository.Object);
            var model = new LabelModel()
            {
                UserId = "7fb3bc2f-bd4c-49df-a2e4-be6740f8c499",
                Label = "MyLabel",
            };

            var data = bussiness.AddLabel(model);

            Assert.NotNull(data);

        }


        /// <summary>
        /// Updates the label.
        /// </summary>
        [Fact]
        public void UpdateLabel()

        {
            var mock = new Mock<ILabelRepositoryManager>();
            var bussiness = new LabelBussinessManager(mock.Object);
            var model = new LabelModel()
            {
                UserId = "Hello Bridgelabz",
                Label = "MyLabel",
            };

            var data = bussiness.UpdateLabel(model,"Hello Bridgelabz");
            Assert.NotNull(data);
        }
    }
}