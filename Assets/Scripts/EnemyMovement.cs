using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{

    private bool wallHit;
    private Rigidbody2D rb2d;
    private bool facingRight = true;
    public bool hasDied;
    private Animator anim;

    public float speed;
    public float jumpforce;
    public float wallHitWidth;
    public float wallHitHeight;

    public Transform wallHitBox;
    public float checkRadius;
    public LayerMask isGround;

    public AudioClip DeathClip;
    public AudioClip DeathClipGoomba;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hasDied = false;
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void FixedUpdate()
    {

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);

        if (wallHit == true)
        {
            speed = speed * -1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (collision.contacts[0].point.y > transform.position.y)
            { Debug.Log("Goomba dead");
                anim.SetBool("isDead", true);
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(DeathClipGoomba);
                Destroy(gameObject, 1);
                speed = speed * 0;
            } else
            {
                hasDied = true;
                Destroy(collision.gameObject, 1);
            }
            if (hasDied == true)
            {
                StartCoroutine("Die");
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(DeathClip);
            }
        }
    }

            IEnumerator Die()
            {
                SceneManager.LoadScene("Scene 1");
                yield return null;

            }
        }
        
    
