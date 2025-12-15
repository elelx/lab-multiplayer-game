using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneUI : MonoBehaviour
{

    public GameObject playerA;
    public GameObject playerD;
    public GameObject playerG;
    public GameObject playerJ;
    public GameObject playerL;


    void Start()
    {
        playerA.SetActive(false);
        playerD.SetActive(false);
        playerG.SetActive(false);
        playerJ.SetActive(false);
        playerL.SetActive(false);

        switch (GameWinner.winnerIndex)
        {
            case 0: playerA.SetActive(true); break;
            case 1: playerD.SetActive(true); break;
            case 2: playerG.SetActive(true); break;
            case 3: playerJ.SetActive(true); break;
            case 4: playerL.SetActive(true); break;
        }
    }
}
