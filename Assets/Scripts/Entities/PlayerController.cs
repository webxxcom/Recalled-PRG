using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : EntityController, ITargetable
{
    private static readonly string IsCarryingGoldStr = "isCarryingGold";

    public TextMeshProUGUI text;
    public Slider staminaSlider;
    public float stamina;
    public float sprintingSpeed;
    public float staminaUsage;
    public float staminaRestore;

    Vector2 movement;
    bool isWalking;
    bool isSprinting;
    bool isSprintingButtonPressed;
    bool isCarryingGold;
    float currentStamina;
    float staminaRestoreLastTime = 0;
    float animatorDefaultSpeed;

    public GameObject GameObject => gameObject;

    void SetStaminaSlider()
    {
        currentStamina = stamina;
        staminaSlider.maxValue = stamina;
        staminaSlider.value = currentStamina;
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

    protected override void Start()
    {
        base.Start();

        sprintingSpeed = speed * 2;
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Collectible"))
        {
            isCarryingGold = true;

            Destroy(collision.gameObject);
            animator.SetBool(IsCarryingGoldStr, isCarryingGold);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void HandleFixedUpdate()
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

    // Update is called once per frame
    void Update()
    {
        RestoreStaminaWithTime();
    }
}
