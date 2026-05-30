using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D PlayerBody;
    public float xVelocity;
    void Start()
    {
        PlayerBody.linearVelocityX = xVelocity;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
