using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour,Controlable
{
    public float HP = 10000;
    public float maxFuel = 2000;
    public float fuel = 0;
    public int maxAmmo = 500;
    public int ammo = 0;
    public Canvas mainUI;
    public GameObject gameOverText;
    public GameObject restartButton;
    public Slider HPSlider;
    public Slider fuelSlider;
    public Slider ammoSlider;

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
        this.HPSlider.maxValue = HP;
        this.HPSlider.minValue = 0;
        this.fuelSlider.maxValue = maxFuel;
        this.fuelSlider.minValue = 0;
        this.ammoSlider.maxValue = maxAmmo;
        this.ammoSlider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.HPSlider.value = this.HP;
        this.ammoSlider.value = this.ammo;
        this.fuelSlider.value = this.fuel;
        if (this.HP <= 0) {
            this.gameOverText.SetActive(true);
            this.restartButton.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnRestartButtonClicked() { 
    
    }

    public void OnPauseButtonClicked() { 
    
    }

    public void OnResumeButtonClicked() { 
    
    }

    public void OnChangeDisplaySettingButtonClicked()
    {

    }
}
