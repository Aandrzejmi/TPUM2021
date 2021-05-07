﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DataAPI
{
    public class EvidenceEntry : IModel
    {
        public int ID { get { return ProductID; } set { ID = ProductID; } }

        public int ProductID { get; set; }
        public int ProductAmount { get; set; }
        public object Clone()
        {
            return new EvidenceEntry() { ProductID = ProductID, ProductAmount = ProductAmount };
        }
    }
}
