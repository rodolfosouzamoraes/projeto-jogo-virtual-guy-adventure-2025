using UnityEngine;

public class FimDoLevel : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se o player colidiu
        if(colisao.gameObject.tag == "Player")
        {
            //Ativo a animação do fim do level
            animator.SetBool("FimDoLevel", true);

            //Ativar o audio de fim do level
            AudioMng.Instance.PlayAudioItemFinal();

            //Finalizo o level
            CanvasGameMng.Instance.CompletouLevel();
        }
    }
}
