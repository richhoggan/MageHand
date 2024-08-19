using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Agent {
    public static class Extensions {

        public static byte[] Serialize<T>(this T data) {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var memoryStream = new MemoryStream()) {
                serializer.WriteObject(memoryStream, data);
                return memoryStream.ToArray();
            }
        }

        public static T Deserialize<T>(this byte[] data) {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var memoryStream = new MemoryStream()) {
                return (T)serializer.ReadObject(memoryStream);
            }
        }
    }
}
