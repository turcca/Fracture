using UnityEngine;
using System.Collections;

public class StopRotation : MonoBehaviour {
	Quaternion rot;

	// Use this for initialization
	void Start () {
		rot = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		gameObject.transform.rotation = rot;
	}
}
