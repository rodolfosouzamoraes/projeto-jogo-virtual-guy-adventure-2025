using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    private Animator animator;

    private bool coletouItem; //Variável para saber se o item foi coletado

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se foi o player que colidiu com o item e se já houve colisao
        if(coletouItem == false && colisao.gameObject.tag == "Player")
        {
            //Diz que já coletou o item
            coletouItem = true;

            //Ativa a animação de coleta do item
            animator.SetTrigger("Coletar");

            //Ativa audio de coleta da fruta
            AudioMng.Instance.PlayAudioFruta();

            //Incrementar a coleta do item
            CanvasGameMng.Instance.IncrementarItemColetavel();
        }
    }

    /// <summary>
    /// Método para poder destruir o item após o fim da animação de coleta
    /// </summary>
    public void DestruirItem()
    {
        Destroy(gameObject);
    }
}
