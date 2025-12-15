using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapMan : MonoBehaviour
{
    RectTransform[] spots;
    RectTransform[] playerIcons;


    public float moveTime = 1f;

    public float finalDelay = 2f;

    int[] playerSpots;

    bool[] playerJoined;


    public bool mapInitialized = false;

    public static MapMan Instance;

    List<int> finalists = new List<int>();

    public string scenename;

    bool graceActive = false;

    int firstFinalist = -1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        if (playerSpots == null)
        {
            playerSpots = new int[5];
            playerJoined = new bool[5];
        }

    }
public void RebindMapUI(GameObject mapUI)
{
    if (mapUI == null)
    {
        Debug.LogError("mapUI is NULL");
        return;
    }

    RectTransform[] allRects = mapUI.GetComponentsInChildren<RectTransform>(true);

    List<RectTransform> spotList = new List<RectTransform>();

    RectTransform iconA = null;
    RectTransform iconD = null;
    RectTransform iconG = null;
    RectTransform iconJ = null;
    RectTransform iconL = null;

    foreach (RectTransform rt in allRects)
    {
        // Spots
        if (rt.name == "0" || rt.name == "1" || rt.name == "2" ||
            rt.name == "3" || rt.name == "4" || rt.name == "5" || rt.name == "6")
        {
            spotList.Add(rt);
        }

        // Icons (CASE SENSITIVE)
        switch (rt.name)
        {
            case "A": iconA = rt; break;
            case "D": iconD = rt; break;
            case "G": iconG = rt; break;
            case "J": iconJ = rt; break;
            case "L": iconL = rt; break;
        }
    }

    spots = spotList.ToArray();
    playerIcons = new RectTransform[]
    {
        iconA,
        iconD,
        iconG,
        iconJ,
        iconL
    };

    Debug.Log($"Rebound Map UI | spots={spots.Length}, icons={playerIcons.Length}");

    foreach (var icon in playerIcons)
        if (icon != null)
            icon.gameObject.SetActive(false);
}




    public void RegisterPlayer(int playerIndex)
    {

        if (playerIcons == null || spots == null) return;


        if (playerJoined[playerIndex])
            return;

        Debug.Log("Registered player index: " + playerIndex);

        playerJoined[playerIndex] = true;

        playerIcons[playerIndex].gameObject.SetActive(true);

        playerSpots[playerIndex] = 0;
        playerIcons[playerIndex].anchoredPosition = spots[0].anchoredPosition;




    }

    public void RefreshIconPositions()
    {
        if (playerIcons == null || spots == null) return;


        for (int i = 0; i < playerIcons.Length; i++)
        {
            if (!playerJoined[i]) continue;

            playerIcons[i].gameObject.SetActive(true);
            playerIcons[i].anchoredPosition = spots[playerSpots[i]].anchoredPosition;
        }
    }


    public void AdvancePlayer(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= playerIcons.Length) return;

        if (playerIcons == null || spots == null) return;


        if (!playerJoined[playerIndex]) return;
        if (playerSpots[playerIndex] >= spots.Length - 1) return;

        int from = playerSpots[playerIndex];
        int to = from + 1;

        playerSpots[playerIndex] = to;

        StopAllCoroutines();
        StartCoroutine(MoveIconUI(playerIndex, from, to));
    }

    IEnumerator MoveIconUI(int playerIndex, int from, int to)
    {
        RectTransform icon = playerIcons[playerIndex];

        Vector2 start = spots[from].anchoredPosition;
        Vector2 target = spots[to].anchoredPosition;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;
            icon.anchoredPosition = Vector2.Lerp(start, target, t);
            yield return null;
        }

        icon.anchoredPosition = target;

        CheckFinalist(playerIndex);
    }


    void CheckFinalist(int playerIndex)
    {
        int finalSpot = spots.Length - 1;

        if (playerSpots[playerIndex] != finalSpot)
            return;

        if (!graceActive)
        {
            firstFinalist = playerIndex;
            finalists.Clear();
            finalists.Add(playerIndex);

            graceActive = true;
            Debug.Log("First finalist: " + playerIndex);
            return;
        }

        if (graceActive && !finalists.Contains(playerIndex))
        {
            finalists.Add(playerIndex);
            Debug.Log("Second finalist: " + playerIndex);

            TriggerTieBreaker();
        }
    }

    void TriggerTieBreaker()
    {
        TieBreakers.tiedPlayers.Clear();
        TieBreakers.tiedPlayers.Add(finalists[0]);
        TieBreakers.tiedPlayers.Add(finalists[1]);

        SceneManager.LoadScene("MiniGame");
    }


    public void EndRound()
    {
        if (!graceActive) return;

        if (finalists.Count == 1)
        {
            GameWinner.winnerIndex = firstFinalist;
            SceneManager.LoadScene("WinScene");
        }
    }


}
