using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAPI.DTOs
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public ClientDTO Client{ get; set; }
        public int ClientID => Client.ID;
        public string ClientName { get => Client.Name; set => Client.Name = value; }
        public string ClientAdress { get => Client.Adress; set => Client.Adress = value; }

        public List<EvidenceEntryDTO> Products { get; set; } = new List<EvidenceEntryDTO>();
    }
}
