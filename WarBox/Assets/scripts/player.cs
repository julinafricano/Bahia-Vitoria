using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int vida = 3;
    public float velocidade = 5.0f;
    private float tempoA;
    private float tempoB;
    public Animator anim;
    private float tempoMortal;
  
    public SpriteRenderer sprite;
    public bool morreu;
    public bool ultimoMoveA;

    public bool ultimoMoveD;

    public bool matouBoss1;
    public bool matouBoss2;


    private void Start()
    {
        anim = GetComponent<Animator>();
        velocidade = 5f;
    }
    void Update()
    {
        tempoMortal = tempoMortal + Time.deltaTime;
        tempoA = tempoA + Time.deltaTime;
        tempoB = tempoB + Time.deltaTime;

        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(movimentoHorizontal, movimentoVertical, 0.0f);

        movimento.Normalize();

        transform.Translate(movimento * velocidade * Time.deltaTime);

    }

}