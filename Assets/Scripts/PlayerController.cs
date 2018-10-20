using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private bool facingRight = true;

    public float speed;
    public float jumpforce;
    public AudioClip JumpClip;
    public AudioClip DeathClip;
    public AudioClip coins;
    public AudioClip WinClip;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    public Text countText;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private int numberOfCoins;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        numberOfCoins = 0;
        SetCountText();
}

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        Application.Quit();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);

        if (facingRight == true && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == false && moveHorizontal < 0)
        {
            Flip();
        }

        if (gameObject.transform.position.y < -7)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(DeathClip);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            numberOfCoins = numberOfCoins + 1;
            SetCountText();
            other.gameObject.SetActive(false);

            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(coins);
        }

        if (other.gameObject.CompareTag("Mushroom"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            other.gameObject.SetActive(false);
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(WinClip);
        }
    }

    void SetCountText()
    { countText.text = "Count: " + numberOfCoins.ToString(); }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb2d.velocity = Vector2.up * jumpforce;

                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(JumpClip);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "CoinBox")
        {
            if (collision.contacts[0].point.y > transform.position.y)
            {
                Debug.Log("Coin Box Hit");
                numberOfCoins = numberOfCoins + 1;
                SetCountText();
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(coins);
          
            }



        }
    }
}

