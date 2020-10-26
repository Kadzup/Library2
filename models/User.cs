using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library2
{
    class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ThirdName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        [InverseProperty("Client")]
        public virtual ICollection<BookOrder> Orders { get; set; }

        public override string ToString()
        {
            return string.Join(" | ", new object[] { Id, FirstName, LastName, ThirdName, BirthDate, Address, Phone });
        }
    }
}
