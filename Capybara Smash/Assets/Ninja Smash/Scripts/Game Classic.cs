using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NinjaSmash;

namespace NinjaSmash {

public class GameSceneClas : MonoBehaviour
{
    
    public Text textScore;
    public Text textMisses;
    public Text OvertextScore;
    public Text OverTextBest;

    public AudioClip hitClip;
    private static AudioSource hitSource;

    public AudioClip musicClip;
    public static AudioSource musicSource;

    public Image hammer;
    public Image blam;

    public GameObject[] ninjas;
    public GameObject OverScene, PausedScene;


    public Sprite[] ninaImages;



    float minNinjaSpawn = 0.7f; //Min time for ninja to spawn *in seconds*
    float maxNinjaSpawn = 1.1f; //Max time for ninja to spawn *in seconds*

    public static bool isGameOver;
    private bool overSceneLoaded;
    private bool paused;
    private bool[] isHoleTaken;

    private int timerNum = 60;
    private int scoreNum;
    private int missesNum;
    int MaxMissAllowed = 5; // Maximum Number of Misses Allowed in Normal Game Mode
    private int rabbitHit = 0;
    int modeNumber; // Used to store Value of Mode Selected ** 1=Normal 2=Timer 3=Hostage 
    int HostageBest;
    int TimerBest;
    int NormalBest;

    // Start is called before the first frame update
    void Start()
    {
        modeNumber = PlayerPrefs.GetInt("modeSelect", 0);
        HostageBest = PlayerPrefs.GetInt("hostageBest", 0);
        TimerBest = PlayerPrefs.GetInt("timerBest", 0);
        NormalBest = PlayerPrefs.GetInt("normalBest", 0);
        isGameOver = false;
        overSceneLoaded = false;
        paused = false;

        scoreNum = 0;
        missesNum = 0;
        rabbitHit = 0;

        isHoleTaken = new bool[7];
        isHoleTaken[0] = false;
        isHoleTaken[1] = false;
        isHoleTaken[2] = false;
        isHoleTaken[3] = false;
        isHoleTaken[4] = false;
        isHoleTaken[5] = false;
        isHoleTaken[6] = false;

      

        hitSource = gameObject.AddComponent<AudioSource>();
        hitSource.playOnAwake = false;
        hitSource.rolloffMode = AudioRolloffMode.Linear;
        hitSource.clip = hitClip;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.rolloffMode = AudioRolloffMode.Linear;
        musicSource.loop = true;
        musicSource.clip = musicClip;

        musicSource.volume = 2;

        if (MenuScene.musicOn)
        {
            musicSource.Play();
        }

        StartCoroutine(popNinjas());


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("modeSelect", 0);
            SceneManager.LoadScene("ModeScene");
        }

        if (isGameOver && !overSceneLoaded)
        {
            overSceneLoaded = true;
            StartCoroutine(showOver());
        }
    }
    // Game Over Screen 
    IEnumerator showOver()
    {
        yield return new WaitForSeconds(0.5f);
        OvertextScore.text = "Счёт: " + scoreNum;
        // High Score for individual Game Scene
        if (modeNumber == 1)
        {
            if (scoreNum >= NormalBest)
            {
                NormalBest = scoreNum;
                PlayerPrefs.SetInt("normalBest", NormalBest);
                OverTextBest.text = "Рекорд: " + NormalBest;
            }
            else
            {
                OverTextBest.text = "Рекорд: " + NormalBest;
            }
        }
        else if (modeNumber == 2)
        {
            if (scoreNum >= TimerBest)
            {
                TimerBest = scoreNum;
                PlayerPrefs.SetInt("timerBest", TimerBest);
                OverTextBest.text = "Рекорд: " + TimerBest;
            }
            else
            {
                OverTextBest.text = "Рекорд: " + TimerBest;
            }

        }
        else
        {
            if (scoreNum >= HostageBest)
            {
                HostageBest = scoreNum;
                PlayerPrefs.SetInt("hostageBest", HostageBest);
                OverTextBest.text = "Рекорд: " + HostageBest;
            }
            else
            {
                OverTextBest.text = "Рекорд: " + HostageBest;
            }
        }
        OverScene.SetActive(true);
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    // Method To Update Score on Each Smash
    public void updateScore(int ninjaType, int score, int timeVal)
    {
        if (modeNumber == 2)
        {
            if (ninjaType < 4)
            {
                scoreNum += score;
            }
            else
            {
                scoreNum -= score;
                rabbitHit += 1;
            }
        }
        else if (modeNumber == 1)
        {
            scoreNum += score;
        }
        else if (modeNumber == 3)
        {
            if (ninjaType < 4)
            {
                scoreNum += score;
            }
            else
            {
                scoreNum -= score;
                rabbitHit += 1;
                textMisses.text = "Заложники: " + rabbitHit;
                if (rabbitHit == 5)
                {
                    isGameOver = true;
                }
            }
        }



        textScore.text = "Счёт: " + scoreNum;

    }

    // Method to update misses
    public void updateMisses()
    {
        missesNum += 1;
        textMisses.text = "Промах: " + missesNum;
        if (missesNum == MaxMissAllowed)
        {
            isGameOver = true;
        }
    }
    // Method to Play Sound 
    public void playHitSound()
    {
        if (MenuScene.soundOn) hitSource.Play();
    }
    // Method for Replaying Game
    public void OnReplayClick()
    {
        if (modeNumber == 1)
        {
            SceneManager.LoadScene("NormalScene");
        }
        else if (modeNumber == 2)
        {
            SceneManager.LoadScene("TimerScene");
        }
        else
        {
            SceneManager.LoadScene("HostageScene");
        }
    }

    public void OnResumeClick()
    {
        paused = false;
        PausedScene.SetActive(false);
        Time.timeScale = 1;
        if (MenuScene.musicOn)
        {
            musicSource.Play();
        }
    }

    public void OnPauseClick()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            PausedScene.SetActive(true);
            if (musicSource.isPlaying)
            {
                musicSource.Pause();
            }
        }
    }

    public void OnHomeClick()
    {
        PlayerPrefs.SetInt("modeSelect", 0);
        SceneManager.LoadScene("MenuScene");
    }

    

    IEnumerator popNinjas()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(minNinjaSpawn, maxNinjaSpawn));
            {
                int randomHole;
                while (true)
                {
                    int rand = Random.Range(1, 8);
                    if (!isHoleTaken[rand - 1])
                    {
                        randomHole = rand - 1;
                        break;
                    }
                }

                int randomNinjaType = Random.Range(0, 5);

                Image ninjaMain = ninjas[randomHole].GetComponentInChildren<Image>();
                ninjaMain.sprite = ninaImages[randomNinjaType];

                Ninja ninjaScript;
                ninjaScript = ninjas[randomHole].GetComponent<Ninja>();
                ninjaScript.pop(randomNinjaType, true);

                isHoleTaken[randomHole] = true;
                StartCoroutine(changeHoleState(randomHole));
            }
        }
    }

    IEnumerator changeHoleState(int holeNum)
    {
        yield return new WaitForSeconds(1.5f);
        isHoleTaken[holeNum] = false;
    }


}
}
