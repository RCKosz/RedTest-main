using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [Header("Anima��o")]
    public Animator enemy_Anim; //Anima��o

    [Header("Movimenta��o")]
    public float enemy_WalkSpeed = 4f; //caminhada
    public Transform enemy_Rotation; //rota��o

    [Header("Ataques")]
    public bool enemy_Punch; //relacionado aos golpes
    public Transform playerLocation; //Localizador do jogador
    public GameObject enemy_PunchRight; //verifica a esfera de soco direito
    public GameObject enemy_PunchLeft; //verifica a esfera de soco esquerdo
    public float enemy_Contador = 0; //quantidades de soco
    public float enemy_cooldown = 1; //tempo entre socos

    private float enemy_NextAttack = 0f; //tempo para os pr�ximos ataques


    void Start()
    {
        
    }

    void Update()
    {
        // Se o inimigo n�o estiver atacando, ele se move em dire��o ao jogador
        if (!enemy_Punch)
        {
            // Calcula a dire��o normalizada para o jogador
            Vector3 playerTarget = (playerLocation.position - transform.position).normalized;

            // Move o inimigo na dire��o do jogador com a velocidade definida
            transform.position += playerTarget * enemy_WalkSpeed * Time.deltaTime;

            // Faz o inimigo olhar para o jogador
            transform.LookAt(playerLocation);

            // Ativa a anima��o de caminhada do inimigo
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
        // Verifica se o objeto dentro do trigger � o jogador
        if (other.tag == "Player")
        {
            // Se o inimigo ainda n�o est� atacando, ele inicia o ataque
            if (!enemy_Punch)
            {
                // Incrementa o contador de ataques para variar a anima��o
                enemy_Contador++;
                if (enemy_Contador > 3) // Se ultrapassar 3, reinicia para 1
                {
                    enemy_Contador = 1;
                }

                // Ativa os objetos representando os punhos do inimigo (possivelmente hitboxes)
                enemy_PunchLeft.SetActive(true);
                enemy_PunchRight.SetActive(true);

                // Define a anima��o de soco com base no contador
                enemy_Anim.SetFloat("Enem_Punch", enemy_Contador);
                enemy_Anim.SetBool("Enem_Attack", true);

                // Define que o inimigo est� atacando
                enemy_Punch = true;
            }

            // Gerencia o cooldown entre os ataques
            if (enemy_Punch)
            {
                enemy_NextAttack += Time.deltaTime; // Incrementa o tempo de espera entre ataques

                // Se o tempo do pr�ximo ataque for maior ou igual ao cooldown, reseta os ataques
                if (enemy_NextAttack >= enemy_cooldown)
                {
                    // Desativa os punhos do inimigo
                    enemy_PunchLeft.SetActive(false);
                    enemy_PunchRight.SetActive(false);

                    // Desativa a anima��o de ataque
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
        // Quando o jogador sai da �rea de ataque, o inimigo para de atacar
        enemy_Punch = false;
        enemy_Anim.SetBool("Enem_Attack", false);
        enemy_NextAttack = 0f; // Reseta o tempo para o pr�ximo ataque
    }


}
