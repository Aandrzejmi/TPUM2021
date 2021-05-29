using NUnit.Framework;
using static CommunicationAPI.Serialization;
using CommunicationAPI.Models;
using System.Collections.Generic;

namespace CommunicationTests
{
    public class Tests
    {
        CClient client;
        COrder oreder;
        CProduct product;
        CEvidenceEntry evEntry1;
        CEvidenceEntry evEntry2;
        CSendRequest request;
        List<CEvidenceEntry> list;

        string clientJSON;
        string orderJSON;
        string productJSON;
        string evEntry1JSON;
        string evEntry2JSON;
        string requestJSON;
        string listJSON;


        [SetUp]
        public void Setup()
        {
            client = new CClient() { ID = 1, Name = "Grzegorz Brz�czyszczykiewicz", Adress = "Chrz�szczyrzew�szczyce 21" };
            product = new CProduct() { ID = 1, Name = "Cement (10kg)", Price = 123.45m };

            request = new CSendRequest() { Type = typeof(CClient).ToString(), RequestedID = null };

            evEntry1 = new CEvidenceEntry() { Product = product, Amount = 7 };
            evEntry2 = new CEvidenceEntry() { Product = product, Amount = 8 };

            oreder = new COrder() { ID = 1, Client = client, Entries = new List<CEvidenceEntry>() { evEntry1, evEntry2 } };

            list = new List<CEvidenceEntry>() { evEntry1, evEntry2, evEntry1 };

            clientJSON = "{\"__type\":\"CClient:#CommunicationAPI.Models\",\"Adress\":\"Chrz�szczyrzew�szczyce 21\",\"ID\":1,\"Name\":\"Grzegorz Brz�czyszczykiewicz\"}";
            productJSON = "{\"__type\":\"CProduct:#CommunicationAPI.Models\",\"ID\":1,\"Name\":\"Cement (10kg)\",\"Price\":123.45}";

            productJSON = "{\"__type\":\"CProduct:#CommunicationAPI.Models\",\"ID\":1,\"Name\":\"Cement (10kg)\",\"Price\":123.45}";

            evEntry1JSON = "{\"__type\":\"CEvidenceEntry:#CommunicationAPI.Models\",\"Amount\":7,\"Product\":" + productJSON + "}";
            evEntry2JSON = "{\"__type\":\"CEvidenceEntry:#CommunicationAPI.Models\",\"Amount\":8,\"Product\":" + productJSON + "}";
            orderJSON = "{\"__type\":\"COrder:#CommunicationAPI.Models\",\"Client\":" + clientJSON + ",\"Entries\":[" + evEntry1JSON + "," + evEntry2JSON + "],\"ID\":1}";

            requestJSON = "{\"__type\":\"CSendRequest:#CommunicationAPI.Models\",\"RequestedID\":null,\"Type\":\"" + typeof(CClient) + "\"}";

            listJSON = $"[{evEntry1JSON},{evEntry2JSON},{evEntry1JSON}]";
        }

        [Test]
        public void SimpleSerializationTest()
        {
            Assert.AreEqual(clientJSON, Serialize(client));
            Assert.AreEqual(productJSON, Serialize(product));
            Assert.AreEqual(requestJSON, Serialize(request));
        }

        [Test]
        public void ComplexSerializationTest()
        {
            Assert.AreEqual(evEntry1JSON, Serialize(evEntry1));
            Assert.AreEqual(orderJSON, Serialize(oreder));
        }

        [Test]
        public void CollectionSerializationTest()
        {
            Assert.AreEqual(listJSON, Serialize(list));
        }

        [Test]
        public void SimpleDeserializationTest()
        {
            Assert.AreEqual(client, Deserialize<CClient>(clientJSON));
            Assert.AreEqual(product, Deserialize<CProduct>(productJSON));
            Assert.AreEqual(request, Deserialize<CSendRequest>(requestJSON));
        }

        [Test]
        public void ComplexDeserializationTest()
        {
            var deserialized = Deserialize<COrder>(orderJSON);

            Assert.AreEqual(oreder, deserialized);
            Assert.AreEqual(client, deserialized.Client);
            Assert.AreEqual(evEntry1, deserialized.Entries[0]);
            Assert.AreEqual(product, deserialized.Entries[1].Product);
        }

        [Test]
        public void CollectionDeserializationTest()
        {
            Assert.AreEqual(list, Deserialize<List<CEvidenceEntry>>(listJSON));
        }
    }
}