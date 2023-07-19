using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupTreeContents.Queries.GetAll;
public class GetAllGroupTreeContentDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
    public string Target { get; set; }
    public string Icon { get; set; }
    public int RowOrder { get; set; }
    public bool ShowOnAuth { get; set; }
    public bool HideOnAuth { get; set; }
    public ICollection<string> Roles { get; set; }
    public GroupTreeContentType Type { get; set; }
}
