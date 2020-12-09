using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Entity : MonoBehaviour
    {
        public float speed;
        public Animator animator;
        public Animator animator_rig;
        public Weapon weapon;

        public float health = 100f;
        public bool is_dead = false;
        public bool is_running = false;
        public bool is_firing = false;
        public Vector3 velocity;

        private void Update() {
            if (health <= 0) is_dead = true;
        }

        public void SetAnimation(string name)
        {
            animator.SetBool("RUNNING", false);
            animator_rig.SetBool("RUNNING", false);
            animator.SetBool("FIRING", false);
            animator_rig.SetBool("FIRING", false);
            animator.SetBool("ISDEAD", false);
            animator_rig.SetBool("ISDEAD", false);
            animator.SetBool("ROLLING", false);
            animator_rig.SetBool("ROLLING", false);

            if (name != "IDLE")
            {
                animator.SetBool(name, true);
                animator_rig.SetBool(name, true);
            }
        }

        public void Damage(float value) {
            health -= value;
        }

        
    }

}

