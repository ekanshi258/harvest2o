using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public float ySpawnCoordinate;

    private float minimumXSpawnCoordinate, maximumXSpawnCoordinate;

    private void Start()
    {
        float halfHeight = (ySpawnCoordinate + ySpawnCoordinate) / 2.0f;
        maximumXSpawnCoordinate = (halfHeight * Screen.width / Screen.height) - 0.5f;
        minimumXSpawnCoordinate = -maximumXSpawnCoordinate;

        this.gameObject.transform.position = new Vector3(maximumXSpawnCoordinate,
            this.transform.position.x,
            this.transform.position.z);
    }
}
