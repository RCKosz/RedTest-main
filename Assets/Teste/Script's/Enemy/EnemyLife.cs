using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("Valor da Vida")]
    public float EnemyLifeMax = 100f; //vida máxima
    public float EnemyLifeCurrent; //vida atual

    [Header("Dano desferido")]
    public float dano; //valor de dano
    public AudioSource hitSom; //localizador do som

    private Enemy_AI enemy; //convoca o script da IA do inimigo
    private Animator enem_animator; //convoca o animator do inimigo

    void Start()
    {
        // Obtém o componente Animator do inimigo (presente em um dos filhos do GameObject)
        enem_animator = GetComponentInChildren<Animator>();

        // Obtém o script de IA do inimigo para poder desativá-lo quando morrer
        enemy = GetComponent<Enemy_AI>();

        // Define a vida atual do inimigo como o valor máximo no início do jogo
        EnemyLifeCurrent = EnemyLifeMax;
    }

    void Update()
    {
        // Verifica se a vida do inimigo chegou a zero
        if (EnemyLifeCurrent == 0)
        {
            // Desativa a IA do inimigo para impedir que ele continue se movendo ou atacando
            enemy.enabled = false;

            // Aciona a animação de morte do inimigo
            enem_animator.SetTrigger("Enem_Death");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Verifica se o inimigo foi atingido pela mão do jogador (provável hitbox do ataque)
        if (collision.tag == "HandPlayer")
        {
            // Reduz a vida do inimigo com base no dano causado pelo jogador
            EnemyLifeCurrent -= collision.gameObject.GetComponentInParent<PlayerLife>().dano;

            // Toca o som de impacto ao ser atingido
            hitSom.Play();
        }

        if (collision.tag == "KickPlayer")
        {
            // Reduz a vida do inimigo com base no dano causado pelo jogador
            EnemyLifeCurrent -= collision.gameObject.GetComponentInParent<PlayerLife>().danoSpecial;

            // Toca o som de impacto ao ser atingido
            hitSom.Play();
        }
    }

}
