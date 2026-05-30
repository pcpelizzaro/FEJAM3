using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D PlayerBody;
    public float xVelocity;

    private InputAction movement;
    private InputAction downAction;
    public float initialAngle = 30f;
    private float prevAngle = 0;
    private float angle = 0;
    void Start()
    {
        PlayerBody.linearVelocityX = xVelocity;
        movement = InputSystem.actions.FindAction("Move");
        PlayerBody.freezeRotation = true;
        //PlayerBody.linearVelocity = Quaternion.AngleAxis(initialAngle, Vector3.forward) * PlayerBody.linearVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Angle : {angle}");
        if(angle != prevAngle)
        {
            Debug.Log("Angle changed");
            PlayerBody.linearVelocity = Quaternion.AngleAxis(initialAngle, Vector3.forward) * PlayerBody.linearVelocity;
            prevAngle = angle; 
        }
        Vector2 move = movement.ReadValue<Vector2>();
        Debug.Log($"Move {move}");
        if (move.y > 0.8f)
        {
            angle = 15f;
            Debug.Log("Up is pressed");

        }
        else if (move.y < -.2f)
        {
            angle = -15f;
        } else
        {
            angle = 0f;
        }
    }
}
