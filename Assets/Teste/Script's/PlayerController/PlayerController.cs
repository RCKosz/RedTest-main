using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movimentação")]
    public float walkSpeed = 4f; //velocidade da caminhada
    public Transform rotacionSimples; //faz a movimentação simples (ala club penguim)

    [Header("Verificação do Chão")]
    public Transform soloTarget; //verifica o chão
    public CharacterController controller; //determina o personagem controlável
    public float sphereCheckRadius = 0.2f; //controla o raio da esfera

    [Header("Extras")]
    public Animator anim; // controla animação

    private bool isGrounded; //variável para saber se tá no chão


    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(soloTarget.position, sphereCheckRadius, 6); // Verifica o chão

        float x = Input.GetAxis("Vertical"); // Obtém a entrada de X
        float z = Input.GetAxis("Horizontal"); // Obtém a entrada de Z

        Vector3 movement = -transform.right * x + transform.forward * z; // Calcula o movimento do vetor do personagem

        // Movimenta o personagem
        controller.Move(movement * walkSpeed * Time.deltaTime);

        // Verifica se há movimento (x ou z diferentes de 0)
        if (movement != Vector3.zero)
        {
            anim.SetBool("Movement", true); // Ativa a animação
        }
        else
        {
            anim.SetBool("Movement", false); // Desativa a animação
        }
        // Verifica a direção do movimento do jogador e ajusta a rotação do objeto correspondente
        if (Input.GetAxis("Horizontal") > 0) // Se o jogador está movendo para frente
        {
            rotacionSimples.rotation = Quaternion.Euler(0, 0, 0); // Rotaciona para frente
        }
        else if (Input.GetAxis("Horizontal") < 0) // Se o jogador está movendo para trás
        {
            rotacionSimples.rotation = Quaternion.Euler(0, 180, 0); // Rotaciona para trás
        }
        else if (Input.GetAxis("Vertical") < 0) // Se o jogador está movendo para a direita
        {
            rotacionSimples.rotation = Quaternion.Euler(0, 90, 0); // Rotaciona para a direita
        }
        else if (Input.GetAxis("Vertical") > 0) // Se o jogador está movendo para a esquerda
        {
            rotacionSimples.rotation = Quaternion.Euler(0, -90, 0); // Rotaciona para a esquerda
        }

    }
}