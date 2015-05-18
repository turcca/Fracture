using UnityEngine;
using System.Collections;

public class ParticleStorm : MonoBehaviour
{
    ParticleSystem pSystem;
    ParticleSystem.Particle[] particles;
    int numParticles;

    public Texture2D flowTexture;
    public GameObject player;

    Vector2[,] flowMap;

    // Use this for initialization
    void Start()
    {
        //int x = Mathf.FloorToInt(transform.position.x / size.x * heightmap.width);
        //int z = Mathf.FloorToInt(transform.position.z / size.z * heightmap.height);
        //Vector3 pos = transform.position;
        //pos.y = heightmap.GetPixel(x, z).grayscale * size.y;
        //transform.position = pos;

        pSystem = gameObject.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[pSystem.maxParticles];

        flowMap = new Vector2[flowTexture.width, flowTexture.height];
        for (int i=0; i<flowTexture.width;++i)
            for (int j=0; j<flowTexture.height;++j)
            {
                flowMap[i, j] = new Vector2(flowTexture.GetPixel(i, j).r - 0.5f, flowTexture.GetPixel(i,j).g * (-1.0f) + 0.5f);
            }

    }

    // Update is called once per frame
    void LateUpdate()
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
        vec.Set(flowMap[x,z].x, 0, flowMap[x,z].y);
        Vector3 flow = vec * 200.0f;

        particle.velocity = flow * 0.5f + particle.velocity * 0.5f;
        particle.color = new Color(1.0f, 1.0f, 1.0f, flow.sqrMagnitude*10.0f);
    }

    private int mapZ(float p)
    {
        return Mathf.Clamp(Mathf.FloorToInt((p+250.0f) / 500.0f * flowTexture.height), 0, flowTexture.height-1);
    }

    private int mapX(float p)
    {
        return Mathf.Clamp(Mathf.FloorToInt((p+500.0f) / 1000.0f * flowTexture.width), 0, flowTexture.width-1);
    }
}
