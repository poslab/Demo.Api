using System.Collections.Generic;
using System.Threading.Tasks;
using DSM = Demo.Services.Model;

namespace Demo.Services.Interfaces
{
    public interface IContactService
    {
        Task<DSM.Contact> GetContactAsync(int userID);
        Task<int> CreateContactAsync(DSM.Contact contact);
        Task<int> UpdateContactAsync(DSM.Contact contact);
        Task<int> DeleteContactAsync(int userID);
        Task<DSM.Contact> SearchContactAsync(string email, string phonenumber);
        Task<List<DSM.Contact>> GetContactsAsync(string state, string city);
    }
}
