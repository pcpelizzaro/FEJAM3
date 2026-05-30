using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float forceY = 100;
    public float forceX = 100;
    private GameObject player;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;
    private float prevXVelocity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        playerBody = player.GetComponent<Rigidbody2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        prevXVelocity = playerBody.linearVelocityX;

        if(gameObject.transform.position.x - player.transform.position.x > 30)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 collisionNormal = contact.normal;
        collisionNormal.y *=  -forceY;
        collisionNormal.x *= forceX;
        playerBody.linearVelocityX = prevXVelocity;
        playerBody.AddForce(collisionNormal);

    }
}