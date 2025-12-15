using UnityEngine;
using TMPro;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public string LeveltoLoad;
    public float timer = 10f;
    float timeLeft;
    public GameObject TimerUI;

    public float HowmuchTimeLeftAlarmClock = 5f;

    [SerializeField] TextMeshProUGUI timerSeconds;
    public AudioSource timesr;
    public AudioClip timers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimerUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f2");

        if (timer <= HowmuchTimeLeftAlarmClock)
        {
            TimerUI.SetActive(true);

            timesr.PlayOneShot(timers);

        }
        if (timer <= 0)
        {
            VotingSystem.roundJustPlayed = true;
            TimerUI.SetActive(false);

            SceneManager.LoadScene(LeveltoLoad);
        }
    }
}
