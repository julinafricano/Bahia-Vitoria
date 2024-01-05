using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 10f; // Distância máxima permitida

    private Vector3 startPosition;

    void Start()
    {
        // Armazenar a posição inicial do tiro
        startPosition = transform.position;

        // Destruir o tiro após 2 segundos
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        // Mover o tiro para frente
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Verificar a distância percorrida
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);

        // Destruir o tiro se atingir a distância máxima
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    // Método chamado quando o tiro entra em um collider (trigger ou não)
    void OnTriggerEnter(Collider other)
    {
        // Destruir o tiro ao colidir com qualquer objeto
        Destroy(gameObject);
    }
}
