using UnityEngine;
using System.Collections;

public class ParticleStorm : MonoBehaviour
{
    ParticleSystem pSystem;
    ParticleSystem.Particle[] particles;
    int numParticles;


    public GameObject player;


    // Use this for initialization
    void Awake()
    {
        //int x = Mathf.FloorToInt(transform.position.x / size.x * heightmap.width);
        //int z = Mathf.FloorToInt(transform.position.z / size.z * heightmap.height);
        //Vector3 pos = transform.position;
        //pos.y = heightmap.GetPixel(x, z).grayscale * size.y;
        //transform.position = pos;

        pSystem = gameObject.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[pSystem.maxParticles];

        ParticleFlowData.buildFlowMap();
    }

    // Update is called once per frame
    void LateUpdate()
    //void Update()
    {
        transform.position = player.transform.position;
        numParticles = pSystem.GetParticles(particles);
        for (int i = 0; i < numParticles; ++i)
        {
            updateParticleVelocity(ref particles[i]);
        }
        pSystem.SetParticles(particles, numParticles);
    }

    int x;
    int z;
    Vector3 vec = new Vector3();
    private void updateParticleVelocity(ref ParticleSystem.Particle particle)
    {
        x = mapX(particle.position.x);
        z = mapZ(particle.position.z);
        vec.Set(ParticleFlowData.getFlowMap()[x,z].x, 0, ParticleFlowData.getFlowMap()[x,z].y);
        Vector3 flow = vec * 30.0f;

        //particle.velocity = flow;  // quick and dirty, jitters turning
        particle.velocity = Vector3.Slerp(particle.velocity, flow, Time.deltaTime*5f);
        //if (particle.lifetime >= particle.startLifetime*0.99f) particle.velocity = flow; // set once
        
        //particle.color = new Color(1.0f, 1.0f, 1.0f, flow.sqrMagnitude*10.0f);
    }

    private int mapZ(float p)
    {
        //return Mathf.Clamp(Mathf.FloorToInt((p+250.0f) / 500.0f * flowTexture.height), 0, flowTexture.height-1);
        return Mathf.Clamp(Mathf.FloorToInt((p+287.5f) / 575.0f * ParticleFlowData.flowTexture.height), 0, ParticleFlowData.flowTexture.height-1);
    }

    private int mapX(float p)
    {
        //return Mathf.Clamp(Mathf.FloorToInt((p+500.0f) / 1000.0f * flowTexture.width), 0, flowTexture.width-1);
        return Mathf.Clamp(Mathf.FloorToInt((p+550.0f) / 1100.0f * ParticleFlowData.flowTexture.width), 0, ParticleFlowData.flowTexture.width-1);
    }

    public float getWarpMagnitude (Vector3 pos)
    {
        int xi = mapX(pos.x);
        int zi = mapZ(pos.z);
        float xf = ParticleFlowData.getFlowMap()[xi,zi].x;
        float yf = ParticleFlowData.getFlowMap()[xi,zi].y;
        return Mathf.Sqrt (xf*xf+yf*yf);
    }
}
