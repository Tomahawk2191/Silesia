using Cinemachine;
using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InspectorModeRotation : MonoBehaviour
    {
        public float horizontalSpeed = 12f;
        public float verticalSpeed = 12f;
        private static bool enabledRot = false;
        private static Transform obj;

        private void Start()
        {
            //PlayerInteract.input.on += onRotateOn;
            //PlayerInteract.input.OnZoomInEvent += onRotateOff;
        }

        void Update() {
            if (enabledRot /*&& Input.GetMouseButton(1)*/)
            {
                float h = horizontalSpeed * Input.GetAxis("Mouse X") * -1;
                float v = verticalSpeed * Input.GetAxis("Mouse Y");
                obj.transform.RotateAround(obj.position, Camera.main.transform.right, v * 2);
                obj.transform.RotateAround(obj.position, Camera.main.transform.up, h * 2);
            }
        }

        private void RotateOn(object sender, EventArgs e)
        {
            enabledRot = true;
        }

        private void RotateOff(object sender, EventArgs e)
        {
            enabledRot = false;
        }


        public static void setEnabledRotation(bool value)
        {
            enabledRot = value;
        }

        public static void setObject(Transform o)
        {
            obj = o;
        }
    }
}