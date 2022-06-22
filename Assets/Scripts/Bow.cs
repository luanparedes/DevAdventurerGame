using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private Rigidbody2D rig;
    private int speed;
    public bool isRight;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        speed = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRight)
            rig.velocity = Vector3.right * speed;
        else
            rig.velocity = Vector3.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
            Destroy(gameObject);
    }
}
