using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerMovementController_2 : MonoBehaviour
{
    [SerializeField] private Rigidbody __rigidbody;

    [SerializeField] private FixedJoystick __joystick;

    [SerializeField] private Animator __animator;

    [SerializeField] private Transform __handle;
    [SerializeField] private Transform __playerCircleEffect;
    [SerializeField] private Transform __collectPoint;

    [SerializeField] private GameObject __startPanel;

    [SerializeField] private float __movingSpeed;
    [SerializeField] private float __rotationSpeed;

    public GameObject failPanel;

    public static PlayerMovementController_2 instance;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (!failPanel.activeSelf)
        {
            float xPos = Mathf.Clamp(transform.position.x, -2.9f, 2.5f);
            float yPos = Mathf.Clamp(transform.position.y, 1.05f, 1.05f);
            float zPos = Mathf.Clamp(transform.position.z, -9.393f, -3.844f);

            transform.position = new Vector3(xPos, yPos, zPos);
        }
        
        if (__joystick.Horizontal != 0 || __joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(__rigidbody.velocity);
            __animator.SetBool("Run", true);
            __animator.SetBool("Idle", false);
        }
        else
        {
            __animator.SetBool("Run", false);
            __animator.SetBool("Idle", true);
        }

        if (__handle.localPosition.x <= -120 || __handle.localPosition.x >= 120 || __handle.localPosition.y <= -120 || __handle.localPosition.y >= 127)
        {
            __rigidbody.velocity = new Vector3(__joystick.Horizontal * 4, __rigidbody.velocity.y, __joystick.Vertical * 4);
        }
        else
        {
            __rigidbody.velocity = new Vector3(__joystick.Horizontal * __movingSpeed, __rigidbody.velocity.y, __joystick.Vertical * __movingSpeed);
        }
    }

    private void Update()
    {
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount > 0)
        {
            __startPanel.SetActive(false);
            Time.timeScale = 1;
            
            if (touch.phase == TouchPhase.Began)
            {
                __playerCircleEffect.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            
            if (touch.phase == TouchPhase.Ended)
            {
                __playerCircleEffect.localScale = new Vector3(0f, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fall") && __collectPoint.childCount == 0 && !EnemyAIMovementController.instance.winPanel.activeSelf)
        {
            __rigidbody.velocity = Vector3.zero;
            transform.DOMoveY(-1, 1f);

            //transform.position = Vector3.zero;

            failPanel.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Fall") && __collectPoint.childCount > 0)
        {
            Destroy(other.gameObject);

            Destroy(PlayerCollectController.instance.collectables[PlayerCollectController.instance.collectables.Count - 1]);

            PlayerCollectController.instance.collectables.RemoveAt(PlayerCollectController.instance.collectables.Count - 1);
        }
    }
}
