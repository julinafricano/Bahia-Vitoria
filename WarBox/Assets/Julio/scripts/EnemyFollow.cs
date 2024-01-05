using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public string playerTag = "Player";
    public string attackAreaTag = "atkare";
    public float speed = 5f;
    public GameObject objetoAInstanciar; // Prefab do tiro
    public float intervaloDeInstancia = 1.5f; // Intervalo entre as instâncias
    public float distanciaMinimaParaAtirar = 8f; // Ajuste para a distância desejada

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
            // Calcular a distância para o jogador
            float distanciaParaJogador = Vector3.Distance(transform.position, player.position);

            // Verificar se é hora de instanciar um objeto e se a distância é maior ou igual à distância mínima para atirar
            if (Time.time - lastInstantiationTime > intervaloDeInstancia && distanciaParaJogador <= distanciaMinimaParaAtirar)
            {
                InstanciarObjeto();
                lastInstantiationTime = Time.time;
            }

            // Calcular a direção para o jogador
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Mover o inimigo na direção do jogador
            transform.Translate(direction * speed * Time.deltaTime);
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
