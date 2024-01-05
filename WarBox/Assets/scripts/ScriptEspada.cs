using UnityEngine;

public class ScriptEspada : MonoBehaviour
{
    private bool inclining = false;
    private float elapsedTime = 0f;
    private float inclineDuration = 0.6f; // 0.6 segundos
    private Vector3 pivotPoint; // Ponto de pivô

    // Adicione colisores
    private BoxCollider2D swordCollider;
    public GameObject character; // Referência ao objeto do personagem
    public GameObject enemy; // Referência ao objeto do inimigo

    void Start()
    {
        // Define o ponto de pivô como uma posição abaixo do objeto
        pivotPoint = transform.position - new Vector3(0f, 0.5f, 0f);

        // Adiciona e configura o colisor da espada
        swordCollider = gameObject.AddComponent<BoxCollider2D>();
        swordCollider.isTrigger = true;
        swordCollider.offset = new Vector2(0f, -0.25f);
        swordCollider.size = new Vector2(1f, 0.5f);

        // Adiciona colisor ao personagem (ajuste conforme necessário)
        BoxCollider2D characterCollider = character.AddComponent<BoxCollider2D>();
        characterCollider.size = new Vector2(1f, 1f);

        // Adiciona colisor ao inimigo (ajuste conforme necessário)
        BoxCollider2D enemyCollider = enemy.AddComponent<BoxCollider2D>();
        enemyCollider.size = new Vector2(1f, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !inclining)
        {
            StartIncline();
        }

        if (inclining)
        {
            InclineObjectDown();
        }
    }

    void StartIncline()
    {
        inclining = true;
        elapsedTime = 0f;
    }

    void InclineObjectDown()
    {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / inclineDuration;
        float inclinationAngle = Mathf.Lerp(0f, -60f, t); // Inclinação de 0 a -60 graus

        // Calcula a posição do objeto em relação ao ponto de pivô
        Vector3 newPosition = Quaternion.Euler(0f, 0f, inclinationAngle) * (transform.position - pivotPoint) + pivotPoint;

        // Ajusta a posição do objeto
        transform.position = newPosition;

        if (elapsedTime >= inclineDuration)
        {
            inclining = false;
            Invoke("ResetInclination", 1f); // Aguarda 1 segundo antes de voltar à posição inicial
        }
    }

    void ResetInclination()
    {
        // Volta à posição inicial
        transform.position = pivotPoint;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == enemy)
        {
            Debug.Log("Inimigo atingido!"); // Adicione sua lógica de atingir o inimigo aqui
        }
    }
}