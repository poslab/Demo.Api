using DRM = Demo.Repository.Model;
using Demo.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Interfaces
{
    public interface IContactRepository<T, in TSelectModel> 
        : IBaseRepository<T, TSelectModel> where TSelectModel 
        : BaseSelectModel
    {
        Task<DRM.Contact> SearchContactAsync(string email, string phonenumber);
        Task<IEnumerable<DRM.Contact>> GetContactsAsync(string state, string city);
    }
}
