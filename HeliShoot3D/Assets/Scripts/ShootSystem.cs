using UnityEngine;
public class ShootSystem : MonoBehaviour
{
    public Vector3 target;

    public float launchForce;
    public Transform shotPoint;
    public Vector3 direction;



    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    private void Start()
    {

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
            points[i].transform.parent = gameObject.transform;
        }


    }

    private void Update()
    {
        Vector3 shooterPoisition = transform.position;
        Vector3 targetPosition = new Vector3(0, target.y, transform.position.z + target.z);
        direction = targetPosition - shooterPoisition;
        transform.forward = direction;



        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = pointPosition(i * spaceBetweenPoints);
        }

    }
    public void Shoot()
    {
        GameManager.Instance.SetButtonFillAmount();
        //GameObject newRocket = Instantiate(rocket, shotPoint.position, shotPoint.rotation);
        GameObject newRocket = ObjectPool.Instance.GetPooledObject(0, shotPoint.transform);
        newRocket.GetComponent<Rigidbody>().velocity = transform.forward * launchForce;
    }
    Vector3 pointPosition(float t)
    {
        Vector3 position = (Vector3)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics.gravity * (t * t);
        return position;
    }
}
