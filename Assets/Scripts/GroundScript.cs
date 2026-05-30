using UnityEngine;

public class GroundScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public GameScript gameState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameState.IsPlayerGrounded = true;
        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        
    }
}
