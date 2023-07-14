using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries.GetAll;
public class GetAllCarItemDto : IDto
{
    public int Id { get; set; }
    public string Plate { get; set; }
    public string Image { get; set; }
    public int Kilometer { get; set; }
}
