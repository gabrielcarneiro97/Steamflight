using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public bool isActive = false;
    public float speed;
    public Team team = Team.NEUTRAL;
    public int life = 3;
    public bool invencible = false;
    public int shield = 0;
    public int maxLife = 3;
    public int maxShield = 2;
    public int permaDamageBuff = 0;

    [HideInInspector]
    public Rigidbody rb;

    public GameObject onHitSoundPrefab;
    public GameObject explosionParticlesPrefab;

    IEnumerator OnHitPlayerInvencibility()
    {
        invencible = true;
        yield return new WaitForSeconds(.5f);
        invencible = false;
    }

    virtual public void DetectHit(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            var projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile.team != team && projectile.team != Team.NEUTRAL)
            {
                var damage = projectile.damage;

                if (shield > 0 && !invencible)
                {
                    shield -= damage;
                    if (shield < 0) shield = 0;
                    damage = 0;
                }

                if (!invencible) OnHitSound();
                if (isActive && !invencible)
                {
                    life -= damage;
                    if (gameObject.tag == "Player") StartCoroutine(OnHitPlayerInvencibility());
                }
                if (life <= 0) OnLifeZero();

                if (projectile.type != ProjectileType.PLASMA_EXPLOSION)
                    Destroy(other.gameObject);
            }
        }
    }

    void OnHitSound()
    {
        if (onHitSoundPrefab == null) return;

        var soundGameObject = Instantiate(onHitSoundPrefab, transform.position, transform.rotation);
        var audioSource = soundGameObject.GetComponent<AudioSource>();
        StartCoroutine(AudioSourceDestroyer(audioSource));
    }

    IEnumerator AudioSourceDestroyer(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(audioSource.gameObject);
    }

    void OnLifeZero()
    {
        if (explosionParticlesPrefab) Instantiate(explosionParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    virtual public void OnTriggerEnter(Collider other)
    {
        DetectHit(other);
    }


}
