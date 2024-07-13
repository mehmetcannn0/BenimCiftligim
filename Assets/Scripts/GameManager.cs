using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Oyun ba�latma kodlar� buraya
    }

    void Update()
    {
        // Oyun g�ncelleme kodlar� buraya
    }
}
