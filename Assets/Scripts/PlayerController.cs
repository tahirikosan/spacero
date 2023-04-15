using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D body;
    [SerializeField]
    FixedJoystick fixedJoystick;

    Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

        if (fixedJoystick.JoystickPoinerDown)
        {
            body.AddForce(fixedJoystick.Direction * 10);
        }
    }
}
