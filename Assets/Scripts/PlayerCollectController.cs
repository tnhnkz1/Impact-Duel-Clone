using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectController : MonoBehaviour
{
    [SerializeField] private GameObject __collectable;

    [SerializeField] private Transform __collectPoint;
    [SerializeField] private Transform __player;

    public float yAxis;

    public List<GameObject> collectables = new List<GameObject>();

    private bool _hasTakenCollectable;

    public static PlayerCollectController instance;

    private void Start()
    {
        instance = this;    
    }

    private void Update()
    {
        var collectablePointIndex = 3;
        
        if (_hasTakenCollectable)
        {
            GameObject newCollectable = Instantiate(__collectable, new Vector3(transform.GetChild(collectablePointIndex).position.x,
            yAxis, transform.GetChild(collectablePointIndex).position.z),
            transform.GetChild(collectablePointIndex).rotation);

            newCollectable.transform.SetParent(transform.GetChild(3).transform);

            collectables.Add(newCollectable);

            if (collectablePointIndex < transform.GetChild(3).childCount - 1)
            {
                collectablePointIndex++;
            }
            else
            {
                collectablePointIndex = 0;
                yAxis += 0.05f;
            }

            _hasTakenCollectable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            _hasTakenCollectable = true;

            Destroy(other.gameObject);
        }
    }
}
