using UnityEngine;
using System.Collections;

public class FlowMaterial: MonoBehaviour  {
	
	public float Cycle = .15f;
	public float FlowSpeed = .05f;
	private float HalfCycle = 0f;
	private float FlowMapOffset0, FlowMapOffset1;
	
	// Use this for initialization
	void Start  () {
		HalfCycle = Cycle * .5f;
		FlowMapOffset0 = 0.0f;
		FlowMapOffset1 = HalfCycle;
		GetComponent<Renderer>().sharedMaterial .SetFloat ("_HalfCycle", HalfCycle);
		GetComponent<Renderer>().sharedMaterial .SetFloat ("_FlowOffset0", FlowMapOffset0);
		GetComponent<Renderer>().sharedMaterial .SetFloat ("_FlowOffset1", FlowMapOffset1);
	}
	
	// Update is called once per frame
	void Update  () {
		FlowMapOffset0 += FlowSpeed * Time .deltaTime ;
		FlowMapOffset1 += FlowSpeed * Time .deltaTime ;
		if (FlowMapOffset0 >= Cycle)
			FlowMapOffset0 = 0.0f;
		
		if (FlowMapOffset1 >= Cycle)
			FlowMapOffset1 = 0.0f;
		
		GetComponent<Renderer>().sharedMaterial .SetFloat ("_FlowOffset0", FlowMapOffset0);
		GetComponent<Renderer>().sharedMaterial .SetFloat ("_FlowOffset1", FlowMapOffset1);
	}
}