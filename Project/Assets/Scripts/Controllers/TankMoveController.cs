using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface Controlable {
    void haveControl();
    void releaseControl();
}

public class TankMoveController : MonoBehaviour, Controlable
{
    public Vector3 movePos;
    public NavMeshAgent agent;
    public Camera mainCamera;
    public Vector3 speed;
    public bool controlled;

    public void haveControl()
    {
        this.controlled = true;
    }

    public void releaseControl()
    {
        this.controlled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        movePos = gameObject.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (controlled && this.gameObject.GetComponent<TankController>().fuel > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Vector3 mousePos = Input.mousePosition;
                mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),
                    out hit,
                    10000))
                {
                    agent.destination = hit.point;
                }
            } 
        }
    }
}
