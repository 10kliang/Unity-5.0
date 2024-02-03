using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Transform[] m_SpawnPoints;
    public GameObject m_EnemyPrefab;
    float _interval = 5f;

    float _time;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0f;
        
            
    }

    void Update()
    {
        _time += Time.deltaTime;
        while (_time >= _interval)
        {
            SpawnNewEnemy();
            _time -= _interval;
        }
    }



    void SpawnNewEnemy()
    {

        int randomNumber = Mathf.RoundToInt(Random.Range(0f, m_SpawnPoints.Length - 1));

        Instantiate(m_EnemyPrefab, m_SpawnPoints[randomNumber].transform.position, Quaternion.identity);


    }

}