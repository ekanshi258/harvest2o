using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushWater : MonoBehaviour
{
    private IEnumerator coroutine;
    private GameManager gameManager;

    private void Start()
    {
        coroutine = ButtonPressed();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(1).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))
            {
                if(hitPoint.transform.position == this.gameObject.transform.position)
                {
                    StartCoroutine(coroutine);
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(1).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                if (hitPoint.transform.position == this.gameObject.transform.position)
                {
                    StartCoroutine(coroutine);
                }
            }
        }
    }

    private IEnumerator ButtonPressed()
    {
        while (true)
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
