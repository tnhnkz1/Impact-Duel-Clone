using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private TouchController _touchController;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float moveSpeed;

    private void Move(Vector3 direction)
    {
        _rigidbody.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    private void Rotate(Vector3 rotation)
    {
        transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
    }

    private void FixedUpdate()
    {
        var direction = new Vector3(_touchController.direction.x, 0, _touchController.direction.y);
        var rotation = new Vector3(_touchController.rotation.x, 0, _touchController.rotation.y);
        Move(direction);
        Rotate(rotation);
    }
}
