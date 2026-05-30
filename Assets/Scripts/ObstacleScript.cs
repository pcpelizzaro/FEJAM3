using Unity.VisualScripting;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float forceY = 100;
    public float forceX = 100;
    public float xMultiplier = 1.20f;
    private GameObject player;
    private Rigidbody2D playerBody;
    private BoxCollider2D playerCollider;
    private GameScript gameState;
    private float prevXVelocity;
    private bool wasHit = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        playerBody = player.GetComponent<Rigidbody2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        gameState = GameObject.FindWithTag("GameController").GetComponent<GameScript>();
        prevXVelocity = playerBody.linearVelocityX;

        if(player.transform.position.x - gameObject.transform.position.x > 30)
        {
            Destroy(gameObject);
        }
    }
    //Não mais usado: usamos trigger
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint2D contact = collision.GetContact(0);
        //Vector2 collisionNormal = contact.normal;
        //collisionNormal.y *=  -forceY;
        //collisionNormal.x *= forceX;
        //playerBody.linearVelocityX = prevXVelocity;
        //playerBody.AddForce(collisionNormal);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 closestPoint = collision.ClosestPoint(transform.position);

        // Calculate the direction vector from the contact point to the player
        Vector2 collisionNormal = ((Vector2)transform.position - closestPoint).normalized;

        //Bump vertical caso acerte na lateral:
        if(Mathf.Abs(collisionNormal.x)>0.9f && Mathf.Abs(collisionNormal.y) < 0.1f)
        {
            if (collisionNormal.y >= 0) collisionNormal.y = 0.5f;
            else collisionNormal.y = -0.5f;
        }
        // Directly set the linear velocity instead of adding force
        // collisionNormal.y determines the up/down direction of the impact (-1 or 1)
        float appliedBump = forceY;
        float xVelocity = prevXVelocity * xMultiplier;
        if (xVelocity < 0.2) xVelocity += 1;
        if (collisionNormal.y > 0) forceY = forceY / 2;
        playerBody.linearVelocity = new Vector2(
            xVelocity,
            Mathf.Sign(collisionNormal.y) * -forceY
        );

        if (!wasHit)
        {
            gameState.Doces += 1;
            gameState.Health -= 1;
            wasHit = true;

        }
    }
}