using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Player;
    public GameObject Camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.transform.position = new Vector3( Player.transform.position.x, Camera.transform.position.y, Camera.transform.position.z);

    }
}
