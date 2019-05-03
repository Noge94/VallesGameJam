using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum StatePlayer {READY, FLYING, OnOVNI, DEATH};

    public float horizontalDrag = 2.0f;
    public float addedHorizontalForce = 10.0f;

    public StatePlayer statePlayer;

    GameObject ovniAttacked;

    Rigidbody2D rigidbody2d;

    float raycastDistance = 0.5f;

    void Start () {
        statePlayer = StatePlayer.FLYING;
        rigidbody2d = this.GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {

        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.green);
        RaycastHit2D hit;
        if (rigidbody2d.velocity.y < 0 && statePlayer == StatePlayer.FLYING)
        {
            hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDistance);

            if (hit.collider != null && hit.rigidbody != this.rigidbody2d)
            {
                //        ovniAttacked = collision.gameObject;
                statePlayer = StatePlayer.OnOVNI;
                rigidbody2d.bodyType = RigidbodyType2D.Static;
            }
        }

        

        switch (statePlayer)
        {
            case StatePlayer.FLYING:
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Debug.Log("Right");
                    this.GetComponent<Rigidbody2D>().AddForce(new Vector2(addedHorizontalForce, 0));
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Debug.Log("Left");
                    this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-addedHorizontalForce, 0));

                }
                else if (rigidbody2d.velocity.x != 0)
                {

                    Debug.Log(rigidbody2d.velocity.x);

                    if (rigidbody2d.velocity.x < 0)
                        this.rigidbody2d.AddForce(new Vector2(horizontalDrag, 0));
                    else
                        this.rigidbody2d.AddForce(new Vector2(-horizontalDrag, 0));

                    if (rigidbody2d.velocity.x > -0.5f && rigidbody2d.velocity.x < 0.5f)
                    {
                        Debug.Log("Correction");
                        rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                    }

                }
                else
                {
                    Debug.Log("Still");
                }
                break;
            case StatePlayer.OnOVNI:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                    rigidbody2d.AddForce(Vector2.up * 1000);
                    statePlayer = StatePlayer.FLYING;
                    Destroy(ovniAttacked);
                }
                break;
        }
    }
}
