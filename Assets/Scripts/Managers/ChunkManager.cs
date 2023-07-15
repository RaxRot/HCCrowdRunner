using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;
    
    
    [Header("Elements")] 
    [SerializeField] private Chunk [] chunkPrefabs;
    private Chunk _chunkInstance;
    [SerializeField] private Chunk firstChunk;
    [SerializeField] private Chunk finishChunkPrefab;
    private bool _isGameStarted;
    private Chunk _chunkToCreate;
    
    [SerializeField] private int platformToSpawn = 10;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreateRandomLevel();
    }

    private void CreateRandomLevel()
    {
        Vector3 chunkPosition=Vector3.zero;
        for (int i = 0; i < platformToSpawn; i++)
        {
            if (!_isGameStarted)
            {
                _isGameStarted = true;
                _chunkToCreate = firstChunk;
            }
            else
            {
                _chunkToCreate = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
            }
           
            if (i>0)
            { 
                chunkPosition.z += _chunkToCreate.GetChunkLength() / 2;
            } 
            
            _chunkInstance = Instantiate(_chunkToCreate,chunkPosition,Quaternion.identity,transform);
            chunkPosition.z += _chunkInstance.GetChunkLength()/2;
        }

        _chunkInstance = Instantiate(finishChunkPrefab, chunkPosition, Quaternion.identity, transform);
        
        GetFinishZPosition();
    }

    public float GetFinishZPosition()
    {
        float zFinishPos = _chunkInstance.transform.position.z;
        return zFinishPos;
    }
    
    public int GetLevel()
    {
        return PlayerPrefs.GetInt(TagManager.PREFS_LEVEL_KEY);
    }
}
