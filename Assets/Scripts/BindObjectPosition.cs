using UnityEngine;

public class BindObjectPosition : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 offset = Vector3.zero;

    void LateUpdate()
    {
        if (targetObject != null)
        {
            transform.position = new Vector3(
                targetObject.transform.position.x + offset.x,
                targetObject.transform.position.y + offset.y,
                transform.position.z + offset.z // zachowaj Z kamery + offset
            );
        }
    }
}