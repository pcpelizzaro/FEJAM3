using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI healthText;
    public GameObject player;
    public ParticleScript particleScript;
    private bool isPlayerStarting = true;
    private bool isPlayerGrounded = true;
    private Vector2 startingPos;
    private int doces = 0;
    private int health = 10;

    private float bleedTimer = 0;
    private float bleedTime = 2;
    private bool isBleeding = false;

    private List<GameObject> obstacles;
    public bool IsPlayerStarting { get => isPlayerStarting; set => isPlayerStarting = value; }
    public bool IsPlayerGrounded { get => isPlayerGrounded; set => isPlayerGrounded = value; }
    public int Doces { get { return doces; } set { doces = value;
            currencyText.text = $"Doces: {value}";
        } }

    public int Health { get { return health; }  
        set { 
            health = value;
            healthText.text = $"Vida: {value}";
            if(value <= 0)
            {
                reset(player);
            }
            else
            {
                isBleeding = true;
                particleScript.switchParticles(true);
                bleedTimer = 0;
            }
        } }

    public Vector2 StartingPos { get => startingPos; set => startingPos = value; }

    void Start()
    {
        particleScript = GameObject.FindGameObjectWithTag("CandyController").GetComponent<ParticleScript>();


    }

    // Update is called once per frame
    void Update()
    {
        bleedTimer += Time.deltaTime;
        if(bleedTimer > bleedTime && isBleeding)
        {
            bleedTimer = 0;
            particleScript.switchParticles(false);
            isBleeding = false;
        }
        
    }
    public void clearGameObjects()
    {
        Debug.Log("Cleared game objects");
        //foreach(GameObject obstacle in obstacles){
        //    Destroy(obstacle);
        //}
        //obstacles.Clear();
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Obstacle");

        // 2. Loop through the array and destroy each individual object
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
    public void addObstacles(GameObject obstacle)
    {
        obstacles.Add(obstacle);

    }
    //TODO: não precisamos que isso seja passado por parametro
    public void reset(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().transform.position = startingPos;
        //playerBody.transform.position = gameState.StartingPos;
        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        player.GetComponent<PlayerScript>().setVisible(false);
        clearGameObjects();
        isPlayerStarting = true;
        Health = 10;
    }
    void addObstacles(GameObject[] obstacles)
    {
        this.obstacles.AddRange(obstacles);
    }
}
