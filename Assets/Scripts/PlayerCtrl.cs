using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Header("Move Params")]
    [SerializeField] private float _jumpForce = 2f;
    private bool _isPlayerMove = false;
    private sbyte _moveDirection = 0;
    private float _wallPosX = 1.815f;
    new Vector3 movementOffset;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        movementOffset = new Vector3(_jumpForce, 0f, 0f);
    }

    void Update()
    {
    }
    private void FixedUpdate()
    {
        Move();
        CheckPos();
    }

    private void Move()
    {
        if (!_isPlayerMove)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < _wallPosX)
            {
                _rb.isKinematic = false;
                _isPlayerMove = true;
                _moveDirection = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -_wallPosX)
            {
                _rb.isKinematic = false;
                _isPlayerMove = true;
                _moveDirection = -1;
            }
            else
            {
                _moveDirection = 0;
            }
            _rb.AddForce(new Vector3(_jumpForce * _moveDirection, 0f, 0f));
        }
    }

    private void CheckPos()
    {
        if(transform.position.x > -_wallPosX || transform.position.x < _wallPosX)
        {
            _rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall")
        {
            Debug.Log("Collision");
            _rb.isKinematic = true;
            _isPlayerMove = false;
            if(transform.position.x > 0)
            {
                transform.SetPositionAndRotation(new Vector3(_wallPosX, 0), Quaternion.identity);
            }
            else
            {
                transform.SetPositionAndRotation(new Vector3(-_wallPosX, 0), Quaternion.identity);
            }
        }
    }
}
