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
            evEntry1 = new CEvidenceEntry() { ProductID = 1, ProductAmount = 7 };
            evEntry2 = new CEvidenceEntry() { ProductID = 2, ProductAmount = 8 };

            oreder = new COrder() { ID = 1, ClientID = 1, Products = new List<CEvidenceEntry>() { evEntry1, evEntry2 } };

            clientJSON = "{\"Adress\":\"Chrz¹szczyrzew¹szczyce 21\",\"ID\":1,\"Name\":\"Grzegorz Brzêczyszczykiewicz\"}";
            productJSON = "{\"ID\":1,\"Name\":\"Cement (10kg)\",\"Price\":123.45}";
            evEntry1JSON = "{\"ProductAmount\":7,\"ProductID\":1}";
            evEntry2JSON = "{\"ProductAmount\":8,\"ProductID\":2}";

            orderJSON = "{\"ClientID\":1,\"ID\":1,\"Products\":[" + evEntry1JSON + "," + evEntry2JSON + "]}";

        }

        [Test]
        public void SimpleSerializationTest()
        {
            Assert.AreEqual(clientJSON, Serialize(client));
            Assert.AreEqual(productJSON, Serialize(product));
            Assert.AreEqual(evEntry1JSON, Serialize(evEntry1));
        }

        [Test]
        public void ComplexSerializationTest()
        {
            Assert.AreEqual(orderJSON, Serialize(oreder));
        }

        [Test]
        public void SimpleDeserializationTest()
        {
            Assert.AreEqual(client, Deserialize<CClient>(clientJSON));
            Assert.AreEqual(product, Deserialize<CProduct>(productJSON));
            Assert.AreEqual(evEntry2, Deserialize<CEvidenceEntry>(evEntry2JSON));
        }

        [Test]
        public void ComplexDeserializationTest()
        {
            var deserialized = Deserialize<COrder>(orderJSON);

            Assert.AreEqual(oreder, deserialized);
            Assert.AreEqual(evEntry1, deserialized.Products[0]);
            Assert.AreEqual(evEntry2, deserialized.Products[1]);
        }
    }
}