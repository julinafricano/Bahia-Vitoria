using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public string playerTag = "Player";
    public string attackAreaTag = "atkare";
    public float speed = 5f;
    public GameObject objetoAInstanciar; // Prefab do tiro
    public float intervaloDeInstancia = 1.5f; // Intervalo entre as inst�ncias
    public float distanciaMinimaParaAtirar = 8f; // Ajuste para a dist�ncia desejada

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
            // Calcular a dist�ncia para o jogador
            float distanciaParaJogador = Vector3.Distance(transform.position, player.position);

            // Verificar se � hora de instanciar um objeto e se a dist�ncia � maior ou igual � dist�ncia m�nima para atirar
            if (Time.time - lastInstantiationTime > intervaloDeInstancia && distanciaParaJogador <= distanciaMinimaParaAtirar)
            {
                InstanciarObjeto();
                lastInstantiationTime = Time.time;
            }

            // Calcular a dire��o para o jogador
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Mover o inimigo na dire��o do jogador
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }


    // M�todo para instanciar o objeto (tiro)
    void InstanciarObjeto()
    {
        // Instanciar o tiro desejado
        GameObject novoTiro = Instantiate(objetoAInstanciar, transform.position, Quaternion.identity);

        // Ajustar a rota��o do tiro se necess�rio (por exemplo, olhando para o jogador)
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        novoTiro.transform.up = direction;
    }
}
