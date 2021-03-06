﻿using AutoMapper;
using Capstone.Entities;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Renter
{
    public class RenterService : IRenterService
    {
        private readonly final_capstoneContext _dbContext;
        private readonly IMapper _mapper;

        public RenterService(final_capstoneContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public RenterInformationResponse GetRenterInfo(int id)
        {
            var renterInfo = _dbContext.RenterInformation.Where(i => i.UserId == id).FirstOrDefault();
            return _mapper.Map<RenterInformationResponse>(renterInfo);
        }

        public bool SaveRenter(RenterInformationRequest request)
        {
            try
            {
                var renterinfo = _mapper.Map<Entities.RenterInformation>(request);

                _dbContext.RenterInformation.Add(renterinfo);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateRenter(RenterInformationRequest request)
        {
            try
            {
                var renterinfo = _mapper.Map<Entities.RenterInformation>(request);

                _dbContext.Update(renterinfo);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
