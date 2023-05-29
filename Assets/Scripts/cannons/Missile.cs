using UnityEngine;

public class Missile : Projectile
{
    public float initialSpeed = 5;
    public float finalSpeed = 20;
    public float acceleration = 80;
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        speed = initialSpeed;
    }


    void SpeedUp()
    {
        var speedStep = acceleration * Time.deltaTime;
        if (speed < finalSpeed) speed += speedStep;
    }

    override public void Update()
    {
        base.Update();
        SpeedUp();
    }
}
