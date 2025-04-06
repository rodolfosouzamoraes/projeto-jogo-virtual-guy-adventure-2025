using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChefeControlador : MonoBehaviour
{
    private Animator animator;
    private List<BoxCollider2D> colisores; //Obter todos os colliders do chefe
    private int vidaChefe = 4;
    private bool estaMovendo; //Diz se o chefe pode mover ou n�o
    public GameObject itemFinal;//Libera o item final ap�s a morte do chefe

    public bool EstaMovendo
    {
        get { return estaMovendo; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ocultar o item final
        itemFinal.SetActive(false);

        //Referenciar o animator
        animator = GetComponent<Animator>();

        //Referenciar os colisores internos
        colisores = GetComponentsInChildren<BoxCollider2D>().ToList();

        //Adicionar o colisor do objeto 
        colisores.Add(GetComponent<BoxCollider2D>());
    }

    public void DecrementarVidaChefe()
    {
        //Decrementar a vida do chefe
        vidaChefe--;

        //Verificar se a vida do chefe acabou
        if (vidaChefe == 0)
        {
            //Parar de se mover
            estaMovendo = false;

            //Destruir os colisores
            foreach (var colisor in colisores)
            {
                Destroy(colisor);
            }

            //Ativa a anima��o de morte do chefe
            animator.SetTrigger("Morte");

            //Ativa audio de morte do chefe
            AudioMng.Instance.PlayAudioMorteChefe();
        }
        else
        {
            //Ativa anima��o de dano
            animator.SetTrigger("Dano");

            //Ativa audio dano inimigo
            AudioMng.Instance.PlayAudioDanoInimigo();
        }
    }

    /// <summary>
    /// Ativa o item final ap�s o fim da anima��o de morte
    /// </summary>
    public void AtivarItemFinal()
    {
        //Habilita o item final
        itemFinal.SetActive(true);

        //Destruo o chefe
        Destroy(gameObject);
    }

    /// <summary>
    /// Habilita a movimenta��o quando o player entrar numa area
    /// </summary>
    public void HabilitaMovimentacao()
    {
        //Diz que pode se mover
        estaMovendo = true;

        //Ativa a anima��o de corrida
        animator.SetBool("Correndo", true);
    }
}
