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

        public void SetName(Name name)
        {
            UserName = name;
        }

        public void SetPassword(Password password)
        {
            Password = password;
        }

        public void SetRoles(IEnumerable<Role> roles)
        {
            var rolesList = roles.ToList();

            if(!rolesList.Any()) throw new ArgumentException("User shall has one role at least.", nameof(roles));

            if (rolesList.Count != rolesList.Distinct().Count())
                throw new ArgumentException("User can't has duplicated roles.", nameof(roles));

            _roles = rolesList;
        }
    }
}
