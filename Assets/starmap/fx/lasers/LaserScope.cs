using UnityEngine;
using System.Collections;


public class LaserScope : MonoBehaviour 
{   
    float scrollSpeed = 0.8f;
    float pulseSpeed = 2.0f;
    
    float noiseSize = 1.0f;
    
    float maxWidth = 0.3f;
    float minWidth  = 0.2f;
    
    public GameObject targetObject = null;
    
    LineRenderer lRenderer;
    float aniTime = 0.0f;
    float aniDir = 1.0f;


    Renderer renderer;
    PerFrameRaycast raycast;
    
    void Start() 
    {
        lRenderer = (LineRenderer) gameObject.GetComponent<LineRenderer> ();
        renderer = GetComponent<Renderer>();
        aniTime = 0.0f;
        
        // Change some animation values here and there
        ChoseNewAnimationTargetCoroutine();
        
        raycast = GetComponent<PerFrameRaycast> ();
    }
    void Update () 
    {
        if (targetObject)
        {
            renderer.material.mainTextureOffset += new Vector2(Time.deltaTime * aniDir * scrollSpeed, 0);
            renderer.material.SetTextureOffset ("_NoiseTex", new Vector2 (-Time.time * aniDir * scrollSpeed, 0.0f));
            
            float aniFactor = Mathf.PingPong (Time.time * pulseSpeed, 1.0f);
            aniFactor = Mathf.Max (minWidth, aniFactor) * maxWidth;
            lRenderer.SetWidth (aniFactor, aniFactor);
            
            lRenderer.SetPosition(0, this.gameObject.transform.position);
            lRenderer.SetPosition(1, targetObject.transform.position);
        }
    }


    public void setTargetObject (GameObject targetObjectToFollow)
    {
        targetObject = targetObjectToFollow;
        lRenderer.enabled = true;
    }
    public void clearTargetObject()
    {
        targetObject = null;
        lRenderer.enabled = false;
    }
    
    IEnumerable ChoseNewAnimationTargetCoroutine () 
    {
        while (true) 
        {
            aniDir = aniDir * 0.9f + Random.Range (0.5f, 1.5f) * 0.1f;
            yield return null;
            minWidth = minWidth * 0.8f + Random.Range (0.1f, 1.0f) * 0.2f;
            yield return new WaitForSeconds (1.0f + Random.value * 2.0f - 1.0f);  
        }   
    }
    


}
