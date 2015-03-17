using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {
    public class result {

        public static result FAILED = new result(false);
        public static result NO_RESULTS = new result(true, "No Results");

        public bool success {get; set;}
        public object data { get; set; }
        public string error { get; set; }


        public result() {
        }


        public result(bool success, string error = null) {
            this.success = success;
            this.error = error;
        }

    }
}
