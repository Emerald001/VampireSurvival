using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject followObject;

    public float followSpeed = 2f;
    public Vector3 offset;

    public void SetFollower(GameObject obj)
    {
        followObject = obj;
    }

    private void Update()
    {
        if (followObject == null)
            return;

        Vector3 targetPosition = followObject.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
