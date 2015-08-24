using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MapMouseCatcher : MonoBehaviour
{
    public PlayerShipMover playerMover;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
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
