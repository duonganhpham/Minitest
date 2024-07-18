using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Business.Model
{
    public class ProductRequestModel
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string ?Description { get; set; }
    }
}
