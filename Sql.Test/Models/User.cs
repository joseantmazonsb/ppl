using System;
using System.Collections.Generic;
using PluggablePersistenceLayer.Sql;

namespace Sql.Test.Models {
    public class User : SqlEntity {
        public virtual string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public virtual DateTime JoinDate { get; set; }
        
        public override bool Equals(object? obj) {
            if (obj is not User asUser) return false;
            return asUser.Email.Equals(Email) && asUser.Nickname.Equals(Nickname);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Nickname, Email);
        }
    }
}