using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;



public class VotingSystem : MonoBehaviour
{

    int voteA = 0;
    int voteD = 0;
    int voteG = 0;
    int voteJ = 0;
    int voteL = 0;

    public static int scoreA = 0;
    public static int scoreD = 0;
    public static int scoreG = 0;
    public static int scoreJ = 0;
    public static int scoreL = 0;



    int turn = 0;
    public static int roundNumber = 0;

    public GameObject votingPanel;
    public TMP_Text whoisvotin;

    public TMP_Text votereslt;
    public TMP_Text timerText;

    bool isVoting = false;

    public TMP_Text voterText;

    public UnityEngine.UI.Image voterImage;

    public Sprite iconA;
    public Sprite iconD;
    public Sprite iconG;
    public Sprite iconJ;
    public Sprite iconL;

    public Transform voteOptionsParent;
    public GameObject voteIconPrefab; 


    public float voteTimer = 10f;

    public GameObject leaderboardPanel;
    public TMP_Text leaderboardText;

    public float leaderboardShowTime = 3f;

    KeyCode lastVoter;


    void Start()
    {
         votingPanel.SetActive(false);
        leaderboardPanel.SetActive(false);

        voteTimer = 10f;
        if (PlayerJoin.players.Count >= 3)
        {
            StartVoting();
        }
    }

    void Update()
    {
        if (!isVoting) return;
        if (PlayerJoin.players.Count < 3) return;

        if (turn >= PlayerJoin.players.Count)
        {
            FinishVoting();
            return;
        }

        KeyCode voterKey = PlayerJoin.players[turn];

        if (voterKey != lastVoter)
        {
            voterText.text = "Player " + voterKey + " is voting";
            voterImage.sprite = GetIcon(voterKey);

            ShowVoteOptions(voterKey);
            lastVoter = voterKey;
        }



        voteTimer -= Time.deltaTime;
        timerText.text = Mathf.Ceil(voteTimer).ToString();

        if (voteTimer <= 0f)
        {
            votereslt.text = "time's up!";
            turn++;
            voteTimer = 10f;
            votereslt.text = ""; 
            return;
        }


        foreach (KeyCode targetKey in PlayerJoin.players)
        {
            if (targetKey == voterKey) continue; // can't vote yourself

            if (Input.GetKeyDown(targetKey))
            {
                AddVote(targetKey);

                turn+=1; //next voterrr
                voteTimer = 10f;

                return; 
            }
        }
    }

    Sprite GetIcon(KeyCode key)
    {
        if (key == KeyCode.A) return iconA;
        if (key == KeyCode.D) return iconD;
        if (key == KeyCode.G) return iconG;
        if (key == KeyCode.J) return iconJ;
        if (key == KeyCode.L) return iconL;
        return null;
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

        ResetVotes();
        turn = 0;

        votereslt.text = "";
        isVoting = false;
        votingPanel.SetActive(false);

        if (roundNumber >= 15)
        {
            FinalWinner();
            return;
        }

        if (roundNumber % 5 == 0)
        {
            ShowLeaderboard(); 
        }
        else
        {
            StartVoting(); 
        }


    }

    void ResetVotes()
    {
        voteA = voteD = voteG = voteJ = voteL = 0;

    }

    void ShowLeaderboard()
    {
        StartCoroutine(LeaderboardRoutine());
    }

    IEnumerator LeaderboardRoutine()
    {
        isVoting = false; 

        leaderboardPanel.SetActive(true);

        leaderboardText.text =
            "LEADERBOARD\n\n" +
            "A: " + scoreA + "\n" +
            "D: " + scoreD + "\n" +
            "G: " + scoreG + "\n" +
            "J: " + scoreJ + "\n" +
            "L: " + scoreL;

        yield return new WaitForSeconds(leaderboardShowTime);

        leaderboardPanel.SetActive(false);

        if (PlayerJoin.players.Count >= 3)
        {
            StartVoting();
        }
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
        voteTimer = 10f;
        lastVoter = KeyCode.None; 

        voteA = voteD = voteG = voteJ = voteL = 0;

        votereslt.text = "";

        votingPanel.SetActive(true);
        isVoting = true;
    
}

    void ShowVoteOptions(KeyCode voter)
    {
        foreach (Transform c in voteOptionsParent)
            Destroy(c.gameObject);

        foreach (KeyCode p in PlayerJoin.players)
        {
            GameObject icon = Instantiate(voteIconPrefab, voteOptionsParent);

            icon.GetComponentInChildren<TMP_Text>().text = p.ToString();
            icon.GetComponent<UnityEngine.UI.Image>().sprite = GetIcon(p);

            if (p == voter)
                icon.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
        }

    }


}
