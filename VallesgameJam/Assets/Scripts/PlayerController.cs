using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public enum StatePlayer {READY, FLYING, OnOVNI, DEATH};
    
    [SerializeField] private GameObject gameOverAnimation;
    [SerializeField]
    ArbolContoller arbol;

    private float addedHorizontalForce = 30.0f;
    private StatePlayer statePlayer;
    private float raycastDistance = 0.5f;
    private UfoController ovniAttacked;
    private Rigidbody2D rigidbody2d;
    public float oxigen = 100.0f;
    public BubbleAttach bubbleAttach;

    public static PlayerController Instance;

    float initialScale;

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

        initialScale = this.bubbleAttach.playerBubble.GetComponent<BubbleController>().BaseScale;
    }
    
    void Start () {
        statePlayer = StatePlayer.READY;
        GetComponent<Animator>().SetBool("OnOvni", true);
        rigidbody2d = this.GetComponent<Rigidbody2D>();
	}

    
    void Update()
    {
        CheckCollisions();
        UpdateOxigen();

        switch (statePlayer)
        {
            case StatePlayer.READY:

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                    arbol.Play();
                }

                break;
            case StatePlayer.FLYING:
                Move();
                CheckBorders();
                
                break;
            case StatePlayer.OnOVNI:
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("<color=green>Pressed Space</color>");
                    
                    GetComponent<Animator>().SetTrigger("Attack");
                    StartCoroutine(HitOvni());
                }
                break;
        }
    }

    private IEnumerator HitOvni()
    {
        yield return new WaitForSeconds(0.276f);
        if (ovniAttacked != null)
        {
            ovniAttacked.Hit();
        }
    }

    public void Jump()
    {
        GetComponent<Animator>().SetBool("OnOvni", false);
        Debug.Log("Jumping");
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        Vector3 direction = Vector3.up* 1500;
        if (ovniAttacked != null)
        {
            direction = ovniAttacked.transform.up* 1500;
        }
        rigidbody2d.AddForce(direction);
        statePlayer = StatePlayer.FLYING;
        transform.parent = null;
//        transform.localScale = Vector3.one;
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
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Horizontal")*addedHorizontalForce, 0));
    
         if (rigidbody2d.velocity.x != 0)
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
//        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.green);
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

    public void BubbleTouched()
    {

        bubbleAttach.playerBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().color = 
            new Color(111f/255f, 235f/255f, 240f/255f, 195f/255f);
        oxigen = 100.0f;
        bubbleAttach.bubbleTouched = false;
    }
    
    private void UpdateOxigen()
    {
        if (statePlayer == StatePlayer.DEATH)
        {
            return;
        }
            
        BubbleDanger();
        OxygenBar.instance.UpdateHealth(oxigen);
        
        oxigen -= 4f*Time.deltaTime;
        
        if(oxigen < 1.0f)
        {
            this.bubbleAttach.playerBubble.SetActive(false);
            oxigen = 0.0f;
            Die();
        }
        else if(this.bubbleAttach.playerBubble.activeSelf && oxigen > 50.0f) {
            // Debug.Log(initialScale + initialScale * (oxigen - 100.0f)/100.0f);
            this.bubbleAttach.playerBubble.GetComponent<BubbleController>().BaseScale = initialScale + initialScale * (oxigen - 100.0f) / 100.0f;
            
        }
    }

    private void BubbleDanger() {
        
        if (oxigen <= 40.0f) {
            Color color = bubbleAttach.playerBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            if (color == new Color(111, 235, 240, 195) || color == new Color(1f, 1f, 0f, 1f))
                bubbleAttach.playerBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 0.0f);
            else
                bubbleAttach.playerBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
        }
    }

    private void OnOvni(UfoController ufo)
    {
        GetComponent<Animator>().SetBool("OnOvni", true);
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
        oxigen -= 10f;
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
        OxygenBar.instance.UpdateHealth(0);
        PlayerPrefs.SetInt("LastScore", ScoreDisplayer.Instance.GetScore());
        if (ScoreDisplayer.Instance.GetScore() > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", ScoreDisplayer.Instance.GetScore());
        }
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
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void LeavingCameraViewPort()
    {
        Die();
    }
}
