using UnityEngine;
using System.Collections;

public class PlayerShipMover : MonoBehaviour
{
    Vector3 target;

    float speed = 8.0f;
    float rotationSpeed = 2.0f;

    public PlayerShipMover()
    {
        speed = Simulation.Parameters.getPlayerShipSpeed();
        rotationSpeed = speed / 5.0f;
    }


    public ParticleStorm ps;

    // Use this for initialization
    void Start()
    {
        target = gameObject.transform.position;
        if (ps == null) ps = GameObject.Find ("weather-particles").GetComponent<ParticleStorm>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.isState(GameState.State.Starmap))
        {
            Vector3 dir = (target - gameObject.transform.position).normalized * Time.deltaTime * speed;
            if (dir.sqrMagnitude < (target - gameObject.transform.position).sqrMagnitude)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
                transform.Translate(new Vector3(0, 0, Mathf.Min(speed, (target - transform.position).magnitude) * Time.deltaTime));
            }
            Root.game.player.setWarpMagnitude(ps.getWarpMagnitude(gameObject.transform.position));
            //Debug.Log("warpMagnitude: "+Mathf.Round (Root.game.player.warpMagnitude*60)/10+"    ("+Mathf.Round (Root.game.player.warpMagnitude*100)/100+")");
        }
    }
    void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 150, 24), "warpMag: "+Mathf.Round (Root.game.player.getWarpMagnitude()*10)/10);
        GUI.Label(new Rect(10, 30, 150, 24), "day: " + Root.game.player.getElapsedDays());
    }

    public void setTarget(Vector3 pos)
    {
        target = pos;
    }
}
