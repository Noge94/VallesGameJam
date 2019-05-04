using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public enum StatePlayer {READY, FLYING, OnOVNI, DEATH};
    
    [SerializeField] private GameObject gameOverAnimation;

    private float addedHorizontalForce = 30.0f;
    private StatePlayer statePlayer;
    private float raycastDistance = 0.5f;
    private UfoController ovniAttacked;
    private Rigidbody2D rigidbody2d;
    public float oxigen = 100.0f;
    public BubbleAttach bubbleAttach;

    public static PlayerController Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;   
        }
        else
        {
            Destroy(this);
        }
    }
    
    void Start () {
        statePlayer = StatePlayer.FLYING;
        rigidbody2d = this.GetComponent<Rigidbody2D>();
	}

    
    void Update()
    {
        CheckCollisions();
        Bubble();

        switch (statePlayer)
        {
            case StatePlayer.FLYING:
                Move();
                CheckBorders();
                
                break;
            case StatePlayer.OnOVNI:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("<color=green>Pressed Space</color>");
                    ovniAttacked.Hit();
                }
                break;
        }
    }

    public void Jump()
    {
        Debug.Log("Jumping");
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2d.AddForce(ovniAttacked.transform.up * 1500);
        statePlayer = StatePlayer.FLYING;
        transform.parent = null;
        transform.localScale = Vector3.one;
        ovniAttacked = null;
    }

    private void CheckBorders()
    {
        if (transform.position.x > Configuration.SCREEN_LIMIT)
        {
            SetXPosition(Configuration.SCREEN_LIMIT);
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
        else if (transform.position.x < -Configuration.SCREEN_LIMIT)
        {
            SetXPosition(-Configuration.SCREEN_LIMIT);
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
            //Debug.Log(rigidbody2d.velocity.x);

            if (rigidbody2d.velocity.x > -0.1f && rigidbody2d.velocity.x < 0.1f)
            {
                Debug.Log("Correction");
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
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

            if (hit.collider != null && hit.rigidbody != this.rigidbody2d && hit.collider.tag == "OVNI")
            {
                OnOvni(hit.collider.gameObject.GetComponent<UfoController>());
            }
        }
    }

    private void Bubble() {
        if (this.bubbleAttach.bubbleTouched) {
            oxigen = 100.0f;
            bubbleAttach.bubbleTouched = false;
        }
        if(oxigen < 0)
        {
            Destroy(this.bubbleAttach.playerBubble);
            oxigen = 0;
        }
        else if(this.bubbleAttach.playerBubble != null) {
            oxigen -= 0.1f;
        }


    }

    private void OnOvni(UfoController ufo)
    {
        CameraController.Instance.SmoothMoveToY(transform.position.y+5f);
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
    
    public void Hit()
    {
        oxigen -= 20f;
        Debug.Log("<color=orange>Player hit</color>");

        if (oxigen < 0)
        {
            
            Debug.Log("<color=red>GAME OVER. Player has no healh.</color>");
            Die();
        }
        else
        {
            StartCoroutine(HitAnimation());
        }
    }

    private void Die()
    {
        if (statePlayer == StatePlayer.DEATH)
        {
            return;
        }
        statePlayer = StatePlayer.DEATH;
        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        Instantiate(gameOverAnimation);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator HitAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return null;
        yield return null;
        yield return null;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 100);
    }

    public void LeavingCameraViewPort()
    {
        Die();
    }
    
    
    
}
