using CloudinaryDotNet;
using Infrastructure.FileUpload.Adapters;
using CloudinaryDotNet.Actions;

namespace Infrastructure.FileUpload.Services.Cloudinary;
public class CloudinaryAdapter : IFileUploadAdapter
{
    public async Task<string> UploadImage(string fileBase64)
    {
        Account account = new Account("dusm8cdbj", "816141497288549", "HNgo1lyYqrN8uOv0VhkPTDtjYGE");

        CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);
       
        var parameters = new ImageUploadParams()
        {
            File = new FileDescription(fileBase64),
        };

        var fileUploadResponse = await cloudinary.UploadAsync(parameters);

        return fileUploadResponse.SecureUri.AbsoluteUri;
    }
}
