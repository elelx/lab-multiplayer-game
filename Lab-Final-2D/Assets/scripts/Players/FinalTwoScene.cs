using UnityEngine;

public class FinalTwoScene : MonoBehaviour
{
    void Start()
    {
        int player1 = FinalTwo.finalists[0];
        int player2 = FinalTwo.finalists[1];

        Debug.Log("Final players are: " + player1 + " and " + player2);

    }
}
