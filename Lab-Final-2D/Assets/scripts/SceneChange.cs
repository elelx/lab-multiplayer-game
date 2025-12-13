using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string SceneName)
    {
        Debug.Log("butt clicked AISDFHAS " + SceneName);
        SceneManager.LoadScene(SceneName);
    }

    public void SceneEnded()
    {
        VotingSystem.roundJustPlayed = true;
        SceneManager.LoadScene("CategorySplit");
    }
}
