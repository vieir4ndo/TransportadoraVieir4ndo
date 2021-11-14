using System;
using System.IO;
using System.Threading.Tasks;
using TV.SER.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace TV.SER
{
    public class AzureStorage : ICloudStorage
    {
        private readonly IStorageConnectionFactory _storageConnectionFactory;

        public AzureStorage(IStorageConnectionFactory storageConnectionFactory)
        {
            _storageConnectionFactory = storageConnectionFactory;
        }

        public async Task DeleteImage(string name)
        {
            Uri uri = new Uri(name);
            string filename = Path.GetFileName(uri.LocalPath);
            var blobContainer = await _storageConnectionFactory.GetContainer();
            var blob = blobContainer.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var blobContainer = await _storageConnectionFactory.GetContainer();
            var thumbnailWidth = 200;
            var extension = Path.GetExtension(file.FileName);
            var encoder = GetEncoder(extension);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(file.FileName));
            using (var stream = file.OpenReadStream())
            {
                using (var output = new MemoryStream())
                using (Image image = Image.Load(stream))
                {
                    var divisor = image.Width / thumbnailWidth;
                    var height = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));

                    image.Mutate(x => x.Resize(thumbnailWidth, height));
                    image.Save(output, encoder);
                    output.Position = 0;
                    await blob.UploadFromStreamAsync(output);
                }
            }

            return blob.Uri.AbsoluteUri;

        }

        /// <summary> 
        /// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
        /// </summary> 
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }

        private static IImageEncoder GetEncoder(string extension)
        {
            IImageEncoder encoder = null;

            extension = extension.Replace(".", "");

            var isSupported = Regex.IsMatch(extension,
                                            "gif|png|jpe?g",
                                            RegexOptions.IgnoreCase);

            if (isSupported)
            {
                switch (extension)
                {
                    case "png":
                        encoder = new PngEncoder();
                        break;
                    case "jpg":
                        encoder = new JpegEncoder();
                        break;
                    case "jpeg":
                        encoder = new JpegEncoder();
                        break;
                    case "gif":
                        encoder = new GifEncoder();
                        break;
                    default:
                        break;
                }
            }

            return encoder;
        }
    }
} 