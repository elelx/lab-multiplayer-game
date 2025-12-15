using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerJoin : MonoBehaviour
{

    public static List<KeyCode> players = new List<KeyCode>(); //makes list -->  store letter data of plauyer who joined

    //new list = bran new empty list

    public GameObject A;


    public GameObject D;

    public GameObject G;

    public GameObject J;

    public GameObject L;

    public GameObject letsPlayButton;

    public static bool allowJoining = true;

    Animator animA, animD, animG, animJ, animL;

    public AudioSource fart;
    public AudioClip fartS;
    public AudioClip springS;

    void Awake()
    {
        animA = A.GetComponent<Animator>();
        animD = D.GetComponent<Animator>();
        animG = G.GetComponent<Animator>();
        animJ = J.GetComponent<Animator>();
        animL = L.GetComponent<Animator>();
    }


    void Start()
    {
        players.Clear();

        letsPlayButton.SetActive(false);
     
    }

    void Update()
    {
        // if u toggle, u toggle in and out of selection, this is how the list gets the char AdGJL
        if (!allowJoining) return;

        CheckJoin(KeyCode.A);
        CheckJoin(KeyCode.D);
        CheckJoin(KeyCode.G);
        CheckJoin(KeyCode.J);
        CheckJoin(KeyCode.L);

        //ChecktheKey = function to check if pressed
    }

    void CheckJoin(KeyCode key) //bascially --> CheckTheKey(KeyCode.A);
    {
        if (Input.GetKeyDown(key))
        {
            bool isJoining;

            if (players.Contains(key))
            {
                players.Remove(key); //if already on list, remove duplicate
                isJoining = false;
            }
            else
            {
                players.Add(key);
                isJoining = true;

            }

            if (isJoining)
            {
                fart.PlayOneShot(fartS);     
            }
            else
            {
                fart.PlayOneShot(springS);  
            }


            UpdateUI(key, players.Contains(key));

            CheckStartButton();

            Debug.Log("player " + string.Join(", ", players));
        }


    }

    void CheckStartButton()
    {

        if (players.Count >= 3)
        {
            letsPlayButton.SetActive(true);

        }
        else
        {
            letsPlayButton.SetActive(false);

        }
    }



    void UpdateUI(KeyCode key, bool isActive)
    {
        if (key == KeyCode.A)
        {
            animA.SetBool("isJoined", isActive);

        }
        if (key == KeyCode.D)
        {
            animD.SetBool("isJoined", isActive);
        }
        if (key == KeyCode.G)
        {
            animG.SetBool("isJoined", isActive);
        }
        if (key == KeyCode.J)
        {
            animJ.SetBool("isJoined", isActive);
        }
        if (key == KeyCode.L)
        {
            animL.SetBool("isJoined", isActive);
        }
    }

   
}
