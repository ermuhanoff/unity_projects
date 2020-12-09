using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class Enemy : Entity
    {
        public GameObject character;
        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            transform.LookAt(character.transform);
            if (is_dead)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                character.GetComponent<Character>().EnemyDead(gameObject.GetComponent<BoxCollider>());
                SetAnimation("ISDEAD");
            }
            else if (is_firing)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                SetAnimation("FIRING");
                // weapon.Fire();
            }
            else if (is_running)
            {
                gameObject.GetComponent<Rigidbody>().velocity = transform.transform.forward * speed;
                SetAnimation("RUNNING");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "character")
                is_firing = true;

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "character")
                is_firing = false;
        }
        public void AnimDeathEnd()
        {
            // animator.Play("enemy_after_die");
            Destroy(gameObject);
        }
    }
}