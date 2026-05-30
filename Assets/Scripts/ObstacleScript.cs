using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float forceY = 100;
    public float forceX = 100;
    public Rigidbody2D playerBody;
    public BoxCollider2D playerCollider;
    private float prevXVelocity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        prevXVelocity = playerBody.linearVelocityX;
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