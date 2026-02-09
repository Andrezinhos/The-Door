using UnityEngine;

public class MobilePlayer : MonoBehaviour
{
    [Header("Joysticks")]
    public Joystick moveJoystick;   // Arraste o joystick da esquerda aqui
    public Joystick lookJoystick;   // Arraste o joystick da direita aqui

    [Header("Configurações de Movimento")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 velocity;

    [Header("Configurações de Câmera")]
    public Transform playerCamera;
    public float rotationSpeed = 100f; // Velocidade de rotação
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // --- 1. MOVIMENTAÇÃO (Joystick Esquerdo) ---
        float moveX = moveJoystick.Horizontal;
        float moveZ = moveJoystick.Vertical;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // --- 2. ROTAÇÃO / OLHAR (Joystick Direito) ---
        float lookX = lookJoystick.Horizontal * rotationSpeed * Time.deltaTime;
        float lookY = lookJoystick.Vertical * rotationSpeed * Time.deltaTime;

        // Rotação Vertical (Câmera - Cima/Baixo)
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotação Horizontal (Corpo do Jogador - Esquerda/Direita)
        transform.Rotate(Vector3.up * lookX);

        // --- 3. GRAVIDADE ---
        ApplyGravity();
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
