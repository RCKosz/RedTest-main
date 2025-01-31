using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Animação")]
    public Animator anim; // Referência ao componente Animator

    [Header("Combate")]
    public bool attack = false; // Flag para indicar se o jogador está atacando
    public float contador = 0; // Contador para sequência de ataques
    public float cooldown = 1; // Tempo de cooldown entre os ataques

    [Header("Combo")]
    public bool specialCombo; //Bool para indicar se o jogador tem a habilidade
    public float timeCombo = 15f; //Tempo crescente do combo
    public float maxTimeCombo = 15f; //Tempo máximo para ativar o combo

    [Header("Barra de Poder")]
    public Slider slider_Power; //barra de poder
    public GameObject icon; //icone do especial

    [Header("Colisores da mão")]
    public GameObject colissionRight;//colisor da mão direita
    public GameObject colissionLeft;//colisor da mão esquerda

    private float nextAttackTime = 0f; //Tempo decorrido desde o último ataque
    private float finalTimeCombo = 2f; //Tempo de execução da animação do combo

    void Start()
    {
        
    }

    void Update()
    {
        // Verifica se o botão de ataque foi pressionado e se o jogador não está atacando no momento
        if (Input.GetButtonDown("Fire1") && !attack)
        {
            contador++; // Incrementa o contador de ataques
            if (contador > 3) // Se o contador ultrapassar 3, reinicia para 1
            {
                contador = 1;
            }
            anim.SetFloat("Combat", contador); // Atualiza o parâmetro "Combat" no Animator para tocar a animação correspondente
            attack = true; // Define que o jogador está atacando
            
            colissionLeft.SetActive(true);//ativa o colisor esquerdo da mão
            colissionRight.SetActive(true);//ativa o colisor direito da mão

        }

        // Se o jogador estiver atacando, conta o tempo para o cooldown
        if (attack)
        {
            nextAttackTime += Time.deltaTime; // Incrementa o tempo decorrido
            if (nextAttackTime >= cooldown) // Verifica se o cooldown terminou
            {
                nextAttackTime = 0f; // Reseta o tempo para o próximo ataque
                attack = false; // Define que o jogador não está mais atacando

                colissionLeft.SetActive(false);//desativa o colisor esquerdo da mão
                colissionRight.SetActive(false);//desativa o colisor direito da mão
            }
        }

        // Atualiza o parâmetro "Attack" no Animator para controlar a transição de animações
        anim.SetBool("Attack", attack);

        // Verifica se o botão de combo especial foi pressionado e se o jogador não está realizando outro combo
        if (Input.GetButtonDown("Fire2") && !specialCombo)
        {
            anim.SetBool("Special", true); // Ativa a animação do ataque especial
            timeCombo = 0f; // Reseta o tempo do combo
            specialCombo = true; // Define que o combo especial está ativo
            icon.SetActive(false);
        }

        // Se o combo especial estiver ativo, atualiza o tempo do combo
        if (specialCombo)
        {
            timeCombo += Time.deltaTime; // Incrementa o tempo do combo com base no tempo real do jogo

            // Desativa a animação do ataque especial após o tempo final do combo
            if (timeCombo >= finalTimeCombo)
            {
                anim.SetBool("Special", false);
            }

            // Finaliza o combo especial após atingir o tempo máximo permitido
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