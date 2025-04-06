using UnityEngine;

public class AnimacaoPlayer : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Ativa a anima��o de Parado do personagem
    /// </summary>
    public void PlayParado()
    {
        animator.SetBool("Parado", true);
        animator.SetBool("Correndo", false);
        animator.SetBool("Caindo", false);
        animator.SetBool("Pulando", false);
        animator.SetBool("DeslizarParede", false);
    }

    /// <summary>
    /// Ativa a anima��o de Correndo do personagem
    /// </summary>
    public void PlayCorrendo()
    {
        animator.SetBool("Parado", false);
        animator.SetBool("Correndo", true);
        animator.SetBool("Caindo", false);
        animator.SetBool("Pulando", false);
        animator.SetBool("DeslizarParede", false);
    }

    /// <summary>
    /// Ativa a anima��o de Caindo do personagem
    /// </summary>
    public void PlayCaindo()
    {
        animator.SetBool("Caindo",true);
        animator.SetBool("Parado", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Pulando", false);
        animator.SetBool("DeslizarParede", false);
    }

    /// <summary>
    /// Ativa a anima��o de Pulando do personagem
    /// </summary>
    public void PlayPulando()
    {
        animator.SetBool("Pulando", true);
        animator.SetBool("Caindo", false);
        animator.SetBool("Parado", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("DeslizarParede", false);
    }

    /// <summary>
    /// Ativa a anima��o de pulo duplo
    /// </summary>
    public void PlayPuloDuplo()
    {
        animator.SetBool("Pulando", false);
        animator.SetBool("Caindo", false);
        animator.SetBool("Parado", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("DeslizarParede", false);
        animator.SetTrigger("PuloDuplo");
    }

    /// <summary>
    /// Ativa anima��o de deslizar da parede
    /// </summary>
    public void PlayDeslizarParede()
    {
        animator.SetBool("DeslizarParede", true);
        animator.SetBool("Pulando", false);
        animator.SetBool("Caindo", false);
        animator.SetBool("Parado", false);
        animator.SetBool("Correndo", false);
    }

    /// <summary>
    /// Ativa a anima��o de dano ao jogador
    /// </summary>
    public void PlayDano()
    {
        
        animator.SetTrigger("Dano");
    }

    /// <summary>
    /// Ativa a anima��o de morte do jogador
    /// </summary>
    public void PlayMorte()
    {
        animator.SetBool("Fim", true);
        animator.SetTrigger("Morte");
    }

    public void PlayAudioMovimentacao()
    {
        AudioMng.Instance.PlayAudioCorrer();
    }
}
