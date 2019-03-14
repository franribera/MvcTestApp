using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvcTestApp.Domain.Infrastructure;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Users
{
    public class User : Aggregate
    {
        public static User Admin = new User(new Name("Admin"), new Password("Admin"), new List<Role> { Role.ADMIN, Role.PAGE_1, Role.PAGE_2, Role.PAGE_3 }) { Id = new Guid("00000000-0000-0000-0000-000000000001") };
        public static User User1 = new User(new Name("User1"), new Password("User1"), new List<Role> {Role.PAGE_1}) {Id = new Guid("00000000-0000-0000-0000-000000000002") };
        public static User User2 = new User(new Name("User2"), new Password("User2"), new List<Role> {Role.PAGE_2}) { Id = new Guid("00000000-0000-0000-0000-000000000003") };
        public static User User3 = new User(new Name("User3"), new Password("User3"), new List<Role> {Role.PAGE_3}) { Id = new Guid("00000000-0000-0000-0000-000000000004") };

        private ICollection<Role> _roles;

        public Name UserName { get; protected set; }
        public Password Password { get; protected set; }
        public ReadOnlyCollection<Role> Roles => _roles.ToList().AsReadOnly();

        protected User()
        {
            _roles = new List<Role>();
        }

        public User(Name userName, Password password, IEnumerable<Role> roles) : this()
        {
            SetName(userName);
            SetPassword(password);
            SetRoles(roles);
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

            if(!rolesList.Any()) throw new ArgumentException("User shall have one role at least.", nameof(roles));

            if (rolesList.Count != rolesList.Distinct().Count())
                throw new ArgumentException("User can't have duplicated roles.", nameof(roles));

            _roles = rolesList;
        }
    }
}
