using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnskinrattrap : MonoBehaviour
{
    public GameObject keyOB;
    public GameObject invOB;
    public GameObject pickUpText;
    public AudioSource keySound;

    public bool inReach;


    public Transform[] spawnpoint;
    public GameObject enempyprefab;

    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        invOB.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);

        }
    }


    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            keyOB.SetActive(false);
            keySound.Play();
            invOB.SetActive(true);
            pickUpText.SetActive(false);
            SpawnNewEnemy();
        }


    }

    void SpawnNewEnemy()
    {

        int randomNumber = Mathf.RoundToInt(Random.Range(0f, spawnpoint.Length - 1));
        int count = 10;

        while(count > 0)
        {
            Instantiate(enempyprefab, spawnpoint[randomNumber].transform.position, Quaternion.identity);
            count--;

        }



    }
}
