using UnityEngine;

public class EnemyShootSwitch : MonoBehaviour
{
    public bool turnOn = true;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.isActive = turnOn;
                return;
            }

            var finalBoss = collider.gameObject.GetComponent<FinalBoss>();
            if (finalBoss != null)
            {
                finalBoss.isActive = turnOn;
                return;
            }
        }
    }
}
