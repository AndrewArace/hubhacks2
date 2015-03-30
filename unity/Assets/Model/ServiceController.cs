using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;
using System;
using Newtonsoft.Json;
using AssemblyCSharp;

namespace AssemblyCSharp
{
	public class ServiceController {

		public enum RequestType
		{
			ALL,
			LIVE
		}

		public NeighborhoodTimer.ServiceControllerCompleted CompletedTask { get; set; }

		private string webRoot = "http://demo.geonetics.com/hubhacks2/api/v1/cloud";
		private string webRootLive = "http://demo.geonetics.com/hubhacks2/api/v1/realtimecloud?lastpull={0}";

		public List<DataReportObj> Results = new List<DataReportObj> ();
		public bool ResultsReceived = false;

		public List<DataReportObj> LiveResults = new List<DataReportObj>();
		public bool LiveResultsReceived = false;

		public string LastTimeCheck = "";
		public DateTime DTLastTimeCheck = DateTime.Now;

		private static ServiceController _instance = null;
		public static ServiceController Instance
		{
			get {
				if(_instance == null) {
					_instance = new ServiceController();
				}
				return _instance;
			}
			set {

			}
		}

		public ServiceController() {

			DTLastTimeCheck = DateTime.Now.AddMinutes (-1);
			LastTimeCheck = DTLastTimeCheck.ToString ("yyyyMMddHHmmss");

		}

		private string queryService(string url) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create (url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			
			string returnObj = new StreamReader(response.GetResponseStream()).ReadToEnd();

			return returnObj;
		}

		private List<DataReportObj> getDataAndConvert(string url) {
			//transform

			string response = queryService (url);
			
			RootObject ro = JsonConvert.DeserializeObject<RootObject> (response);
			List<DataReportObj> results = new List<DataReportObj>(ro.Data);
			
			return results;
		}

		public void GetAllData() {
			//build the police data url

			string url = string.Format ("{0}", webRoot);

			ServiceController.Instance.Results = getDataAndConvert(url); 
			ServiceController.Instance.ResultsReceived = true;

			CompletedTask.Invoke (ServiceController.Instance.Results, RequestType.ALL);
		}

		public void GetLiveData() {

			//TODO: replace this timestamp with the last time checkfa
			string url = string.Format (webRootLive, LastTimeCheck);
			DTLastTimeCheck = DTLastTimeCheck.AddSeconds (10);
			LastTimeCheck = DTLastTimeCheck.ToString ("yyyyMMddHHmmss");

			ServiceController.Instance.LiveResults = getDataAndConvert(url); 
			ServiceController.Instance.LiveResultsReceived = true;


			CompletedTask.Invoke (ServiceController.Instance.LiveResults, RequestType.LIVE);
		}
	}
}