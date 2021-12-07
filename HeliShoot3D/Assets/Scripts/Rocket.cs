using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody _rigidbody;
    private void Start()
    {
        //rocket effect
        _rigidbody = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, -Vector3.right);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            //BOM effect shield
            Camera.main.DOShakeRotation(1f, 1f);
            ObjectPool.Instance.GetPooledObject(3, transform);
            gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
//|| other.gameObject.CompareTag("shield")
        if (other.gameObject.CompareTag("platform"))
        {
            //BOM effect
            Camera.main.DOShakeRotation(1f, 1f);
            ObjectPool.Instance.GetPooledObject(3, transform);
            gameObject.SetActive(false);
        }
    }
}
