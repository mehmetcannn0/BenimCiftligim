using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("týklandý");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Animal animal = hit.collider.GetComponent<Animal>();
                if (animal != null)
                {
                    Debug.Log("týklandý animal");

                    // Hayvan durum deðiþikliði
                    animal.ChangeState(Animal.State.Walk);
                }

                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null)
                {
                    Debug.Log("týklandý plant");

                    // Bitki büyütme
                    if (plant.growthStage ==0)
                    {
                        plant.PlantSeed();
                    }
                     else if (plant.growthStage ==1) 
                    {
                    plant.Grow();

                    }
                    else if(plant.growthStage ==2) 
                    {
                        plant.Harvest();
                    }
                }

                 
                    Field field = hit.collider.GetComponent<Field>();
                    if (field != null)
                    {
                    Debug.Log("týklandý field");

                    gameManager.PlantSeedAtField(field);
                    }
                 


            }

            Debug.Log("týklandý ground");
            gameManager.SelectSeed(0);

        }
    }


}
