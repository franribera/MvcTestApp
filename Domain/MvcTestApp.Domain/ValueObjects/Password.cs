using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using MvcTestApp.Domain.Infrastructure;

namespace MvcTestApp.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        public string Value { get; protected set; }

        protected Password()
        {
        }

        public Password(string value) : this()
        {
            if (value.Trim() == string.Empty) throw new ArgumentException("Password cannot be empty.", nameof(value));

            Value = ComputeHash(value);
        }

        protected static string ComputeHash(string value)
        {
            // I'm aware of the vulnerabilities...
            var valueBytes = Encoding.Unicode.GetBytes(value);
            var passwordHash = MD5.Create().ComputeHash(valueBytes);
            return Convert.ToBase64String(passwordHash);
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}