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
            CreateMap<RenterInformationRequest, Entities.RenterInformation>();
            CreateMap<PendingLeasesRequest, Entities.PendingLeases>();
        }
    }
}
