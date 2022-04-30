using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseController : MonoBehaviour, Controlable
{
    public float HP = 10000;
    public float maxFuel = 2000;
    public float fuel = 0;
    public int maxAmmo = 500;
    public int ammo = 0;
    public int currentSceneIndex;
    public bool displayEnabled;
    public Canvas mainUI;
    public Text gameOverText;
    public Text gamePauseText;
    public Text statusButtonText;
    public Text fuelText;
    public Text ammoText;
    public Button restartButton;
    public Button resumeButton;
    public Button setDisplayButton;
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
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
            statusButtonText.text = "Display Base Status Detail";
            this.displayEnabled = false;
        }
        else
        {
            this.fuelSlider.gameObject.SetActive(true);
            this.ammoSlider.gameObject.SetActive(true);
            this.ammoText.gameObject.SetActive(true);
            this.fuelText.gameObject.SetActive(true);
            statusButtonText.text = "Hide Base Status Detail";
            this.displayEnabled = true;
        }
    }
}
