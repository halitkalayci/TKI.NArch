using CloudinaryDotNet;
using Infrastructure.FileUpload.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileUpload.Services.Cloudinary;
public class CloudinaryAdapter : IFileUploadAdapter
{
    public Task<string> UploadImage(string fileBase64)
    {
        Account account = new Account("dusm8cdbj", "816141497288549", "HNgo1lyYqrN8uOv0VhkPTDtjYGE");
    }
}
