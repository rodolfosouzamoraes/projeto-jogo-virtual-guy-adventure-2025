using UnityEngine;

public class DanoPlayer : MonoBehaviour
{
    private PlayerControlador playerControlador;

    private void Start()
    {
        playerControlador = GetComponent<PlayerControlador>();    
    }

    public void EfetuarDano()
    {
        //Verificar se acabou o jogo
        if (CanvasGameMng.Instance.FimDeJogo == true) return;

        //Ativa anima��o de dano
        playerControlador.AnimacaoPlayer.PlayDano();

        //Ativar audio Dano
        AudioMng.Instance.PlayAudioDanos();

        //Resetar a fisica do jogador
        playerControlador.MovimentarPlayer.ResetarFisicaDeMovimentacao();

        //Arremessar o jogador
        playerControlador.MovimentarPlayer.ArremessarPlayer();

        //Decrementar a vida do jogador
        CanvasGameMng.Instance.DecrementarVidaJogador();
    }

    /// <summary>
    /// Desalibita as moviventa��es e as fisicas do player ao morrer
    /// </summary>
    public void MatarJogador()
    {
        //Remover a gravidade do player
        playerControlador.MovimentarPlayer.RemoverGravidade();

        //Remover a as for�as direcionais do player
        playerControlador.MovimentarPlayer.ResetarFisicaDeMovimentacao();

        //Ativar anima��o de morte
        playerControlador.AnimacaoPlayer.PlayMorte();

        //Ativar audio de morte
        AudioMng.Instance.PlayAudioMortePlayer();
    }
}
