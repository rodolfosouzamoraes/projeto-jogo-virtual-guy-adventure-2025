using System.Collections;
using System.Data.Common;
using UnityEngine;

public class MovimentarPlayer : MonoBehaviour
{
    public LimitePlayer limiteDireita;
    public LimitePlayer limiteEsquerda;
    public LimitePlayer limiteCabeca;
    public LimitePlayer limitePe;    

    public float velocidade; //Velocidade de movimenta��o
    public float forcaPuloY; //For�a do pulo no eixo Y
    private float forcaPuloX; //For�a do pulo no eixo X
    private bool estaPulando; //Diz se o player est� em modo de pulo
    private bool puloDuplo; //Perminte o personagem efetuar um pulo duplo
    private bool pularDaParede; //Permite pular da parede
    private bool habilitaPulo; //Habilita o pulo
    private Coroutine coroutinePulo; //Contador de tempo para poder limitar o pulo do player 

    private PlayerControlador playerControlador; //C�digos do controlador do player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Referencia do PlayerControlador
        playerControlador = GetComponent<PlayerControlador>();

        //Habilitar o pulo da parede ao iniciar game
        pularDaParede = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se o jogo acabou
        if (CanvasGameMng.Instance.FimDeJogo == true) return;

        Movimentar();
        Pular();
        PularDaParede();
    }

    /// <summary>
    /// L�gica de movimenta��o do personagem
    /// </summary>
    private void Movimentar()
    {
        //Pegar a entrada do usu�rio para fazer a movimenta��o
        float eixoX = Input.GetAxis("Horizontal");

        //Verificar se chegou nos limites da esquerda e direita
        if (eixoX > 0 && limiteDireita.estaNoLimite == true) { eixoX = 0; }
        else if (eixoX < 0 && limiteEsquerda.estaNoLimite == true) { eixoX = 0; }


        //Olhar para o lado onde o jogador est� movendo
        if (eixoX > 0) {
            playerControlador.FlipCorpo.OlharDireita();
        }
        else if (eixoX < 0)
        {
            playerControlador.FlipCorpo.OlharEsquerda();
        }

        //Verificar se est� no ch�o para poder ativar as anima��es de movimento
        if(limitePe.estaNoLimite == true)
        {
            //Verificar se o player est� movendo
            if(eixoX != 0)
            {
                //Ativa anima��o de corrida
                playerControlador.AnimacaoPlayer.PlayCorrendo();
            }
            else
            {
                //Ativa anima��o de parado
                playerControlador.AnimacaoPlayer.PlayParado();
            }
        }
        else
        {
            //Ativa anima��o de queda
            playerControlador.AnimacaoPlayer.PlayCaindo();
        }

        //Dire��o da movimenta��o
        Vector3 direcaoMovimento = new Vector3(eixoX,0,0);

        //Movimentar o personagem no sentido da dire��o
        transform.position += direcaoMovimento * velocidade * Time.deltaTime;
    }

    private void Pular()
    {
        //Obter a entrada do usu�rio para poder efetuar o pulo
        if (Input.GetButtonDown("Jump"))
        {
            //Verificar se o pulo est� habilitado
            if (habilitaPulo == true)
            {
                //Ativa anima��o de pulo
                playerControlador.AnimacaoPlayer.PlayPulando();

                //Ativar audio de pulo
                AudioMng.Instance.PlayAudioPular();

                //Desabilita o pulo
                habilitaPulo = false;

                //Habilitar o est� pulando
                estaPulando = true;

                //Habilitar o pulo duplo
                puloDuplo = true;

                //Ativar o tempo do pulo
                AtivarTempoPulo();
            }
            else
            {
                //Verificar se pode fazer o pulo duplo
                if(puloDuplo == true)
                {
                    //Ativa a anima��o de pulo duplo
                    playerControlador.AnimacaoPlayer.PlayPuloDuplo();

                    //Ativar audio de pulo
                    AudioMng.Instance.PlayAudioPular();

                    //Habilito novamente o pulo
                    estaPulando = true;

                    //Desabilito pulo duplo
                    puloDuplo = false;

                    //Ativo um novo tempo de pulo
                    AtivarTempoPulo();
                }
            }
        }

        EfetuarPulo();
    }

    /// <summary>
    /// M�todo para poder fazer o jogador simular o pulo
    /// </summary>
    private void EfetuarPulo()
    {
        //Verificar se o player pode subir
        if (estaPulando == true)
        {
            if(limiteCabeca.estaNoLimite == false)
            {
                //Zerar as for�as do rigibody
                ResetarFisicaDeMovimentacao();

                //Alterar as propriedades do rigidbody para fazer o player subir
                playerControlador.Rigidbody2D.gravityScale = 0;

                //Direcionar o pulo do player
                Vector3 direcaoPulo = new Vector3(forcaPuloX, forcaPuloY, 0);

                //Mover o player para cima, simbolizando o pulo
                transform.position += direcaoPulo * velocidade * Time.deltaTime;
            }            
        }
        else
        {
            //Fazer o player voltar a cair
            playerControlador.Rigidbody2D.gravityScale = 4;
        }
    }

    private void AtivarTempoPulo()
    {
        //Verificar se j� existe um tempo a ser contado
        if(coroutinePulo != null)
        {
            StopCoroutine(coroutinePulo);
        }

        //Iniciar um novo contador de tempo
        coroutinePulo = StartCoroutine(TempoPulo());
    }

    private IEnumerator TempoPulo()
    {
        //Permite 0.3 segundo o player subindo
        yield return new WaitForSeconds(0.3f);

        //Desabilita a variavel que permite o player subir
        estaPulando = false;

        //Zerar For�a do pulo no eixo X
        forcaPuloX = 0;
    }

    private void PularDaParede()
    {
        //Verifica se est� no ch�o para poder habilitar novamente o pulo da parede
        if(limitePe.estaNoLimite == true)
        {
            pularDaParede = true;
        }

        //S� pula da parede se for permitido
        if(pularDaParede == false) { return; }

        //Verificar se o player n�o est� no ch�o e a cabe�a
        //n�o esta no limite e se est� em algumas das extremidades
        if (limitePe.estaNoLimite == false && limiteCabeca.estaNoLimite == false && 
            (limiteEsquerda.estaNoLimite == true || limiteDireita.estaNoLimite == true))
        {
            //Ativar a anima��o de Deslizar da Parede
            playerControlador.AnimacaoPlayer.PlayDeslizarParede();

            //Fazer o player olhar para o lado oposto a qual ele est� deslizando pela parede
            if (limiteEsquerda.estaNoLimite == true) playerControlador.FlipCorpo.OlharEsquerda();
            else playerControlador.FlipCorpo.OlharDireita();

            //Obter a entrada do usu�rio para poder efetuar o pulo pela parede
            if (Input.GetButtonDown("Jump"))
            {
                //Aplicar uma for�a em X na dire��o contraria a parede que ele est� encostado
                if (limiteDireita.estaNoLimite == true) {
                    forcaPuloX = forcaPuloY * -1;
                }
                else if(limiteEsquerda.estaNoLimite == true){
                    forcaPuloX = forcaPuloY;
                }
                else
                {
                    forcaPuloX = 0;
                }

                //Ativa anima��o de pulo
                playerControlador.AnimacaoPlayer.PlayPulando();

                //Ativar audio de pulo
                AudioMng.Instance.PlayAudioPular();

                //Habilitar o pulo duplo
                puloDuplo = true;

                //Habilitar o pulo continuo
                estaPulando = true;

                //Desabilita pulo da parede
                pularDaParede = false;

                //Ativar um novo tempo de pulo
                AtivarTempoPulo();
            }
        }
    }

    /// <summary>
    /// Resetar as for�as do rigidbody2d do player
    /// </summary>
    public void ResetarFisicaDeMovimentacao()
    {
        playerControlador.Rigidbody2D.linearVelocity = Vector3.zero;
    }

    /// <summary>
    /// Arremessar o player para uma dire��o aleat�ria
    /// </summary>
    public void ArremessarPlayer()
    {
        //Sortear um numero entre 0 e 1 para poder definir qual a dire��o a ser arremessado
        int valorSorteado = new System.Random().Next(0, 2);

        //Definir a dire��o em X a ser arremessado
        int direcaoX = valorSorteado == 0 ? -1000 : 1000;

        //Aplicar a for�a no player
        playerControlador.Rigidbody2D.AddForce(new Vector2(direcaoX, 1000));
    }

    /// <summary>
    /// M�todo para remover a gravidade
    /// </summary>
    public void RemoverGravidade()
    {
        //Remover a gravidade do player
        playerControlador.Rigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    /// <summary>
    /// M�todo para tirar as fun��es do player
    /// </summary>
    public void CongelarPlayer()
    {
        //Desabilitar as fisicas
        ResetarFisicaDeMovimentacao();

        //Remover a gravidade
        RemoverGravidade();

        //Ativar anima��o de parado
        playerControlador.AnimacaoPlayer.PlayParado();
    }

    public void HabilitaPulo()
    {
        habilitaPulo = true;
    }
}
