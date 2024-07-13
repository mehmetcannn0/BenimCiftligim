using UnityEngine;

public class Plant : MonoBehaviour
{
    public Sprite harvested;
    public Sprite phase1;
    public Sprite phase2;
    private SpriteRenderer spriteRenderer;
    public int growthStage; // 0: harvested, 1: phase1, 2: phase2

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = harvested;
        growthStage = 0;
    }

    public void PlantSeed()
    {
        if (growthStage == 0)
        {
            spriteRenderer.sprite = phase1;
            growthStage = 1;
        }
    }

    public void Grow()
    {
        if (growthStage == 1)
        {
            spriteRenderer.sprite = phase2;
            growthStage = 2;
        }
    }

    public void Harvest()
    {
        if (growthStage == 2)
        {
            spriteRenderer.sprite = harvested;
            growthStage = 0;
        }
    }
}
