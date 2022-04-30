using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable {
    void getHit(float attack);
}

public class HitController : MonoBehaviour, IHitable
{
    public void getHit(float attack)
    {
        gameObject.GetComponent<TankController>().hitPoints -= attack;
        if (gameObject.GetComponent<TankController>().hitPoints <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
