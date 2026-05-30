using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;
    public GameObject obstacle5;

    private GameObject[] obstacles;


    public GameObject player;
    public GameScript gameState;
    public int nObstaclesRange = 5;
    private float timePassed = 0;
    public float timeBetweenObstacles = 1;
    public float ySpread = 5;
    void Start()
    {
        obstacles = new GameObject[] { obstacle1, obstacle2, obstacle3, obstacle4, obstacle5 };

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if((timePassed > timeBetweenObstacles) && !gameState.IsPlayerStarting)
        {
            float xPosition = player.transform.position.x + 20;
            int nObstacles = Mathf.RoundToInt(Random.Range(1, nObstaclesRange));

            HashSet<int> uniqueNumbers = new HashSet<int>();

            // Keep looping until the set reaches the desired size
            while (uniqueNumbers.Count < nObstacles)
            {
                int newNumber = Mathf.RoundToInt(Random.Range(-ySpread, ySpread)); // Numbers from 1 to 10
                uniqueNumbers.Add(newNumber); // HashSet won't add it if it already exists
            }
            foreach(int yOffset in uniqueNumbers)
            {
                int obstacleId = Mathf.RoundToInt(Random.Range(0, 5));
                //float yOffset = Random.Range(-ySpread, ySpread);
                float yPosition = player.transform.position.y - yOffset;
                //Abortar se o obstáculo for ficar baixo emais
                if (yPosition < -3) continue;
                float zPosition = player.transform.position.z;
                GameObject obstacle = Instantiate<GameObject>(obstacles[obstacleId], new Vector3(xPosition, yPosition, zPosition), new Quaternion());
                //gameState.addObstacles(obstacle);

            }
            timePassed = 0;
        }

        
    }
}
