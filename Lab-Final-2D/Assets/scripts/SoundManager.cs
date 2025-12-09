using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Static instance to ensure only one exists

    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // If not, set this instance as the static instance
            Instance = this;
            // Prevent this GameObject from being destroyed on scene load
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy the new duplicate
            Destroy(gameObject);
        }


    }
    //private void Update()
    //{
    //    if (SceneManager.GetActiveScene().name == "End")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
