using System.Security.AccessControl;
using TV.SER.DTOs;
using System.Threading.Tasks;

namespace TV.SER.Interfaces
{
    public interface IEmail
    {
         Task Send(string emailAdress, string body, EmailOptionsDTO options);
    }
}