using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed, rotationSpeed;

    private Vector3 moveDirection = Vector3.zero;
    private float angle;

    [SerializeField] private Joystick movJoystick, rotJoystick;
    [SerializeField] private Transform rotateAt;
    private bool lockRot;

    private void Update()
    {
        moveDirection = new Vector3(movJoystick.Horizontal, 0, movJoystick.Vertical);

        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (rotJoystick.isActiveAndEnabled)
        {
            angle = (Mathf.Atan2(rotJoystick.Horizontal, rotJoystick.Vertical) * Mathf.Rad2Deg / rotationSpeed) * Time.deltaTime;
        }

        if (lockRot)
            rotateAt.rotation *= Quaternion.Euler(0, angle, 0);
        else
        {
            transform.rotation *= Quaternion.Euler(0, angle, 0);
            rotateAt.rotation = transform.rotation;
        }
    }

    public void ToggleLockRotation()
    {
        lockRot = !lockRot;
    }
}
