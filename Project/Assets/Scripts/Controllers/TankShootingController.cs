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
    public GameObject targetObject;
    public ParticleSystem shootFire;
    private float attackAbility;
    // Start is called before the first frame update
    void Start()
    {
        savedShootingGap = shootingGap;
        savedViewGap = viewGap;
        attackAbility = this.gameObject.GetComponent<TankController>().attack;
    }

    // Update is called once per frame
    void Update()
    {
        shootingGap -= Time.deltaTime;
        if (shootingGap <= 0.0f)
        {
            shootingGap = -1.0f;
            this.shootFire.Stop();
            viewGap -= Time.deltaTime;
            if (viewGap <= 0.0f)
            {
                if (aimOnTarget(out GameObject targetObject))
                {
                    shootingGap = savedShootingGap;
                    shoot(targetObject);
                }
                viewGap = savedViewGap;
            }
        }
    }
   
    private bool aimOnTarget(out GameObject targetObject) {
        targetObject = null;
        Ray checkingRay = new Ray(shootingPosFront.transform.position, shootingPosFront.transform.position - shootingPosRear.transform.position);
        bool hittedObject = Physics.Raycast(checkingRay, out RaycastHit hitInfo, 200);
        if (hittedObject)
        {
            targetObject = hitInfo.collider.gameObject;
            if (targetObject.tag == enemyTag)
            {
                return true;
            }
        }
        return false;
    }

    private void shoot(GameObject hittedObject) {
        if (this.gameObject.GetComponent<TankController>().ammo > 0)
        {
            this.shootFire.Play();
            hittedObject.GetComponent<IHitable>().getHit(attackAbility);
            this.gameObject.GetComponent<TankController>().ammo--;
        }
    }
}
