using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private EnemyShootSystem enemyShooter;

    public event Action<float> OnHealthPctChanged = delegate { };
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void OnTriggerEnter(Collider other)
    {

        //ForHeli
        if (gameObject.CompareTag("Heli") & other.gameObject.CompareTag("Havan"))
        {
            Camera.main.DOShakeRotation(1f, 1f);
            ObjectPool.Instance.GetPooledObject(3, transform);
            other.gameObject.SetActive(false);
            ModifyHealth(-GameManager.Instance.EnemyDamage);
            if (currentHealth <= 0)
            {
                ObjectPool.Instance.GetPooledObject(4, transform);
                GameManager.Instance.LoseGame();
                gameObject.SetActive(false);
            }


        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //ForEnemy
        if (gameObject.CompareTag("Enemy") & other.gameObject.CompareTag("Rocket"))
        {
            Camera.main.DOShakeRotation(1f, 1f);
            ObjectPool.Instance.GetPooledObject(3, transform);
            ModifyHealth(-GameManager.Instance.HeliDamage);
            other.gameObject.SetActive(false);
            if (currentHealth <= 0)
            {
                HelicopterController.Instance.MoveOn();
                enemyShooter.enabled = false;
                gameObject.SetActive(false);
                ObjectPool.Instance.GetPooledObject(2, transform);
            }
        }
    }

}
