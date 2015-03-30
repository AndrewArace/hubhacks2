using UnityEngine;
using System.Collections;

public class GenerateClouds : MonoBehaviour {

	public GameObject Clouds = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick () {
		if (Clouds == null) {
			return;
		}

		GameObject newStorm = (GameObject)GameObject.Instantiate (Clouds, Clouds.transform.position, Clouds.transform.rotation);
		newStorm.SetActive (true);
		ParticleSystem[] ps = newStorm.GetComponents<ParticleSystem> ();
		Gradient g = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[2];
		GradientAlphaKey[] gak = new GradientAlphaKey[2];
		gck [0].color = Color.red;
		gck [0].time = 0;
		gck [1].color = Color.black;
		gck [1].time = 1;
		gak [0].alpha = 1;
		gak [0].time = 0;
		gak [1].alpha = 0;
		gak [1].time = 1;
		g.SetKeys (gck, gak);

		//ps[0].
	}
}
