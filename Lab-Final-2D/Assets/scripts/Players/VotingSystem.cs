using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class VotingSystem : MonoBehaviour
{
    public UnityEngine.UI.Image iconAImage;
    public UnityEngine.UI.Image iconDImage;
    public UnityEngine.UI.Image iconGImage;
    public UnityEngine.UI.Image iconJImage;
    public UnityEngine.UI.Image iconLImage;

    public Sprite iconA;
    public Sprite iconD;
    public Sprite iconG;
    public Sprite iconJ;
    public Sprite iconL;

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

    public float voteTimer = 10f;

    public GameObject leaderboardPanel;
    public TMP_Text leaderboardText;

    public float leaderboardShowTime = 3f;

    KeyCode lastVoter;
    public static bool roundJustPlayed = false;

    bool ballotSwitch = false;
    public Animator BAnim;
    public float BAnimTime = 1.2f;

    public MapMan mapManager;
    public GameObject mapUI;    
    public float mapShowTime = 2f;

    void Start()
    {
         votingPanel.SetActive(false);
        leaderboardPanel.SetActive(false);

        voteTimer = 10f;

        if (roundJustPlayed && PlayerJoin.players.Count >= 3)
        {
            StartVoting();
            roundJustPlayed = false; 
        }
    }

    void Update()
    {
        if (ballotSwitch) return;
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

           UpdateVoteSideIcons(voterKey);
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
                StartCoroutine(VoteTransitionRoutine(targetKey));
                return; 
            }
        }
    }

    IEnumerator VoteTransitionRoutine(KeyCode targetKey)
    {
        ballotSwitch = true;
        isVoting = false; 

        AddVote(targetKey);

        if (BAnim != null)
            BAnim.SetTrigger("Flip");

        yield return new WaitForSeconds(BAnimTime);

        turn++;
        voteTimer = 10f;

        ballotSwitch = false;
        isVoting = true;
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

    int KeyToPlayerIndex(KeyCode key)
    {
        if (key == KeyCode.A) return 0;
        if (key == KeyCode.D) return 1;
        if (key == KeyCode.G) return 2;
        if (key == KeyCode.J) return 3;
        if (key == KeyCode.L) return 4;

        return -1;
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
    public void StartVoting()
    {
        turn = 0;
        voteTimer = 10f;
        lastVoter = KeyCode.None;

        voteA = voteD = voteG = voteJ = voteL = 0;

        votereslt.text = "";

       // BuildVoteSideIcons();

        votingPanel.SetActive(true);
        isVoting = true;

        RegisterMapPlayers();

    }

    void RegisterMapPlayers()
    {

        foreach (KeyCode key in PlayerJoin.players)
        {
            int index = KeyToPlayerIndex(key);

            if (index != -1)
            {
                mapManager.RegisterPlayer(index);
            }
        }
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

        StartCoroutine(EndRoundRoutine(winner));

    }

    IEnumerator EndRoundRoutine(KeyCode winner)
    {
        Debug.Log("END ROUND ROUTINE STARTED"); 

        isVoting = false;

        votereslt.text = "Winner: " + winner;
        yield return new WaitForSeconds(2f);

        votingPanel.SetActive(false);
        votereslt.text = "";


        Debug.Log("TURNING MAP UI ON");


        mapUI.SetActive(true);
        Debug.Log("MAP UI ACTIVE: " + mapUI.activeSelf);

        //  winner key to player index
        int winnerIndex = KeyToPlayerIndex(winner);

        // move winner up the map
        mapManager.AdvancePlayer(winnerIndex);

        yield return new WaitForSeconds(mapShowTime);

        mapUI.SetActive(false);


        ResetVotes();
        turn = 0;

        if (roundNumber % 5 == 0)
        {
            ShowLeaderboard();
        }
    }


    void UpdateVoteSideIcons(KeyCode voter)
    {
        UpdateSingleIcon(iconAImage, KeyCode.A, voter);
        UpdateSingleIcon(iconDImage, KeyCode.D, voter);
        UpdateSingleIcon(iconGImage, KeyCode.G, voter);
        UpdateSingleIcon(iconJImage, KeyCode.J, voter);
        UpdateSingleIcon(iconLImage, KeyCode.L, voter);
    }

    void UpdateSingleIcon(UnityEngine.UI.Image img, KeyCode key, KeyCode voter)
    {
        if (img == null) return;

        img.gameObject.SetActive(true); 

        if (!PlayerJoin.players.Contains(key))
        {
            img.color = new Color(1f, 1f, 1f, 0.3f); 
            return;
        }

        if (key == voter)
        {
            img.color = Color.gray;
        }
        else
        {
            img.color = Color.white;
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

 

}
