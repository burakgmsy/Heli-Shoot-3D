using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoreRocket : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    private int scoreBlock;
    Rigidbody _rigidbody;
    private int counter = 0;
    public Transform _camera;
    private Vector3 offset;
    private bool x;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        offset = transform.position - _camera.position;
        Camera.main.DOShakeRotation(10f, 1f);
        transform.DOMove(new Vector3(0, 3f, transform.position.z), 1.5f).OnComplete(() =>
        {
            x = true;
        });

    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //_camera.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        _camera.transform.position = new Vector3(2.83f, _camera.transform.position.y, transform.position.z - offset.z);
        //Debug.Log((int)GameManager.Instance.CurrentScore);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ScoreCounter"))
        {
            Debug.Log("GameManager: " + (int)GameManager.Instance.CurrentScore);
            counter++;
            Debug.Log("Counter: " + counter);
            if ((int)GameManager.Instance.CurrentScore < 1)
            {
                transform.DORotate(new Vector3(45f, 0, 0), 1f);
                speed = 0;
                _rigidbody.useGravity = true;
            }
            if (counter == (int)GameManager.Instance.CurrentScore)
            {
                transform.DORotateQuaternion(Quaternion.Euler(new Vector3(90f, 0, 0)), 2f);
                speed = 1;
                _rigidbody.useGravity = true;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            //patlama
            ObjectPool.Instance.GetPooledObject(3, transform);
            _rigidbody.isKinematic = true;
            GameManager.Instance.FinalScoreText();
            GameManager.Instance.WinGame();
            gameObject.SetActive(false);
        }
    }

}
