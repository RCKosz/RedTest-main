using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [Header("Valor da Vida")]
    public float playerLifeMax = 100f; //vida m�xima
    private float playerLifeCurrent; //vida atual

    [Header("Barra de vida")]
    public Slider slider_Life; //barra de vida

    [Header("Dano desferido")]
    public float dano; //valor de dano
    public float danoSpecial; //valor de dano no especial
    public AudioSource hitSom; //localiza som de dano

    [Header("Convoca Menu")]
    public GameObject menu; //convoca o canvas do Menu

    private PlayerCombat playerCombat; //convoca o script de Combate
    private PlayerController playerController; //convoca o script de Movimenta��o
    private Animator playerAnimator; //convoca o animator do jogador

    void Start()
    {
        // Obt�m o script de combate do jogador
        playerCombat = GetComponent<PlayerCombat>();

        // Obt�m o script de controle do jogador
        playerController = GetComponent<PlayerController>();

        // Obt�m o componente Animator do jogador (presente em um dos filhos do GameObject)
        playerAnimator = GetComponentInChildren<Animator>();

        // Define a vida atual do jogador como o valor m�ximo no in�cio do jogo
        playerLifeCurrent = playerLifeMax;
    }

    void Update()
    {
        // Atualiza a barra de vida do jogador com o valor atual de vida
        slider_Life.value = playerLifeCurrent;

        // Verifica se a vida do jogador chegou a zero
        if (playerLifeCurrent == 0)
        {
            // Desativa o combate do jogador para impedir que ele ataque ap�s morrer
            playerCombat.enabled = false;

            // Desativa o controle do jogador para impedir movimenta��o ap�s a morte
            playerController.enabled = false;

            // Ativa a anima��o de morte do jogador
            playerAnimator.SetTrigger("Death");

            // Ativa o menu de game over ou respawn
            menu.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Verifica se o jogador foi atingido pelo ataque do inimigo (provavelmente uma hitbox de soco)
        if (collision.gameObject.tag == "HandEnemy")
        {
            // Reduz a vida do jogador com base no dano causado pelo inimigo
            playerLifeCurrent -= collision.gameObject.GetComponentInParent<EnemyLife>().dano;

            // Toca o som de impacto ao ser atingido
            hitSom.Play();
        }
    }

}
