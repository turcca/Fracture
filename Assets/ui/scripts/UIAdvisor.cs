using UnityEngine;
using System.Collections;

public class UIAdvisor : MonoBehaviour
{
    public delegate void AdvisorSelectedDelegate(string pos);

    private string advisorPosition = "";
    private Character character;
    private GameObject adviseFrame;
    AdvisorSelectedDelegate callback;

    // Use this for initialization
    void Start()
    {
        adviseFrame = transform.GetChild(0).gameObject;
        NGUITools.SetActive(adviseFrame, false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setup(string p, AdvisorSelectedDelegate d)
    {
        advisorPosition = p;
        character = Universe.singleton.player.getCharacter(advisorPosition);
        callback = d;
    }

    void OnClick()
    {
        callback(advisorPosition);
    }
}
