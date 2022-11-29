using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed = 360;

    private PlayerControls _controls;
    private Rigidbody _rbody;
    private Vector3 _moveInput;

    private void Awake()
    {
        _controls = new PlayerControls();

        _rbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rbody.MovePosition(transform.position + (transform.forward * _moveInput.magnitude) * _speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        GatherInput();
        Look();
    }

    private void GatherInput()
    {
        Vector2 temp = _controls.Movement.Movement.ReadValue<Vector2>();
        if (temp != Vector2.zero)
            _moveInput = new Vector3(temp.x, 0, temp.y);
        else
            _moveInput = Vector2.zero;
    }

    private void Look()
    {
        if (_moveInput != Vector3.zero)
        {
            var relative = (transform.position + _moveInput.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative,Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}