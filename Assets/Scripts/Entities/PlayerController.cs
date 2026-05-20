using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movement : EntityController
{
    private static readonly string IsCarryingGoldStr = "isCarryingGold";

    public TextMeshProUGUI text;
    public Slider healthSlider;
    public Slider staminaSlider;
    public float stamina;
    public float sprintingSpeed;
    public float staminaUsage;
    public float staminaRestore;

    StairMovementComponent stairComponent;

    Vector2 movement;
    bool isWalking;
    bool isSprinting;
    bool isSprintingButtonPressed;
    bool isCarryingGold;
    float currentStamina;
    float staminaRestoreLastTime = 0;
    float animatorDefaultSpeed;

    void SetHealthSlider()
    {
        currentHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = currentHealth;
    }
    void SetStaminaSlider()
    {
        currentStamina = stamina;
        staminaSlider.maxValue = stamina;
        staminaSlider.value = currentStamina;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        text.SetText("The health is " + currentHealth);
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
        Vector2 finalMovement = ApplyEnvironmentMovement(movement);

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

            HandleSpriteFlip(finalMovement);
        }
        else
            rb.linearVelocity *= 0.9f;

        staminaSlider.value = currentStamina;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Collectible"))
        {
            isCarryingGold = true;

            TakeDamage(10);
            Destroy(collision.gameObject);
            animator.SetBool(IsCarryingGoldStr, isCarryingGold);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
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
