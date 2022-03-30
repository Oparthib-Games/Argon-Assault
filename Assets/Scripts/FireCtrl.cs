using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    [SerializeField] private float fireSpeed = 100f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform[] spawnPoses;

    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }

    void Fire() {
        foreach (var spawn_pos in spawnPoses) {
            GameObject bulletGO = Instantiate(bullet, spawn_pos.position, Quaternion.Euler(90, 0, 0)) as GameObject;
            
            // Setting parent
            GameObject spawn_parent = GameObject.Find("Spawn Parent");
            if(!spawn_parent) spawn_parent = new GameObject("Spawn Parent");
            bulletGO.transform.SetParent(spawn_parent.transform);

            // Rigidbody & Add Force
            Rigidbody bulletRB = bulletGO.GetComponent<Rigidbody>();
            if(!bulletRB) {
                bulletRB = bulletGO.AddComponent<Rigidbody>() as Rigidbody;
                bulletRB.useGravity = false;
            }
            bulletRB.AddRelativeForce(Vector3.up * fireSpeed);
            Destroy(bulletGO, 5);
        }
    }
}
