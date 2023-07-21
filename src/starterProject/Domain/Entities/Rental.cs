using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Rental : Entity<long>
{
    public long CarId { get; set; }
    public int CustomerId { get; set; }

    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string PaymentId { get; set; }
    public short Status { get; set; }

    public virtual Car Car { get; set; }
    public virtual Customer Customer { get; set; }

}
