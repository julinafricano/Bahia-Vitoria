using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float rotationSpeed = 360f; // Velocidade de rota��o
    public float attackRate = 2f; // Ataques por segundo

    private float nextAttackTime = 0f;
    private bool isRotating = false;
    private float rotationAmount = 0f;
    public float tempopulo = 1f;

    public AudioClip Som;
    public AudioSource SomAttack;

    void Start() {
        SomAttack = GetComponent<AudioSource>();
        SomAttack.clip = Som;
    }

    void Update()
    {

        tempopulo = tempopulo + Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && tempopulo >= 1)
        {
            tempopulo = 0f;
        }
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J) && tempopulo >= 1) // Mude para o bot�o desejado
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                SomAttack.Play();
            }
        }

        if (isRotating)
        {
            RotateSword();
        }
    }

    void Attack()
    {
        // Detecta inimigos na �rea de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Aplica dano aos inimigos atingidos
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // Adicione l�gica adicional aqui, como causar dano ao inimigo
            EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Se o inimigo tiver um script EnemyHealth, causa dano
                DealDamage(enemyHealth);
            }
            else
            {
                // Se n�o tiver um script EnemyHealth, apenas imprime no console
                Debug.Log("Atacou: " + enemyCollider.name);
            }
        }

        // Inicia a rota��o
        isRotating = true;
    }

    void DealDamage(EnemyHealth enemyHealth)
    {
        // L�gica para causar dano ao inimigo
        enemyHealth.TakeDamage(attackDamage);
        Debug.Log("Causou " + attackDamage + " de dano ao inimigo.");
    }

    void RotateSword()
    {
        // Gira a espada como uma roda
        float rotationStep = rotationSpeed * Time.deltaTime;
        rotationAmount += rotationStep;

        // Reseta a rota��o para o in�cio ap�s 360 graus
        if (rotationAmount >= 360f)
        {
            rotationAmount = 0f;
            isRotating = false;
        }

        // Ajusta a rota��o Z a cada quadro
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotationAmount);
    }

    void OnDrawGizmosSelected()
    {
        // Desenha um gizmo para representar a �rea de ataque
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}