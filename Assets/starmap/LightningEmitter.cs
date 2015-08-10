using UnityEngine;
using System.Collections;

public class LightningEmitter : MonoBehaviour {

    public float brightness = 1.0f;
    public float frequency = 1.0f;

    ParticleSystem ps;
    ParticleStorm flow;

    ParticleSystem.Particle particle;

    float accumulator;

	void Start () 
    {
        accumulator = 0;
        ps = gameObject.GetComponent<ParticleSystem>();
        if (flow == null) flow = GameObject.Find ("weather-particles").GetComponent<ParticleStorm>();
        particle = new ParticleSystem.Particle();
            particle.size = ps.startSize;
            particle.startLifetime = ps.startLifetime;
            particle.lifetime = ps.startLifetime;
            particle.velocity = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    //void Update () 
    {
        if (Random.value < Time.fixedDeltaTime*frequency)
        {
            emitCustomParticle();
        }
	}


    void emitCustomParticle()
    {
        Vector3 pos = new Vector3(
            gameObject.transform.position.x + Random.Range(-70.0f, 70.0f), 
            gameObject.transform.position.y + Random.Range(-2.0f, 2.0f), 
            gameObject.transform.position.z + Random.Range(-35.0f, 35.0f));
        float warpMag = flow.getWarpMagnitude(pos);
        
        if (Random.value * warpMag < 0.1f + accumulator)
        {
            accumulator = (warpMag < 0.2f) ? accumulator/2.0f : warpMag-0.2f; 
            particle.size = ps.startSize * (Random.value + 1) /2;
            particle.lifetime = ps.startLifetime * (Random.value + 1) /2;
            particle.position = pos;
            particle.rotation = Random.value *360;
            particle.color = (Color32) new Color(1.0f, 1.0f, 1.0f, 
                                                 // aplha calculation
                                                 warpMag * 
                                                 brightness * 
                                                 (Random.value+1.0f)/2.0f); // random: 0.5 - 1.0;

            ps.Emit(particle);
            
            //Debug.Log ("EMIT "+gameObject.name+" ("+ps.particleCount+") pos: "+pos.ToString()+" @warp "+Mathf.Round (warpMag*10)/10+" [acc: "+Mathf.Round (accumulator*100)/100+"], rot: "+Mathf.Round (particle.rotation)+", vel: 0, size: "+Mathf.Round (particle.size*10)/10+", life: "+Mathf.Round (particle.lifetime*100)/100+"/"+particle.startLifetime+", alpha: "+particle.color.a);
        }
    }
}
