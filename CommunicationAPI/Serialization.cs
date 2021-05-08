using System;
using System.Collections.Generic;
using System.Text;
using CommunicationAPI.Models;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CommunicationAPI
{
    public static class Serialization
    {
        private static DataContractJsonSerializerSettings _settings = new DataContractJsonSerializerSettings()
        {
            
        };


        public static string Serialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), _settings);

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), _settings);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}
