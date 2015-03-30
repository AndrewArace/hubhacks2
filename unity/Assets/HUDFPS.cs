using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HUDFPS : MonoBehaviour {

	/* Public Variables */
	public float frequency = 0.5f;
	
	/* **********************************************************************
	 * PROPERTIES
	 * *********************************************************************/
	public int FramesPerSec { get; protected set; }
	
	/* **********************************************************************
	 * EVENT HANDLERS
	 * *********************************************************************/
	/*
	 * EVENT: Start
	 */
	private void Start() {
		StartCoroutine(FPS());
	}
	
	/*
	 * EVENT: FPS
	 */
	private IEnumerator FPS() {
		for(;;){
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(frequency);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;
			
			// Display it
			FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
			gameObject.GetComponent<Text>().text = FramesPerSec.ToString() + " fps";
		}
	}
}
