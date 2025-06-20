using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using NinjaSmash;
using UnityEngine.UI;
namespace NinjaSmash {
public class Ninja : MonoBehaviour
{
    // Animator variable for ninja
    private Animator animator;

    // Boolean to check if ninja can be smashed
    private bool canSmash;
    private GameScene gameScene;

    public Animator hammer;
    public Animator blam;
    public Text scoreText;

    private int currentNinjaType;
    int modeNumber;

    // Use this for initialization
    void Start()
    {
        modeNumber = PlayerPrefs.GetInt("modeSelect", 0);
        // Get animator from gameobject
        animator = GetComponent<Animator>();

        // Ninjas are hiding at start so they cannot be smashed
        canSmash = false;

        currentNinjaType = 0;

        gameScene = GameObject.Find("Scene").GetComponent<GameScene>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    // On Tap to Ninja Performing Various Actions
    // modeNumber used as PlayerPrefs to detect level and to act accordingly for ninja objects
    public void onSmash()
    {
        if (canSmash)
        {
            canSmash = false;
            hammer.SetTrigger("hit");
            blam.SetTrigger("blam");
            gameScene.playHitSound();
            int scoreNum = 0, timeNum = 0;
            if (modeNumber == 2)
            {
                if (currentNinjaType == 0)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+10 очков";
                    scoreNum = 10;
                    

                    
                }
                else if (currentNinjaType == 1)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+15 очков";
                    scoreNum = 15;
                }
                else if (currentNinjaType == 2)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+20 очков";
                    scoreNum = 20;
                }
                else if (currentNinjaType == 3)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+5 очков";
                    scoreNum = 5;
                }
                else
                {
                    scoreText.color = new Color32(255, 0, 0, 255);
                    scoreText.text = "-50 очков";
                    scoreNum = 50;
                }
            }
            else if (modeNumber == 1)
            {
                if (currentNinjaType == 0)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+10 очков";
                    scoreNum = 10;
                }
                else if (currentNinjaType == 1)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+15 очков";
                    scoreNum = 15;
                }
                else if (currentNinjaType == 2)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+20 очков";
                    scoreNum = 20;
                }
                else if (currentNinjaType == 3)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+5 очков";
                    scoreNum = 5;
                }
                else if (currentNinjaType == 4)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+5 очков";
                    scoreNum = 5;
                }

            }
            if (modeNumber == 3)
            {
                if (currentNinjaType == 0)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+10 очков";
                    scoreNum = 10;
                    Instantiate(scoreText , transform.position , Quaternion.identity);
                   
                }
                else if (currentNinjaType == 1)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+15 очков";
                    scoreNum = 15;
                }
                else if (currentNinjaType == 2)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+20 очков";
                    scoreNum = 20;
                }
                else if (currentNinjaType == 3)
                {
                    scoreText.color = new Color32(0, 255, 0, 255);
                    scoreText.text = "+5 очков";
                    scoreNum = 5;
                }
                else
                {
                    scoreText.color = new Color32(255, 0, 0, 255);
                    scoreText.text = "-50 очков";
                    scoreNum = 50;
                }
            }


            gameScene.updateScore(currentNinjaType, scoreNum, timeNum);
            scoreText.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("pop");
        }
    }

    // Method to pop ninjas out by triggering its animation state
    public void pop(int ninjaType, bool canUpdate)
    {
        if (!GameScene.isGameOver)
        {
            animator.SetTrigger("pop");
            canSmash = true;

            currentNinjaType = ninjaType;

            // GetComponent<Collider2D>().enabled = true;

            StartCoroutine(popEnds(canUpdate));
        }

    }

    // Method to hide ninja back into hole after a few second
    IEnumerator popEnds(bool canUpdate)
    {
        yield return new WaitForSeconds(1.0f);
        if (canSmash)
        {
            if (modeNumber == 1 && !GameScene.isGameOver)
            {
                gameScene.updateMisses();
            }

        }
        // GetComponent<Collider2D>().enabled = false;
        canSmash = false;

    }

}
}






