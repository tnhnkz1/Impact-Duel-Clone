using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform __target;
    
    [SerializeField] private float __chaseSpeed;

    private Vector3 _offset = new Vector3(0, 2, -3.6f);

    void Start()
    {
        if (!__target)
        {
            __target = GameObject.FindObjectOfType<PlayerMovementController>().transform;
        }
    }

    private void LateUpdate()
    {
        if (!PlayerMovementController_2.instance.failPanel.activeSelf)
        {
            transform.position = Vector3.Lerp(transform.position, __target.position + _offset, __chaseSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position;
        }
    }
}
