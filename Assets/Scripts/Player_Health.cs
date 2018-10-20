using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour {

    public int health;
    public bool hasDied;

    public AudioClip DeathClip;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    void Start () {
        hasDied = false;
	}

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    void Update() {
        if (gameObject.transform.position.y < -9)
        {
            hasDied = true;
        }
        if (hasDied == true) {
            StartCoroutine("Die");
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(DeathClip);
        }
    }

    IEnumerator Die()  {
        SceneManager.LoadScene("Scene 1");
        yield return null;

    }
	}
