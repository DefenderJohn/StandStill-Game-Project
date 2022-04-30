using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour,Controlable
{
    public float HP;
    public float fuel;
    public int ammo;
    public Canvas mainUI;

    public void haveControl()
    {
        return;
    }

    public void releaseControl()
    {
        return;
    }

    public void setDestination(Vector3 destination)
    {
        return;
    }

    public void setEnemy(GameObject enemy)
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.HP <= 0) {

            Time.timeScale = 0;
        }
    }
}
