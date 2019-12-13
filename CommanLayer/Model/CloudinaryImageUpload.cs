// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloudinaryImageUpload.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.Model
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Cloudinary Image Upload
    /// </summary>
    public class CloudinaryImageUpload
    {
        /// <summary>
        /// Upload Image On Cloud
        /// </summary>
        /// <param name="file">file</param>
        /// <returns>Url</returns>
        public string UploadImageOnCloud(IFormFile file)
        {
            var nameOfImage = file.FileName;

            ////Here provide the information of user account like CloudName,ApiKey,SecretKey
            Account myAccount = new Account("mypersonalimagecloud", "195418184769875", "KsBsYEpjmd2RJ-B11TIAtlEOots");

            //// Create the instance of Cloudinary
            Cloudinary _cloudinary = new Cloudinary(myAccount);   
            var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(nameOfImage, stream)
            };

            //// Upload the Image on Cloud Using Upload()
            var uploadResult = _cloudinary.Upload(uploadParams);

            //// Return the Url of Image
            return uploadResult.Uri.ToString();
        }
   }
}