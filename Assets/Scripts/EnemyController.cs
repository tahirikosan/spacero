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

    [SerializeField]
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player_bullet"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHp -= damage;
        healtBar.transform.localScale = new Vector3(currentHp / MAX_HP, 1);
        if (currentHp <= 0)
        {
            scoreManager.UpdateScore(10);
            Destroy(this.gameObject);
        }
    }
}
