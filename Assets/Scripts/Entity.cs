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

        // void FixedUpdate()
        // {
        //     Debug.Log("HEre");
        //     Move();
        // }

        // public void Move() { }

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

        public void AnimDeathEnd()
        {
            Destroy(gameObject);
        }
    }

}

