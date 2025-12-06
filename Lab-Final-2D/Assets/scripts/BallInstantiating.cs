using UnityEngine;

public class BallInstantiating : MonoBehaviour
{
    public GameObject ball;
    public Transform spawnPoint1;
    //public Transform spawnPoint2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnBalls", 2f, 3f);
        //Instantiate(ball, spawnPoint1.position, spawnPoint1.rotation);
       // Instantiate(ball, spawnPoint2.position, spawnPoint2.rotation);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnBalls()
    {
        Instantiate(ball, spawnPoint1.position, spawnPoint1.rotation);
        Debug.Log("Delta time is: " + Time.deltaTime);
    }
}
