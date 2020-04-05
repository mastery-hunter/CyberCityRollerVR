using UnityEngine;

public class CameraOffsetContainer : MonoBehaviour {

    public Transform target;
    public float rotateSpeed = 5;

    private float storedAngleX = 0, storedAngleY = 0;

    private void Update()
    {
        storedAngleX = Input.GetAxis("Mouse X") * rotateSpeed;
        storedAngleY = Input.GetAxis("Mouse Y") * -rotateSpeed;

        transform.position = target.position;
        transform.Rotate(new Vector3(storedAngleY, storedAngleX, 0));
    }
}
