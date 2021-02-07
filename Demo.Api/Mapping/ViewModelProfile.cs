using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DRM = Demo.Repository.Model;
using DSM = Demo.Services.Model;
using VM = Demo.Api.ViewModel;

namespace Demo.Api.Mapping
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            // Map Data Model To Domain Model
            CreateMap<DRM.Contact, DSM.Contact>();
            CreateMap<DRM.Company, DSM.Company>();
            CreateMap<DRM.Address, DSM.Address>();

            //=============================================================

            // Map Domain Model To Data Model
            CreateMap<DSM.Contact, DRM.Contact>();
            CreateMap<DSM.Company, DRM.Company>();
            CreateMap<DSM.Address, DRM.Address>();

            //=============================================================

            // Map View Model To Domain Model
            CreateMap<VM.Contact, DSM.Contact>();
            CreateMap<VM.Company, DSM.Company>();
            CreateMap<VM.Address, DSM.Address>();

            //=============================================================

            // Map Domain Model To View Model
            CreateMap<DSM.Contact, VM.Contact>();
            CreateMap<DSM.Company, VM.Company>();
            CreateMap<DSM.Address, VM.Address>();

            //=============================================================
        }
    }
}
