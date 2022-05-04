using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Vector3 movePos;
    public NavMeshAgent agent;
    public Camera mainCamera;
    public GameObject enemyInContact;
    public bool contactWithEnemy;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
        agent.destination = movePos;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<TankController>().fuel > 0)
        {
            if (!contactWithEnemy)
            {
                foreach (GameObject friends in GameObject.FindGameObjectsWithTag("Friendly"))
                {
                    if (Vector3.Distance(friends.gameObject.transform.position, this.gameObject.transform.position) <= 30)
                    {
                        agent.isStopped = true;
                        contactWithEnemy = true;
                        enemyInContact = friends;
                        break;
                    }
                }
            }
            else
            {
                if (enemyInContact == null || Vector3.Distance(enemyInContact.transform.position, this.transform.position) > 130)
                {
                    agent.isStopped = false;
                    contactWithEnemy = false;
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }
}

