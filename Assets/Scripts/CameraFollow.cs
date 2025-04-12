using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followObjectTransform;

    public float cameraSpeed = 3f;
    public Vector3 offset;

    [Header("Follow settings")]
    public bool followX = true;
    public bool followY = false;

    void LateUpdate()
    {
        if(followObjectTransform == null)
        {
            return;
        }

        float targetX = followX ? followObjectTransform.position.x + offset.x : transform.position.x;
        float targetY = followY ? followObjectTransform.position.y + offset.y : transform.position.y;


        Debug.Log(followObjectTransform.position.x);

        Vector3 desiredPosition = new Vector3(targetX, targetY, transform.position.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
        transform.position = smoothPosition;

    }
}
