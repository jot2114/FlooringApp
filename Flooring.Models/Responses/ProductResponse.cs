﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class ProductResponse : Response
    {
        public List<Product> ProductList { get; set; }
    }
}
