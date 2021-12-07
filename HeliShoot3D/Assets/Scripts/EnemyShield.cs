using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    public GameObject brokenShield;
    private Rigidbody _rigidbody;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;
    Animator animator;


    private void Start()
    {
        _rigidbody = brokenShield.GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rocket"))
        {
            other.gameObject.SetActive(false); // rocket
            brokenShield.transform.position = new Vector3(0f, 4f, 46f);
            brokenShield.transform.rotation = this.transform.rotation;

            _rigidbody.AddForce(Vector3.up, ForceMode.Impulse);
            brokenShield.SetActive(true);
            animator.enabled = false;
            meshRenderer.enabled = false;
            boxCollider.enabled = false;

            Camera.main.DOShakeRotation(1f, 1f);
            ObjectPool.Instance.GetPooledObject(3, transform);
            StartCoroutine(DeActiveShield(2));
        }
    }
    IEnumerator DeActiveShield(float time)
    {
        yield return new WaitForSeconds(time);
        brokenShield.SetActive(false);
    }

}
