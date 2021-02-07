using AutoMapper;
using Demo.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Api.Test.Controllers
{
    public class ContactControllerTest
    {
        [Fact]
        public async Task Successful_Get_Contact_Returns_Okay()
        {
            // Arrange
            var contactService = new Mock<Demo.Services.Interfaces.IContactService>();

            var mapper = new Mock<IMapper>();

            var mockLogger = new Mock<ILogger<ContactController>>();

            var testController = new ContactController(mapper.Object, contactService.Object, mockLogger.Object);

            // Act
            var result = await testController.Get(1);

            // Assert     
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Bad_Get_Contact_Returns_BadRequest()
        {
            // Arrange
            var contactService = new Mock<Demo.Services.Interfaces.IContactService>();

            var mapper = new Mock<IMapper>();

            var mockLogger = new Mock<ILogger<ContactController>>();

            var testController = new ContactController(mapper.Object, contactService.Object, mockLogger.Object);

            // Act
            var result = await testController.Get(0);

            // Assert    
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
