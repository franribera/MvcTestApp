using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Application.Commands.Users.Update;

namespace MvcTestApp.Application.Tests.Commands.Users.Update
{
    [TestClass]
    public class UpdateUserRequestTests
    {
        [TestMethod]
        public void Id_Must_Be_Different_Than_Empty_Guid()
        {
            Assert.ThrowsException<ArgumentException>(() => new UpdateUserRequest(Guid.Empty, "","", new List<string>()));
        }
    }
}
