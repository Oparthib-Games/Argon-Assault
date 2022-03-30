using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();

    [SerializeField] private enum difficultyEnum{O, A, B, C, D, E, F};
    [SerializeField] private difficultyEnum difficulty;
    [SerializeField] private float distance_sm = 20;
    [SerializeField] private Vector3 nextSpawnPos = new Vector3(0, 0, 0);

    void Start() {
        SpawnNewBlock();
        SpawnNewBlock();
        SpawnNewBlock();
    }

    void Update() {

    }

    public void SpawnNewBlock() {
        Debug.Log("Spawning new block.");
        
        int rand_index = Random.Range (0, blocks.Count);

        GameObject curr_block = blocks[rand_index];
        
        Instantiate(curr_block, nextSpawnPos, Quaternion.identity);
        
        SetNextSpawnPos();
    }

    private void SetNextSpawnPos() {
        nextSpawnPos = new Vector3(0, 0, nextSpawnPos.z + distance_sm);
    }
}
