using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplodeController : MonoBehaviour
{
    [SerializeField] private Light bombLight;
    [SerializeField] private Transform __fallingObject;

    private float timer;

    private float minTime = 0.1f;
    private float maxTime;

    private float currentTime;

    private float startingTime = 12;

    public bool hasExploded;

    public static BombExplodeController instance;

    private void Start()
    {
        instance = this;
        
        timer = Random.Range(minTime, maxTime);

        currentTime = startingTime;

        transform.SetParent(ObjectSpawner.instance.transform);
    }

    private void Update()
    {
        Explode();

        currentTime -= 1 *Time.deltaTime;

        if (currentTime < 0)
        {
            Instantiate(__fallingObject);

            __fallingObject.position = new Vector3(transform.position.x, 1.047f, transform.position.z);

            Destroy(gameObject);
        }
        else if (currentTime < 1)
        {
            hasExploded = true;
        }

        if (currentTime > 7)
        {
            maxTime = 1;
        }
        else if (currentTime < 7)
        {
            maxTime = 0.2f;
        }
        else if (currentTime <= 3)
        {
            maxTime = 0.001f;
        }
    }

    private void Explode()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            bombLight.enabled = !bombLight.enabled;
            timer = Random.Range(minTime,maxTime);
        }
    }
}
