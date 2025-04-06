using UnityEngine;

public class InimigoPassaroAzul : MonoBehaviour
{
    public float velocidade; //Velocidade de movimenta��o do passaro
    public Vector3 posicaoFinal; //Posi��o final para onde o pass�ro deve ir
    private SpriteRenderer corpoPassaroAzul; //Vari�vel para manipular o sprite do passaro azul
    private Vector3 posicaoInicial; //Posi��o ao iniciar o jogo
    private Vector3 posicaoAlvo; //Posi��o para onde o passaro deve ir
    private bool estaMorto; //Vari�vel que diz se o passaro morreu
    private Animator animator; //Controla a anima��o do passaro

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Configura a posi��o inicial do passaro
        posicaoInicial = transform.position;

        //Diz para onde o passaro deve ir
        posicaoAlvo = posicaoFinal;

        //Configura o animator
        animator = GetComponent<Animator>();

        //Configura o SpriteRenderer
        corpoPassaroAzul = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Chamar a movimenta��o do passaro
        MovimentarPassaro();

        //Calcular a distancia entre o passaro e o alvo
        CalcularDistanciaAlvo();
    }

    /// <summary>
    /// M�todo para movimentar o passaro
    /// </summary>
    private void MovimentarPassaro()
    {
        //Movimentar o passaro para uma posicao alvo
        transform.position = Vector3.MoveTowards(
            transform.position,
            posicaoAlvo,
            velocidade * Time.deltaTime
        );
    }

    /// <summary>
    /// Calcular a distancia entre o passaro e o alvo para onde ele deve ir.
    /// Inverter o corpo quando chegar na posi��o
    /// </summary>
    private void CalcularDistanciaAlvo()
    {
        //Verificar a distancia do passaro em rela��o ao alvo
        if(Vector3.Distance(transform.position, posicaoAlvo) < 0.001f)
        {
            //Verificar o flip do sprite para saber a nova dire��o do passaro
            if(corpoPassaroAzul.flipX == false)
            {
                //Alterar a posi��o alvo para a posi��o inicial
                posicaoAlvo = posicaoInicial;
            }
            else
            {
                //Alterar a posi��o do alvo para a posi��o final
                posicaoAlvo = posicaoFinal;
            }

            //Inverter o flip do sprite para olhar em dire��o onde deve ir
            corpoPassaroAzul.flipX = !corpoPassaroAzul.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se o player colidiu com a cabeca do passaro
        if(colisao.gameObject.tag == "Player" && estaMorto == false)
        {
            //Dizer que morreu o passaro
            estaMorto = true;

            //Arremessar o player
            colisao.GetComponent<PlayerControlador>().MovimentarPlayer.ArremessarPlayer();

            //Ativar a anima��o de morte
            animator.SetTrigger("Morte");

            //Ativa audio de dano
            AudioMng.Instance.PlayAudioDanoInimigo();
        }
    }

    public void DestruirPassaro()
    {
        Destroy(gameObject);
    }
}
