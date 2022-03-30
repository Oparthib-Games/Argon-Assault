using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum typeEnum{O, A, B, C, D, E, F};
    public typeEnum type;
    private bool isTriggeredOnce = false;
    
    void Start() {
        
    }

    void Update() {
         
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !isTriggeredOnce) {
            Debug.Log("Player Triggered");
            
            BlockSpawner block_spawner = FindObjectOfType<BlockSpawner>();
            block_spawner.SpawnNewBlock();
            isTriggeredOnce = true;
        }
    }
}
