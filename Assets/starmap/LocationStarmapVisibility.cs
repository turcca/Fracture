using UnityEngine;
using System.Collections;

public class LocationStarmapVisibility : MonoBehaviour 
{
    public ParticleSystem ps = null;

    public float opacity;

    public Location location;

	void Start () 
    {
        ps = GetComponentInChildren<ParticleSystem>();

        if (ps != null)
        {
            initLocation();
            calculateSize();
        }
	}
    // -----------------------
	void Update () 
    {
	    if (ps != null)
        {
            // distance dimming
            float distance = Vector3.Distance(this.gameObject.transform.position, Root.game.player.position);

            if (distance > 50.0f)
            {
                setOpacity(0.0f);
            }
            else
            {
                setOpacity (opacity);
            }
        }
	}
    // -----------------------

    void initLocation()
    {
        location = Root.game.locations[this.gameObject.name];
    }

    void calculateSize()
    {
        opacity = getOpacity();

        // size (lifetime of particles = spread)
        ps.startLifetime = 

            // population
            // x^1.7/x^1.15 /10 +4
            // 0 = 4, 1 = 4.1, 10 = 4.4, 100 = 5.3, 300 = 6.3, 1000 = 8.5
            (Mathf.Pow(location.features.population, 1.7f) / Mathf.Pow(location.features.population, 1.15f) /10.0f +4.0f) *
            // psy rating
            (location.ideology.effects.psych/2.0f +1.0f)
            // navigator?
            ;
        // alpha
        setOpacity(opacity);
    }

    private void setOpacity(float inputOpacity)
    {
        ps.startColor = new Color(1.0f, 1.0f, 1.0f, inputOpacity);
    }

    float getOpacity()
    {
        // x^1.5/x^1.25 /100 +0.04
        // 1 = 0.05, 10 = 0.058, 100 = 0.072, 300 = 0.082, 1000 = 0.096
        // 10/255: 0.04, 20/255: 0.0784, 24/255 = 0.094, 30/255: 0.12 (int value from colour picker in editor 0-255)
        return (Mathf.Pow(location.features.population, 1.5f) / Mathf.Pow(location.features.population, 1.25f) /100.0f +0.04f) *
            // psy rating
            (location.ideology.effects.psych/1.0f +1.0f);
    }
}
