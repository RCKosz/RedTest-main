using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movimenta��o")]
    public float walkSpeed = 4f; //velocidade da caminhada
    public Transform rotacionSimples; //faz a movimenta��o simples (ala club penguim)

    [Header("Verifica��o do Ch�o")]
    public Transform soloTarget; //verifica o ch�o
    public CharacterController controller; //determina o personagem control�vel
    public float sphereCheckRadius = 0.2f; //controla o raio da esfera

    [Header("Extras")]
    public Animator anim; // controla anima��o

    private bool isGrounded; //vari�vel para saber se t� no ch�o


    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(soloTarget.position, sphereCheckRadius, 6); // Verifica o ch�o

        float x = Input.GetAxis("Vertical"); // Obt�m a entrada de X
        float z = Input.GetAxis("Horizontal"); // Obt�m a entrada de Z

        Vector3 movement = -transform.right * x + transform.forward * z; // Calcula o movimento do vetor do personagem

        // Movimenta o personagem
        controller.Move(movement * walkSpeed * Time.deltaTime);

        // Verifica se h� movimento (x ou z diferentes de 0)
        if (movement != Vector3.zero)
        {
            anim.SetBool("Movement", true); // Ativa a anima��o
        }
        else
        {
            anim.SetBool("Movement", false); // Desativa a anima��o
        }
        // Verifica a dire��o do movimento do jogador e ajusta a rota��o do objeto correspondente
        if (Input.GetAxis("Horizontal") > 0) // Se o jogador est� movendo para frente
        {
            rotacionSimples.rotation = Quaternion.Euler(0, 0, 0); // Rotaciona para frente
        }
        else if (Input.GetAxis("Horizontal") < 0) // Se o jogador est� movendo para tr�s
        {
            rotacionSimples.rotation = Quaternion.Euler(0, 180, 0); // Rotaciona para tr�s
        }
        else if (Input.GetAxis("Vertical") < 0) // Se o jogador est� movendo para a direita
        {
            rotacionSimples.rotation = Quaternion.Euler(0, 90, 0); // Rotaciona para a direita
        }
        else if (Input.GetAxis("Vertical") > 0) // Se o jogador est� movendo para a esquerda
        {
            rotacionSimples.rotation = Quaternion.Euler(0, -90, 0); // Rotaciona para a esquerda
        }

    }
}