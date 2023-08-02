using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class FileUploads : Entity<Guid>
{
    public string FileName { get; set; }
    public string Destination { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}
