using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    public float hitPoints;
    public float attack;
    public float aimingTime;
    public float loadingTime;
    public Slider slider;
    public GameObject turrent;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = this.hitPoints;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = this.hitPoints;
    }
}
