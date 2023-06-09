using UnityEngine;

public class Plasma : Projectile
{
    public GameObject explosionPrefab;
    PlasmaExplosion explosion;
    override public void Start()
    {
        base.Start();
        explosion = explosionPrefab.GetComponent<PlasmaExplosion>();
        explosion.DefineTeam(team);
    }

    override public void Update()
    {
        base.Update();
    }

    override public void OnDestroy()
    {
        base.OnDestroy();
        GameObject explosionGameObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
