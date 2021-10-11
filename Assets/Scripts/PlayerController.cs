using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float OX = 0, OY = 0, OZ = 0;
    private bool isTouchingGround = false;
    private bool isNearGround = false;
    private bool jumpPressed = false;
    private float maxDistance = 0.5f;
    private float maxPlayerVelocity = 10f;
    private Plane planeGround;

    public float speed = 30f;
    public float jumpPower = 150f;

    Plane GetPlaneFromGameObject(string objectName)
    {
        var filter = GameObject.Find(objectName).GetComponent<MeshFilter>();
        Vector3 normal = new Vector3();

        if (filter && filter.mesh.normals.Length > 0)
            normal = filter.transform.TransformDirection(filter.mesh.normals[0]);

        return new Plane(normal, transform.position);
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        planeGround = GetPlaneFromGameObject("Ground");
    }

    void Update()
    {
        OX = Input.GetAxis("Vertical");
        OZ = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetButton("Jump");

        if (jumpPressed)
        {
            OX /= 2;
            OY /= 2;
        }

        isNearGround = !isTouchingGround && planeGround.GetDistanceToPoint(transform.position) < maxDistance;
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rigidBody.velocity[0]) >= maxPlayerVelocity) { OX = 0; }
        if (Mathf.Abs(rigidBody.velocity[2]) >= maxPlayerVelocity) { OZ = 0; }
        
        if (OX != 0 || OZ != 0) { rigidBody.AddForce(new Vector3(OX, OY, OZ) * speed); }

        if(jumpPressed)
        {
            if(isTouchingGround)
            {
                jumpPressed = false;
                rigidBody.AddForce(new Vector3(1, jumpPower, 1));
            }

            if(isNearGround)
            {
                rigidBody.AddForce(new Vector3(1, jumpPower / 10f, 1));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            isTouchingGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isTouchingGround = false;
        }
    }
}
