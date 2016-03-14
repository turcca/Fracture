using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LocationStarmapVisibility : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    public ParticleSystem ps = null;
    public TextMesh nameTag;
    public Collider collider_;
    TextOutline outline;
    Color nameTagColorTarget;
    Color outlineColorTarget;
    //bool visibility;
    public bool fadeNameTag = true;

    public float opacity;
    public bool inPlayerRange = false;

    public Location location;

    
	void Start () 
    {
        ps = GetComponentInChildren<ParticleSystem>();

        if (ps != null)
        {
            initLocation();
            calculateSize();
            setupNameTag();
        }
	}
    // -----------------------
	void Update () 
    {
	    if (ps != null)
        {
            // distance dimming
            float distance = Vector3.Distance(this.gameObject.transform.position, Root.game.player.position);

            if (distance > 50.0f * getConnectedPenaltyMul() + opacity*200f -15f ) // modifiers: psy str, disconnected // TODO sensors str/navigation
            {
                setParticleOpacity(0.0f);
                inPlayerRange = false;
                collider_.enabled = false;
            }
            else
            {
                setParticleOpacity (opacity);
                inPlayerRange = true;
                collider_.enabled = true;
            }
        }
        // fade nameTag alpha in/out
        if (fadeNameTag)
        {
            float rateOfChange = 0.15f; // adjusted in conjuction with particlesystem life (turning on/off delay)

            if (inPlayerRange && 
                location.features.visibility == Data.Location.Visibility.Connected)
            {
                // fade in
                if (nameTag.color.a < nameTagColorTarget.a)
                {
                    nameTag.color = new Color(nameTagColorTarget.r, nameTagColorTarget.g, nameTagColorTarget.b, Mathf.Min(nameTag.color.a + rateOfChange * Time.deltaTime, nameTagColorTarget.a));
                }
                if (getTextOutline().outlineColor.a < outlineColorTarget.a)
                {
                    getTextOutline().outlineColor = new Color(outlineColorTarget.r, outlineColorTarget.g, outlineColorTarget.b, Mathf.Min(getTextOutline().outlineColor.a + rateOfChange * Time.deltaTime, outlineColorTarget.a));
                }
            }
            else
            {
                // fade out
                if (nameTag.color.a > 0)
                {
                    nameTag.color = new Color(nameTagColorTarget.r, nameTagColorTarget.g, nameTagColorTarget.b, Mathf.Max(nameTag.color.a - rateOfChange * Time.deltaTime, 0));
                }
                if (getTextOutline().outlineColor.a > 0)
                {
                    getTextOutline().outlineColor = new Color(outlineColorTarget.r, outlineColorTarget.g, outlineColorTarget.b, Mathf.Max(getTextOutline().outlineColor.a - rateOfChange * Time.deltaTime, 0));
                }
            }
        }
    }

    // -----Event trigger------
    public void eventPointerOver(bool entering)
    {
        if (location.features.visibility == Data.Location.Visibility.Connected)
        {
            Simulation.StarmapVisualization.mouseOverLocationForInfo(this, entering);
            if (entering) Simulation.StarmapVisualization.lineUI.setTargetObject(this.gameObject);
            else Simulation.StarmapVisualization.lineUI.clearTargetObject();
        }
    }
    public void eventPointerClick()
    {
        MapMoveTarget.setTarget(location.position);
    }

    // -----------------------


    void initLocation()
    {
        location = Root.game.locations[this.gameObject.name];
    }

    public void setupNameTag(/*bool visibility = false*/)
    {
        if (outline == null)
        {
            // initialize
            nameTag.text = location.features.name;
            getTextOutline().setup();
        }
        //nameTag.gameObject.SetActive(visibility);
        //this.visibility = visibility;
    }

    public TextOutline getTextOutline()
    {
        if (outline == null)
        {
            outline = nameTag.gameObject.GetComponent<TextOutline>();
            if (outline == null) { Debug.LogError("No TextOutline on the object: " + this.gameObject.name); return null; }
            // init: save editor colors as targets
            outlineColorTarget = outline.outlineColor;
            nameTagColorTarget = nameTag.color;
        }
        return outline;
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
            (location.ideology.effects[Simulation.Effect.psych] /2.0f +1.0f)
            // navigator?
            ;
        // alpha
        setParticleOpacity(opacity);
    }

    private void setParticleOpacity(float inputOpacity)
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
            (location.ideology.effects[Simulation.Effect.psych] /1.0f +1.0f);

        // location values vary ~ between 0.03 - 0.13
    }
    float getConnectedPenaltyMul()
    {
        return location.features.visibility == Data.Location.Visibility.Connected ? 1f : 0.5f;
    }
}
