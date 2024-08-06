using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class InspectorModeRotation : MonoBehaviour
    {
        public float horizontalSpeed = 8.0F;
        public float verticalSpeed = 8.0F;
        private static bool enabledRot = false;
        private static Transform obj;

        void Update() {
            if (enabledRot && Input.GetMouseButton(1))
            {
                float h = horizontalSpeed * Input.GetAxis("Mouse X");
                float v = verticalSpeed * Input.GetAxis("Mouse Y");
                obj.transform.RotateAround(obj.position, Camera.main.transform.right, v);
                obj.transform.RotateAround(obj.position, Camera.main.transform.up, h);
            }
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