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
    public float timer = 0f;
    public Slider sliderUI;
    public float valueee;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = phase1;
        growthStage = 1;
    }

    void Update()
    {
        valueee = (/*isWatered == 0 ? timer :*/ ((isWatered * plantIndex * 5) + timer)) / ((plantIndex + 1) * (plantIndex * 5));
        sliderUI.value = valueee;
        if (growthStage == 1)
        {
            if (timer >= plantIndex * 5f)
            {
                if (isWatered >= plantIndex)
                {
                    Grow();
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
        Emotion_2.SetActive(true);
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
        if (timer >= plantIndex * 5f)
        {
            isWatered++;
            timer = 0;
            Emotion_2.SetActive(false);
        }
    }
}
