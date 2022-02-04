using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TextMeshProUGUI volumeTextValue = null;
    [SerializeField] private UnityEngine.UI.Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.6f;
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("General Stuff")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    public string _MainMenu;

    void Start()
    {
        SetGlobalVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }

        if (!isPaused)
        {
            DeactivateMenu();
        }
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(_MainMenu);
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;
    }

    public void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void DisableMenu()
    {
        pauseMenuUI.SetActive(false);
    }

        public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void SetGlobalVolume()
    {
        float startv = PlayerPrefs.GetFloat("masterVolume");
        volumeSlider.value = startv;
        SetVolume(startv);
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }
}
