using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Ship
{
    public GameObject bossAreaGameObject;
    Renderer bossAreaRenderer;
    Renderer bossRenderer;
    float objectWidth;
    float objectHeight;

    void Start()
    {
        bossAreaRenderer = bossAreaGameObject.GetComponent<Renderer>();
        bossRenderer = GetComponent<Renderer>();
        objectWidth = bossRenderer.bounds.size.x / 2;
        objectHeight = bossRenderer.bounds.size.z / 2;
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;

        var gameXPos = bossAreaRenderer.transform.position.x;
        var gameMinX = bossAreaRenderer.bounds.min.x + gameXPos + objectWidth;
        var gameMaxX = bossAreaRenderer.bounds.max.x + gameXPos - objectWidth;

        var gameZPos = bossAreaRenderer.transform.position.z;
        var gameMinZ = bossAreaRenderer.bounds.min.z + objectHeight;
        var gameMaxZ = bossAreaRenderer.bounds.max.z - objectHeight;

        position.x = Mathf.Clamp(position.x, gameMinX, gameMaxX);
        position.z = Mathf.Clamp(position.z, gameMinZ, gameMaxZ);

        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        ClampPosition();
    }
}
