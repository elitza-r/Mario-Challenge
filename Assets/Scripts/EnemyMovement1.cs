using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMovement1 : MonoBehaviour
{

    private bool wallHit;
    private Rigidbody2D rb2d;
    private Animator anim;

    public float wallHitWidth;
    public float wallHitHeight;

    //ground check
    public Transform wallHitBox;
    public float checkRadius;
    public LayerMask isGround;
    public int numberOfCoins;





    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        numberOfCoins = 5;
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void FixedUpdate()
    {

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);

        if (wallHit == true)
        {
      
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (collision.contacts[0].point.y < transform.position.y)
            {
                Debug.Log("Coin Box Hit");
                numberOfCoins = numberOfCoins - 1;
                {
                    if (numberOfCoins == 0)
                        Destroy(gameObject);
                }
            }



        }
    }

}
       
        
    
