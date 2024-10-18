using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z);
        transform.Translate(Vector3.left*Time.deltaTime);
    }
}
