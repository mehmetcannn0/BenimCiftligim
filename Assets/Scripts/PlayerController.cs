using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("t�kland�");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Animal animal = hit.collider.GetComponent<Animal>();
                if (animal != null)
                {
                    Debug.Log("t�kland� animal");

                    // Hayvan durum de�i�ikli�i
                    animal.ChangeState(Animal.State.Walk);
                }

                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null)
                {
                    Debug.Log("t�kland� plant");

                    // Bitki b�y�tme
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
                    Debug.Log("t�kland� field");

                    gameManager.PlantSeedAtField(field);
                    }
                 


            }

            Debug.Log("t�kland� ground");
            gameManager.SelectSeed(0);

        }
    }


}
