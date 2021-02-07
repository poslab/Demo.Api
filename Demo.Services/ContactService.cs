using AutoMapper;
using Demo.Repository.Interfaces;
using Demo.Services.Interfaces;
using Demo.Services.Model;
using Demo.Services.SelectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRM = Demo.Repository.Model;
using DSM = Demo.Services.Model;

namespace Demo.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository<DRM.Contact, ContactSelectModel> _contactRepository;

        public ContactService(
            IMapper mapper,
            IContactRepository<DRM.Contact, ContactSelectModel> contactRepository)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<Contact> GetContactAsync(int userID)
        {
            var contact = await _contactRepository.GetSingleRecordAsync(new ContactSelectModel() { UserID = userID });
            return _mapper.Map<DSM.Contact>(contact);
        }

        public async Task<int> CreateContactAsync(DSM.Contact contact)
        {
            var drm_contact = _mapper.Map<DRM.Contact>(contact);
            return await _contactRepository.InsertAsync(drm_contact);
        }
        public async Task<int> UpdateContactAsync(DSM.Contact contact)
        {
            var drm_contact = _mapper.Map<DRM.Contact>(contact);
            return await _contactRepository.UpdateAsync(drm_contact);
        }
        public async Task<int> DeleteContactAsync(int userID)
        {
            return await _contactRepository.DeleteAsync(new ContactSelectModel() { UserID = userID });
        }

        public async Task<DSM.Contact> SearchContactAsync(string email, string phonenumber)
        {
            var contact = await _contactRepository.SearchContactAsync(email, phonenumber);
            return _mapper.Map<DSM.Contact>(contact);
        }
        public async Task<List<DSM.Contact>> GetContactsAsync(string state, string city)
        {
            var contact = await _contactRepository.GetContactsAsync(state, city); 
            return _mapper.Map<List<DSM.Contact>>(contact);
        }

    }
}
