using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAIMovementController : MonoBehaviour
{
    [SerializeField] private Animator __animator;

    [SerializeField] private Transform __bombForEnemy;
    [SerializeField] private Transform __bombForPlayer;

    public GameObject winPanel;

    public bool hasCollidedWithFallingObject;

    public static EnemyAIMovementController instance;

    private void Start()
    {
        __animator.SetBool("Idle", true);

        instance = this;
    }

    private void FixedUpdate()
    {
        if (EnemyAIBombDetection.instance.hasEntered && !hasCollidedWithFallingObject && EnemyAIBombDetection.instance.bombs.Count != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, EnemyAIBombDetection.instance.newBomb.gameObject.transform.position, 0.03f);

            transform.rotation = Quaternion.LookRotation(EnemyAIBombDetection.instance.newBomb.gameObject.transform.position);

            __animator.SetBool("Run", true);
            __animator.SetBool("Idle", false);
        }
        else
        {
            __animator.SetBool("Idle", true);
            __animator.SetBool("Run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fall") && !PlayerMovementController_2.instance.failPanel.activeSelf)
        {
            transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.DOMoveY(-5, 5f);

            __animator.SetBool("Run", false);
            __animator.SetBool("Idle", true);

            winPanel.SetActive(true);

            hasCollidedWithFallingObject = true;
        }
    }
}
