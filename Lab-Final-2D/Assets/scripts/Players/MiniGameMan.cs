using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MiniGameMan : MonoBehaviour
{
    public static MiniGameMan Instance;

    public TieBreakerPlaays playerA;
    public TieBreakerPlaays playerD;
    public TieBreakerPlaays playerG;
    public TieBreakerPlaays playerJ;
    public TieBreakerPlaays playerL;

    List<int> alivePlayers = new List<int>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerA.gameObject.SetActive(false);
        playerD.gameObject.SetActive(false);
        playerG.gameObject.SetActive(false);
        playerJ.gameObject.SetActive(false);
        playerL.gameObject.SetActive(false);

        //  onlu tied players shows
        if (TieBreakers.tiedPlayers.Contains(0))
        {
            playerA.Setup(0, KeyCode.A);
            playerA.gameObject.SetActive(true);
            alivePlayers.Add(0);
        }

        if (TieBreakers.tiedPlayers.Contains(1))
        {
            playerD.Setup(1, KeyCode.D);

            playerD.gameObject.SetActive(true);
            alivePlayers.Add(1);
        }

        if (TieBreakers.tiedPlayers.Contains(2))
        {
            playerG.Setup(2, KeyCode.G);

            playerG.gameObject.SetActive(true);
            alivePlayers.Add(2);
        }

        if (TieBreakers.tiedPlayers.Contains(3))
        {
            playerJ.Setup(3, KeyCode.J);

            playerJ.gameObject.SetActive(true);
            alivePlayers.Add(3);
        }

        if (TieBreakers.tiedPlayers.Contains(4))
        {
            playerL.Setup(4, KeyCode.L);

            playerL.gameObject.SetActive(true);
            alivePlayers.Add(4);
        }
    }

    public void PlayerDied(int index)
    {
        alivePlayers.Remove(index);

        if (alivePlayers.Count == 1)
        {
            GameWinner.winnerIndex = alivePlayers[0];




            SceneManager.LoadScene("WinScene");
        }
    }
}


