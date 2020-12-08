using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class Enemy : Entity
    {
        public Transform character_pos;
        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            transform.LookAt(character_pos);

            if (weapon.is_firing)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                SetAnimation("FIRING");
                weapon.Fire();
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = transform.transform.forward * speed;
                SetAnimation("RUNNING");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "character")
                weapon.is_firing = true;

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "character")
                weapon.is_firing = false;
        }
    }
}