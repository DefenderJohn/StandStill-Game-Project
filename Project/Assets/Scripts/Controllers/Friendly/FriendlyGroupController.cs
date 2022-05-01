using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyGroupController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject controlledGameObject=null;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),
                out hit,
                10000))
            {
                if (hit.collider.gameObject.tag == "Friendly")
                {
                    if (controlledGameObject == null)
                    {
                        controlledGameObject = hit.collider.gameObject;
                    }
                    if (controlledGameObject.GetComponent<Controlable>().haveControl(this.controlledGameObject))
                    {
                        controlledGameObject = hit.collider.gameObject;
                        if (this.controlledGameObject != null) controlledGameObject.GetComponent<Controlable>().releaseControl();
                    }
                }
                else if (hit.collider.gameObject.tag == "EnemyTank")
                {
                    if (this.controlledGameObject != null) controlledGameObject.GetComponent<Controlable>().setEnemy(hit.collider.gameObject);
                }
                else
                {
                    if(this.controlledGameObject != null) controlledGameObject.GetComponent<Controlable>().setDestination(hit.point);
                }
            }
        }
    }
}
