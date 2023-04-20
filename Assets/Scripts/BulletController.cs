using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    Rigidbody2D body;
    private Transform target;
    [SerializeField]
    float speed = 200;

    public void Setup(Transform target)
    {
        this.target = target;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (target != null)
        {
            body.AddForce((target.position - transform.position) * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
