using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int arrowDamage = 10; // Dano causado pela flecha

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a flecha colidiu com um inimigo
        if (collision.CompareTag("Enemy"))
        {
            // Obt�m o componente de sa�de do inimigo
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            // Causa dano ao inimigo se tiver um componente de sa�de
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(arrowDamage);
            }

            // Destroi a flecha ap�s atingir um inimigo (ajuste conforme necess�rio)
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player") && !collision.CompareTag("Ground") && !collision.CompareTag("Untagged")) // Se a flecha n�o colidir com o jogador, destroi a flecha ao colidir com outros objetos
        {
            Destroy(gameObject);
        }
    }
}
