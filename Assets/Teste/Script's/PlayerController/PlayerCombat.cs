using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Anima��o")]
    public Animator anim; // Refer�ncia ao componente Animator

    [Header("Combate")]
    public bool attack = false; // Flag para indicar se o jogador est� atacando
    public float contador = 0; // Contador para sequ�ncia de ataques
    public float cooldown = 1; // Tempo de cooldown entre os ataques

    [Header("Combo")]
    public bool specialCombo; //Bool para indicar se o jogador tem a habilidade
    public float timeCombo = 15f; //Tempo crescente do combo
    public float maxTimeCombo = 15f; //Tempo m�ximo para ativar o combo

    [Header("Barra de Poder")]
    public Slider slider_Power; //barra de poder
    public GameObject icon; //icone do especial

    [Header("Colisores da m�o")]
    public GameObject colissionRight;//colisor da m�o direita
    public GameObject colissionLeft;//colisor da m�o esquerda

    private float nextAttackTime = 0f; //Tempo decorrido desde o �ltimo ataque
    private float finalTimeCombo = 2f; //Tempo de execu��o da anima��o do combo

    void Start()
    {
        
    }

    void Update()
    {
        // Verifica se o bot�o de ataque foi pressionado e se o jogador n�o est� atacando no momento
        if (Input.GetButtonDown("Fire1") && !attack)
        {
            contador++; // Incrementa o contador de ataques
            if (contador > 3) // Se o contador ultrapassar 3, reinicia para 1
            {
                contador = 1;
            }
            anim.SetFloat("Combat", contador); // Atualiza o par�metro "Combat" no Animator para tocar a anima��o correspondente
            attack = true; // Define que o jogador est� atacando
            
            colissionLeft.SetActive(true);//ativa o colisor esquerdo da m�o
            colissionRight.SetActive(true);//ativa o colisor direito da m�o

        }

        // Se o jogador estiver atacando, conta o tempo para o cooldown
        if (attack)
        {
            nextAttackTime += Time.deltaTime; // Incrementa o tempo decorrido
            if (nextAttackTime >= cooldown) // Verifica se o cooldown terminou
            {
                nextAttackTime = 0f; // Reseta o tempo para o pr�ximo ataque
                attack = false; // Define que o jogador n�o est� mais atacando

                colissionLeft.SetActive(false);//desativa o colisor esquerdo da m�o
                colissionRight.SetActive(false);//desativa o colisor direito da m�o
            }
        }

        // Atualiza o par�metro "Attack" no Animator para controlar a transi��o de anima��es
        anim.SetBool("Attack", attack);

        // Verifica se o bot�o de combo especial foi pressionado e se o jogador n�o est� realizando outro combo
        if (Input.GetButtonDown("Fire2") && !specialCombo)
        {
            anim.SetBool("Special", true); // Ativa a anima��o do ataque especial
            timeCombo = 0f; // Reseta o tempo do combo
            specialCombo = true; // Define que o combo especial est� ativo
            icon.SetActive(false);
        }

        // Se o combo especial estiver ativo, atualiza o tempo do combo
        if (specialCombo)
        {
            timeCombo += Time.deltaTime; // Incrementa o tempo do combo com base no tempo real do jogo

            // Desativa a anima��o do ataque especial ap�s o tempo final do combo
            if (timeCombo >= finalTimeCombo)
            {
                anim.SetBool("Special", false);
            }

            // Finaliza o combo especial ap�s atingir o tempo m�ximo permitido
            if (timeCombo >= maxTimeCombo)
            {
                specialCombo = false;
                icon.SetActive(true);
            }
        }

        // Atualiza a barra de energia (slider) com o tempo do combo especial
        slider_Power.value = timeCombo;
    }
}