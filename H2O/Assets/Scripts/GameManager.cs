using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pureWater;
    public GameObject pollutedWater;
    public float startTime;
    public float repeatRate;
    public float ySpawnCoordinate;
    public int waterPoints = 0;
    public int healthPoints = 50;
    public Slider waterPointsSlider;
    public Slider energyPointsSlider;
    public Slider goodWaterPointsSlider;
    public float energyTime;
    public Bucket player;
    public Animator gameOverAnimation;
    public int goodWaterPoints;
    public int goodWater;
    public Animator person;
    public Animator water;
    public Sprite[] backgrounds;
    public SpriteRenderer backgroundImage;
    public AudioClip gameOverAudio;
    public TextMeshProUGUI savedNumber;
    public Image fillColour;
    public Color yellow;
    public Color red;

    private float timer;
    private GameObject spawnedObjects;
    private float minimumXSpawnCoordinate, maximumXSpawnCoordinate;
    private int backgroundNumber = 1;

    private void Start()
    {
        Input.multiTouchEnabled = true;

        spawnedObjects = new GameObject("Enemies Parent");
        
        InvokeRepeating("SpawnWater", startTime, repeatRate);
        //InvokeRepeating("Spawn PowerUps", powerUpsStartTime, Random.Range(powerUpsSpawnRepeat - 5, powerUpsSpawnRepeat + 5));

        float halfHeight = (ySpawnCoordinate + ySpawnCoordinate) / 2.0f;
        maximumXSpawnCoordinate = (halfHeight * Screen.width / Screen.height) - 0.5f;
        minimumXSpawnCoordinate = -maximumXSpawnCoordinate;
    }

    private void Update()
    {
        waterPointsSlider.value = waterPoints;
        energyPointsSlider.value = healthPoints;
        goodWaterPointsSlider.value = goodWaterPoints;

        waterPoints = Mathf.Min(waterPoints, 100);
        healthPoints = Mathf.Min(healthPoints, 100);
        goodWater = Mathf.Min(goodWater, 100);

        if(waterPoints < 0)
        {
            waterPoints = 0;
        }
        if (healthPoints <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Bucket"), 0.5f);
            this.GetComponent<AudioSource>().clip = gameOverAudio;
            this.GetComponent<AudioSource>().loop = false;
            savedNumber.text = "You helped " + backgroundNumber.ToString() + " people with water.";
            gameOverAnimation.SetTrigger("Game Over");
        }

        timer += Time.deltaTime;

        //player.velocity = (healthPoints * 0.0008f) + 0.02f;
        
        if(timer > 5f)
        {
            timer = 0f;
            repeatRate -= 0.1f;
        }
        Debug.Log(repeatRate);
        Mathf.Clamp(repeatRate, 1.5f, 0.5f);

    }

    private void SpawnWater()
    {
        float i = Random.Range(0, 10);

        if (i < 6)
            InstantiatePureWater();
        else
            InstantiatePollutedWater();
    }

    private void InstantiatePureWater()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minimumXSpawnCoordinate, maximumXSpawnCoordinate), ySpawnCoordinate, 0f);
        Instantiate(pureWater, spawnPosition, Quaternion.identity, spawnedObjects.transform);
    }

    private void InstantiatePollutedWater()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minimumXSpawnCoordinate, maximumXSpawnCoordinate), ySpawnCoordinate, 0f);
        Instantiate(pollutedWater, spawnPosition, Quaternion.identity, spawnedObjects.transform);
    }

    public void Status()
    {
        if (goodWaterPoints >= goodWaterPointsSlider.maxValue)
        {
            goodWaterPoints = 0;
            goodWater = 0;
            backgroundImage.sprite = backgrounds[backgroundNumber];
            backgroundNumber++;
            person.SetTrigger("Donate");
            water.SetTrigger("Start");
        }
    }

    public void ChangeColour()
    {
        if(healthPoints < 70 && healthPoints > 30)
        {
            fillColour.color = yellow;
        }
        else if(healthPoints < 30)
        {
            fillColour.color = red;
        }
    }
}
