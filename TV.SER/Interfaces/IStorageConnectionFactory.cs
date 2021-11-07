using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;

namespace TV.SER.Interfaces
{
    public interface IStorageConnectionFactory
    {
        Task<CloudBlobContainer> GetContainer();
    }
}