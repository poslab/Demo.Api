using Demo.Repository.Interfaces;
using Demo.SharedKernel;
using Demo.SharedKernel.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRM = Demo.Repository.Model;

namespace Demo.Repository
{
    public class ContactRepository<T, TSelectModel> : BaseRepository<T, TSelectModel>, IContactRepository<T, TSelectModel>
        where TSelectModel : BaseSelectModel
    {
        public ContactRepository(IOptions<ConnectionStrings> connectionStringsOption) : base(connectionStringsOption.Value.DemoDB)
        {
        }
        public override async Task<T> GetSingleRecordAsync(TSelectModel whereClause)
        {
            // Dummy task
            await Task.Delay(0);

            var company = new DRM.Company();

            company.CompanyID = 1;
            company.CompanyName = "Google";

            var address = new DRM.Address();
            address.AddressID = 1;
            address.Address1 = "123 Good Place";
            address.City = "Big City";
            address.PostalCode = "23456";
            address.State = "IL";

            var contact = new DRM.Contact();
            contact.UserID = 1;
            contact.FirstName = "Test 1";
            contact.LastName = "Demo";
            contact.HomePhone = "8472203453";
            contact.Company = company;
            contact.Address = address;
            contact.ImageFileUrl = "www.google.com/image/32168";

            return (T)Convert.ChangeType(contact, typeof(T));
        }
        public async Task<DRM.Contact> SearchContactAsync(string email, string phonenumber)
        {
            // Database handling
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    return (await conn.QueryAsync<DRM.Contact>($"SearchContact_Select", new { Email = email, PhoneNumber = phonenumber }, commandType: CommandType.StoredProcedure)).ToList().FirstOrDefault();
            //}

            // Dummy task
            await Task.Delay(0);

            var company = new DRM.Company();

            company.CompanyID = 2;
            company.CompanyName = "Microsoft";

            var address = new DRM.Address();
            address.AddressID = 2;
            address.Address1 = "345 Good Place";
            address.City = "Little City";
            address.PostalCode = "23456";
            address.State = "IL";

            var contact = new DRM.Contact();
            contact.UserID = 2;
            contact.FirstName = "Test 2";
            contact.LastName = "Demo";
            contact.HomePhone = "8472203453";
            contact.Company = company;
            contact.Address = address;
            contact.ImageFileUrl = "www.google.com/image/32168";
            
            return contact;
        }
        public async Task<IEnumerable<DRM.Contact>> GetContactsAsync(string state, string city)
        {
            // Database handling
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    return (await conn.QueryAsync<DRM.Contact>($"GetContacts_Select", new { State = state, City = city }, commandType: CommandType.StoredProcedure)).ToList();
            //}

            // Dummy task
            await Task.Delay(0);

            var res = new List<DRM.Contact>();

            var company = new DRM.Company();

            company.CompanyID = 2;
            company.CompanyName = "Microsoft";

            var address = new DRM.Address();
            address.AddressID = 2;
            address.Address1 = "345 Good Place";
            address.City = "Little City";
            address.PostalCode = "23456";
            address.State = "IL";

            var contact = new DRM.Contact();
            contact.UserID = 2;
            contact.FirstName = "Test 2";
            contact.LastName = "Demo";
            contact.HomePhone = "8472203453";
            contact.Company = company;
            contact.Address = address;
            contact.ImageFileUrl = "www.google.com/image/32168";

            res.Add(contact);

            company = new DRM.Company();

            company.CompanyID = 1;
            company.CompanyName = "Google";

            address = new DRM.Address();
            address.AddressID = 1;
            address.Address1 = "123 Good Place";
            address.City = "Big City";
            address.PostalCode = "23456";
            address.State = "IL";

            contact = new DRM.Contact();
            contact.UserID = 1;
            contact.FirstName = "Test 1";
            contact.LastName = "Demo";
            contact.HomePhone = "8472203453";
            contact.Company = company;
            contact.Address = address;
            contact.ImageFileUrl = "www.google.com/image/32168";

            res.Add(contact);

            return res;
        }
    }
}
