using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        // Destruir o tiro após 2 segundos
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        // Mover o tiro para frente
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // Método chamado quando o tiro entra em um collider (não trigger)
    void OnCollisionEnter(Collision collision)
    {
        // Destruir o tiro ao colidir com qualquer objeto
        Destroy(gameObject);
    }
}
