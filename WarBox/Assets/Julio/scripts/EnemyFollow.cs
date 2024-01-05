using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public string playerTag = "Player";
    public string attackAreaTag = "atkare";
    public float speed = 5f;
    public GameObject objetoAInstanciar; // Prefab do tiro
    public float intervaloDeInstancia = 1.5f; // Intervalo entre as instâncias

    private Transform player;
    private float lastInstantiationTime;

    void Start()
    {
        // Encontrar o jogador usando a tag
        player = GameObject.FindGameObjectWithTag(playerTag).transform;

        if (player == null)
        {
            Debug.LogError("Player not found.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calcular a direção para o jogador
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Mover o inimigo na direção do jogador
            transform.Translate(direction * speed * Time.deltaTime);
        }

        // Verificar se é hora de instanciar um objeto
        if (Time.time - lastInstantiationTime > intervaloDeInstancia)
        {
            InstanciarObjeto();
            lastInstantiationTime = Time.time;
        }
    }

    // Chamado quando outro collider entra no trigger
    void OnTriggerEnter(Collider other)
    {
        // Verificar se o objeto no trigger tem a tag 'atkare'
        if (other.CompareTag(attackAreaTag))
        {
            lastInstantiationTime = Time.time;
        }
    }

    // Método para instanciar o objeto (tiro)
    void InstanciarObjeto()
    {
        // Instanciar o tiro desejado
        GameObject novoTiro = Instantiate(objetoAInstanciar, transform.position, Quaternion.identity);

        // Ajustar a rotação do tiro se necessário (por exemplo, olhando para o jogador)
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        novoTiro.transform.up = direction;
    }
}
