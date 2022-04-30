using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAimingController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject turrent;
    public GameObject gun;
    public GameObject virtualAiming;
    public bool forceSetEnemy=false;
    public float speed = 10f;
    public Quaternion xzRotationDir;
    public Quaternion yRotationDir;
    public Quaternion rotationOriginal;
    public Vector3 rotationV3;
    public float pitchLimitation;
    public string enemyTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setupEnemy();
        Quaternion currentFrameAngle = setUpTurrentRotation();
        turrent.transform.rotation = currentFrameAngle;
    }

    private void setupEnemy() {
        if (this.gameObject.tag == "EnemyTank" || !forceSetEnemy)
        {
            float minDistance = float.MaxValue;
            enemy = virtualAiming;
            foreach (GameObject tempEnemy in GameObject.FindGameObjectsWithTag(enemyTag))
            {
                if (Vector3.Distance(tempEnemy.transform.position, this.transform.position) < minDistance)
                {
                    enemy = tempEnemy;
                    minDistance = Vector3.Distance(tempEnemy.transform.position, this.transform.position);
                }
            }
        }
        else if(enemy == null)
        {
            forceSetEnemy = false;
            setupEnemy();
        }
    }

    private Quaternion setUpTurrentRotation() {
        float singleStep = speed * Time.deltaTime;
        rotationV3 = enemy.transform.position - this.transform.position;
        rotationOriginal = Quaternion.LookRotation(enemy.transform.position - this.transform.position);
        pitchLimitation = Mathf.Sqrt((enemy.transform.position - this.transform.position).x * (enemy.transform.position - this.transform.position).x + (enemy.transform.position - this.transform.position).z * (enemy.transform.position - this.transform.position).z) *0.1f;
        if (Mathf.Abs((enemy.transform.position - this.transform.position).y) < pitchLimitation)
        {
            xzRotationDir = Quaternion.LookRotation(new Vector3((enemy.transform.position - this.transform.position).x, (enemy.transform.position - this.transform.position).y, (enemy.transform.position - this.transform.position).z));
        }
        else if ((enemy.transform.position - this.transform.position).y > 0)
        {
            xzRotationDir = Quaternion.LookRotation(new Vector3((enemy.transform.position - this.transform.position).x, pitchLimitation, (enemy.transform.position - this.transform.position).z));
        }
        else {
            xzRotationDir = Quaternion.LookRotation(new Vector3((enemy.transform.position - this.transform.position).x, -pitchLimitation, (enemy.transform.position - this.transform.position).z));
        }
        return Quaternion.RotateTowards(turrent.transform.rotation,xzRotationDir, singleStep);
    }
    private Quaternion setUpGunRotation()
    {
        float singleStep = speed * Time.deltaTime;
        yRotationDir = Quaternion.LookRotation(new Vector3((enemy.transform.position - this.transform.position).x, 0, 0));
        return Quaternion.RotateTowards(this.transform.rotation, yRotationDir, singleStep);
    }
}
