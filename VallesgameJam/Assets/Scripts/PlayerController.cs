using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum StatePlayer {READY, FLYING, OnOVNI, DEATH};

    public float horizontalDrag = 2.0f;
    public float addedHorizontalForce = 10.0f;

    public StatePlayer statePlayer;

    UfoController ovniAttacked;

    Rigidbody2D rigidbody2d;

    float raycastDistance = 0.5f;

    void Start () {
        statePlayer = StatePlayer.FLYING;
        rigidbody2d = this.GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        CheckCollisions();

        switch (statePlayer)
        {
            case StatePlayer.FLYING:
                Move();
                CheckBorders();
                
                break;
            case StatePlayer.OnOVNI:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                    rigidbody2d.AddForce(ovniAttacked.transform.up * 1000);
                    statePlayer = StatePlayer.FLYING;
                    transform.parent = null;
                    transform.localScale = Vector3.one;
                    ovniAttacked.Hit();
                    ovniAttacked = null;
                }
                break;
        }
    }

    private void CheckBorders()
    {
        if (transform.position.x > 8f)
        {
            SetXPosition(8f);
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
        else if (transform.position.x < -8f)
        {
            SetXPosition(-8f);
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Right");
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(addedHorizontalForce, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Left");
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-addedHorizontalForce, 0));
        }
        else if (rigidbody2d.velocity.x != 0)
        {
            Debug.Log(rigidbody2d.velocity.x);

            if (rigidbody2d.velocity.x > -0.1f && rigidbody2d.velocity.x < 0.1f)
            {
                Debug.Log("Correction");
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
            else
            {
                if (rigidbody2d.velocity.x < 0)
                    this.rigidbody2d.AddForce(new Vector2(horizontalDrag, 0));
                else
                    this.rigidbody2d.AddForce(new Vector2(-horizontalDrag, 0));
            }
        }
    }

    private void CheckCollisions()
    {
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.green);
        RaycastHit2D hit;
        if (rigidbody2d.velocity.y < 0 && statePlayer == StatePlayer.FLYING)
        {
            hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDistance);

            if (hit.collider != null && hit.rigidbody != this.rigidbody2d)
            {
                OnOvni(hit.collider.gameObject.GetComponent<UfoController>());
            }
        }
    }

    private void OnOvni(UfoController ufo)
    {
        ovniAttacked = ufo;
        ovniAttacked.UnderPlayer();
        transform.rotation = Quaternion.identity;
        transform.parent = ovniAttacked.transform;
        statePlayer = StatePlayer.OnOVNI;
        rigidbody2d.bodyType = RigidbodyType2D.Static;
    }
    
    

    private void SetXPosition(float xPosition)
    {
        Vector3 position = transform.position;
        position.x = xPosition;
        transform.position = position;
    }
}
