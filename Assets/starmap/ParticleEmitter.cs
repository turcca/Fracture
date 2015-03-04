using UnityEngine;
using System.Collections;

public class ParticleEmitter : MonoBehaviour
{
    public Transform playerPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(playerPos.position.x + Random.Range(-20.0f, 20.0f), -5.0f, playerPos.position.z + Random.Range(-20.0f, 20.0f));
        Vector3 vel = new Vector3(0, 0, 0);
        float size = 5.0f;
        float life = 10.0f;
        Color32 color = new Color32(255, 255, 255, 255);
        gameObject.GetComponent<ParticleSystem>().Emit(pos, vel, size, life, color);
    }
}
