using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D body;
    [SerializeField]
    FixedJoystick fixedJoystick;

    Vector2 moveVector;
    [SerializeField]
    private float moveSpeed;

    private float MAX_HP = 100;
    private float currentHp = 100;

    [SerializeField]
    private Image imgHp;

    [SerializeField]
    private GameObject bullet;

    private float bulletDuration = 0.5f;
    private float bulletTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fixedJoystick.JoystickPoinerDown)
        {
            if (bulletTimer >= bulletDuration)
            {
                bulletTimer = 0;
                Attack();
            }
            bulletTimer += Time.deltaTime;
        }
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
            body.AddForce(fixedJoystick.Direction * moveSpeed);
        }
    }

    private void HandleRotation()
    {
        float hAxis = fixedJoystick.Horizontal;
        float vAxis = fixedJoystick.Vertical;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, -zAxis);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy_bullet"))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHp -= damage;
        imgHp.fillAmount = currentHp / MAX_HP;
        if (currentHp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        // Restart game
    }

    private void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BulletController>().Setup(GameObject.FindGameObjectWithTag("enemy").transform);
    }
}
