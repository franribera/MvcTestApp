using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Application.Commands.Users.Create;

namespace MvcTestApp.Application.Tests.Commands.Users.Create
{
    [TestClass]
    public class CreateUserRequestTests
    {
        [TestMethod]
        public void UserName_Must_Be_Set()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CreateUserRequest(null, "", new List<string>()));
        }

        [TestMethod]
        public void Password_Must_Be_Set()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CreateUserRequest("", null, new List<string>()));
        }

        [TestMethod]
        public void Roles_Must_Be_Set()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CreateUserRequest("", "", null));
        }

        [TestMethod]
        public void Roles_Shall_Contain_One_Role_At_Least()
        {
            Assert.ThrowsException<ArgumentException>(() => new CreateUserRequest("", "", new List<string>()));
        }

        [TestMethod]
        public void Roles_Cannot_Contain_Duplicates()
        {
            Assert.ThrowsException<ArgumentException>(() => new CreateUserRequest("", "", new List<string> {"role", "role"}));
        }
    }
}