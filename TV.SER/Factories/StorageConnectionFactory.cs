using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using TV.SER.DTOs;
using TV.SER.Interfaces;

namespace TV.SER.Factories
{
    public class StorageConnectionFactory : IStorageConnectionFactory
    {
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobContainer;
        private readonly CloudStorageOptionsDTO _storageOptions;

        public StorageConnectionFactory(CloudStorageOptionsDTO storageOptions)
        {
            _storageOptions = storageOptions;
        }

        public async Task<CloudBlobContainer> GetContainer()
        {
            if (_blobContainer != null)
                return _blobContainer;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageOptions.ConnectionString);

            _blobClient = storageAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(_storageOptions.ProfilePicsContainer);
            await _blobContainer.CreateIfNotExistsAsync();

            await _blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            return _blobContainer;
        }
    }
}