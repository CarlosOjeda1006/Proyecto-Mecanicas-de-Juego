using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform cameraTarget;
    internal static object current;

    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = cameraTarget.position;
        transform.rotation = cameraTarget.rotation;
    }
}
