using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    [Header("Configurações de Rotação")]
    [SerializeField] public float velocidadeRotacao = 50f;
    [SerializeField] public float anguloMaximo = 45f;

    [Header("Configurações de Força")]
    [SerializeField] public float forcaLancamento = 10f; // Força total do tiro

    private InputAction movimento;
    private InputAction launchAction; // Adicionado para detectar o disparo
    private PlayerScript playerScript;
    private GameScript gameState; // Adicionado para verificar o estado do jogo

    private float anguloAtualY = 0f;

    private void Start()
    {
        movimento = InputSystem.actions.FindAction("Move");
        launchAction = InputSystem.actions.FindAction("Jump");

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerScript>();
        gameState = playerScript.gameState; // Pega a referência do gameState do player
    }

    void Update()
    {
        if (!gameState.IsPlayerStarting) return;
        // 1. Lógica de Rotação do Canhão
        float direcao = 0f;
        Vector2 move = movimento.ReadValue<Vector2>();

        if (move.x > 0.1f) direcao = -1f;
        else if (move.x < -0.1f) direcao = 1f;

        if (direcao != 0f)
        {
            anguloAtualY += direcao * velocidadeRotacao * Time.deltaTime;
            anguloAtualY = Mathf.Clamp(anguloAtualY, -anguloMaximo, anguloMaximo);
            transform.localRotation = Quaternion.Euler(0f, 0f, anguloAtualY);
        }

        // 2. Lógica de Disparo Direcionado
        if (launchAction.WasPressedThisFrame() && gameState.IsPlayerStarting == true)
        {
            // Define a direção base (para onde o canhão aponta quando o ângulo é 0).
            // Se o canhão apontar para a direita por padrão, use Vector2.right. Se apontar para cima, Vector2.up.
            Vector2 direcaoBase = new Vector2(1,1);

            // Cria a rotação baseada no ângulo atual do canhão (eixo Z para 2D)
            Quaternion rotacaoCanhao = Quaternion.Euler(0f, 0f, anguloAtualY);

            // Rotaciona a direção base e multiplica pela intensidade da força
            Vector2 velocidadeCalculada = (rotacaoCanhao * direcaoBase) * forcaLancamento;

            // Lança o jogador com o vetor rotacionado perfeito
            playerScript.launch(velocidadeCalculada);
        }
    }
}