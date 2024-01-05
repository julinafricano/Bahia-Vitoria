using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 10f; // Dist�ncia m�xima permitida

    private Vector3 startPosition;

    void Start()
    {
        // Armazenar a posi��o inicial do tiro
        startPosition = transform.position;

        // Destruir o tiro ap�s 2 segundos
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        // Mover o tiro para frente
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Verificar a dist�ncia percorrida
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);

        // Destruir o tiro se atingir a dist�ncia m�xima
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    // M�todo chamado quando o tiro entra em um collider (trigger ou n�o)
    void OnTriggerEnter(Collider other)
    {
        // Destruir o tiro ao colidir com qualquer objeto
        Destroy(gameObject);
    }
}
