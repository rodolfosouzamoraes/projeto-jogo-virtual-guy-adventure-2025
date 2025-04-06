using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    private Animator animator;

    private bool coletouItem; //Vari�vel para saber se o item foi coletado

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se foi o player que colidiu com o item e se j� houve colisao
        if(coletouItem == false && colisao.gameObject.tag == "Player")
        {
            //Diz que j� coletou o item
            coletouItem = true;

            //Ativa a anima��o de coleta do item
            animator.SetTrigger("Coletar");

            //Ativa audio de coleta da fruta
            AudioMng.Instance.PlayAudioFruta();

            //Incrementar a coleta do item
            CanvasGameMng.Instance.IncrementarItemColetavel();
        }
    }

    /// <summary>
    /// M�todo para poder destruir o item ap�s o fim da anima��o de coleta
    /// </summary>
    public void DestruirItem()
    {
        Destroy(gameObject);
    }
}
