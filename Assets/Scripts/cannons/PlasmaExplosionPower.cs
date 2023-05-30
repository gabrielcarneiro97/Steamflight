using UnityEngine;

public class PlasmaExplosionPower : Projectile
{
    public int maxSize = 30;
    public float scaleStep = 40f;
    override public void Start()
    {
        var rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Scale()
    {
        var scale = transform.localScale;
        var step = scaleStep * Time.deltaTime;
        transform.localScale = new Vector3(scale.x + step, scale.y + step, scale.z + step);

        if (scale.x > maxSize)
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
