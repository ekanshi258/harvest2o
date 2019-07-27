using UnityEngine;

public class DownwardMover : MonoBehaviour
{
    public float velocity;
    [HideInInspector] public bool falling = true;

    private GameManager gameManager;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, this.velocity);

        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!falling)
            return;

        if(this.gameObject.tag == "Pure Water")
        {
            gameManager.waterPoints += 10;
        }
        else if (this.gameObject.tag == "Polluted Water")
        {
            gameManager.waterPoints -= 2;
        }
        else
        {
            gameManager.waterPoints -= 5;
        }
       
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        falling = false;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.08f, transform.position.z);
        GetComponent<Animator>().SetTrigger("End Game");
        Destroy(this.gameObject, 0.5f);
    }
}
