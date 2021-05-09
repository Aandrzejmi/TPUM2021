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

        string clientJSON;
        string orderJSON;
        string productJSON;
        string evEntry1JSON;
        string evEntry2JSON;

        [SetUp]
        public void Setup()
        {
            client = new CClient() { ID = 1, Name = "Grzegorz Brzêczyszczykiewicz", Adress = "Chrz¹szczyrzew¹szczyce 21" };
            product = new CProduct() { ID = 1, Name = "Cement (10kg)", Price = 123.45m };

            evEntry1 = new CEvidenceEntry() { Product = product, Amount = 7 };
            evEntry2 = new CEvidenceEntry() { Product = product, Amount = 8 };

            oreder = new COrder() { ID = 1, Client = client, Entries = new List<CEvidenceEntry>() { evEntry1, evEntry2 } };

            clientJSON = "{\"Adress\":\"Chrz¹szczyrzew¹szczyce 21\",\"ID\":1,\"Name\":\"Grzegorz Brzêczyszczykiewicz\"}";
            productJSON = "{\"ID\":1,\"Name\":\"Cement (10kg)\",\"Price\":123.45}";

            evEntry1JSON = "{\"Amount\":7,\"Product\":" + productJSON + "}";
            evEntry2JSON = "{\"Amount\":8,\"Product\":" + productJSON + "}";
            orderJSON = "{\"Client\":" + clientJSON + ",\"Entries\":[" + evEntry1JSON + "," + evEntry2JSON + "],\"ID\":1}";

        }

        [Test]
        public void SimpleSerializationTest()
        {
            Assert.AreEqual(clientJSON, Serialize(client));
            Assert.AreEqual(productJSON, Serialize(product));
        }

        [Test]
        public void ComplexSerializationTest()
        {
            Assert.AreEqual(evEntry1JSON, Serialize(evEntry1));
            Assert.AreEqual(orderJSON, Serialize(oreder));
        }

        [Test]
        public void SimpleDeserializationTest()
        {
            Assert.AreEqual(client, Deserialize<CClient>(clientJSON));
            Assert.AreEqual(product, Deserialize<CProduct>(productJSON));
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
    }
}