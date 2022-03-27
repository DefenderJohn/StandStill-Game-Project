using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMoveController : MonoBehaviour
{
    public Vector3 movePos;
    public NavMeshAgent agent;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        movePos = gameObject.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 
                out hit, 
                100))
            {
                agent.destination = hit.point;
            }
        }
    }
}
