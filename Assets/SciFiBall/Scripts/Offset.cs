using UnityEngine;

public class Offset : MonoBehaviour {

    public Transform playerBall;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}

    private void LateUpdate()
    {
        //storedAngle += Input.GetAxis("Mouse X") * rotateSpeed;
        //Quaternion rotation = Quaternion.Euler(0, storedAngle, 0);
        //transform.position = playerBall.position - (rotation * offset);

        transform.LookAt(playerBall);
    }
}
