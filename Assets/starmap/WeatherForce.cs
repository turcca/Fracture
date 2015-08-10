using UnityEngine;
using System;
using System.IO;
using System.Collections;


public class WeatherForce : MonoBehaviour {

    public GameObject player;

    public ParticleSystem ps;
    public int particlesSpawned = 20000;
    public float particleMultiplier = 1.0f;

    public float boxSizeX = 70.0f;
    public float boxSizeZ = 70.0f;


    Vector3[,] weather;
    float particlesInSecond;
    int effectiveRate;

    int xResolution;
    int zResolution;

    float spawnR;

    float xSize;
    float zSize;
    float xUnit;
    float zUnit;
    float xUnitHalf;
    float zUnitHalf;

    float ltLow;

    ParticleSystem.Particle[] particles;

	// Use this for initialization
	void Start () 
    {
        initialize();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    // spawn
        //spawnParticles();
	}

    void LateUpdate ()
    {
        weatherInfluence();
    }

    // ------------------------------------


    void initialize()
    {
        ps = this.gameObject.GetComponent<ParticleSystem>();

        // GRAPHICS QUALITY
        particlesSpawned = (int)((float)particlesSpawned * particleMultiplier);  // graphics settings

        // particles spawned per second
        if (ps.startLifetime == 0) Debug.LogWarning ("WARNING: particle startLifetime = 0");
        else particlesInSecond = (float)particlesSpawned / ps.startLifetime;

        weather = loadVector3ArrayFromFile( Application.dataPath+ "/Resources/starmap/weather.map");
        particles = new ParticleSystem.Particle[particlesSpawned];
        //weather = nodes.gameObject.GetComponent(NodeMesh);

        spawnR  = boxSizeX/2.0f;

        xResolution = 550;
        zResolution = 360;

        xSize = boxSizeX/2.0f; //pSystem.boxX/2;
        zSize = boxSizeZ/2.0f; //pSystem.boxZ/2;

        xUnit = xSize / (float)xResolution;
        zUnit = zSize / (float)zResolution;
        xUnitHalf = xUnit /2.0f;
        zUnitHalf = zUnit /2.0f;

        // lower bound multiplier for startLifeTime
        ltLow = ps.startLifetime *0.66f;
    }


    void spawnParticles() 
    {
        int x;
        int z;

        effectiveRate = (int) Mathf.Min(Mathf.Max(particlesInSecond / 2.0f * Time.deltaTime, 1.0f), (float)particlesSpawned * 2.0f - (float)ps.particleCount); 
        Vector3 playerPos = player.transform.position; //this.gameObject.transform.position;
        playerPos.x /= 100.0f;
        playerPos.z /= 100.0f;
        Debug.Log ("effectiveRate: "+effectiveRate+" (particlesSpawned: "+particlesSpawned+" / particlesInSecond: "+particlesInSecond+" / COUNT: "+ps.particleCount);
        for (int i = 0; i < effectiveRate; i++) 
        {
            // spawn pos 
            Vector3 pos = new Vector3(playerPos.x + randomRange(), 
                                      playerPos.y + UnityEngine.Random.value, // y
                                      playerPos.z + randomRange() );     // pos = Vector3( this.transform.position.x + Random.Range(-spawnR, spawnR), this.transform.position.y + Random.Range(0, 1), this.transform.position.z + Random.Range(-spawnR, spawnR) );

            // translate position coordinates to weather grid
            x = (int)((pos.x - xUnitHalf) / xUnit);
            z = (int)((pos.z - zUnitHalf) / zUnit);

            if (x >= 0 && z >= 0 && x < xResolution && z < zResolution) 
            {
                // Emit variable -sized particle
                ps.Emit(
                    // pos
                    pos, 
                    // velocity
                    weather[x,z], 
                    // size
                    ps.startSize, 
                    //energy (lifetime/speed: faster particles have faster animation and shorter life) 
                    UnityEngine.Random.Range(ltLow, ps.startLifetime) / (weather[x,z].magnitude*0.6f), 
                    // colour
                    ps.startColor
                    );//, Random.value*360, Random.value*200);
            }
        }

    }
    float randomRange()
    {
        return UnityEngine.Random.value * 2.0f * spawnR - spawnR ;
    }

    void weatherInfluence()
    {
        if (!ps.IsAlive()) ps.SetParticles(null, 0);
        
        else if (true)//!GameTime.timePaused)
        {       
            // grab particle pool
            int count = ps.GetParticles(particles);
            //if (debugging && count > particleMax) Debug.LogWarning("ParticleSystem ("+this.gameObject.name+") exceeds particleMax ["+count+"/"+particleMax+"]");

            int x;
            int z;

            // go through all particles [within cap limits]
            for (int i = 0; i < count; i++) {
                
                // translate position coordinates to weather grid
                x = (int)((particles[i].position.x - xUnitHalf) / xUnit);
                z = (int)((particles[i].position.z - zUnitHalf) / zUnit);
                
                //if (toleranceTriggers) oldV = particles[i].velocity.sqrMagnitude; // if you reall want OLD VELOCITY, do it here (costs one boolean check per particle)
                
                // assign particle velocity from grid and weatherForce multiplier
                particles[i].velocity = Vector3.Lerp(particles[i].velocity, weather[x,z] , Time.deltaTime) ; //* weatherMultiplier -- COSTLY?
            }
            
            // copy them back to the particle emitter
            ps.SetParticles(particles, count);
        }    
    }


    // IO
    /*
    static function saveVector3ArrayToFile(fileName : String, array : Vector3[,])
    {
        if (File.Exists(fileName))
        {
            Debug.Log("Overwriting: "+fileName+".");
            //Debug.Log(fileName+" already exists.");
            //return;
        }
        var sr = File.CreateText(fileName);
        
        var xLength : int = array.GetLength(0);
        var yLength : int = array.GetLength(1);
        
        sr.WriteLine (array.GetLength(0));
        sr.WriteLine (array.GetLength(1));
        for (var y : int = 0; y < yLength; y++) // rows
        {
            for (var x : int = 0; x < xLength; x++) // columns
            {
                sr.WriteLine (array[x,y].x);                
                sr.WriteLine (array[x,y].y);                
                sr.WriteLine (array[x,y].z);                
            }
        }
        sr.Close();
    }
    */
    Vector3[,] loadVector3ArrayFromFile(string file)
    {
        if(File.Exists(file))
        {
            StreamReader sr = File.OpenText(file);
            string line = sr.ReadLine();
            int xLength = System.Int32.Parse(line);
            line = sr.ReadLine();
            int yLength = System.Int32.Parse(line);

            Vector3[,] array = new Vector3[xLength,yLength];   

            for (int y = 0; y < yLength; y++) // rows
            {
                for (int x = 0; x < xLength; x++) // columns
                {
                    array[x,y] = new Vector3(float.Parse(sr.ReadLine()),float.Parse(sr.ReadLine()),float.Parse(sr.ReadLine()));
                }
            }
            sr.Close();
            return array;
        }
        else
        {
            Debug.Log("Could not Open the file: " + file + " for reading.");
        }
        return null;
    }

}
