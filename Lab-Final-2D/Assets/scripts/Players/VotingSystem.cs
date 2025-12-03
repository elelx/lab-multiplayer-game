using UnityEngine;
using System.Collections.Generic;
using TMPro;



public class VotingSystem : MonoBehaviour
{

    int voteA = 0;
    int voteD = 0;
    int voteG = 0;
    int voteJ = 0;
    int voteL = 0;

    int scoreA = 0;
    int scoreD = 0;
    int scoreG = 0;
    int scoreJ = 0;
    int scoreL = 0;


    int turn = 0;
    int roundNumber = 0;

    public GameObject votingPanel;
    public TMP_Text whoisvotin;

    public TMP_Text votereslt;
    public TMP_Text timerText;


    public float voteTimer = 10f;



    void Start()
    {
       // votingPanel.SetActive(false);

        if (PlayerJoin.players.Count >= 3)
        {
            StartVoting();
        }
    }

    void Update()
    {
        if (PlayerJoin.players.Count < 3) return;

        if (turn >= PlayerJoin.players.Count)
        {
            FinishVoting();
            return;
        }

        whoisvotin.text = PlayerJoin.players[turn] + " is voting...";


        KeyCode voterKey = PlayerJoin.players[turn];

        voteTimer -= Time.deltaTime;
        timerText.text = Mathf.Ceil(voteTimer).ToString();

        if (voteTimer <= 0f)
        {
            votereslt.text = "time's up!";
            turn+=1;
            voteTimer = 10f;
            return;
        }


        foreach (KeyCode targetKey in PlayerJoin.players)
        {
            if (targetKey == voterKey) continue; // can't vote yourself

            if (Input.GetKeyDown(targetKey))
            {
                AddVote(targetKey);

                turn+=1; //next voterrr

                return; 
            }
        }
    }

    void AddVote(KeyCode who)
    {
        if (who == KeyCode.A) voteA+=1;
        if (who == KeyCode.D) voteD+=1;
        if (who == KeyCode.G) voteG+=1;
        if (who == KeyCode.J) voteJ+=1;
        if (who == KeyCode.L) voteL+=1;

        votereslt.text = "voted for " + who;

    }

    void FinishVoting()
    {
        int highest = voteA;
        KeyCode winner = KeyCode.A;

        if (voteD > highest)
        {
            highest = voteD;
            winner = KeyCode.D;
        }
        if (voteG > highest)
        {
            highest = voteG;
            winner = KeyCode.G;
        }
        if (voteJ > highest)
        {
            highest = voteJ;
            winner = KeyCode.J;
        }
        if (voteL > highest)
        {
            highest = voteL;
            winner = KeyCode.L;
        }

        Debug.Log(" Winner of round: " + winner);

        if (winner == KeyCode.A) scoreA+=1;
        if (winner == KeyCode.D) scoreD+=1;
        if (winner == KeyCode.G) scoreG+=1;
        if (winner == KeyCode.J) scoreJ+=1;
        if (winner == KeyCode.L) scoreL+=1;

        roundNumber++;

        // every 5 rounds
        if (roundNumber % 5 == 0) //the % reminder after dvidiesion if the number dvieded by 5 leaves no left over, then show the leader board
        {
            ShowLeaderboard();
        }

        if (roundNumber >= 15)
        {
            FinalWinner();

        }

        ResetVotes();

        turn = 0;

        votereslt.text = "";

        votingPanel.SetActive(false);

    }

    void ResetVotes()
    {
        voteA = voteD = voteG = voteJ = voteL = 0;

    }
    void ShowLeaderboard()
    {
        Debug.Log("A " + scoreA);
        Debug.Log("D" + scoreD);
        Debug.Log("G " + scoreG);
        Debug.Log("J " + scoreJ);
        Debug.Log("L " + scoreL);

        KeyCode leader = KeyCode.A;
        int best = scoreA;

        if (scoreD > best)
        {
            best = scoreD;
            leader = KeyCode.D;
        }
        if (scoreG > best)
        {
            best = scoreG;
            leader = KeyCode.G;
        }
        if (scoreJ > best)
        {
            best = scoreJ;
            leader = KeyCode.J;
        }
        if (scoreL > best)
        {
            best = scoreL;
            leader = KeyCode.L;
        }

        Debug.Log(" Leader: " + leader + " with " + best + " points");
    }


    void FinalWinner()
    {
        KeyCode winner = KeyCode.A;
        int best = scoreA;

        if (scoreD > best)
        {
            best = scoreD;
            winner = KeyCode.D;
        }
        if (scoreG > best)
        {
            best = scoreG;
            winner = KeyCode.G;
        }
        if (scoreJ > best)
        {
            best = scoreJ;
            winner = KeyCode.J;
        }
        if (scoreL > best)
        {
            best = scoreL;
            winner = KeyCode.L;
        }

        Debug.Log("FINAL WINNER IS: " + winner);
    }

    public void StartVoting()
    {
        turn = 0;
        votereslt.text = "";

        votingPanel.SetActive(true);

    }



}
