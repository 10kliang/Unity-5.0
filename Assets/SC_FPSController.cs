using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float tempwalkingSpeed = 7.5f;
    public float temprunningSpeed = 11.5f;
    public float initialwalkingSpeed = 7.5f;
    public float initialrunningSpeed = 11.5f;
    public float crouchingwalkingSpeed = 1f;
    public float crouchingrunningSpeed = 2f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public CharacterController playerController;
    public float crouchSpeed, normalHeight, crouchHeight;
    public Vector3 offset;
    public Transform player;
    bool crouching;

    public static int playerHP = 100;
    public static bool isGameOver;
    public TextMeshProUGUI playerHPText;
    public GameObject bloodyScreen;
    public GameObject gameOverUI;


    public int damageAmount = 10;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;



    public AudioSource footsteps;
    public AudioSource heartbeat;
    public AudioSource hit;
    public AudioSource death;



    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isGameOver = false;

    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? temprunningSpeed : tempwalkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? temprunningSpeed : tempwalkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            footsteps.Play();

        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            footsteps.Stop();
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);


            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                crouching = !crouching;
            }
            if (crouching == true)
            {
                
                playerController.height = playerController.height - crouchSpeed * Time.deltaTime;
                temprunningSpeed = crouchingrunningSpeed;
                tempwalkingSpeed = crouchingwalkingSpeed;
                if (playerController.height <= crouchHeight)
                {
                    playerController.height = crouchHeight;
                }
            }
            if (crouching == false)
            {
                playerController.height = playerController.height + crouchSpeed * Time.deltaTime;
                tempwalkingSpeed = initialwalkingSpeed;
                temprunningSpeed = initialrunningSpeed;
                if (playerController.height < normalHeight)
                {
                    player.position = player.position + offset * Time.deltaTime;
                }
                if (playerController.height >= normalHeight)
                {
                    playerController.height = normalHeight;
                }
            }


        }

        


        
        if (isGameOver)
        {
            playerHPText.text = "Health: 0";
            heartbeat.Stop();
            PlayerDead();
        }
        else
            playerHPText.text = "Health: " + playerHP;
    }

    private void PlayerDead()
    {
        canMove = false;

        GetComponentInChildren<Animator>().enabled = true;

        GetComponent<ScreenFader>().StartFade();

        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
    }

    public void TakeDamage(int damageAmount)
    {
        playerHP -= damageAmount;
        if (playerHP <= 100 && playerHP > 80)
        {
            bloodyScreen.GetComponent<Image>().color = new Color32(61, 45, 45, 50);
        }


        if (playerHP <= 80 && playerHP > 60)
        {
            bloodyScreen.GetComponent<Image>().color = new Color32(88, 62, 45, 255);
        }

        if (playerHP <= 60 && playerHP > 40)
        {
            bloodyScreen.GetComponent<Image>().color = new Color32(142, 71, 75, 255);
        }



        if (playerHP <= 40 && playerHP > 20)
        {
            bloodyScreen.GetComponent<Image>().color = new Color32(185, 65, 71, 255);

            heartbeat.Play();
        }

        if (playerHP <= 20)
        {
            bloodyScreen.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }


        if (playerHP <= 0)
        {
            isGameOver = true;
            death.Play();
        }

        else
        {
            StartCoroutine(BloodyScreenEffect());
        }
    }

    private IEnumerator BloodyScreenEffect()
    {
        if(bloodyScreen.activeInHierarchy == false)
            bloodyScreen.SetActive(true);

        var image = bloodyScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;


        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (bloodyScreen.activeInHierarchy)
            bloodyScreen.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SkinRat")
        {
            
            hit.Play();

            
            TakeDamage(damageAmount);
        }
    }


}
