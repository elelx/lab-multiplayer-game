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

    void Start()
    {
        letsPlayButton.SetActive(false);
    }

    void Update()
    {
        // if u toggle, u toggle in and out of selection, this is how the list gets the char AdGJL

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
            if (players.Contains(key))
            {
                players.Remove(key); //if already on list, remove duplicate

            }
            else
            {
                players.Add(key);

               
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
            A.SetActive(isActive);
        }
        if (key == KeyCode.D)
        {
            D.SetActive(isActive);
        }
        if (key == KeyCode.G)
        {
            G.SetActive(isActive);
        }
        if (key == KeyCode.J)
        {
            J.SetActive(isActive);
        }
        if (key == KeyCode.L)
        {
            L.SetActive(isActive);
        }
    }

   
}
