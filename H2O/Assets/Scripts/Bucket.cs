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

    private GameManager gameManager;
    private IEnumerator coroutine;
    private Vector3 position;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        coroutine = FlushWater();
        
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(coroutine);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(coroutine);
        }
    }

    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            float moveRight = Input.GetAxis("Horizontal");
            this.transform.Translate(Vector3.right * moveRight * velocity);
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                position = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0f));
                float moveRight = 0f;

                if (position.x > this.transform.position.x)
                    moveRight = 1f;
                else if (position.x < this.transform.position.x)
                    moveRight = -1f;

                this.transform.Translate(Vector3.right * moveRight * velocity);
            }
        }
    }

    private IEnumerator FlushWater()
    {
        while(true)
        {
            if (gameManager.waterPoints >= 10)
            {
                gameManager.waterPoints -= 10;
                gameManager.energyPoints += 5;
                yield return new WaitForSeconds(0.75f);
            }
            else
                break;
        }
    }
}
