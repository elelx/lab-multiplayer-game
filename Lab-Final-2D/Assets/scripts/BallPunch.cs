using UnityEngine;

public class BallPunch : MonoBehaviour
{
    public float punchPower = 30;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.left * punchPower);

    }
}
