using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

public class PlaceXY {

	public GameObject placementBase = null;
	public GameObject parentObj = null;
	public string XCoord = "";
	public string YCoord = "";
	public double isHappy = 0;

	public PlaceXY() {

	}

	public static Color HSVToRGB(float H, float S, float V)
	{
		if (S == 0f)
			return new Color (V, V, V);
		else if (V == 0f)
			return Color.black;
		else
		{
			Color col = Color.black;
			float Hval = H * 6f;
			int sel = Mathf.FloorToInt(Hval);
			float mod = Hval - sel;
			float v1 = V * (1f - S);
			float v2 = V * (1f - S * mod);
			float v3 = V * (1f - S * (1f - mod));
			switch (sel + 1)
			{
			case 0:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 1:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			case 2:
				col.r = v2;
				col.g = V;
				col.b = v1;
				break;
			case 3:
				col.r = v1;
				col.g = V;
				col.b = v3;
				break;
			case 4:
				col.r = v1;
				col.g = v2;
				col.b = V;
				break;
			case 5:
				col.r = v3;
				col.g = v1;
				col.b = V;
				break;
			case 6:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 7:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			}
			col.r = Mathf.Clamp(col.r, 0f, 1f);
			col.g = Mathf.Clamp(col.g, 0f, 1f);
			col.b = Mathf.Clamp(col.b, 0f, 1f);
			return col;
		}
	}

	public GameObject PlaceObjAtXY() {
		if (placementBase == null || string.IsNullOrEmpty(XCoord) || string.IsNullOrEmpty(YCoord)) {
			return null;
		}
		
		//read in the x
		float dXCoord = 0, dYCoord = 0;
		
		if (!float.TryParse (XCoord, out dXCoord) || !float.TryParse (YCoord, out dYCoord)) {
			return null;
		}
		
		//fix the axis
		dXCoord = -1 * dXCoord;
		dYCoord = -1 * dYCoord;
		
		GameObject newObj = (GameObject)GameObject.Instantiate (placementBase, new Vector3(dXCoord, 700, dYCoord), placementBase.transform.rotation);

		ParticleSystem[] ps = newObj.GetComponents<ParticleSystem> ();

		//make this color based on is happy
		//flip the happy value so that 10 is green 0 is red
		float isHappyInvert = (float)(10 - isHappy);


		float hue = Mathf.Floor((float)((100 - (isHappyInvert * 10)) * 120 / 100));
		float sat = 1;
		float bri = 1;

		HSBColor hsb = new HSBColor (hue / 360, sat, bri);

		Color cloudColor = hsb.ToColor ();

		ps [0].Stop ();
		ps [0].Clear ();
		ps [0].startColor = hsb.ToColor ();
		ps [0].Play ();

		if (parentObj != null) {
			newObj.transform.parent = parentObj.transform;
		}

		newObj.SetActive (true);

		return newObj;
	}

}
