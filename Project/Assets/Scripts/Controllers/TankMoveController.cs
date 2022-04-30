using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMoveController : MonoBehaviour
{
    public Vector3 movePos;
    public NavMeshAgent agent;
    public Camera mainCamera;
    public Vector3 speed;

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
        
        if (Input.GetMouseButtonDown(0))
        {
            //agent.Resume();
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

        //if (Mathf.Abs(Vector3.Distance(this.transform.position, movePos)) <= 1)
        //{
        //    agent.Stop();
        //}
    }
}
