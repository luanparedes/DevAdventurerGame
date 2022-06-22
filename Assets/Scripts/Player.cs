using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool isJumping;
    private bool isBowAttacking;
    private float movement;

    private Rigidbody2D rig;
    private Animator anim;
    
    public Transform firePoint;
    public GameObject bow;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        speed = 5;
        jumpForce = 15;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        BowAttack();
    }

    public void Move()
    {
        movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if(movement > 0 && !isJumping && !isBowAttacking)
        {
            anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if(movement < 0 && !isJumping && !isBowAttacking)
        {
            anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isBowAttacking)
            anim.SetInteger("transition", 0);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
    }

    public void BowAttack()
    {
        StartCoroutine("BowFire");
    }

    IEnumerator BowFire()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isBowAttacking = true;
            GameObject bow1 = Instantiate(bow, firePoint.position, firePoint.rotation);
            anim.SetInteger("transition", 3);

            if(transform.rotation.y >= 0)
                bow1.GetComponent<Bow>().isRight = true;
            else if(transform.rotation.y < 0)
                bow1.GetComponent<Bow>().isRight = false;

            yield return new WaitForSeconds(0.5f);
            isBowAttacking = false;
            anim.SetInteger("transition", 0);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
            isJumping = false;
    }
}
