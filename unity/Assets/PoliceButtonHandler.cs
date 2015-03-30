using UnityEngine;
using System.Collections;
using System;

public class PoliceButtonHandler : MonoBehaviour {

	GameObject mainStorm = null;
	GameObject boston = null;

	GameObject newStorm = null;

	// Use this for initialization
	void Start () {
		mainStorm = GameObject.FindGameObjectWithTag ("cloud");

		boston = GameObject.FindGameObjectWithTag ("boston");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {

		if (mainStorm == null || boston == null) {
			return;
		}

		if (newStorm != null) {
			Destroy(newStorm);
			newStorm = null;
			return;
		}

		PlaceXY pxy = new PlaceXY ();
		pxy.placementBase = mainStorm;
		pxy.parentObj = boston;
		pxy.XCoord = "-35010.55";
		pxy.YCoord = "-3798.597";
		newStorm = pxy.PlaceObjAtXY ();
	}
}
