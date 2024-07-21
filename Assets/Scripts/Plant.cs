using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public int plantIndex;
    public Sprite harvested;
    public Sprite phase1;
    public Sprite phase2;
    private SpriteRenderer spriteRenderer;
    public int growthStage; // 0: harvested, 1: phase1, 2: phase2
    public int isWatered = 0;
    public GameObject Emotion_2;
    public GameObject Emotion_3;
    public float timer = 0f;
    public Slider sliderUI;
    public long timestamp = 0;
    private float waitingCoefficient = 60f;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = phase1;
        growthStage = 1;
        plantIndex += 1; 

        if (timestamp>1)
        {
            timer += Mathf.RoundToInt(CalculateTimeDifference());
        }
        InvokeRepeating("SaveTimestamp", 1.0f, 10.0f);


    }

    void Update()
    {
        sliderUI.value = (((isWatered * plantIndex * waitingCoefficient) + timer)) / ((plantIndex + 1) * (plantIndex * waitingCoefficient));
        if (growthStage == 1)
        {
           
            if (timer >= plantIndex * waitingCoefficient)
            {
                if (isWatered >= plantIndex)
                {
                    Grow();
                    ChangeToEmotion3();
                }
                else
                {
                    ChangeToEmotion2();
                }
            }
            else
            {
                
                timer += Time.deltaTime;
            }
        }
    }
 

    void ChangeToEmotion2()
    {
        if (!Emotion_2.activeSelf)
        { 
            Emotion_2.SetActive(true);
        }
    }
    void ChangeToEmotion3()
    {
        if (!Emotion_3.activeSelf)
        { 
            Emotion_3.SetActive(true);
        }
    }

    public void PlantSeed()
    {
        growthStage = 1;
        spriteRenderer.sprite = phase1;
        isWatered = 0;
    }

    public void Grow()
    {
        if (growthStage == 1 && isWatered >= plantIndex)
        { 
            spriteRenderer.sprite = phase2;
            growthStage = 2;
            //Emotion_2.SetActive(false);
        }
    }

    public void Harvest()
    {
        if (growthStage == 2)
        {
            timer = 0;

            spriteRenderer.sprite = harvested;
            growthStage = 0;
            isWatered = 0;
        }
    }

    public void WaterPlant()
    {
        if (timer >= plantIndex * waitingCoefficient)
        { 
            isWatered++;
            timer = 0;
            Emotion_2.SetActive(false);
        }
    }
    public void SaveTimestamp()
    {
        Debug.Log("SaveTimestamp");

        timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); 
    }
    public int CalculateTimeDifference()
    {
        Debug.Log("CalculateTimeDifference");
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        int timeDifference = (int)(currentTime - timestamp); 
        return (int)timeDifference;
    }
}
