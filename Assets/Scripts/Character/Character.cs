using UnityEngine;

namespace Game
{
    public class Character : Entity
    {
        public FloatingJoystick floatingJoystick;

        private float oldAngle = 0.0f;
        private float THRESH = 0.7f;

        void FixedUpdate()
        {
            Move();
        }


        public void Move()
        {

            float fH = floatingJoystick.Horizontal;
            float fV = floatingJoystick.Vertical;
            float h = fH >= THRESH ? THRESH : fH <= -THRESH ? -THRESH : 0f;
            float v = fV >= THRESH ? THRESH : fV <= -THRESH ? -THRESH : 0f;
            if (h != 0 || v != 0)
            {
                Vector3 dirLook = Vector3.forward * fH + Vector3.right * fV;
                float angle = Mathf.Atan2(dirLook.z, dirLook.x) * Mathf.Rad2Deg;
                if (angle != oldAngle)
                {
                    oldAngle = angle;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                }

                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(fH, 0f, fV) * speed;

                SetAnimation("RUNNING");
            }
            else
            {
                SetAnimation("IDLE");
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (transform.position.z > 50)
                {
                    // SetAnimation("ISDEAD");
                    SetAnimation("FIRING");
                    weapon.Fire();
                }

            }
        }
    }
}

