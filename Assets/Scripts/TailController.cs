using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D connectedBody;
    private HingeJoint2D hingeJoint2D;

    private GameObject bullet;

    public void Setup(Rigidbody2D rigidbody2D, GameObject bullet)
    {
        connectedBody = rigidbody2D;
        this.bullet = bullet;
    }

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
        hingeJoint2D.connectedBody = connectedBody;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        var enemy = GameObject.FindGameObjectWithTag("enemy");
        if (enemy != null)
        {
            Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BulletController>().Setup(enemy.transform);
        }
    }
}
