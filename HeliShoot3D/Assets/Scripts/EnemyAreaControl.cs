using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaControl : MonoBehaviour
{
[SerializeField] private EnemyShootSystem enemyShooter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heli"))
        {
            enemyShooter.enabled = true;
            HelicopterController.Instance.StopAndShoot();
        }

    }
}
