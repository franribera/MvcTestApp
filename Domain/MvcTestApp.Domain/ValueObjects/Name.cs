using System;
using System.Collections.Generic;
using MvcTestApp.Domain.Infrastructure;

namespace MvcTestApp.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; protected set; }

        protected Name()
        {
        }

        public Name(string value) : this()
        {
            if (value.Trim() == string.Empty) throw new ArgumentException("Name cannot be empty.", nameof(value));

            Value = value;
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}