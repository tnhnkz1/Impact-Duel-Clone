using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Rigidbody __rigidbody;

    [SerializeField] private Animator animator;

    private Touch _touch;

    private Vector3 _touchUp;
    private Vector3 _touchDown;

    private bool _dragStarted;
    private bool _isMoving;

    private float movementSpeed;

    void Update()
    {
        float xPos = Mathf.Clamp(transform.position.x, -2.9f, 2.5f);
        float zPos = Mathf.Clamp(transform.position.z, -9.393f, -3.844f);

        transform.position = new Vector3(xPos, transform.position.y, zPos);

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
                _isMoving = true;

                _touchUp = _touch.position;
                _touchDown = _touch.position;

                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);

            }
        }

        if (_dragStarted)
        {
            movementSpeed = 0.5f;

            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

            Debug.Log(movementSpeed);

            if (_touch.phase == TouchPhase.Moved)
            {
                _touchDown = _touch.position;

                animator.SetBool("Run", true);

                
            }

            if (_touch.phase == TouchPhase.Ended)
            {
                _touchDown = _touch.position;

                _dragStarted = false;
                _isMoving = false;

                animator.SetBool("Idle", true);
                animator.SetBool("Run", false);
            }
            
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed * Time.deltaTime);
            
        }

        Quaternion CalculateRotation()
        {
            Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
            return temp;
        }

        Vector3 CalculateDirection()
        {
            Vector3 temp = (_touchDown - _touchUp).normalized;
            temp.z = temp.y;
            temp.y = 0;
            return temp;
        }
    }

}
