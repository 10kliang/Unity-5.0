using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform = default;
    private Vector3 _orignalPosOfCam = default;
    public float shakeFrequency = default;

    private void Start()
    {
        _orignalPosOfCam = cameraTransform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            CameShake();

        else if (Input.GetKeyUp(KeyCode.W))
            StopShake();
    }

    private void CameShake()
    {
        cameraTransform.position = _orignalPosOfCam + Random.insideUnitSphere * shakeFrequency;
    }

    private void StopShake()
    {
        cameraTransform.position = _orignalPosOfCam;
    }
}   

