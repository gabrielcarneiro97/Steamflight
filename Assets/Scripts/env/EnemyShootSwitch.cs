using UnityEngine;

public class EnemyShootSwitch : MonoBehaviour
{
    public bool turnOn = true;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.isActive = turnOn;
        }
    }
}
