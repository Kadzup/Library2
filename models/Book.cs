using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library2
{
    class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public DateTime IssueDate { get; set; }

        public int Price { get; set; }

        [InverseProperty("Book")]
        public virtual ICollection<BookOrder> Orders { get; set; }

        public override string ToString()
        {
            return string.Join(" | ", new object[] { Id, Title, AuthorName, IssueDate, Price });
        }
    }
}
