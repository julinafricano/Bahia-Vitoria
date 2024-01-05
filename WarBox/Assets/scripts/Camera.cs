using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    public Transform player; // Arraste o GameObject do jogador aqui
    public CinemachineVirtualCamera vcam;
    public float minX = -10f; // Defina o valor mínimo de X aqui
    public float maxX = 10f; // Defina o valor máximo de X aqui
    public float minY = -5f; // Defina o valor mínimo de Y aqui
    public float maxY = 5f; // Defina o valor máximo de Y aqui

    private void Start()
    {
        // Atribua o jogador como alvo da câmera
        vcam.Follow = player;
        vcam.LookAt = player;
    }

    private void Update()
    {
        // Limita a posição da câmera
        Vector3 pos = vcam.transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        vcam.transform.position = pos;
    }
}