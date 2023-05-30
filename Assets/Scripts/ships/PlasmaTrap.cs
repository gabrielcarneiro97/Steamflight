using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaTrap : Enemy
{
    public GameObject plasmaExplosionPrefab;
    PlasmaExplosionPower plasmaExplosionPower;

    public int damage = 1;
    public int maxSize = 10;
    public float scaleStep = 20f;

    public float cooldown = 5f;
    bool isAvaible = false;

    new public void Start()
    {
        base.Start();
        StartCoroutine(Cooldown());
    }

    // Update is called once per frame
    new public void Update()
    {
        if (isAvaible && isActive) Shoot();
    }

    void Shoot()
    {
        plasmaExplosionPower = plasmaExplosionPrefab.GetComponent<PlasmaExplosionPower>();
        plasmaExplosionPower.team = team;
        plasmaExplosionPower.maxSize = maxSize;
        plasmaExplosionPower.scaleStep = scaleStep;
        plasmaExplosionPower.damage = damage;
        Instantiate(plasmaExplosionPrefab, transform.position, transform.rotation);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isAvaible = false;
        yield return new WaitForSeconds(cooldown);
        isAvaible = true;
    }


}
