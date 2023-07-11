using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car : Entity<long>
    {
        public string Image { get; set; }
        public int Kilometer { get; set; }
        public string Plate { get; set; }
        public short MinFindeksCreditRate { get; set; }
        public CarStates CarState { get; set; } // 0 = Available, 1= Rented, 2=Mainteance
    }
}
