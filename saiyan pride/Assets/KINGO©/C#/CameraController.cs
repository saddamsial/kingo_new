using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform enemy;
    public float followSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    public float minFOV = 20f;
    public float maxFOV = 60f;
    public float zoomSpeed = 5f;
    public float maxYDistance = 2f;
    public float ShakeDuration = 1f;
    public float magnitude;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // follow the target with smoothing
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // adjust zoom based on distance to enemy
        float distanceToEnemy = Vector3.Distance(target.position, enemy.position);
        float yDistanceToEnemy = Mathf.Abs(target.position.y - enemy.position.y);

        float targetDistance = Mathf.Lerp(minDistance, maxDistance, distanceToEnemy / 10f); // adjust divisor for desired zoom range

        if (yDistanceToEnemy > maxYDistance)
        {
            targetDistance += (yDistanceToEnemy - maxYDistance);
        }

        if (cam.orthographic)
        {
            // orthographic camera
            float targetSize = Mathf.Lerp(maxFOV, minFOV, distanceToEnemy / 10f); // adjust divisor for desired zoom range
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
        }
        else
        {
            // perspective camera
            float targetFOV = Mathf.Lerp(minFOV, maxFOV, distanceToEnemy / 10f); // adjust divisor for desired zoom range
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        }

        // calculate new camera position based on target position and distance
        Vector3 newCameraPosition = target.position - transform.forward * targetDistance;

        // check if there's any obstacles between camera and target
        RaycastHit hitInfo;
        if (Physics.Linecast(target.position, newCameraPosition, out hitInfo))
        {
            // if there's an obstacle, move the camera to the obstacle point
            newCameraPosition = hitInfo.point;
        }

        // set camera position
        transform.position = newCameraPosition;
    }



public IEnumerator ShakeCamera()
{
    Vector3 originalPos = transform.position;

    float elapsedTime = 0.0f;

    while (elapsedTime < ShakeDuration)
    {
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;

        transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

        elapsedTime += Time.deltaTime;

        yield return null;
    }

    transform.position = originalPos;
}













}



