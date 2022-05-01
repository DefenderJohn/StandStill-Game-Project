using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ResourceManagement
{
    float getFuel();
    void addFuel(float value);
    float getHP();
    void heal(float value);
    int getAmmo();
    void setAmmo(int value);
    float getSupply();
}

public class TankController : MonoBehaviour, ResourceManagement
{
    public float hitPoints;
    public int ammo;
    public float fuel;
    public float collectedSupply;
    public float fuelConsumingRatio;
    public float attack;
    public float aimingTime;
    public float loadingTime;
    public Canvas canvas;
    public Slider HPSlider;
    public Slider ammoSlider;
    public Slider fuelSlider;
    public GameObject turrent;
    public Camera mainCamera;
    public Vector3 lastframePos;
    public Vector3 currentSpeed;

    public void addFuel(float value)
    {
        this.fuel += value;
    }

    public int getAmmo()
    {
        return this.ammo;
    }

    public float getFuel()
    {
        return this.fuel;
    }

    public float getHP()
    {
        return this.hitPoints;
    }

    public float getSupply()
    {
        return collectedSupply;
    }

    public void heal(float value)
    {
        this.hitPoints += value;
    }

    public void setAmmo(int value)
    {
        this.ammo += value;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Cameras").GetComponent<CameraController>().mainCamera;
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
        canvas.gameObject.transform.rotation = mainCamera.transform.rotation;
        currentSpeed = this.gameObject.transform.position - lastframePos;
        lastframePos = this.gameObject.transform.position;

        float fuelConsumeHorizontal = Mathf.Sqrt(currentSpeed.x * currentSpeed.x + currentSpeed.z * currentSpeed.z);
        float fuelConsumeVertical = currentSpeed.y;
        float fuelConsume = fuelConsumeHorizontal * 0.5f * fuelConsumingRatio + fuelConsumeVertical * 1.5f * fuelConsumingRatio + 0.1f * fuelConsumingRatio;
        fuel -= fuelConsume;
        HPSlider.value = this.hitPoints;
        fuelSlider.value = this.fuel;
        ammoSlider.value = this.ammo;

    }
}
