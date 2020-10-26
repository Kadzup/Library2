using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library2
{
    class BookOrder
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserTicket { get; set; }

        [ForeignKey("UserTicket")]
        public User User { get; set; }

        public Guid BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public DateTime PickDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public override string ToString()
        {
            return string.Join(" | ", new object[] { Id, UserTicket, BookId, PickDate, ReturnDate });
        }
    }
}
