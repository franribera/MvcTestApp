using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Application.Commands.Users.Delete;

namespace MvcTestApp.Application.Tests.Commands.Users.Delete
{
    [TestClass]
    public class DeleteUserRequestTests
    {
        [TestMethod]
        public void UserName_Must_Be_Set()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DeleteUserRequest(null));
        }
    }
}
