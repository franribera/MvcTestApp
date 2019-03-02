using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Tests.Users
{
    [TestClass]
    public class UserTests
    {
        private User _defaultUser;

        [TestInitialize]
        public void TestInitialize()
        {
            _defaultUser = new User(new Name("Name"), new Password("Password"));
        }

        [TestMethod]
        public void AddRole_AddsRoleToUserRolesCollection()
        {
            // Act
            _defaultUser.AddRole(Role.PAGE_1);

            // Assert
            Assert.IsTrue(_defaultUser.Roles.Contains(Role.PAGE_1));
        }

        [TestMethod]
        public void AddRole_ExistingRole_ThrowsException()
        {
            // Act
            _defaultUser.AddRole(Role.PAGE_1);

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => _defaultUser.AddRole(Role.PAGE_1));
        }

        [TestMethod]
        public void AddRole_NotExistingRole_RoleIsAdded()
        {
            // Act
            _defaultUser.AddRole(Role.PAGE_1);
            _defaultUser.AddRole(Role.PAGE_2);

            // Assert
            Assert.IsTrue(_defaultUser.Roles.Contains(Role.PAGE_1));
            Assert.IsTrue(_defaultUser.Roles.Contains(Role.PAGE_2));
        }
    }
}
