using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMovement2 : MonoBehaviour
{

    private bool wallHit;
    private Rigidbody2D rb2d;
    private Animator anim;

    public float wallHitWidth;
    public float wallHitHeight;
    public float speed;

    //ground check
    public Transform wallHitBox;
    public float checkRadius;
    public LayerMask isGround;
    public int numberOfMushrooms;
    public GameObject Mushroom;





    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        numberOfMushrooms = 1;
    }

    void Awake()
    {
    }

    // Update is called once per frame
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
            if (collision.contacts[0].point.y < transform.position.y)
            {
                Debug.Log("Mushroom Box Hit");
                numberOfMushrooms = numberOfMushrooms - 1;
                {
                    if (numberOfMushrooms == 0)
                        Instantiate(Mushroom, new Vector3(10, 4, 0), transform.rotation);
                        Destroy(gameObject);
                }
            }



        }
    }

}
       
        
    
