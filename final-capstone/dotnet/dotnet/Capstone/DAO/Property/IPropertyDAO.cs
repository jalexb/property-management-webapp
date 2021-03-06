﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IPropertyDAO
    {
        List<PropertyAndAddress> getProperties();

        PropertyAndAddress GetProperty(int id);
        decimal GetPropertyPrice(int property_id);
    }
}
