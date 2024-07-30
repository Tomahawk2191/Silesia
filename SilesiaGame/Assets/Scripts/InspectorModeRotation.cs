using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class InspectorModeRotation : MonoBehaviour
    {
        public float horizontalSpeed = 2.0F;
        public float verticalSpeed = 2.0F;
        private bool enabledRot = false;
        private Transform obj;

        void Update() {
            if (enabledRot && Input.GetMouseButton(1))
            {
                float h = horizontalSpeed * Input.GetAxis("Mouse X");
                float v = verticalSpeed * Input.GetAxis("Mouse Y");
                obj.Rotate(v, h, 0, Space.World);
            }
        }

        public void setEnabledRotation(bool value)
        {
            enabledRot = value;
        }

        public void setObject(Transform o)
        {
            obj = o;
        }
    }
}