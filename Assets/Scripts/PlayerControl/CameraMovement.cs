using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class CameraMovement : MonoBehaviour
    {

        public float fCameraSpeed;

        public float fCameraSensitivity;

        private float rotationX = 0.0f;
        private float rotationY = 0.0f;

        // Update is called once per frame
        void Update()
        {
            CheckMouseMovement();
            CheckAxisMovement();
        }

        void CheckMouseMovement()
        {
            if (!Input.GetButton("Fire2"))
            {
                Cursor.visible = true;
                return;
            }

            Cursor.visible = false;
            rotationX += Input.GetAxis("Mouse X") * fCameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * fCameraSensitivity * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
        }

        void CheckAxisMovement()
        {
            // Check input to see if we want to move the camera
            float fForwardAxis = Input.GetAxisRaw("Vertical");
            float fSidewaysAxis = Input.GetAxisRaw("Horizontal");

            Vector3 forwardVect = this.transform.forward;
            Vector3 rightVect = this.transform.right;
            Vector3 cameraVelocityVect = forwardVect * fForwardAxis * fCameraSpeed * Time.deltaTime;
            cameraVelocityVect += rightVect * fSidewaysAxis * fCameraSpeed * Time.deltaTime;

            this.transform.position += cameraVelocityVect;
        }
    }
}
