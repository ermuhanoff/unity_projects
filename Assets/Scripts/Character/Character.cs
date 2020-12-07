using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public float speed;
    public float bulletSpeed;
    public FloatingJoystick floatingJoystick;
    public Transform tr;
    public Animator animator;
    public Animator animator_rig;
    public GameObject bullet;
    public Transform bullet_start;

    public GameObject muzzle;

    private const float THRESH = 0.7f;
    private float oldAngle = 0.0f;
    private int fireSate = 0;
    private int fireTimeToShoot = 10;

    private void Update()
    {

    }

    public void FixedUpdate()
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
                tr.rotation = Quaternion.AngleAxis(angle, Vector3.down);
                tr.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            }

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(fH, 0f, fV) * speed;
            SetAnimation("RUNNING");
        }
        else
        {
            SetAnimation("IDLE");
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (tr.position.z > 50)
            {
                SetAnimation("FIRING");
                Fire();
            }

        }

    }

    private void Fire()
    {
        if (fireSate >= fireTimeToShoot)
        {
            fireSate = 0;

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
        else fireSate++;
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
