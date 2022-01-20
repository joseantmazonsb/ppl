using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using PluggablePersistenceLayer.Sql;

namespace Sql.Test.Models {
    public class Booking : SqlEntity {
        private Guid UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override bool Equals(object? obj) {
            if (obj is not Booking asBooking) return false;
            return asBooking.User.Equals(User)
                && asBooking.StartDate.ToString(CultureInfo.InvariantCulture).Equals(StartDate.ToString(CultureInfo.InvariantCulture)) 
                && asBooking.EndDate.ToString(CultureInfo.InvariantCulture).Equals(EndDate.ToString(CultureInfo.InvariantCulture)) 
                && asBooking.Location.Equals(Location);
        }

        public override int GetHashCode() {
            return HashCode.Combine(User, StartDate, EndDate, Location);
        }
    }
}