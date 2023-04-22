using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    Rigidbody2D body;
    Transform playerTransform;
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float speed = 5;

    private float bulletDuration = 0.5f;
    private float bulletTimer = 0;

    private float MAX_HP = 100;
    private float currentHp = 100;

    [SerializeField]
    private GameObject healtBar;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        scoreManager = GameObject.FindGameObjectWithTag("score_manager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletTimer >= bulletDuration)
        {
            bulletTimer = 0;
            Shoot();
        }
        bulletTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        body.AddForce((playerTransform.position - transform.position) * speed);
    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("player_bullet"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }



    private void TakeDamage(int damage)
    {
        currentHp -= damage;
        healtBar.transform.localScale = new Vector3(currentHp / MAX_HP, 1);
        if (currentHp == 0)
        {
            Destroy(this.gameObject);
            scoreManager.UpdateScore(10);
        }
    }
}
