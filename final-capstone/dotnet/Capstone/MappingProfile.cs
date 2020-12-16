using AutoMapper;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.AddressTable, AddressTableResponse>();
            CreateMap<RenterInformationRequest, Entities.RenterInformation>();
            CreateMap<Entities.RenterInformation, RenterInformationResponse>();
            CreateMap<LeaseRequest, Entities.Lease>();
            CreateMap<Entities.Lease, LeaseResponse>();
            CreateMap<Entities.Properties, PropertiesResponse>();
            CreateMap<Entities.MaintenanceRequest, MaintenanceResponse>();
        }
    }
}
