using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float rotationAmount = 4f;
    public float rotationSpeed = 5f;
    public float jumpScaleMultiplier = 1.5f;
    public float jumpDuration = 1f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool isJumping;
    private int health = 5;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        // Restrição para tomar dano apenas a cada 0.5 segundo
        if (!CanTakeDamage())
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            UpdateRotation();
        }
        else
        {
            ReturnToZeroRotation();
        }

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (Input.GetKey(KeyCode.A))
        {
            Move(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(1);
        }

        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);

        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            StartJump();
        }

        if (isJumping)
        {
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DanoInimigo") && CanTakeDamage())
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            // Implemente a lógica de derrota (game over) aqui
            Debug.Log("Game Over!");
        }
        else
        {
            // Implemente lógica adicional ao tomar dano, se necessário
            Debug.Log("Vida restante: " + health);

            // Faz o jogador piscar de vermelho
            StartCoroutine(FlashRed());
        }
    }

    private bool CanTakeDamage()
    {
        // Verifica se o jogador pode tomar dano
        return (Time.time - lastDamageTime) >= damageCooldown;
    }

    private float lastDamageTime = 0f;
    private float damageCooldown = 0.5f;

    private void StartJump()
    {
        isJumping = true;
        StartCoroutine(SmoothScaleChange(originalScale, originalScale * jumpScaleMultiplier, jumpDuration));
        Invoke("EndJump", jumpDuration);
    }

    private void Jump()
    {
        // Implemente lógica adicional se necessário
    }

    private void EndJump()
    {
        isJumping = false;
        // Restaura a escala ao final do pulo
        transform.localScale = originalScale;
    }

    private IEnumerator SmoothScaleChange(Vector3 startScale, Vector3 targetScale, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Certifique-se de que a escala final seja exatamente a escala original
        transform.localScale = targetScale;
    }

    private void UpdateRotation()
    {
        float oscillation = Mathf.Sin(Time.time * 5f) * rotationAmount;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, oscillation);
    }

    private void ReturnToZeroRotation()
    {
        float targetRotationZ = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, 0f, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetRotationZ);
    }

    private void Move(int direction)
    {
        // Move o jogador para a esquerda ou direita e inverte a escala
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private IEnumerator FlashRed()
    {
        // Muda a cor do sprite para vermelho por um curto período
        spriteRenderer.color = Color.red;

        // Aguarda um curto período
        yield return new WaitForSeconds(0.2f);

        // Retorna à cor original
        spriteRenderer.color = Color.white;

        // Atualiza o tempo do último dano
        lastDamageTime = Time.time;
    }
}