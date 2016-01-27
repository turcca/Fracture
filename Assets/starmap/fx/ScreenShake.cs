using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]

public class ScreenShake : MonoBehaviour {

    public static ScreenShake Instance;
    public Camera cam { get; private set; }

    private float shakeIntensity = 0f;
    public float shakeDecay = 0.05f;


    Vector3 originPosition;
    Quaternion originRotation;

    Transform player;

    void Awake() { Instance = this; }
    void Start ()
    {
        if (cam == null) cam = gameObject.GetComponent<Camera>();
        if (player == null) player = GameObject.Find("Player").gameObject.transform;
    }
	
	void Update ()
    {
        if (GameState.isState(GameState.State.Starmap))
        {
            if (shakeIntensity > 0.2f)
            {
                transform.position = originPosition + Random.insideUnitSphere * shakeIntensity;
                transform.rotation = new Quaternion(
                                originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .0003f,
                                originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .0003f,
                                originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .0003f,
                                originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .0003f);
            }
        }
        shakeIntensity -= (shakeDecay * Time.deltaTime);
    }

    public void shake(float amount = 0.3f)
    {
        if (GameState.isState(GameState.State.Starmap) && amount > 0.01f)
        {
            if (cam.transform.position.y > 50f)
            {
                originPosition = transform.position;
                originRotation = transform.rotation;

                shakeIntensity = Mathf.Clamp(shakeIntensity + amount * 10f - 0.01f, 0f, 1f);
                //Debug.Log("screen shake intensity: " + shakeIntensity);
            }
            // dip player ship
            if (amount > 0.05f && player.position.y > -10f)
            {
                player.position = new Vector3(player.position.x, player.position.y - Mathf.Max(amount, shakeIntensity) * 5f, player.position.z);
                //Debug.Log("dipping y: "+ Mathf.Max(amount, shakeIntensity) * 5f);
            }
        }
    }

}
