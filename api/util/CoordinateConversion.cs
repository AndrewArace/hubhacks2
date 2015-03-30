using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Configuration;


namespace gov.cityofboston.hubhacks2.api.util {

    public static class CoordinateConversion {

        [DllImport("corpscon_v6.dll")]
        private static extern int corpscon_default_config();

        [DllImport("corpscon_v6.dll")]
        private static extern int SetInSystem(int val);

        [DllImport("corpscon_v6.dll")]
        private static extern int SetInDatum(int val);


        [DllImport("corpscon_v6.dll")]
        private static extern int SetInUnits(int val);


        [DllImport("corpscon_v6.dll")]
        private static extern int SetInZone(int val);


        [DllImport("corpscon_v6.dll")]
        private static extern int SetOutSystem(int val);


        [DllImport("corpscon_v6.dll")]
        private static extern int SetOutDatum(int val);


        [DllImport("corpscon_v6.dll")]
        private static extern int SetOutUnits(int val);


        [DllImport("corpscon_v6.dll")]
        private static extern int SetOutZone(int val);


        [DllImport("corpscon_v6.dll")]
        private static extern int corpscon_initialize_convert();


        [DllImport("corpscon_v6.dll")]
        private static extern int SetXIn(double val);


        [DllImport("corpscon_v6.dll")]
        private static extern int SetYIn(double val);


        [DllImport("corpscon_v6.dll")]
        private static extern int corpscon_convert();


        [DllImport("corpscon_v6.dll")]
        private static extern double GetXOut();


        [DllImport("corpscon_v6.dll")]
        private static extern double GetYOut();

        [DllImport("corpscon_v6.dll")]
        private static extern int SetNadconPath(char[] path);

        [DllImport("corpscon_v6.dll")]
        private static extern int corpscon_clean_up();

        public static double[] StatePlaneUnityShift(double[] sp) {
            double[] newSp = new double[2];
            newSp[0] = sp[0] - 200000.000;
            newSp[1] = sp[1] - 893000.000;
            return newSp;
        }


        public static double[] LatLongToMAStatePlane(double longitude, double latitude, ref string strErrorMessage) {
            int ret = 0;
            double[] gp = new double[3];
            ret = 0;

            //Set default configuration.
            ret = corpscon_default_config();
            if ((ret != 1)) {
                strErrorMessage = "Could not initialize default config." + " (code = " + ret + ")";
                return gp;
            }

            //Use Set functions to systems, datums, zones, units, etc.
            //Input is Geographic
            ret = SetInSystem(1);
            if ((ret != 1)) {
                strErrorMessage = "Could not set input Geographic coordinate system." + " (code = " + ret + ")";
                return gp;
            }

            //Input is NAD83/86
            ret = SetInDatum(1983);
            if ((ret != 1)) {
                strErrorMessage = "Could not set input datum." + " (code = " + ret + ")";
                return gp;
            }

            //Output is a state plane
            ret = SetOutSystem(2);
            if ((ret != 1)) {
                strErrorMessage = "Could not set output to State Plane." + " (code = " + ret + ")";
                return gp;
            }

            //Output Datum is NAD83/86
            ret = SetOutDatum(1983);
            if ((ret != 1)) {
                strErrorMessage = "Could not set output datum." + " (code = " + ret + ")";
                return gp;
            }

            //Output to MA State Plane (Mainland)
            ret = SetOutZone(2001);
            if ((ret != 1)) {
                strErrorMessage = "Could not set output to MA State Plane." + " (code = " + ret + ")";
                return gp;
            }

            //Output is in Meters
            ret = SetOutUnits(3);
            if ((ret != 1)) {
                strErrorMessage = "Could not set output to MA State Plane." + " (code = " + ret + ")";
                return gp;
            }

            //Configure the DLL internally.
            ret = corpscon_initialize_convert();
            if ((ret != 1)) {
                strErrorMessage = "Could initialize conversion." + " (code = " + ret + ")";
                return gp;
            }

            //Set input values.
            ret = SetXIn(longitude);
            if ((ret != 1)) {
                strErrorMessage = "Could set input Longitude." + " (code = " + ret + ")";
                return gp;
            }

            //Set input values.
            ret = SetYIn(latitude);
            if ((ret != 1)) {
                strErrorMessage = "Could set input Latitude." + " (code = " + ret + ")";
                return gp;
            }

            //Convert values input above.
            ret = corpscon_convert();
            if ((ret != 1)) {
                strErrorMessage = "Could perform conversion." + " (code = " + ret + ")";
                return gp;
            }

            //Get output values.
            gp[0] = GetXOut();
            gp[1] = GetYOut();

            //Clean up after conversions.
            ret = corpscon_clean_up();
            if ((ret != 1)) {
                strErrorMessage = "Cleanup failed." + " (code = " + ret + ")";
                return gp;
            }

            strErrorMessage = "";
            return gp;
        }


        public static double[] MAStatePlaneToLatLong(double maXin, double maYIn, ref string strErrorMessage) {
            int ret = 0;
            double[] gp = new double[3];

            ret = 0;

            //Set default configuration.
            ret = corpscon_default_config();
            if ((ret != 1)) {
                strErrorMessage = "Could not initialize default config." + " (code = " + ret + ")";
                return gp;
            }
            /*
            ret = SetNadconPath(ConfigurationManager.AppSettings["NADCON_PATH"].ToCharArray());
            if ((ret != 1)) {
                strErrorMessage = "Could not enter NADCON path." + " (code = " + ret + ")";
                return gp;
            }
            */
            //Use Set functions to systems, datums, zones, units, etc.
            //Input is State Plane
            ret = SetInSystem(2);
            if ((ret != 1)) {
                strErrorMessage = "Could not set input State Plane." + " (code = " + ret + ")";
                return gp;
            }

            //Input is NAD83/86
            ret = SetInDatum(1983);
            if ((ret != 1)) {
                strErrorMessage = "Could not set input datum." + " (code = " + ret + ")";
                return gp;
            }

            //input to MA State Plane 
            ret = SetInZone(2001);
            if ((ret != 1)) {
                strErrorMessage = "Could not set input to MA State Plane." + " (code = " + ret + ")";
                return gp;
            }

            //Input is in Meters
            ret = SetInUnits(3);
            if ((ret != 1)) {
                strErrorMessage = "Could not set input to Meters." + " (code = " + ret + ")";
                return gp;
            }

            //Output is a geographic
            ret = SetOutSystem(1);
            if ((ret != 1)) {
                strErrorMessage = "Could not set output to Geographic." + " (code = " + ret + ")";
                return gp;
            }

            //Output Datum is NAD83/86
            ret = SetOutDatum(1983);
            if ((ret != 1)) {
                strErrorMessage = "Could not set output datum." + " (code = " + ret + ")";
                return gp;
            }

            //Configure the DLL internally.
            ret = corpscon_initialize_convert();
            if ((ret != 1)) {
                strErrorMessage = "Could initialize conversion." + " (code = " + ret + ")";
                return gp;
            }

            //Set input values.
            ret = SetXIn(maXin);
            if ((ret != 1)) {
                strErrorMessage = "Could set input X." + " (code = " + ret + ")";
                return gp;
            }

            //Set input values.
            ret = SetYIn(maYIn);
            if ((ret != 1)) {
                strErrorMessage = "Could set input Y." + " (code = " + ret + ")";
                return gp;
            }

            //Convert values input above.
            ret = corpscon_convert();
            if ((ret != 1)) {
                strErrorMessage = "Could perform conversion." + " (code = " + ret + ")";
                return gp;
            }

            //Get output values.
            gp[0] = GetXOut();
            gp[1] = GetYOut();

            //Clean up after conversions.
            ret = corpscon_clean_up();
            if ((ret != 1)) {
                strErrorMessage = "Cleanup failed." + " (code = " + ret + ")";
                return gp;
            }

            strErrorMessage = "";
            return gp;
        }
    }
}

