using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalJumpScare : MonoBehaviour
{
    public GameObject FlashImg;
    public AudioSource Sound;
    public float duration = 4f;
    public GameObject EndCam;
    public GameObject EndScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayJumpScare());
    }

    IEnumerator PlayJumpScare()
    {
        // Enable flash image and play sound
        FlashImg.SetActive(true);
        Sound.Play();

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Disable flash image, sound, and the camera
        FlashImg.SetActive(false);
        Sound.Stop();
        gameObject.SetActive(false); // Disable the camera
        EndCam.SetActive(true);
        EndScreen.SetActive(true);
    }
}
