namespace gov.cityofboston.hubhacks2.api {

    public partial class Activation {

        public static Activation CreateActivation() {

            var a = new Activation();
            a.Created = System.DateTime.UtcNow;
            a.LastSeen = System.DateTime.UtcNow;

            return a;
        }
    }
}