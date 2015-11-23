using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimateUVsheet : MonoBehaviour {

    // a sprite sheet animation, from top left corner to bottom right, in rows
    public int uvAnimationTileX = 8; //Here you can place the number of columns of your sheet. 
    public int uvAnimationTileY = 8; //Here you can place the number of rows of your sheet. 

    public int cappedCount = 0; // count of sprites if less than full grid. 0 means using full grid.

    public float framesPerSecond = 15.0f;

    private Renderer myRenderer;

    public int i;
    private float updateDeltaTime;
    private float timeToUpdate;
    private List<Vector2> list;

	// Use this for initialization
	void Start () 
    {
        updateDeltaTime = 1.0f/framesPerSecond;
        myRenderer = GetComponent<Renderer>();
        Vector2 size = new Vector2 (1.0f / (float)uvAnimationTileX, 1.0f / (float)uvAnimationTileY);
        myRenderer.material.SetTextureScale ("_MainTex", size);
        list = new List<Vector2>();

        initList(size);
        i = (int)Random.Range (0, cappedCount); // randomize starting sprite
	}
	
	void Update () 
    {
        timeToUpdate += Time.deltaTime;
        if (timeToUpdate > updateDeltaTime)
        {
            timeToUpdate -= updateDeltaTime;
            i++;
            //i = i<cappedCount ? i++ : 0; // loop index
            if (i>=cappedCount) i=0;
            myRenderer.material.SetTextureOffset ("_MainTex", list[i]);
        }
    }


    void initList(Vector2 size)
    {
        int count = 0;

        for (int y = 0; y<uvAnimationTileY; y++)
        {
            for (int x = 0; x<uvAnimationTileX; x++)
            {
                list.Add(cellOffset(x,y, size));
                count++;
                if (cappedCount > 0 && count >= cappedCount) return;
            }
        }
        cappedCount = count;
    }

    Vector2 cellOffset(int iX, int iY, Vector2 size)
    {
        return new Vector2 ((float)iX * size.x, 1.0f - size.y - (float)iY * size.y);
	}
}