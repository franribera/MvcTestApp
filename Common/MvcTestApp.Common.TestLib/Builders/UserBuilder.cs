using System;
using System.Collections.Generic;
using System.Reflection;
using MvcTestApp.Domain.Users;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Common.TestLib.Builders
{
    public class UserBuilder
    {
        private Name _userName;
        private Password _password;
        private readonly List<Role> _roles;

        public UserBuilder()
        {
            _userName = new Name(Guid.NewGuid().ToString());
            _password = new Password(Guid.NewGuid().ToString());
            _roles = new List<Role>();
        }

        public User Build()
        {
            var user = (User)Activator.CreateInstance(typeof(User), true);
            user.GetType().GetProperty(nameof(User.UserName))?.SetValue(user,_userName);
            user.GetType().GetProperty(nameof(User.Password))?.SetValue(user,_password);
            user.GetType()
                .GetField("_roles", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)?
                .SetValue(user,_roles);

            return user;
        }

        public UserBuilder WithName(Name value)
        {
            _userName = value;
            return this;
        }

        public UserBuilder WithPassword(Password value)
        {
            _password = value;
            return this;
        }

        public UserBuilder WithRoles(params Role[] value)
        {
            _roles.AddRange(value);
            return this;
        }
    }
}
