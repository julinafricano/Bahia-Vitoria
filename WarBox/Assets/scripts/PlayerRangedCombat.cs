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
            if (Input.GetKeyDown(KeyCode.K)) // Mude para o bot�o desejado
            {
                ShootArrow();
                nextFireTime = Time.time + 1f / fireRate;
                SomAttack.Play();
            }
        }
    }

    void ShootArrow()
    {
        // Cria uma inst�ncia da flecha
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

        // Obt�m ou adiciona o componente Rigidbody2D da flecha
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Se n�o houver Rigidbody2D, adiciona um
            rb = arrow.AddComponent<Rigidbody2D>();
        }

        // Obt�m a dire��o da flecha considerando a escala do firePoint
        Vector2 arrowDirection = firePoint.right;

        // Inverte o sprite da flecha se estiver indo para a esquerda
        if (firePoint.localScale.x < 0)
        {
            Vector3 arrowScale = arrow.transform.localScale;
            arrowScale.x *= -1;
            arrow.transform.localScale = arrowScale;
        }

        // Aplica uma for�a para fazer a flecha se mover na dire��o local do firePoint
        rb.AddForce(arrowDirection * (firePoint.localScale.x > 0 ? arrowSpeed : -arrowSpeed), ForceMode2D.Impulse);

        // Destr�i a flecha ap�s um determinado tempo (ajuste conforme necess�rio)
        Destroy(arrow, 5f);
    }

    void OnDrawGizmosSelected()
    {
        // Desenha um gizmo para representar a posi��o de disparo
        if (firePoint != null)
        {
            Gizmos.DrawWireSphere(firePoint.position, 0.1f);
        }
    }
}
