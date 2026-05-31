using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private GameObject player;

    private ParticleSystem[] particleSystems;
    private Transform[] transforms;
    private Quaternion initialRotation;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        transforms = GetComponentsInChildren<Transform>();
        initialRotation = transforms[0].rotation;
    }
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        //foreach(var transform in transforms)
        //{
        //    transform.rotation = initialRotation;
        //}

    }
    public void switchParticles(bool state, int number = 3)
    {
        foreach (ParticleSystem system in particleSystems)
        {
            var emission = system.emission;
            emission.enabled = state;
        }
    }
}
