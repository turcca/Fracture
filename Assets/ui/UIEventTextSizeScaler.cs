using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIEventTextSizeScaler : MonoBehaviour
{
    public int preferredTextSize = 16;
    public int scalerTextSize;

    RectTransform rectTransform;
    public Text eventText;
    public List<Text> eventChoices = new List<Text>();


    
    void Awake()
    {
        preferredTextSize = eventText.fontSize;
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable()");
        scalerTextSize = preferredTextSize;
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null) Debug.LogError("no RectTransform present on this object");
        else init();
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.sizeDelta.y > 0)
        {
            init();
            // overflow, reduce font sizes
            scalerTextSize--;
            Debug.Log("UPDATE: overflow, reduced font sizes to " + scalerTextSize);
            eventText.fontSize = scalerTextSize;
            foreach (Text t in eventChoices)
            {
                t.fontSize = scalerTextSize;
            }
        }
    }


    void init()
    {        
        updateEventChoices();
        // reset font sizes
        eventText.fontSize = scalerTextSize;
        foreach (Text t in eventChoices)
        {
            t.fontSize = scalerTextSize;
        }
    }
    
    void updateEventChoices()
    {
        eventChoices.Clear();
        foreach (var child in this.gameObject.GetComponentsInChildren<Text>())
        {
            if (child.gameObject.name == "EventChoice(Clone)")
            {
                eventChoices.Add(child);
            }
        }
    }
}
