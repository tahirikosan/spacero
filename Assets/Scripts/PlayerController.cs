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
        HandleRotation();
    }

    private void HandleMovement()
    {

        if (fixedJoystick.JoystickPoinerDown)
        {
            body.AddForce(fixedJoystick.Direction * 10);
        }
    }

    private void HandleRotation()
    {
        float hAxis = fixedJoystick.Horizontal;
        float vAxis = fixedJoystick.Vertical;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, -zAxis);
    }
}
