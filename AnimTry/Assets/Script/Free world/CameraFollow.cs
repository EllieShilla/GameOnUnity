using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    float smoothSpeed=0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        if (target!=null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }
        else
        {
            target = GameObject.Find("Standing W_Briefcase Idle").transform;
        }
    }


}
