using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MapMouseCatcher : MonoBehaviour
{
    public PlayerShipMover playerMover;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000.0f, LayerMask.GetMask("MouseCatcher")))
            {
                playerMover.setTarget(hit.point);
            }
        }
    }
}
