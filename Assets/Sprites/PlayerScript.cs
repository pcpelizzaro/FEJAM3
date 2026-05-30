using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D playerBody;
    public float xVelocity;
    public float yVelocity;
    public GameScript gameState;

    private InputAction movement;
    private InputAction launchAction;
    private InputAction resetAction;
    public float angleIncrement = 30f;
    private float prevAngle = 0;
    private float angle = 0;
    private bool isStarting = true;
    void Start()
    {
        //playerBody.linearVelocityX = xVelocity;
        gameState.StartingPos = playerBody.transform.position;
        movement = InputSystem.actions.FindAction("Move");
        launchAction = InputSystem.actions.FindAction("Jump");
        resetAction = InputSystem.actions.FindAction("Reset");
        playerBody.freezeRotation = true;
        isStarting = true;
        //PlayerBody.linearVelocity = Quaternion.AngleAxis(initialAngle, Vector3.forward) * PlayerBody.linearVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Angle : {angle}");
        if(angle != prevAngle)
        {
            Debug.Log("Angle changed");
            playerBody.linearVelocity = Quaternion.AngleAxis(angle -prevAngle, Vector3.forward) * playerBody.linearVelocity;
            playerBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            prevAngle = angle; 
        }
        Vector2 move = movement.ReadValue<Vector2>();
        Debug.Log($"Move {move}");
        if (move.y > 0.8f && !gameState.IsPlayerGrounded)
        {
            angle = angleIncrement;
            Debug.Log("Up is pressed");

        }
        else if (move.y < -.2f && !gameState.IsPlayerGrounded)
        {
            angle = -angleIncrement;
        } else
        {
            angle = 0f;
        }
            
        //Lançamento
        Debug.Log($"isStarting: {isStarting}");
        if (launchAction.IsPressed() && gameState.IsPlayerStarting == true)
        {
            playerBody.linearVelocity = new Vector2(xVelocity, yVelocity);
            gameState.IsPlayerStarting = false;
            gameState.IsPlayerGrounded = false;
        }

        if(!gameState.IsPlayerStarting && resetAction.IsPressed())
        {
            //playerBody.transform.position = gameState.StartingPos;
            //playerBody.linearVelocity = new Vector2(0, 0);
            //gameState.clearGameObjects();
            //gameState.IsPlayerStarting = true;
            gameState.reset(gameObject);
        }


    }
}
