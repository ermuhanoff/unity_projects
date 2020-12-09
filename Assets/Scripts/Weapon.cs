using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Weapon : MonoBehaviour
    {
        public GameObject muzzle;
        public GameObject bullet;
        public Transform bullet_start;

        public int fireTimeToShoot = 10;
        public float bulletSpeed;

        private int fireSate = 0;
        private int delay = 0;

        private void Start()
        {
            // chracter_pos = 
        }

        public void Fire(int t = 0)
        {
            if (delay >= t)
            {
                if (fireSate >= fireTimeToShoot)
                {
                    fireSate = 0;

                    GameObject m = Instantiate(muzzle, new Vector3(bullet_start.position.x, bullet_start.position.y, bullet_start.position.z), bullet_start.rotation);
                    var main = m.GetComponentInChildren<ParticleSystem>().main;


                    var psMuzzle = m.GetComponent<ParticleSystem>();

                    if (psMuzzle != null) Destroy(m, psMuzzle.main.duration);
                    else
                    {
                        var psChid = m.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(m, psChid.main.duration);
                    }

                    GameObject b = Instantiate(bullet, bullet_start.position, bullet_start.rotation);

                    b.GetComponent<Rigidbody>().velocity = bullet_start.transform.forward * bulletSpeed;
                }
                else fireSate++;
            }
            else delay++;
        }
    }
}