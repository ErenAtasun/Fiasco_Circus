using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket h�z�
    public float maxHoldTime = 3f; // Maksimum bas�l� tutma s�resi
    public float stopDuration = 1f; // Durma s�resi

    Animator playerAnimator;

    private float holdTime = 0f; // Bas�l� tutma s�resi �l��m�
    private bool isHolding = false; // Tu� bas�l� m� kontrol�

    private Rigidbody2D rb;

    
    public enum PlayerState
    {
        Moving,
        Stop,
    }

    public PlayerState currentPlayerState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Tu�a bas�l� tutma s�resini �l�
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            holdTime += Time.deltaTime;

            // Bas�l� tutma s�resi kontrol�
            if (holdTime > maxHoldTime && !isHolding)
            {
                isHolding = true;
                Debug.Log("3tiems");
                StartCoroutine(StopPlayer());
            }
        }
        else
        {
            // Tu� b�rak�ld���nda s�reyi s�f�rla
            holdTime = 0f;
            isHolding = false;
        }

        if (currentPlayerState != PlayerState.Stop)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
    }

    


    // Oyuncuyu belirli bir s�re durdurma fonksiyonu
    IEnumerator StopPlayer()
    {
        currentPlayerState = PlayerState.Stop;
        rb.velocity = Vector2.zero;
        playerAnimator.SetBool("Fall", true);
        yield return new WaitForSeconds(stopDuration);
        Debug.Log("5tiems");
        currentPlayerState = PlayerState.Moving;
        playerAnimator.SetBool("Fall", false);
        holdTime = 0f;
        isHolding = false;
    }
}
