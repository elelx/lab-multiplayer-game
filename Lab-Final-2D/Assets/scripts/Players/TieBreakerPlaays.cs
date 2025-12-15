using UnityEngine;

public class TieBreakerPlaays : MonoBehaviour
{

    public int playerIndex;
    public KeyCode jumpKey;

    Rigidbody2D rb;
    bool alive = true;

    public float jumpForce = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(int index, KeyCode key)
    {
        playerIndex = index;
        
        jumpKey = key;
    }

    //game play here ( this ties to the minigame manager to check what key needs to berees)
    void Update()
    {
        if (!alive) return;

        if (Input.GetKeyDown(jumpKey))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Die();
        }
    }

    void Die()
    {
        alive = false;

        gameObject.SetActive(false);

        //this will checkw ho is alive and the win scene will load 
        MiniGameMan.Instance.PlayerDied(playerIndex);
    }
}
