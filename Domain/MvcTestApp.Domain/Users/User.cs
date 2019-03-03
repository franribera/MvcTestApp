using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvcTestApp.Domain.Infrastructure;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Users
{
    public class User : Aggregate
    {
        private ICollection<Role> _roles;

        public Name UserName { get; protected set; }
        public Password Password { get; protected set; }
        public ReadOnlyCollection<Role> Roles => _roles.ToList().AsReadOnly();

        protected User()
        {
            _roles = new List<Role>();
        }

        public User(Name userName, Password password) : this()
        {
            UserName = userName;
            Password = password;
        }

        public void AddRole(Role role)
        {
            if(_roles.Contains(role)) throw new InvalidOperationException("User already has the specified role assigned.");

            _roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            if (!_roles.Contains(role)) throw new InvalidOperationException("User does not have the specified role.");

            _roles.Remove(role);
        }
    }
}
