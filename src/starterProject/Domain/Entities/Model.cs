using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Model : Entity<long>
{
    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; }
    public string Name { get; set; }
}
