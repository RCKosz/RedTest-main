using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [Header("Animação")]
    public Animator enemy_Anim; //Animação

    [Header("Movimentação")]
    public float enemy_WalkSpeed = 4f; //caminhada
    public Transform enemy_Rotation; //rotação

    [Header("Ataques")]
    public bool enemy_Punch; //relacionado aos golpes
    public Transform playerLocation; //Localizador do jogador
    public GameObject enemy_PunchRight; //verifica a esfera de soco direito
    public GameObject enemy_PunchLeft; //verifica a esfera de soco esquerdo
    public float enemy_Contador = 0; //quantidades de soco
    public float enemy_cooldown = 1; //tempo entre socos

    private float enemy_NextAttack = 0f; //tempo para os próximos ataques


    void Start()
    {
        
    }

    void Update()
    {
        // Se o inimigo não estiver atacando, ele se move em direção ao jogador
        if (!enemy_Punch)
        {
            // Calcula a direção normalizada para o jogador
            Vector3 playerTarget = (playerLocation.position - transform.position).normalized;

            // Move o inimigo na direção do jogador com a velocidade definida
            transform.position += playerTarget * enemy_WalkSpeed * Time.deltaTime;

            // Faz o inimigo olhar para o jogador
            transform.LookAt(playerLocation);

            // Ativa a animação de caminhada do inimigo
            enemy_Anim.SetBool("Enem_Walk", true);
        }
        else
        {
            // Se o inimigo estiver atacando, ele para de andar
            enemy_Anim.SetBool("Enem_Walk", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Verifica se o objeto dentro do trigger é o jogador
        if (other.tag == "Player")
        {
            // Se o inimigo ainda não está atacando, ele inicia o ataque
            if (!enemy_Punch)
            {
                // Incrementa o contador de ataques para variar a animação
                enemy_Contador++;
                if (enemy_Contador > 3) // Se ultrapassar 3, reinicia para 1
                {
                    enemy_Contador = 1;
                }

                // Ativa os objetos representando os punhos do inimigo (possivelmente hitboxes)
                enemy_PunchLeft.SetActive(true);
                enemy_PunchRight.SetActive(true);

                // Define a animação de soco com base no contador
                enemy_Anim.SetFloat("Enem_Punch", enemy_Contador);
                enemy_Anim.SetBool("Enem_Attack", true);

                // Define que o inimigo está atacando
                enemy_Punch = true;
            }

            // Gerencia o cooldown entre os ataques
            if (enemy_Punch)
            {
                enemy_NextAttack += Time.deltaTime; // Incrementa o tempo de espera entre ataques

                // Se o tempo do próximo ataque for maior ou igual ao cooldown, reseta os ataques
                if (enemy_NextAttack >= enemy_cooldown)
                {
                    // Desativa os punhos do inimigo
                    enemy_PunchLeft.SetActive(false);
                    enemy_PunchRight.SetActive(false);

                    // Desativa a animação de ataque
                    enemy_Anim.SetBool("Enem_Attack", false);

                    // Reseta o tempo de ataque e permite que o inimigo ataque novamente
                    enemy_NextAttack = 0f;
                    enemy_Punch = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quando o jogador sai da área de ataque, o inimigo para de atacar
        enemy_Punch = false;
        enemy_Anim.SetBool("Enem_Attack", false);
        enemy_NextAttack = 0f; // Reseta o tempo para o próximo ataque
    }


}
