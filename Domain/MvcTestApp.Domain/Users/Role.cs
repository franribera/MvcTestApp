using System.Collections.Generic;
using MvcTestApp.Domain.Infrastructure;
using MvcTestApp.Domain.ValueObjects;

namespace MvcTestApp.Domain.Users
{
    public class Role : ValueObject
    {
        // Built-in roles
        public static Role PAGE_1 = new Role(new Name("PAGE_1"));
        public static Role PAGE_2 = new Role(new Name("PAGE_2"));
        public static Role PAGE_3 = new Role(new Name("PAGE_3"));

        public Name Name { get; protected set; }

        protected Role()
        {
        }

        public Role(Name name) : this()
        {
            Name = name;
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}