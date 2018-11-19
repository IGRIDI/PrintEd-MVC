using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace lr3
{
    [Serializable]
    public class Publication
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Author { get; set; }
        public int Purchases { get; set; }
        [NonSerialized()]
        public DateTime Data_n;
      
        public DateTime Data
        {
            get { return this.Data_n; }
            set { this.Data_n = value; }
        }

        public Publication(string name, int cost, string author, int purchases, DateTime data)
        {
            Name = name;
            Cost = cost;
            Author = author;
            Purchases = purchases;
            Data = data;
            //data = DateTime.Now;
        }
    }
}
