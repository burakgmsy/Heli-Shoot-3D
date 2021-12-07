using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HelicopterController : Singleton<HelicopterController>
{
    [SerializeField] private float topLimit;
    [SerializeField] private float botLimit;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject scoreRocket;
    [SerializeField] private float yAxisValue;
    [SerializeField] private Transform govde;
    public bool isUp, isDown;

    private void FixedUpdate()
    {
        if (isUp)
        {
            yAxisValue = 1;
        }
        else if (isDown)
        {
            yAxisValue = -1;
        }
        else
        {
            yAxisValue = 0;
            GameManager.Instance.MoveUpDownFree(false);
            SmoothMove(0);
        }
        var speed = GameManager.Instance.Speed;
        var speedYAxis = GameManager.Instance.SpeedYAxis;
        transform.Translate(new Vector3(0, yAxisValue * speedYAxis, 1 * speed) * Time.deltaTime);
        cameraTransform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        LimitYAxis(topLimit, botLimit);
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("scoreTap"))
        {
            GameManager.Instance.Speed = 0;
            GameManager.Instance.OpenTabScreen();
            cameraTransform.DORotate(new Vector3(20f, -25f), 3f);
            cameraTransform.DOMove(new Vector3(2.83f, 5f, 85f), 3f).OnComplete(() =>
            {
                scoreRocket.SetActive(true);
            });

            transform.DOMove(new Vector3(0, 4, 90), 3f).OnComplete(() =>
            {
                GameManager.Instance.CloseTabScreen();
                //this.gameObject.SetActive(false);
            });
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gem"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.AddGem();
        }
    }
    public void MoveOn()
    {
        GameManager.Instance.Speed = 5;
        shooter.gameObject.SetActive(false);
        GameManager.Instance.ShootScreen(false);
    }
    public void StopAndShoot()
    {
        GameManager.Instance.Speed = 0;
        shooter.gameObject.SetActive(true);
        GameManager.Instance.ShootScreen(true);
    }
    private void LimitYAxis(float yBoundTop, float yBoundBot)
    {
        if (transform.position.y > yBoundTop)
        {
            transform.position = new Vector3(transform.position.x, yBoundTop, transform.position.z);

        }
        if (transform.position.y < yBoundBot)
        {
            transform.position = new Vector3(transform.position.x, yBoundBot, transform.position.z);

        }
    }

    public void MoveUp(bool value)
    {
        SmoothMove(+15);
        GameManager.Instance.MoveUpUI(true);
        Debug.Log("MoveUp");
        isUp = value;
    }
    public void MoveDown(bool value)
    {
        SmoothMove(-15);
        GameManager.Instance.MoveDownUI(true);
        Debug.Log("MoveDown");
        isDown = value;
    }

    public void SmoothMove(float value)
    {
        govde.transform.DORotate(new Vector3(value, 180, 0), 1.5f);
    }
}
