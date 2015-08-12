using UnityEngine;
using System.Collections;

public class WeatherDebugGizmos : MonoBehaviour {

    public bool on = false;
    public int Nth; // draw every Nth gizmo
    public ParticleStorm ps;

    GameObject created;
    ParticleFlowData data;

    void Start()
    {
        data = new ParticleFlowData ();

        if (on)
        {
            ps = gameObject.GetComponent<ParticleStorm>();
            if (ps == null) 
            {
                Debug.LogWarning ("WARNING: no ParticleStorm, no debugging");
                on = false;
            }
            else 
            {
                Debug.Log ("Debugging weather gizmos...");
                created = CreateObject();
                drawGizmos ();
            }
        }
    }

	// Use this for initialization
	//void OnDrawGizmos () 
    void drawGizmos () 
    {
        if (on)
        {
            Debug.Log ("drawing:");
            Vector2 v = new Vector2(0,0);
            int count = 0;

            for (int i=0; i<data.flowTexture.width;++i)
                for (int j=0; j<data.flowTexture.height;++j)
            {
                //Debug.Log ("i: "+i+", j: "+j);
                if (i % Nth == 0 && j % Nth == 0)
                {
                    //ps.flowMap[i, j] = new Vector2(flowTexture.GetPixel(i, j).r - 0.5f, ps.flowTexture.GetPixel(i,j).g * (-1.0f) + 0.5f);
                    v = data.flowMap[i,j];

                    Color c = assignColor(Mathf.Sqrt (v.x*v.x+v.y*v.y));

                    if (c.a != 0.0f)
                    {
                        GameObject m = Instantiate(created);//, new Vector3(toMapX(i), 0.0f, toMapZ(j)));
                        m.gameObject.transform.position = new Vector3(toMapX(i), 6.0f, toMapZ(j));
                        //m.transform.parent = .transform;
                        m.GetComponent<Renderer>().material.color = c;
                        count++;
                    }
                }
            }
            Debug.Log ("created "+count+" gizmos");
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    Color assignColor(float magnitude)
    {
        if (magnitude < 0.083f)     return new Color (0.0f, 0.0f, 0.0f, 0.0f);
        else if (magnitude < 0.17f) return new Color (0.0f, 0.2f, 0.5f, 1.0f);
        else if (magnitude < 0.20f) return new Color (0.0f, 0.4f, 0.6f, 1.0f);
        else if (magnitude < 0.25f) return new Color (0.0f, 0.6f, 0.7f, 1.0f);
        else if (magnitude < 0.33f) return new Color (0.4f, 0.1f, 0.5f, 1.0f);
        else if (magnitude < 0.50f) return new Color (0.6f, 0.0f, 0.3f, 1.0f);
        else if (magnitude < 0.67f) return new Color (0.7f, 0.0f, 0.2f, 1.0f);
        else if (magnitude < 0.83f) return new Color (0.9f, 0.0f, 0.1f, 1.0f);
        else return new Color (1.0f, 0.0f, 0.0f, 1.0f);
    }
    private float toMapX(int p)
    {
        return p*1100.0f / data.flowTexture.width -550.0f;
    }
    private float toMapZ(int p)
    {
        return p*575.0f / data.flowTexture.height -287.5f;
    }
    
    /*    
    private int mapX(float p)
    {
        return Mathf.Clamp(Mathf.FloorToInt((p+500.0f) / 1000.0f * flowTexture.width), 0, flowTexture.width-1);
    }
    private int mapZ(float p)
    {
        return Mathf.Clamp(Mathf.FloorToInt((p+250.0f) / 500.0f * flowTexture.height), 0, flowTexture.height-1);
    }
    */



    // Mesh debug tools
    //-----------------------------------------------------------------
    // Create a quad mesh
    public Mesh CreateMesh() {
        
        Mesh mesh = new Mesh();
        
        Vector3[] vertices = new Vector3[]
        {
            new Vector3( 1, 1,  0),
            new Vector3( 1, -1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(-1, -1, 0),
        };
        
        Vector2[] uv = new Vector2[]
        {
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 0),
        };
        
        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,
        };
        
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }
    
    // Create a mesh and bind the 'msn' texture to it
    GameObject CreateObject() {  

        // Create object
        Mesh _m1 = CreateMesh();
        var item = (GameObject) new GameObject(
            "DebugPlane", 
            typeof(MeshRenderer), // Required to render
            typeof(MeshFilter)    // Required to have a mesh
            );
        item.GetComponent<MeshFilter>().mesh = _m1;
        
        // Set texture
        var tex = (Texture) Resources.Load ("msn");
        item.GetComponent<Renderer>().material.mainTexture = tex;
        
        // Set shader for this sprite; unlit supporting transparency
        // If we dont do this the sprite seems 'dark' when drawn. 
        var shader = Shader.Find ("Unlit/Color");
        item.GetComponent<Renderer>().material.shader = shader;
        
        // Set position & rot
        item.transform.position = new Vector3(0, 0, 0);
        item.transform.localEulerAngles = new Vector3(90,0,0);
        return item;
    }
    //-----------------------------------------------------------------
}
