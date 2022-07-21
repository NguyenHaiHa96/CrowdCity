using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCSpwaner : MonoBehaviour
{
    public GameObject npcPrefab;
    [SerializeField] private Transform[] spawnPositions;  
    [SerializeField] private float timeToSpawn;

    private int randomPositionIndex;
    private float spawnCountDown;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spawnCountDown = Random.Range(0f, timeToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;      
        if (timer > spawnCountDown)
        {
            SpawnNPC();
            timer = 0;
            spawnCountDown = Random.Range(0f, timeToSpawn);
        }      
    }

    private void SpawnNPC()
    {
        randomPositionIndex = Random.Range(0, spawnPositions.Length);
        var newNPC = Instantiate(npcPrefab);
        newNPC.transform.position = spawnPositions[randomPositionIndex].position;
        newNPC.GetComponent<NPC>().StartPosition = spawnPositions[randomPositionIndex].position;
    }
}
