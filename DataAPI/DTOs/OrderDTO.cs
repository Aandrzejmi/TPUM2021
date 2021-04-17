using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI.DTOs
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public List<EvidenceEntryDTO> Products { get; set; } = new List<EvidenceEntryDTO>();
        public ClientDTO Client{ get; set; }
    }
}
