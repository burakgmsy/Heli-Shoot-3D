using UnityEngine;
using DG.Tweening;
using System.Collections;

public class EnemyShootSystem : MonoBehaviour
{
    public Transform shooter;
    public Transform shotPoint;
    public Transform target;
    public float shootTime = 5f;
    public float spawnCountdown = 5f;

    private void Update()
    {
        Vector3 shooterPoisition = transform.position;
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = targetPosition - shooterPoisition;
        shooter.transform.forward = -direction;
        ShotDelay();
    }
    private void ShotDelay()
    {
        if (spawnCountdown <= 0f)
        {
            Shot();

            spawnCountdown = shootTime;
        }
        spawnCountdown -= Time.deltaTime;
    }
    void Shot()
    {
        //var offset = transform.position
        GameObject newBullet = ObjectPool.Instance.GetPooledObject(1, shotPoint);
        newBullet.transform.DOMove(target.transform.position, 2.5f).OnComplete(() =>
            {
                newBullet.gameObject.SetActive(false);
            });
    }
}
