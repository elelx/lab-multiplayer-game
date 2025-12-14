using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapMan : MonoBehaviour
{
    // All map nodes (Node_1 → Node_6)
    public Transform[] nodes;

    // Player icons in order (A, D, G, J, L)
    public Transform[] playerIcons;

    // How long the icon moves between nodes
    public float moveTime = 1f;

    // Delay before final scene loads
    public float finalDelay = 2f;

    // Tracks what node each player is on
    int[] playerNodeIndex;

    // Tracks which players actually joined
    bool[] playerJoined;

    // Stores finalists (max 2)
    List<int> finalists = new List<int>();


    void Awake()
    {
        // Initialize arrays based on player count
        playerNodeIndex = new int[playerIcons.Length];
        playerJoined = new bool[playerIcons.Length];

        // Hide all icons at start
        for (int i = 0; i < playerIcons.Length; i++)
        {
            playerIcons[i].gameObject.SetActive(false);
            playerNodeIndex[i] = 0;
        }
    }

    // Called by VotingSystem when a player joins
    public void RegisterPlayer(int playerIndex)
    {
        Debug.Log("Registered player index: " + playerIndex);

        // Mark player as joined
        playerJoined[playerIndex] = true;

        // Show their icon
        playerIcons[playerIndex].gameObject.SetActive(true);

        // Place icon at Node 1
        playerIcons[playerIndex].position = nodes[0].position;
    }

    // Called by VotingSystem when someone wins a round
    public void AdvancePlayer(int playerIndex)
    {
        // Ignore players who never joined
        if (!playerJoined[playerIndex])
            return;

        // Stop if already at final node
        if (playerNodeIndex[playerIndex] >= nodes.Length - 1)
            return;

        // Move player forward one node
        playerNodeIndex[playerIndex]++;

        // Animate movement
        StartCoroutine(
            MoveIcon(playerIndex, nodes[playerNodeIndex[playerIndex]].position)
        );
    }

    // Smoothly moves the icon to the next node
    IEnumerator MoveIcon(int playerIndex, Vector3 target)
    {
        Transform icon = playerIcons[playerIndex];
        Vector3 start = icon.position;

        float t = 0f;

        // Lerp over time
        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;
            icon.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        // Snap exactly to target
        icon.position = target;

        // Check if this player became a finalist
        CheckFinalist(playerIndex);
    }

    // Checks if player reached final node
    void CheckFinalist(int playerIndex)
    {
        // If player reached Node 6
        if (playerNodeIndex[playerIndex] == nodes.Length - 1)
        {
            // Avoid duplicates
            if (!finalists.Contains(playerIndex))
            {
                finalists.Add(playerIndex);
                Debug.Log("Player " + playerIndex + " is a finalist!");
            }

            // If we now have 2 finalists → endgame
            if (finalists.Count == 2)
            {
                StartCoroutine(FinalShowdownDelay());
            }
        }
    }

    // Waits, then loads final scene
    IEnumerator FinalShowdownDelay()
    {
        Debug.Log("FINAL SHOWDOWN INCOMING");

        // Small dramatic pause
        yield return new WaitForSeconds(finalDelay);

        // finalists[0] and finalists[1] are your two players

        Debug.Log("Finalists are: " + finalists[0] + " and " + finalists[1]);

        // Clear old data just in case
        FinalTwo.finalists.Clear();

        // Store the two finalists
        FinalTwo.finalists.Add(finalists[0]);
        FinalTwo.finalists.Add(finalists[1]);

        // Load final scene
        SceneManager.LoadScene("FinalScene");
    }
}
