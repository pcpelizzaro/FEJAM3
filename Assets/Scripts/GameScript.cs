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
    public UpgradeMenuScript upgradeMenuScript;
    
    public int maxHealth = 5;
    public int healthUpgrade1 = 10;
    public int healthUpgrade2 = 15;
    public int healthUpgrade3 = 20;
    public int healthLevel = 0;
    private int[] healthLevels;

    public float maxAngle = 5;
    public float angleUpgrade1 = 15;
    public float angleUpgrade2 = 30;
    public float angleUpgrade3 = 45;
    public int angleLevel = 0;
    private float[] angleLevels;



    public enum GameState
    {
        Playing,
        Upgrading
    }


    private GameState gameState = GameState.Playing;
    private bool isPlayerStarting = true;
    private bool isPlayerGrounded = true;
    private Vector2 startingPos;
    private int doces = 0;
    private int health;



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
                reset();
            }
            else
            {
                isBleeding = true;
                particleScript.switchParticles(true);
                bleedTimer = 0;
            }
        } }

    public Vector2 StartingPos { get => startingPos; set => startingPos = value; }
    public GameState GameState1 { get => gameState; set => gameState = value; }

    void Start()
    {
        particleScript = GameObject.FindGameObjectWithTag("CandyController").GetComponent<ParticleScript>();
        healthLevels = new int[] { maxHealth, healthUpgrade1, healthUpgrade2, healthUpgrade3 };
        angleLevels = new float[] {maxAngle, angleUpgrade1, angleUpgrade2, angleUpgrade3 };

        Health = getMaxHealth();

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
    public void reset()
    {
        player.GetComponent<Rigidbody2D>().transform.position = startingPos;
        //playerBody.transform.position = gameState.StartingPos;
        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        player.GetComponent<PlayerScript>().setVisible(false);
        clearGameObjects();
        //isPlayerStarting = true;
        Health = healthLevels[healthLevel];
        upgradeMenuScript.OpenMenu();
        gameState = GameState.Upgrading;
    }
    void addObstacles(GameObject[] obstacles)
    {
        this.obstacles.AddRange(obstacles);
    }
    public int getMaxHealth()
    {
        return healthLevels[healthLevel];
    }
    public float getMaxAngle()
    {
        return angleLevels[angleLevel];
    }
}
