using UnityEngine;

public class ContatoPlataformaFlutuante : MonoBehaviour
{
    private Animator animator;
    private bool saiuDaPlataforma; //Dizer se o player saiu ou não da plataforma
    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        animator = GetComponent<Animator>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        //Remove Gravidade
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerExit2D(Collider2D colisao)
    {
        //Verificar se o player que saiu do contato com a plataforma e se ele não saiu antes
        if(colisao.gameObject.tag == "Player" && saiuDaPlataforma == false)
        {
            //Dizer que o player saiu da plataforma
            saiuDaPlataforma = true;

            //Habilita gravidade
            rigidbody2d.bodyType = RigidbodyType2D.Dynamic;

            //Ativar a animação de queda da plataforma
            animator.SetTrigger("Caindo");

            //Destruir o objeto
            Destroy(gameObject, 0.25f);
        }
    }
}
