using UnityEngine;

public class PlayerRangedCombat : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;
    public float arrowSpeed = 10f;
    public float fireRate = 2f;

    private float nextFireTime = 0f;

    public AudioClip Som;
    public AudioSource SomAttack;

    void Update()
    {
        SomAttack = GetComponent<AudioSource>();
        SomAttack.clip = Som;
        HandleInput();
    }

    void HandleInput()
    {
        if (Time.time >= nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.K)) // Mude para o botão desejado
            {
                ShootArrow();
                nextFireTime = Time.time + 1f / fireRate;
                SomAttack.Play();
            }
        }
    }

    void ShootArrow()
    {
        // Cria uma instância da flecha
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

        // Obtém ou adiciona o componente Rigidbody2D da flecha
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Se não houver Rigidbody2D, adiciona um
            rb = arrow.AddComponent<Rigidbody2D>();
        }

        // Obtém a direção da flecha considerando a escala do firePoint
        Vector2 arrowDirection = firePoint.right;

        // Inverte o sprite da flecha se estiver indo para a esquerda
        if (firePoint.localScale.x < 0)
        {
            Vector3 arrowScale = arrow.transform.localScale;
            arrowScale.x *= -1;
            arrow.transform.localScale = arrowScale;
        }

        // Aplica uma força para fazer a flecha se mover na direção local do firePoint
        rb.AddForce(arrowDirection * (firePoint.localScale.x > 0 ? arrowSpeed : -arrowSpeed), ForceMode2D.Impulse);

        // Destrói a flecha após um determinado tempo (ajuste conforme necessário)
        Destroy(arrow, 5f);
    }

    void OnDrawGizmosSelected()
    {
        // Desenha um gizmo para representar a posição de disparo
        if (firePoint != null)
        {
            Gizmos.DrawWireSphere(firePoint.position, 0.1f);
        }
    }
}
