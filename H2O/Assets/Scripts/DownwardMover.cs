﻿using UnityEngine;

public class DownwardMover : MonoBehaviour
{
    public float velocity;
    [HideInInspector] public bool falling = true;
    public AudioClip pureWaterAudio;
    public AudioClip pollutedWaterAudio;

    private GameManager gameManager;
    public AudioSource audioSource;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, this.velocity);

        audioSource = GameObject.FindGameObjectWithTag("Bucket").GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bucket")
        {
            if (!falling)
                return;

            gameManager.waterPoints += 10;

            if (this.gameObject.tag == "Polluted Water")
            {
                gameManager.healthPoints -= 20;
                gameManager.ChangeColour();
                audioSource.clip = pollutedWaterAudio;
                audioSource.Play();
            }
            else
            {
                gameManager.goodWater += 10;
                gameManager.goodWater = Mathf.Min(gameManager.goodWater, 100);
                audioSource.clip = pureWaterAudio;
                audioSource.Play();
            }

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        falling = false;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.08f, transform.position.z);
        GetComponent<Animator>().SetTrigger("End Game");
        Destroy(this.gameObject, 0.5f);
    }
}
