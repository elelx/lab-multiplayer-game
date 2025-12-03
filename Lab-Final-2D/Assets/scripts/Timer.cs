using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public string LeveltoLoad;
    public float timer = 10f;
    float timeLeft;
    [SerializeField] TextMeshProUGUI timerSeconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f2");
        if (timer <= 0)
        {
            SceneManager.LoadScene(LeveltoLoad);
        }
    }
}
