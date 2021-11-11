using System;
using System.IO;
using System.Threading.Tasks;
using TV.SER.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;

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
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(file.FileName));
            using (var stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }
            return blob.Uri.AbsoluteUri;
        }
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}