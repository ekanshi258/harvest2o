using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [System.Serializable]
    public class BoundaryLimits
    {
        public float yMaximum, yMinimum;
        [HideInInspector] public float xMinimum, xMaximum;
    }

    public BoundaryLimits boundaryLimits;
    public float velocity;
    public Animator textWarning;

    private GameManager gameManager;
    private Vector3 position;
    private bool inRange = false;
    private static int touch;
    private float maxTime = 1f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        float halfHeight = (boundaryLimits.yMaximum - boundaryLimits.yMinimum) / 2.0f;
        boundaryLimits.xMaximum = (halfHeight * Screen.width / Screen.height) - 0.5f;
        boundaryLimits.xMinimum = -boundaryLimits.xMaximum;
    }

    private void Update()
    {
        this.transform.position = new Vector2(
                                                Mathf.Clamp(this.transform.position.x, boundaryLimits.xMinimum, boundaryLimits.xMaximum),
                                                this.transform.position.y
                                              );
    }

    private void FixedUpdate()
    {
        /*if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            float moveRight = Input.GetAxis("Horizontal");
            this.transform.Translate(Vector3.right * moveRight * velocity);
        }
        else
        {*/
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            position = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0f));
            if ((position - this.transform.position).magnitude > 2.5f)
            {
                float moveRight = 0f;

                if (position.x > this.transform.position.x)
                {
                    moveRight = 1f;
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (position.x < this.transform.position.x)
                {
                    moveRight = -1f;
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }

                this.transform.Translate(Vector3.right * moveRight * velocity);
            }
        }*/
        if(Input.acceleration.x >= 0.1)
            this.GetComponent<SpriteRenderer>().flipX = false;
        else
            this.GetComponent<SpriteRenderer>().flipX = true;

        this.transform.Translate(Input.acceleration.x / 2.5f, 0f, 0f);

        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    public void FlushWater()
    {
        if (inRange)
        {
            gameManager.goodWaterPoints += gameManager.goodWater;
            gameManager.Status();
            gameManager.waterPoints = 0;
            gameManager.goodWater = 0;
        }
        else
        {
            textWarning.SetTrigger("Warning");
        }
    }
}
