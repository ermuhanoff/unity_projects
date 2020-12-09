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
                character.GetComponent<Character>().EnemyDead(gameObject.GetComponent<CapsuleCollider>());
                SetAnimation("ISDEAD");
            }
            else if (is_firing)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                SetAnimation("FIRING");
                weapon.Fire();
            }
            else if (is_running)
            {
                gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
                SetAnimation("RUNNING");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "character" && other.GetType().ToString() != "UnityEngine.SphereCollider")
            {
                is_firing = true;
                is_running = false;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "character" && other.GetType().ToString() != "UnityEngine.SphereCollider")
            {
                is_firing = false;
                is_running = true;
            }
        }
        public void AnimDeathEnd()
        {
            // animator.Play("enemy_after_die");
            Destroy(gameObject);
        }
    }
}