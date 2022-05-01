using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseController : MonoBehaviour, Controlable, IHitable
{
    public float HP = 10000;
    public float maxFuel = 2000;
    public float fuel = 0;
    public int maxAmmo = 500;
    public int ammo = 0;
    public float maxHeal = 10000;
    public float heal = 0;
    public int currentSceneIndex;
    public bool displayEnabled;
    public Canvas mainUI;
    public Text gameOverText;
    public Text gamePauseText;
    public Text statusButtonText;
    public Text fuelText;
    public Text ammoText;
    public Text healText;
    public Button restartButton;
    public Button resumeButton;
    public Button setDisplayButton;
    public Slider HPSlider;
    public Slider fuelSlider;
    public Slider ammoSlider;
    public Slider healSlider;

    public Slider refuelCurrentSlider;
    public Slider refuelTargetSlider;
    public Slider reloadCurrentSlider;
    public Slider reloadTargetSlider;
    public Slider repairCurrentSlider;
    public Slider repairTargetSlider;
    public Slider dropoffCurrentSlider;
    public Slider dropoffTargetSlider;

    public Text refuelCurrentText;
    public Text refuelTargetText;
    public Text reloadCurrentText;
    public Text reloadTargetText;
    public Text repairCurrentText;
    public Text repairTargetText;
    public Text dropoffCurrentText;
    public Text dropoffTargetText;

    public GameObject refuelMenu;
    public GameObject reloadMenu;
    public GameObject repairMenu;
    public GameObject dropoffMenu;

    public Text refuelText;
    public Text reloadText;
    public Text repairText;
    public Text dropoffText;

    public char refueling;
    public char reloading;
    public char repairing;
    public char dropping;

    public float refuelRatio;
    public float reloadRatio;
    public float repairRatio;
    public float dropRatio;
    public float tempAmmo;

    public Canvas baseUI;
    public GameObject supplyingObject;

    public bool haveControl(GameObject caller)
    {
        this.baseUI.gameObject.SetActive(true);
        supplyingObject = caller;
        return false;
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
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        this.HPSlider.maxValue = HP;
        this.HPSlider.minValue = 0;
        this.fuelSlider.maxValue = maxFuel;
        this.fuelSlider.minValue = 0;
        this.ammoSlider.maxValue = maxAmmo;
        this.ammoSlider.minValue = 0;
        this.healSlider.maxValue = maxHeal;
        this.healSlider.minValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        this.HPSlider.value = this.HP;
        this.ammoSlider.value = this.ammo;
        this.fuelSlider.value = this.fuel;
        if (refueling == 'W')
        {

            if (refuelTargetSlider.value - supplyingObject.gameObject.GetComponent<ResourceManagement>().getFuel() > 1 && this.fuel > 0)
            {
                supplyingObject.gameObject.GetComponent<ResourceManagement>().addFuel(Time.deltaTime * refuelRatio);
                this.fuel -= (Time.deltaTime * refuelRatio);
                refuelCurrentSlider.value = supplyingObject.gameObject.GetComponent<ResourceManagement>().getFuel();
            }
            else if (refuelTargetSlider.value - supplyingObject.gameObject.GetComponent<ResourceManagement>().getFuel() < 1 && this.fuel < maxFuel)
            {
                supplyingObject.gameObject.GetComponent<ResourceManagement>().addFuel(Time.deltaTime * refuelRatio * -1);
                this.fuel += (Time.deltaTime * refuelRatio);
                refuelCurrentSlider.value = supplyingObject.gameObject.GetComponent<ResourceManagement>().getFuel();
            }
            else
            {
                refueling = 'C';
                this.refuelText.text = "Compeleted!";
            }
        }

        if (reloading == 'W')
        {
            if (reloadTargetSlider.value - supplyingObject.gameObject.GetComponent<ResourceManagement>().getAmmo() > 1 && this.ammo > 0)
            {
                tempAmmo += Time.deltaTime * reloadRatio;
                if (tempAmmo > 1)
                {
                    supplyingObject.gameObject.GetComponent<ResourceManagement>().setAmmo(1);
                    reloadCurrentSlider.value = supplyingObject.gameObject.GetComponent<ResourceManagement>().getAmmo();
                    tempAmmo--;
                    this.ammo--;
                }
            }
            else if (refuelTargetSlider.value - supplyingObject.gameObject.GetComponent<ResourceManagement>().getAmmo() < 1 && this.ammo < maxAmmo)
            {
                tempAmmo += Time.deltaTime * reloadRatio;
                if (tempAmmo > 1)
                {
                    supplyingObject.gameObject.GetComponent<ResourceManagement>().setAmmo(-1);
                    reloadCurrentSlider.value = supplyingObject.gameObject.GetComponent<ResourceManagement>().getAmmo();
                    tempAmmo--;
                    this.ammo++;
                }
            }
            else
            {
                reloading = 'C';
                this.reloadText.text = "Compeleted!";
            }
        }

        if (repairing == 'W')
        {
            if (repairTargetSlider.value - supplyingObject.gameObject.GetComponent<ResourceManagement>().getHP() > 1 && this.heal > 0)
            {
                supplyingObject.gameObject.GetComponent<ResourceManagement>().heal(Time.deltaTime * refuelRatio);
                this.heal -= (Time.deltaTime * refuelRatio);
                repairCurrentSlider.value = supplyingObject.gameObject.GetComponent<ResourceManagement>().getHP();
            }
            else if (repairTargetSlider.value - supplyingObject.gameObject.GetComponent<ResourceManagement>().getHP() < 1 && this.heal < maxHeal)
            {
                supplyingObject.gameObject.GetComponent<ResourceManagement>().heal(Time.deltaTime * refuelRatio * -1);
                this.heal += (Time.deltaTime * refuelRatio);
                repairCurrentSlider.value = supplyingObject.gameObject.GetComponent<ResourceManagement>().getHP();
            }
            else
            {
                repairing = 'C';
                this.repairText.text = "Compeleted!";
            }
        }


        if (this.HP <= 0)
        {
            this.gameOverText.gameObject.SetActive(true);
            this.restartButton.gameObject.SetActive(true);
            Time.timeScale = 0.01f;
        }
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(currentSceneIndex);
        this.gamePauseText.gameObject.SetActive(false);
        this.resumeButton.gameObject.SetActive(false);
        this.setDisplayButton.gameObject.SetActive(false);
        this.gameOverText.gameObject.SetActive(false);
        this.restartButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPauseButtonClicked()
    {

        this.gamePauseText.gameObject.SetActive(true);
        this.resumeButton.gameObject.SetActive(true);
        this.setDisplayButton.gameObject.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        this.gamePauseText.gameObject.SetActive(false);
        this.resumeButton.gameObject.SetActive(false);
        this.setDisplayButton.gameObject.SetActive(false);
    }

    public void OnChangeDisplaySettingButtonClicked()
    {
        if (displayEnabled)
        {
            this.fuelSlider.gameObject.SetActive(false);
            this.ammoSlider.gameObject.SetActive(false);
            this.ammoText.gameObject.SetActive(false);
            this.fuelText.gameObject.SetActive(false);
            this.healSlider.gameObject.SetActive(false);
            this.healText.gameObject.SetActive(false);
            statusButtonText.text = "Display Base Status Detail";
            this.displayEnabled = false;
        }
        else
        {
            this.fuelSlider.gameObject.SetActive(true);
            this.ammoSlider.gameObject.SetActive(true);
            this.ammoText.gameObject.SetActive(true);
            this.fuelText.gameObject.SetActive(true);
            this.healSlider.gameObject.SetActive(true);
            this.healText.gameObject.SetActive(true);
            statusButtonText.text = "Hide Base Status Detail";
            this.displayEnabled = true;
        }
    }

    public void getHit(float attack)
    {
        this.HP -= attack;
    }

    public void OnRefuelButtonClicked()
    {
        switch (refueling)
        {
            case 'F':
                this.refuelCurrentSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxFuel();
                this.refuelCurrentSlider.minValue = 0;
                this.refuelTargetSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxFuel();
                this.refuelTargetSlider.minValue = 0;
                this.refuelCurrentSlider.value = this.supplyingObject.GetComponent<ResourceManagement>().getFuel();
                refuelMenu.SetActive(true);
                refueling = 'W';
                refuelText.text = "Cancel";
                break;
            case 'W':
                refuelMenu.SetActive(false);
                refueling = 'F';
                refuelText.text = "Refuel&Unfuel";
                break;
            case 'C':
                refuelText.text = "Refuel&Unfuel";
                refuelMenu.SetActive(false);
                this.baseUI.gameObject.SetActive(false);
                refueling = 'F';
                break;
        }
    }

    public void OnReloadButtonClicked()
    {
        switch (reloading)
        {
            case 'F':
                this.reloadCurrentSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxAmmo();
                this.reloadCurrentSlider.minValue = 0;
                this.reloadTargetSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxAmmo();
                this.reloadTargetSlider.minValue = 0;
                this.reloadCurrentSlider.value = this.supplyingObject.GetComponent<ResourceManagement>().getAmmo();
                refuelMenu.SetActive(true);
                refueling = 'W';
                refuelText.text = "Cancel";
                break;
            case 'W':
                refuelMenu.SetActive(false);
                refueling = 'F';
                refuelText.text = "Reload&Unload";
                break;
            case 'C':
                refuelText.text = "Reload&Unload";
                refuelMenu.SetActive(false);
                this.baseUI.gameObject.SetActive(false);
                refueling = 'F';
                break;
        }
    }

    public void OnRepairButtonClicked()
    {
        switch (repairing)
        {
            case 'F':
                this.repairCurrentSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxHP();
                this.repairCurrentSlider.minValue = 0;
                this.repairTargetSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxHP();
                this.repairTargetSlider.minValue = 0;
                this.repairCurrentSlider.value = this.supplyingObject.GetComponent<ResourceManagement>().getHP();
                refuelMenu.SetActive(true);
                refueling = 'W';
                refuelText.text = "Cancel";
                break;
            case 'W':
                refuelMenu.SetActive(false);
                refueling = 'F';
                refuelText.text = "Repair";
                break;
            case 'C':
                refuelText.text = "Repair";
                refuelMenu.SetActive(false);
                this.baseUI.gameObject.SetActive(false);
                refueling = 'F';
                break;
        }
    }

    public void OnDropButtonClicked()
    {
        switch (dropping)
        {
            case 'F':
                this.dropoffCurrentSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxSupply();
                this.dropoffCurrentSlider.minValue = 0;
                this.dropoffTargetSlider.maxValue = this.supplyingObject.GetComponent<ResourceManagement>().getMaxSupply();
                this.dropoffTargetSlider.minValue = 0;
                this.dropoffCurrentSlider.value = this.supplyingObject.GetComponent<ResourceManagement>().getSupply();
                refuelMenu.SetActive(true);
                refueling = 'W';
                refuelText.text = "Cancel";
                break;
            case 'W':
                refuelMenu.SetActive(false);
                refueling = 'F';
                refuelText.text = "Drop Off\nSupplies";
                break;
            case 'C':
                refuelText.text = "Drop Off\nSupplies";
                refuelMenu.SetActive(false);
                this.baseUI.gameObject.SetActive(false);
                refueling = 'F';
                break;
        }
    }
}
