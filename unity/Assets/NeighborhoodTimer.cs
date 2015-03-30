using UnityEngine;
using System.Collections.Generic;
using System;
using AssemblyCSharp;
using System.Threading;
using System.Globalization;

public class NeighborhoodTimer : MonoBehaviour {

	public delegate void ServiceControllerCompleted(List<DataReportObj> results, ServiceController.RequestType type);

	private float timePassedRenderCloud = 0;
	private float timePassedRemoveCloud = 0;
	private float timePassedLiveQuery = 0;
	private float timePassedLiveQueryRemoveCloud = 0;
	private int secondsPerCloud = 30;
	private int secondsToAddCloud = 3;
	private int secondsPerLiveQuery = 10;
	private int secondsPerLivequeryRemoveCloud = 60;
	private int cloudsPerUpdate = 3;

	public GameObject mainStorm = null;
	GameObject boston = null;

	GameObject chkTransportation = null;
	UIToggle chkTransportationToggle = null;

	GameObject chkSafety = null;
	UIToggle chkSafetyToggle = null;

	GameObject chkBostonShine = null;
	UIToggle chkBostonShineToggle = null;

	GameObject tglLiveReports = null;
	UIToggle tglLiveReportsToggle = null;

	UILabel lblTime = null;

	public static List<DataReportObj> reports = new List<DataReportObj>();

	public static List<DataReportObj> livereports = new List<DataReportObj>();
	public static Dictionary<int, bool> livereportshash = new Dictionary<int, bool> ();
	public bool LiveModeOn = false;

	private List<GameObject> renderedClouds = new List<GameObject> ();

	public void ServiceControllerCompletedCallback(List<DataReportObj> results, ServiceController.RequestType type) {
		if (type == ServiceController.RequestType.ALL) {
			reports.AddRange(results);
		} else if (type == ServiceController.RequestType.LIVE) {
			livereports.AddRange(results);

		}
	}

	// Use this for initialization
	void Start () {
		//mainStorm = GameObject.FindGameObjectWithTag ("cloud");
		boston = GameObject.FindGameObjectWithTag ("boston");

		//get the ui checkboxes
		chkTransportation = GameObject.Find ("UI Root/BottomPanel/ScrollViewBorder/Transportation - Simple Checkbox");
		chkTransportationToggle = getToggle (chkTransportation);

		chkSafety = GameObject.Find ("UI Root/BottomPanel/ScrollViewBorder/Safety - Simple Checkbox");
		chkSafetyToggle = getToggle (chkSafety);

		chkBostonShine = GameObject.Find ("UI Root/BottomPanel/ScrollViewBorder/Boston Shine - Simple Checkbox");
		chkBostonShineToggle = getToggle (chkBostonShine);

		tglLiveReports = GameObject.Find ("UI Root/BottomPanel/LiveMode");
		tglLiveReportsToggle = getToggle (tglLiveReports);

		GameObject temp = GameObject.Find ("UI Root/BottomPanel/lblTime");
		lblTime = temp.GetComponents<UILabel> ()[0];

		//start a thread to populate the reports list
		Thread t = new Thread (new ThreadStart (ServiceController.Instance.GetAllData));
		ServiceController.Instance.CompletedTask += ServiceControllerCompletedCallback;
		t.Start ();
	}

	private UIToggle getToggle(GameObject go) {
		UIToggle[] parts = go.GetComponents<UIToggle> ();
		if (parts.Length > 0) {
			return parts[0];
		}
		return null;
	}

	private bool CheckTransToggle(DataReportObj dro) {
		if (chkTransportationToggle != null && chkTransportationToggle.value && dro.Category == "Transportation") {
			return true;
		}
		return false;
	}

	private bool CheckSafetyToggle(DataReportObj dro) {
		if (chkSafetyToggle != null && chkSafetyToggle.value && dro.Category == "Safety") {
			return true;
		}
		return false;
	}

	private bool CheckBostonShineToggle(DataReportObj dro) {
		if (chkBostonShineToggle != null && chkBostonShineToggle.value && dro.Category == "Clean") {
			return true;
		}
		return false;
	}

	public void LiveModeClick() {
		//is the live mode on?
		if (tglLiveReportsToggle.value) {
			/*chkSafetyToggle.value = false;
			chkTransportationToggle.value = false;
			chkBostonShineToggle.value = false;*/

			removeAllClouds();

			executeLiveQuery();

			LiveModeOn = true;
		} else {
			/*chkSafetyToggle.value = true;
			chkBostonShineToggle.value = true;
			chkTransportationToggle.value = true;*/

			LiveModeOn = false;

			removeAllClouds();
		}

		timePassedRenderCloud = 0;
		timePassedRemoveCloud = 0;
		timePassedLiveQuery = 0;
		timePassedLiveQueryRemoveCloud = 0;
	}

	private void executeLiveQuery() {
		Thread liveModeThread = new Thread(new ThreadStart(ServiceController.Instance.GetLiveData));
		ServiceController.Instance.CompletedTask = ServiceControllerCompletedCallback;
		liveModeThread.Start();
	}

	private void renderObj(DataReportObj obj) {
		PlaceXY pxy = new PlaceXY();
		pxy.placementBase = mainStorm;
		pxy.parentObj = boston;
		pxy.XCoord = obj.XCoord.ToString();
		pxy.YCoord = obj.YCoord.ToString();
		pxy.isHappy = obj.Happy;
		GameObject cloud = pxy.PlaceObjAtXY();


		try {
			DateTime dt = DateTime.ParseExact (obj.TimeStamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
			lblTime.text = string.Format ("{0}, {1}", dt.ToShortDateString(), dt.ToShortTimeString ());
		}
		catch(FormatException ex) {
			System.Diagnostics.Debug.WriteLine(string.Format("Invalid datetime: {0} for: {1}", obj.TimeStamp, obj.Id));
		}
		
		renderedClouds.Add(cloud);
	}

	private void removeCloud() {
		if(renderedClouds.Count > 0) {
			GameObject curCloud = renderedClouds[0];
			renderedClouds.RemoveAt(0);
			Destroy(curCloud);
			curCloud = null;
		}
	}

	private void removeAllClouds() {
		while (renderedClouds.Count > 0) {
			removeCloud();
		}
	}
	
	// Update is called once per frame
	void Update () {
		timePassedRenderCloud += Time.deltaTime;
		timePassedRemoveCloud += Time.deltaTime;
		timePassedLiveQuery += Time.deltaTime;
		timePassedLiveQueryRemoveCloud += Time.deltaTime;

		if (LiveModeOn) {
			if(livereports == null) {
				return;
			}

			if(timePassedLiveQuery > secondsPerLiveQuery) {
				executeLiveQuery();
				timePassedLiveQuery = 0;
			}

			while(livereports.Count > 0) {
				DataReportObj curdro = livereports[0];
				livereports.RemoveAt(0);

				if(!livereportshash.ContainsKey(curdro.Id)) {
					livereportshash.Add(curdro.Id, true);
				
					if(CheckSafetyToggle(curdro) || CheckTransToggle(curdro) || CheckBostonShineToggle(curdro) || curdro.Category == "General") {
						renderObj(curdro);
					}
				}
			}

			if(timePassedLiveQueryRemoveCloud > secondsPerLivequeryRemoveCloud) {
				removeCloud();
				timePassedLiveQueryRemoveCloud = 0;
			}

		} else {
			if (reports == null) {
				return;
			}

			if (timePassedRenderCloud > secondsToAddCloud) {
				//10 seconds have passed, lets render a cloud
				if(reports.Count > 0) {

					for(int i = 0; i < cloudsPerUpdate; i++) {
						DataReportObj dro = NeighborhoodTimer.reports[0];
						NeighborhoodTimer.reports.RemoveAt(0);

						//make sure we want to draw this point
						if(CheckSafetyToggle(dro) || CheckTransToggle(dro) || CheckBostonShineToggle(dro) || dro.Category == "General") {
							renderObj(dro);
						}
					}
				}

				timePassedRenderCloud = 0;
			}

			if (timePassedRemoveCloud > secondsPerCloud) {
				//secondsPerCloud seconds passed we should probably remove a cloud
				
				removeCloud();

				timePassedRemoveCloud = 0;
			}
		}
	}
}
