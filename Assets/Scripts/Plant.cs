using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int plantIndex;
    public Sprite harvested;
    public Sprite phase1;
    public Sprite phase2;
    private SpriteRenderer spriteRenderer;
    public int growthStage; // 0: harvested, 1: phase1, 2: phase2
    public bool isWatered = false;  
    public GameObject Emotion_2; 
    public float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = phase1;
        growthStage = 1;  
    }

    void Update()
    { 
        if (isWatered)
        {
            if (growthStage == 1)
            {
                if (timer >= 5f)
                {
                    Grow();
                }
                else
                {
                timer += Time.deltaTime;

                }
            }
        }
        else
        {
            if (growthStage == 1)
            { 
                if (timer >= 5f)
                {
                    ChangeToEmotion2();
                }
                else
                {
                timer += Time.deltaTime;

                }
            }
        }
    }

     

    void ChangeToEmotion2()
    { 
        Emotion_2.SetActive(true);
    }
     

    public void PlantSeed()
    {
        growthStage = 1;
        spriteRenderer.sprite = phase1;
        isWatered = false;  
    }

    public void Grow()
    {
        if (growthStage == 1 && isWatered)
        {
            timer = 0;
            spriteRenderer.sprite = phase2;
            growthStage = 2; 
            Emotion_2.SetActive(false);
           

        }
    }

    public void Harvest()
    {
        if (growthStage == 2)
        {
            spriteRenderer.sprite = harvested;
            growthStage = 0;
            isWatered = false;  
        }
    }

    public void WaterPlant()
    {
        if (timer>=5)
        {
            isWatered = true;
            timer = 0;
            Emotion_2.SetActive(false);
        }
    }
}
