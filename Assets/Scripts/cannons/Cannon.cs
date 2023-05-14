using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject fireSoundPrefab;
    public GameObject projectilePrefab;
    bool isAvaible = true;
    public float cooldown = 1f;
    public ProjectileType type = ProjectileType.BULLET;
    public Team team = Team.NEUTRAL;
    Transform shootPoint;

    public int damageBuff = 0;

    public int permaDamageBuff = 0;
    public bool ghostShoot = false;

    void Start()
    {
        shootPoint = transform.Find("ShootPoint");
    }

    public void DefineTeam(Team team)
    {
        this.team = team;
    }

    void PlaySound()
    {
        if (fireSoundPrefab == null) return;

        var soundGameObject = Instantiate(fireSoundPrefab, transform.position, transform.rotation);
        var audioSource = soundGameObject.GetComponent<AudioSource>();
        StartCoroutine(AudioSourceDestroyer(audioSource));
    }

    IEnumerator AudioSourceDestroyer(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(audioSource.gameObject);
    }

    public void Shoot()
    {
        if (projectilePrefab == null)
        {
            return;
        }

        if (ghostShoot)
        {
            StartCoroutine(CooldownWeapon());
            ghostShoot = false;
            return;
        }

        if (!isAvaible)
        {
            return;
        }

        var projectile = projectilePrefab.GetComponent<Projectile>();
        projectile.DefineTeam(team);
        projectile.BuffDamage(damageBuff);
        projectile.BuffDamage(permaDamageBuff);
        Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
        StartCoroutine(CooldownWeapon());
        PlaySound();
    }


    IEnumerator CooldownWeapon()
    {
        isAvaible = false;
        yield return new WaitForSeconds(cooldown);
        isAvaible = true;
    }
}
