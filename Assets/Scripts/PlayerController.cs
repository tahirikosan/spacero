using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D body;
    [SerializeField]
    FixedJoystick fixedJoystick;

    Vector2 moveVector;
    private readonly int  MOVE_SPEED_MULTIPLIER = 40;
    private float moveSpeed = 40f;

    private static float MAX_HP = 500;
    private float currentHp = MAX_HP;

    [SerializeField]
    private Image imgHp;

    [SerializeField]
    private GameObject bullet;

    private float bulletDuration = 0.5f;
    private float bulletTimer = 0f;

    [SerializeField]
    private List<GameObject> tails;
    [SerializeField]
    private GameObject tail;

    [SerializeField]
    private Text txtLevel;
    private int level = 0;

    public int Level { get => level; set => level = value; }

    [SerializeField]
    private GameObject destroyEffect;

    [SerializeField]
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        var tailObj = Instantiate(tail, transform.position, Quaternion.identity);
        tailObj.GetComponent<TailController>().Setup(body, bullet);
        tails.Add(tailObj);

        txtLevel.text = "Level: " + level;
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
                TailAttack();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy_bullet"))
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
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            GameOver();
        }
    }

    private void GameOver()
    {
        scoreManager.SetHighScore();
        // Restart game
        SceneManager.LoadScene(0);
    }

    private void Attack()
    {
        var enemy = GameObject.FindGameObjectWithTag("enemy");
        if (enemy != null)
        {
            Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BulletController>().Setup(enemy.transform);
        }
    }

    private void TailAttack()
    {
        foreach (GameObject tailObj in tails)
        {
            tailObj.GetComponent<TailController>().Attack();
        }
    }


    public void UpdateLevel()
    {
        var prevTail = tails[level];
        var tailObj = Instantiate(tail, prevTail.transform.position, Quaternion.identity);
        tailObj.GetComponent<TailController>().Setup(prevTail.GetComponent<Rigidbody2D>(), bullet);
        tails.Add(tailObj);

        level++;
        moveSpeed = level * MOVE_SPEED_MULTIPLIER;
        txtLevel.text = "Level: " + level;

        currentHp = MAX_HP;
        imgHp.fillAmount = currentHp / MAX_HP;
    }
}
