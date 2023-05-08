using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaExplosion : Projectile
{

    override public void Start()
    {
        var rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Scale()
    {
        var scale = transform.localScale;
        var scaleStep = 15f * Time.deltaTime;
        transform.localScale = new Vector3(scale.x + scaleStep, scale.y + scaleStep, scale.z + scaleStep);

        if (scale.x > 7)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    override public void Update()
    {
        Scale();
    }
}
