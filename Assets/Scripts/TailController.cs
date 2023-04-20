using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D connectedBody;
    private HingeJoint2D hingeJoint2D;

    public void Setup(Rigidbody2D rigidbody2D)
    {
        connectedBody = rigidbody2D;
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
}
