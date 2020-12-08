
using UnityEngine;

public class Main : MonoBehaviour
{
    public Transform main;
    public Transform charcter;

    public float speed = 8f;

    void Update()
    {
        Vector3 p1 = new Vector3(charcter.position.x, 120.0f, charcter.position.z - 60.0f);
        transform.position = Vector3.Lerp(main.position, p1, Time.deltaTime * speed);
    }
}
