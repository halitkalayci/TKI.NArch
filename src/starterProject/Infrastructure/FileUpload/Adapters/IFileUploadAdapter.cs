using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileUpload.Adapters;
public interface IFileUploadAdapter
{
    Task<string> UploadImage(string fileBase64);
}
