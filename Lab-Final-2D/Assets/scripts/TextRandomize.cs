using UnityEngine;
using TMPro;

public class TextRandomize : MonoBehaviour
{
    public string[] myChallenges;

    public TextMeshProUGUI textMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayRandomCallenge();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayRandomCallenge()
    {
        if (myChallenges !=null && myChallenges.Length > 0)
        {
            int randomIndex = Random.Range(0, myChallenges.Length);

            textMeshPro.text = myChallenges[randomIndex];
        }
        else
        {
            Debug.Log("U NEED TO ADD QUESTIONS TO ARRAY");
        }
    }


}
