using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float bulletSpeed;
    public Transform tr;
    public Animator animator;
    public Animator animator_rig;
    public GameObject bullet;
    public Transform bullet_start;
    public Transform character_pos;

    public GameObject muzzle;

    private int fire_sate = 0;
    private int fire_time_to_shoot = 10;

    private bool is_firing = false;

    private void Update()
    {

    }

    public void FixedUpdate()
    {
        tr.LookAt(character_pos);

        if (is_firing)
        {
             gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            SetAnimation("FIRING");
            Fire();
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = tr.transform.forward * speed;
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

    private void Fire()
    {
        if (fire_sate >= fire_time_to_shoot)
        {
            fire_sate = 0;

            GameObject m = Instantiate(muzzle, new Vector3(bullet_start.position.x, bullet_start.position.y, bullet_start.position.z), tr.rotation);
            var main = m.GetComponentInChildren<ParticleSystem>().main;


            var psMuzzle = m.GetComponent<ParticleSystem>();

            if (psMuzzle != null) Destroy(m, psMuzzle.main.duration);
            else
            {
                var psChid = m.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(m, psChid.main.duration);
            }

            GameObject b = Instantiate(bullet, bullet_start.position, tr.rotation);

            b.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
        }
        else fire_sate++;
    }

    private void SetAnimation(string name)
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
}
