using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {
    public class result {

        public bool success {get; set;}
        public object data { get; set; }
        public string error { get; set; }

    }
}
