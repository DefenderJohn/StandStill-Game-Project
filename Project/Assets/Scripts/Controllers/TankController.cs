using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    public float hitPoints;
    public int ammo;
    public float fuel;
    public float fuelConsumingRatio;
    public float attack;
    public float aimingTime;
    public float loadingTime;
    public Slider HPSlider;
    public Slider ammoSlider;
    public Slider fuelSlider;
    public GameObject turrent;
    // Start is called before the first frame update
    void Start()
    {
        HPSlider.maxValue = this.hitPoints;
        HPSlider.minValue = 0;
        fuelSlider.maxValue = this.fuel;
        fuelSlider.minValue = 0;
        ammoSlider.maxValue = this.ammo;
        ammoSlider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = this.gameObject.GetComponent<TankMoveController>().speed;
        float fuelConsumeHorizontal = Mathf.Sqrt(speed.x * speed.x + speed.z * speed.z);
        float fuelConsumeVertical = speed.y;
        float fuelConsume = fuelConsumeHorizontal * 0.5f * fuelConsumingRatio + fuelConsumeVertical * 1.5f * fuelConsumingRatio;
        fuel -= fuelConsume;
        HPSlider.value = this.hitPoints;
        fuelSlider.value = this.fuel;
        ammoSlider.value = this.ammo;

    }
}
