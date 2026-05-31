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


    private ParticleScript particleScript;
    private InputAction movement;
    private InputAction launchAction;
    private InputAction resetAction;
    private InputAction particleAction;
    public float angleIncrement = 30f;
    private float prevAngle = 0;
    private float angle = 0;
    private bool isStarting = true;
    private bool switchedParticles = false; 
    void Start()
    {
        //playerBody.linearVelocityX = xVelocity;
        gameState.StartingPos = playerBody.transform.position;
        particleScript = GameObject.FindGameObjectWithTag("CandyController").GetComponent<ParticleScript>();

        movement = InputSystem.actions.FindAction("Move");
        launchAction = InputSystem.actions.FindAction("Jump");
        resetAction = InputSystem.actions.FindAction("Reset");
        particleAction = InputSystem.actions.FindAction("Attack");
        playerBody.freezeRotation = true;
        isStarting = true;
        setVisible(false);
        //PlayerBody.linearVelocity = Quaternion.AngleAxis(initialAngle, Vector3.forward) * PlayerBody.linearVelocity;
    }
    public void setVisible(bool state)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = state;
    }
    public void launch(Vector2 velocity)
    {
        playerBody.linearVelocity = velocity;
        gameState.IsPlayerStarting = false;
        gameState.IsPlayerGrounded = false;
        setVisible(true);
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
        //Tecla para cima
        if (move.y > 0.8f && !gameState.IsPlayerGrounded)
        {
            angle = angleIncrement;

        }
        //Tecla para baixo
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
            //launch(new Vector2(xVelocity, yVelocity));
            //playerBody.linearVelocity = new Vector2(xVelocity, yVelocity);
            //gameState.IsPlayerStarting = false;
            //gameState.IsPlayerGrounded = false;
            //setVisible(true);
        }

        if(!gameState.IsPlayerStarting && resetAction.IsPressed())
        {
            //playerBody.transform.position = gameState.StartingPos;
            //playerBody.linearVelocity = new Vector2(0, 0);
            //gameState.clearGameObjects();
            //gameState.IsPlayerStarting = true;
            gameState.reset(gameObject);
        }

        //Particle test
        //if (particleAction.IsPressed())
        //{
        //    particleScript.switchParticles(true);
        //}
        //else
        //{
        //    particleScript.switchParticles(false);
        //}


    }
}
