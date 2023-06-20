using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Brand : Entity<long>
{
    public string Name { get; set; }
    public string Logo { get; set; }
}
