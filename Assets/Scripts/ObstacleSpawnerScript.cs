using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject obstacle1;
    public GameObject player;
    private float timePassed = 0;
    public float timeBetweenObstacles = 1;
    public float ySpread = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >timeBetweenObstacles)
        {
            float xPosition = player.transform.position.x + 20;
            float yOffset = Random.Range(-ySpread, ySpread);
            float yPosition = player.transform.position.y - yOffset;
            float zPosition = player.transform.position.z;
            Instantiate(obstacle1, new Vector3(xPosition, yPosition, zPosition), new Quaternion()); 
            timePassed = 0;
        }

        
    }
}
