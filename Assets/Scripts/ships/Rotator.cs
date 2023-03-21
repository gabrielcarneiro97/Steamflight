using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator
{
    private int maxRotation = 45;
    private float rotationSpeed = 100f;

    private Transform transform;
    private Vector3 lastPosition;

    private Rigidbody rb;

    public Rotator(int maxRotation, float rotationSpeed, Transform transform, Rigidbody rb = null)
    {
        this.maxRotation = maxRotation;
        this.rotationSpeed = rotationSpeed;
        this.transform = transform;
        this.rb = rb;
    }

    void Rotation(float velX)
    {
        var facingForward = transform.rotation.eulerAngles.y == 0;
        var rotZ = transform.rotation.eulerAngles.z;

        var maxLeftRotation = maxRotation;
        var maxRightRotation = -maxRotation;

        var convertRotZ = rotZ > 180 ? rotZ - 360 : rotZ;

        if (velX != 0)
        {
            var directionX = facingForward ? (velX > 0 ? 1 : -1) : velX > 0 ? -1 : 1;
            // directionX + => right
            // directionX - => left

            if ((convertRotZ > maxRightRotation && directionX == 1) || (convertRotZ < maxLeftRotation && directionX == -1) || convertRotZ == 0)
            {
                var angle = Mathf.Clamp(convertRotZ - directionX * rotationSpeed * Time.deltaTime, maxRightRotation, maxLeftRotation);
                transform.rotation = (Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle));
            }
        }
        else
        {
            if (convertRotZ > 3 && (int)convertRotZ <= maxLeftRotation)
            {
                transform.rotation = (Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, convertRotZ - rotationSpeed * Time.deltaTime));
            }
            else if (convertRotZ < -3 && (int)convertRotZ >= maxRightRotation)
            {
                transform.rotation = (Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, convertRotZ + rotationSpeed * Time.deltaTime));
            }
        }
    }

    public void Rotate()
    {
        var velX = 0f;
        if (rb == null)
        {
            velX = transform.position.x - lastPosition.x;
            Rotation(velX);
            lastPosition = transform.position;
            return;
        }
        velX = rb.velocity.x;
        Rotation(velX);
    }
}
