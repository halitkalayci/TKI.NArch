using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payment.Services.Models;
public class Payment3DResponseModel
{
    public string Html { get; set; }
    public string Link { get; set; }
    public string ConversationId { get; set; }
}
