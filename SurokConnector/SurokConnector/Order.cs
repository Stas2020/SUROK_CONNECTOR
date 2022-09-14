using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace SurokConnector
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public string order_guid;
        [DataMember]
        public int order_num;
        [DataMember]
        public int waiter_id;
        [DataMember]
        public int table_num;
        [DataMember]
        public bool refund;
        [DataMember]
        public List<Item> items;
        public string GetJsonStr()
        {
            MemoryStream memory_stream = new MemoryStream();
            DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(Order), new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("dd.MM.yyyy HH:mm:ss")
            });

            Serializer.WriteObject(memory_stream, this);

            memory_stream.Position = 0;
            StreamReader sr = new StreamReader(memory_stream);
            string json_order = sr.ReadToEnd();

            return json_order;
        }

    }
}
