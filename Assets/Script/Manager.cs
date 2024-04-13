using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    public SpriteRenderer[] colours;
    public AudioSource[] buttonSounds; 
    private int colourSelect;

    public float stayLit;
    private float stayLitCounter;

    public float waitBetweenLight;
    private float waitBetweenCounter;

    private bool shouldBeLit;
    private bool shouldBeDark;

    public List<int> activeSequence;
    private int positionInSequence;

    private bool gameActive;
    private int inputInSequence;

    private bool gameStart = false;

    public AudioSource correct;
    public AudioSource incorrect;

    public Text scoreText;
    public Text currectStatus;


    Vector3 accelerationDir;

    
    void Start()
    {
        if (!PlayerPrefs.HasKey("HiScore"))
        {
            PlayerPrefs.SetInt("HiScore", 0);
            
        }

        scoreText.text = "Score: 0 - High Score: " + PlayerPrefs.GetInt("HiScore");
    }

    
    void Update()
    {

        accelerationDir = Input.acceleration;

        if (accelerationDir.sqrMagnitude >=10f)
        {
            StartGame();
        }

        if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;


            if (stayLitCounter < 0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
                buttonSounds[activeSequence[positionInSequence]].Stop();
                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLight;

                positionInSequence++;
            }
        }

        if (shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if (positionInSequence >= activeSequence.Count)
            {
                shouldBeDark = false;
                gameActive = true;

            }
            else
            {
                if (waitBetweenCounter < 0)
                {

                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                    buttonSounds[activeSequence[positionInSequence]].Play();
                    stayLitCounter = stayLit;

                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    public void StartGame()

    {
        if (gameStart == false)
        {
            activeSequence.Clear();

            positionInSequence = 0;
            inputInSequence = 0;
            colourSelect = Random.Range(0, colours.Length);

            activeSequence.Add(colourSelect);
            colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
            buttonSounds[activeSequence[positionInSequence]].Play();
            stayLitCounter = stayLit;


            shouldBeLit = true;

            scoreText.text = "Score: 0 - High Score: " + PlayerPrefs.GetInt("HiScore");
            gameStart = true;

            currectStatus.text = "Try your best! Good luck!";
        }
        
    


}

    public void ColourPress(int whichbutton)
    {
        if(gameActive)
        { 

          if (activeSequence[inputInSequence] == whichbutton)
        {
            Debug.Log("Correct");
                inputInSequence++;
                

                if (inputInSequence >= activeSequence.Count)

                {
                    if (activeSequence.Count > PlayerPrefs.GetInt("HiScore"))
                        {
                        PlayerPrefs.SetInt("HiScore",activeSequence.Count);
                    }
                    //activeSequence.Count 目前分數
                    scoreText.text = "Score: " + activeSequence.Count +" - High Score: " + PlayerPrefs.GetInt("HiScore"); 

                    positionInSequence = 0;
                    inputInSequence = 0;
                    colourSelect = Random.Range(0, colours.Length);

                    activeSequence.Add(colourSelect);
                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                    buttonSounds[activeSequence[positionInSequence]].Play();
                    stayLitCounter = stayLit;

                    shouldBeLit = true;

                    gameActive = false;

                    correct.Play();

                    if (activeSequence.Count == 1)

                    {
                        currectStatus.text = "Good Job, Keep Going";

                    } else if (activeSequence.Count == 2)

                    {
                        currectStatus.text = "Well Done";

                    } else if  (activeSequence.Count == 3)

                    {
                        currectStatus.text = "Excellent";

                    } else if (activeSequence.Count == 4)

                    {
                        currectStatus.text = "Outstanding";
                    }

                    else if (activeSequence.Count == 4)

                    {
                        currectStatus.text = "Keep going. You got this.";
                    }

                    else if (activeSequence.Count == 5)

                    {
                        currectStatus.text = "You are awesome! Keep going!";
                    }



                }
        }
        else 
        {
                Debug.Log("Wrong");
                incorrect.Play();
                gameActive = false;
                gameStart = false;

                currectStatus.text = "Game Over, " + "Shake Phone To Restart";



            }

        }
    }
}
