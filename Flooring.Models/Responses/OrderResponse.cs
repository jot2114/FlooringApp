﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class OrderResponse : Response
    {
        public List<Order> OrderList { get; set; }
    }
}
