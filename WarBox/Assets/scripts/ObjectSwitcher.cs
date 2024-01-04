using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;

    void Start()
    {
        // Certifica-se de que apenas um objeto está ativo no início
        ActivateObject(object1);
        DeactivateObject(object2);
    }

    void Update()
    {
        // Verifica a entrada do teclado para alternar entre os objetos
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateObject(object1);
            DeactivateObject(object2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateObject(object2);
            DeactivateObject(object1);
        }
    }

    void ActivateObject(GameObject obj)
    {
        // Ativa o objeto
        obj.SetActive(true);
    }

    void DeactivateObject(GameObject obj)
    {
        // Desativa o objeto
        obj.SetActive(false);
    }
}