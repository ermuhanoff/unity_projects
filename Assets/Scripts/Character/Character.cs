using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class Character : Entity
    {
        public FloatingJoystick floatingJoystick;
        public List<Collider> enemies = new List<Collider>();

        private float oldAngle = 0.0f;
        private float angle = 0.0f;
        private float THRESH = 0.7f;


        // private void Update() {

        // }

        void FixedUpdate()
        {
            InputController();
            if (enemies.Count == 0) is_firing = false;
            else is_firing = true;
            Move();
        }

        public void InputController()
        {
            float fH = floatingJoystick.Horizontal;
            float fV = floatingJoystick.Vertical;
            float h = fH >= THRESH ? THRESH : fH <= -THRESH ? -THRESH : 0f;
            float v = fV >= THRESH ? THRESH : fV <= -THRESH ? -THRESH : 0f;

            is_running = (h != 0 || v != 0) ? true : false;

            if (is_running)
            {
                Vector3 dirLook = Vector3.forward * fH + Vector3.right * fV;
                angle = Mathf.Atan2(dirLook.z, dirLook.x) * Mathf.Rad2Deg;
                velocity = new Vector3(fH, 0f, fV) * speed;
            }
        }

        public void EnemyDead(Collider c)
        {
            enemies.Remove(c);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "enemy" && other.GetType().ToString() != "UnityEngine.SphereCollider")
            {
                enemies.Insert(enemies.Count, other);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "enemy" && other.GetType().ToString() != "UnityEngine.SphereCollider")
            {
                enemies.Remove(other);
            }

        }

        private void FireEnemy()
        {
            if (is_firing)
            {
                var e = enemies[0];
                gameObject.transform.LookAt(e.transform);
                // if (gameObject.transform.forward == e.transform.forward);
                Debug.Log(weapon.transform.forward);
                Debug.Log(e.transform.forward);
                weapon.Fire();
            }
        }
        public void Move()
        {

            if (is_dead)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                SetAnimation("ISDEAD");
            }
            else if (is_running)
            {
                if (angle != oldAngle)
                {
                    oldAngle = angle;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                }
                gameObject.GetComponent<Rigidbody>().velocity = velocity;
                SetAnimation("RUNNING");
            }
            else if (is_firing)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                SetAnimation("FIRING");
                FireEnemy();
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                SetAnimation("IDLE");
            }
        }
    }
}

