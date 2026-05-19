using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private static readonly string IsCarryingGoldStr = "isCarryingGold";
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public TextMeshProUGUI text;
    public Slider hpSlider;
    public Slider staminaSlider;
    public int hp;
    public float speed;
    public float stamina;
    public float sprintingSpeed;
    public float staminaUsage;
    public float staminaRestore;

    Vector2 movement;
    bool isWalking;
    bool isSprinting;
    bool isSprintingButtonPressed;
    bool isCarryingGold;
    bool isOnStairs;
    int currentHp;
    float currentSpeed;
    float currentStamina;
    float staminaRestoreLastTime = 0;
    float animatorDefaultSpeed;

    void SetHealthSlider()
    {
        currentHp = hp;
        hpSlider.maxValue = hp;
        hpSlider.value = currentHp;
    }
    void SetStaminaSlider()
    {
        currentStamina = stamina;
        staminaSlider.maxValue = stamina;
        staminaSlider.value = currentStamina;
    }

    void TakeDamage(int damage)
    {
        currentHp -= damage;
        hpSlider.value = currentHp;
        text.SetText("The health is " + currentHp);
    }

    void RestoreStaminaWithTime()
    {
        staminaRestoreLastTime += Time.deltaTime;

        bool canRestoreStamina = currentStamina < stamina
            && !isSprinting
            && !isSprintingButtonPressed;
        if (staminaRestoreLastTime > 0.3 && canRestoreStamina)
        {
            currentStamina += staminaRestore;
            staminaRestoreLastTime = 0;
        }
    }

    void StopSprinting()
    {
        isSprinting = false;
        currentSpeed = speed;
        animator.speed = animatorDefaultSpeed;
    }

    void StartSprinting()
    {
        isSprinting = true;
        currentSpeed = sprintingSpeed;
        animator.speed = animatorDefaultSpeed * 2;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprintingSpeed = speed * 2;
        SetHealthSlider();
        SetStaminaSlider();
        animatorDefaultSpeed = animator.speed;
        currentSpeed = speed;
    }

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        isWalking = movement != Vector2.zero;

        animator.SetBool("isRunning", isWalking);
    }

    private void OnSprint(InputValue value)
    {
        text.SetText("The button is " + (value.isPressed ? "Pressed" : "Released"));

        if (value.isPressed)
        {
            StartSprinting();
            isSprintingButtonPressed = true;
        }
        else
        {
            StopSprinting();
            isSprintingButtonPressed = false;
        }
    }

    private void HandleMovement()
    {
        Vector2 finalMovement = movement;

        if (isOnStairs && Mathf.Abs(finalMovement.x) > 0.01f)
            finalMovement.y += -finalMovement.x * 0.8f;

        if (isSprinting)
        {
            if (currentStamina - staminaUsage <= 0.3)
                StopSprinting();
            else
                currentStamina -= staminaUsage;
        }

        if (isWalking)
        {
            if (isCarryingGold)
                finalMovement /= 2;

            rb.linearVelocity = finalMovement * currentSpeed;

            if (finalMovement.x < 0)
                spriteRenderer.flipX = true;
            if (finalMovement.x > 0)
                spriteRenderer.flipX = false;
        }
        else
            rb.linearVelocity *= 0.9f;

        staminaSlider.value = currentStamina;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
            isOnStairs = true;

        if (collision.CompareTag("Collectible"))
        {
            isCarryingGold = true;

            TakeDamage(10);
            Destroy(collision.gameObject);
            animator.SetBool(IsCarryingGoldStr, isCarryingGold);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
            isOnStairs = false;
        if (collision.CompareTag(IsCarryingGoldStr))
            isCarryingGold = false;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        RestoreStaminaWithTime();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
