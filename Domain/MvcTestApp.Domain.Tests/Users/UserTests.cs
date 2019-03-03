using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Tests.Builders;
using MvcTestApp.Domain.Users;

namespace MvcTestApp.Domain.Tests.Users
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void AddRole_UserWithoutRoles_AddsRoleToUser()
        {
            // Arrange
            var user = new UserBuilder().Build();

            // Act
            user.AddRole(Role.PAGE_1);

            // Assert
            Assert.IsTrue(user.Roles.Contains(Role.PAGE_1));
        }

        [TestMethod]
        public void AddRole_ExistingRole_ThrowsException()
        {
            // Arrange
            var user = new UserBuilder().Build();

            // Act
            user.AddRole(Role.PAGE_1);

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => user.AddRole(Role.PAGE_1));
        }

        [TestMethod]
        public void AddRole_NotExistingRole_RoleIsAdded()
        {
            // Arrange
            var user = new UserBuilder()
                .WithRoles(new[]{ Role.PAGE_1 })
                .Build();

            // Act
            user.AddRole(Role.PAGE_2);

            // Assert
            Assert.IsTrue(user.Roles.Contains(Role.PAGE_2));
        }

        [TestMethod]
        public void RemoveRole_UserHasTheSpecifiedRole_RemovesRoleFromUser()
        {
            // Arrange
            var user = new UserBuilder()
                .WithRoles(new[] { Role.PAGE_1 })
                .Build();

            // Act
            user.RemoveRole(Role.PAGE_1);

            // Assert
            Assert.IsFalse(user.Roles.Contains(Role.PAGE_1));
        }

        [TestMethod]
        public void RemoveRole_NotExistingRole_ThrowsException()
        {
            // Arrange
            var user = new UserBuilder()
                .WithRoles(new[] { Role.PAGE_1 })
                .Build();

            // Act - Assert
            Assert.ThrowsException<InvalidOperationException>(() => user.RemoveRole(Role.PAGE_2));
        }
    }
}
