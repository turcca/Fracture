using UnityEngine;
using System.Collections;

public class ParticleStorm : MonoBehaviour
{
    ParticleSystem pSystem;
    ParticleSystem.Particle[] particles;
    int numParticles;


    public GameObject player;
    ParticleFlowData data;


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

        data = new ParticleFlowData();
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
        vec.Set(data.flowMap[x,z].x, 0, data.flowMap[x,z].y);
        Vector3 flow = vec * 30.0f;

        particle.velocity = flow; // * 0.5f + particle.velocity * 0.5f;
        //particle.color = new Color(1.0f, 1.0f, 1.0f, flow.sqrMagnitude*10.0f);
    }

    private int mapZ(float p)
    {
        //return Mathf.Clamp(Mathf.FloorToInt((p+250.0f) / 500.0f * flowTexture.height), 0, flowTexture.height-1);
        return Mathf.Clamp(Mathf.FloorToInt((p+287.5f) / 575.0f * data.flowTexture.height), 0, data.flowTexture.height-1);
    }

    private int mapX(float p)
    {
        //return Mathf.Clamp(Mathf.FloorToInt((p+500.0f) / 1000.0f * flowTexture.width), 0, flowTexture.width-1);
        return Mathf.Clamp(Mathf.FloorToInt((p+550.0f) / 1100.0f * data.flowTexture.width), 0, data.flowTexture.width-1);
    }

    public float getWarpMagnitude (Vector3 pos)
    {
        int xi = mapX(pos.x);
        int zi = mapZ(pos.z);
        float xf = data.flowMap[xi,zi].x;
        float yf = data.flowMap[xi,zi].y;
        return Mathf.Sqrt (xf*xf+yf*yf);
    }
}
