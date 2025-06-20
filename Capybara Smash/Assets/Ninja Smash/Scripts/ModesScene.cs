using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using NinjaSmash;

namespace NinjaSmash {
public class ModesScene : MonoBehaviour
{

    // Music variable
    public AudioClip musicClip;
    public static AudioSource musicSource;
    // modeNumber Used to store the Game Mode selected by User
    int modeNumber;


    // Use this for initialization
    void Start()
    {

        // Initialize music
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.rolloffMode = AudioRolloffMode.Linear;
        musicSource.loop = true;
        musicSource.clip = musicClip;

        if (MenuScene.musicOn)
        {
            musicSource.Play(); // Play music if music is on
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           SceneManager.LoadScene("MenuScene"); // goto menu screen if back key is pressed
        }
    }
    public void OnNormalBtnClick()
    {
        modeNumber = 1; // Storing Level Information 
        PlayerPrefs.SetInt("modeSelect",modeNumber);
        SceneManager.LoadScene("NormalScene");

    }
    public void OnTimerBtnClick()
    {
		modeNumber = 2;
		PlayerPrefs.SetInt("modeSelect",modeNumber);
        SceneManager.LoadScene("TimerScene");
    }
    public void OnHostageBtnClick()
    {
		modeNumber = 3;
		PlayerPrefs.SetInt("modeSelect",modeNumber);
        SceneManager.LoadScene("HostageScene");
    }
}
}
