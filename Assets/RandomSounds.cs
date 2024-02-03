using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBird : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float frequencyMin;
    [SerializeField] private float frequencyMax;


    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(SC_FPSController.isGameOver == false)
        {
            StartCoroutine(RandomPlayer());
        }
        else
        {
            audioSource.Stop();
        }




    }

    private IEnumerator RandomPlayer()
    {
        float waitTime;
        while (true)
        {
            waitTime = Random.Range(frequencyMin, frequencyMax);
            yield return new WaitForSeconds(waitTime);
            PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        int soundIdx = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[soundIdx]);
    }

}

