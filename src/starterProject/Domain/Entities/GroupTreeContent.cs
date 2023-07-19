using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class GroupTreeContent : Entity<int>
{
    public string Title { get; set; }
    public string Target { get; set; }
    public string Icon { get; set; }
    public int RowOrder { get; set; }
    public bool ShowOnAuth { get; set; }
    public bool HideOnAuth { get; set; }
    public int? ParentId { get; set; }
    public GroupTreeContentType Type { get; set; }
}
