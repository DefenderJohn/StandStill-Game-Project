using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShootingController : MonoBehaviour
{
    public float shootingGap;
    private float savedShootingGap;
    public float viewGap;
    private float savedViewGap;
    public string enemyTag;
    public GameObject shootingPosFront;
    public GameObject shootingPosRear;
    public GameObject checkedObject;
    // Start is called before the first frame update
    void Start()
    {
        savedShootingGap = shootingGap;
        savedViewGap = viewGap;
    }

    // Update is called once per frame
    void Update()
    {
        shootingGap -= Time.deltaTime;
        if (shootingGap <= 0.0f)
        {
            shootingGap = -1.0f;
            viewGap -= Time.deltaTime;
            if (viewGap <= 0.0f)
            {
                if (aimOnTarget())
                {
                    shootingGap = savedShootingGap;
                    shoot();
                }
                viewGap = savedViewGap;
            }
        }
    }
   
    private bool aimOnTarget() {
        Ray checkingRay = new Ray(shootingPosFront.transform.position, shootingPosFront.transform.position - shootingPosRear.transform.position);
        bool hittedObject = Physics.Raycast(checkingRay, out RaycastHit hitInfo, 200);
        if (hittedObject)
        {
            checkedObject = hitInfo.collider.gameObject;
            if (checkedObject.tag == enemyTag)
            {
                return true;
            }
        }
        return false;
    }

    private void shoot() {
        print("shooted");
    }
}
