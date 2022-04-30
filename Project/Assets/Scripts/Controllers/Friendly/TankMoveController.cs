using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface Controlable {
    void haveControl();
    void releaseControl();
    void setEnemy(GameObject enemy);
    void setDestination(Vector3 destination);
}

public class TankMoveController : MonoBehaviour, Controlable
{
    public Vector3 movePos;
    public NavMeshAgent agent;
    public Camera mainCamera;
    public Vector3 speed;
    public bool controlled;
    public Vector3 dest;

    public void haveControl()
    {
        this.controlled = true;
    }

    public void releaseControl()
    {
        this.controlled = false;
    }

    public void setDestination(Vector3 destination)
    {
        this.dest = destination;
    }

    public void setEnemy(GameObject enemy)
    {
        this.gameObject.GetComponent<TankAimingController>().enemy = enemy;
        this.gameObject.GetComponent<TankAimingController>().forceSetEnemy = true;
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
            agent.destination = dest;
        }
        if (this.gameObject.GetComponent<TankController>().fuel <= 0)
        {
            agent.isStopped = true;
        }
    }
}
