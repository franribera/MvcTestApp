using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Common.TestLib.Builders;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Domain.Tests.Users
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void SetRoles_EmptyRolesCollection_ThrowsException()
        {
            // Arrange
            var user = new UserBuilder().WithRoles(Role.PAGE_1).Build();

            // Act
            Assert.ThrowsException<ArgumentException>(() => user.SetRoles(new List<Role>()));
        }

        [TestMethod]
        public void SetRoles_DuplicatedROles_ThrowsException()
        {
            // Arrange
            var user = new UserBuilder().WithRoles().Build();
            var roles = new List<Role> { Role.PAGE_1, Role.PAGE_1 };

            // Act
            Assert.ThrowsException<ArgumentException>(() => user.SetRoles(roles));
        }
    }
}
