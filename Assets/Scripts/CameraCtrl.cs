using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;

    private float offset;
    
    void Start()
    {
        cam = Camera.main;
        if(!cam) Debug.LogWarning("Camera not found");
        if(!player) Debug.LogWarning("Player not found");

        if(!cam || !player) Time.timeScale = 0;

        offset = cam.transform.position.z - player.transform.position.z;
    }

    void Update()
    {
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, player.transform.position.z + offset);
    }
}
