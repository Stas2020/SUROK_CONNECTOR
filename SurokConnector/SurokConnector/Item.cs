using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SurokConnector
{
    [DataContract]
    public class Item
    {
        [DataMember]
        public int barcode;
        [DataMember]
        public String name;
        [DataMember]
        public decimal amount;
        [DataMember]
        public decimal price;
        [DataMember]
        public bool it_ordered;
        [DataMember]
        public List<Item> modifier_items;
    }
}
