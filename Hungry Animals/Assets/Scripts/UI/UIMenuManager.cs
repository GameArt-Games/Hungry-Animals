using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIMenuManager : MonoBehaviour
{
    public DontDistroyManager dontDistroyManager;
    public AudioManager audioManager;

    public GameObject playBtn;

    [SerializeField] GameObject _loadingPanel;
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;
    [SerializeField] Toggle _fullScreenToggle;
    [SerializeField] TMP_Dropdown _resulotionDropdown;
    [SerializeField] TMP_Dropdown _qualityDropdown;

    Resolution[] _resolutions;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    void Start()
    {
        StartCoroutine(LoadingClose());
        dontDistroyManager = GameObject.FindGameObjectWithTag("DontDistroyManager").GetComponent<DontDistroyManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (!dontDistroyManager.isGameRunning)
            {
                SetSettings();
                dontDistroyManager.isGameRunning = true;
            }
        }
    }

    IEnumerator LoadingClose()
    {
        yield return new WaitForSeconds(1);
        _loadingPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetSettings()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        _fullScreenToggle.isOn = GetFullScreen();
        GetResolution();
        GetQuality();
    }

    public void SetMusicVolume(float volume)
    {
        audioManager.audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        audioManager.audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        if (isFullScreen)
        {
            PlayerPrefs.SetInt("isFullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isFullScreen", 0);
        }
    }

    public bool GetFullScreen()
    {
        int isFullScreen = PlayerPrefs.GetInt("isFullScreen", 1);
        return isFullScreen == 1 ? true : false;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, GetFullScreen());
    }

    public void GetResolution()
    {
        _resolutions = Screen.resolutions;
        _resulotionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height + " @ " + _resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (GetFullScreen())
            {
                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            else
            {
                if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }
        _resulotionDropdown.AddOptions(options);
        _resulotionDropdown.value = currentResolutionIndex;
        _resulotionDropdown.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void GetQuality()
    {
        int value = QualitySettings.GetQualityLevel();
        _qualityDropdown.value = value;
    }
}
