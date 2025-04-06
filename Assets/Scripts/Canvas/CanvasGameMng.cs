using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasGameMng : MonoBehaviour
{
    //Criar uma variável para instanciar o objeto
    public static CanvasGameMng Instance;

    private void Awake()
    {
        //Criar a instancia estática
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        //Destroi o gameobject caso já exista uma instancia da classe
        Destroy(gameObject);
    }

    private bool fimDeJogo;//Diz se o jogo acabou
    private PlayerControlador playerControlador; //Códigos que controlam os atributos do player

    [Header("Vida do Player")]
    public Image imgVida;//Imagem da vida
    public Sprite[] sptsVida;//Os sprites que vão aparecer na img da vida
    private int totalVidas;//Quantidade de vidas do player 

    [Header("Controle do Painel Topo")]
    public TextMeshProUGUI txtTotalItensColetados;//Texto que exibe o total de itens coletados
    private int totalItensColetados;//Variável para poder armazentar os itens coletados
    public TextMeshProUGUI txtTempoJogo;//Texto que exibe o tempo do jogo
    public float tempoJogo;//Variavel com o tempo do level atual
    public GameObject pnlTopo; //Variável para manipular o objeto topo da tela

    [Header("Controle do Painel Lv Completado")]
    public GameObject pnlLevelCompletado; //Variável para manipular o objeto do painel de fim de jogo
    public TextMeshProUGUI txtTotalItensColetadosFinal; //Texto que exibe o total final de itens coletados
    public Image imgIconeMedalha; //Imagem da medalha
    public Sprite[] sptsMedalhas; //Sprites com as 3 medalhas
    private float qtdDeItensColetaveisNoLevel;// Quantidade de itens coletaveis que existe no level ao começar o jogo
    private int idMedalha; //Identificador da medalha que o jogador conseguiu no level
    private int idLevel; //Identificador do level atual

    public bool FimDeJogo
    {
        get { return fimDeJogo; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Adicionar um segundo a mais no tempo de jogo
        tempoJogo += 1;

        //Adicionar o total de vidas que o player tem ao iniciar o jogo
        totalVidas = sptsVida.Length - 1;

        //Zerar o total de itens coletados
        totalItensColetados = 0;

        //Atualizar o texto com o total de itens coletados
        txtTotalItensColetados.text = $"x{totalItensColetados}";

        //Atualizar o texto com o tempo atual
        txtTempoJogo.text = tempoJogo.ToString();

        //Pegar a referencia do controle do player
        playerControlador = FindFirstObjectByType<PlayerControlador>();

        //Ocultar o painel de level completado ao começar o jogo
        pnlLevelCompletado.SetActive(false);

        //Habilitar o painel topo
        pnlTopo.SetActive(true);

        //Diz quantos itens coletaveis há no inicio do level
        qtdDeItensColetaveisNoLevel = FindObjectsByType<ItemColetavel>(FindObjectsSortMode.None).Length;

        //Identificar a cena atual
        idLevel = SceneManager.GetActiveScene().buildIndex;

        //Configuração para rodar apenas quando o level for inicial manualmente
        Volume volume = DBMng.ObterVolume();

        //Altera o volume
        AudioMng.Instance.MudarVolume(volume);

        //Tocar o audio do level
        AudioMng.Instance.PlayAudioLevel();

        //Ocultar o canvas loading
        CanvasLoadingMng.Instance.OcultarPainelLoading();
    }

    private void Update()
    {
        //Verificar se clicou na tecla ESC para voltar ao menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VoltarAoMenu();
        }

        //Contar o tempo do jogo
        ContarTempo();
    }

    public void DecrementarVidaJogador()
    {
        //Decrementar a vida do jogador
        totalVidas--;

        //Verificar se o jogador tem vidas para continuar o jogo
        if (totalVidas < 1)
        {
            //Finaliza o jogo
            GameOver();            
        }
        else
        {
            //Atualiza a imagem da vida para o sprite correspondente
            imgVida.sprite = sptsVida[totalVidas];
        }
    }

    /// <summary>
    /// Método para finalizar o jogo
    /// </summary>
    public void GameOver()
    {
        //Dizer que o jogador morreu
        fimDeJogo = true;

        //Zerar as vidas do jogador
        totalVidas = 0;

        //Colocar o sprite de vida zerada na imagem
        imgVida.sprite = sptsVida[totalVidas];

        //Desabilitar Funções do jogador
        playerControlador.DanoPlayer.MatarJogador();

        //Contar o tempo para reiniciar a cena
        StartCoroutine(ReiniciarLevel());
    }

    /// <summary>
    /// Tempo para poder resetar o level
    /// </summary>
    /// <returns></returns>
    IEnumerator ReiniciarLevel()
    {
        //Aguardar 3 segundos para resetar o level
        yield return new WaitForSeconds(3f);

        //Reiniciar o level atual
        ResetarLevelAtual();
    }

    /// <summary>
    /// Reinicia a cena atual
    /// </summary>
    public void ResetarLevelAtual()
    {
        //Reiniciar a cena do jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReiniciarLevelPelaUI()
    {
        CanvasLoadingMng.Instance.ExibirPainelLoading();

        //Reinicia a cena do jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Método para incrementar um item ao total de itens coletados
    /// </summary>
    public void IncrementarItemColetavel()
    {
        //Incrementar o item na variavel
        totalItensColetados++;

        //Atualizar o texto com o total de itens coletados
        txtTotalItensColetados.text = $"x{totalItensColetados}";
    }

    private void ContarTempo()
    {
        //Verificar se o jogo acabou para parar de contar o tempo
        if (fimDeJogo == true) return;

        //Incrementar o tempo na variavel 
        tempoJogo -= Time.deltaTime;

        //Verificar se o tempo acabou
        if(tempoJogo <= 0)
        {
            //Finalizar o jogo
            GameOver();
        }
        else 
        { 
            //Atualizar o texto com o tempo atual
            txtTempoJogo.text = ((int)tempoJogo).ToString();
        }
    }

    /// <summary>
    /// Método para finalizar o level
    /// </summary>
    public void CompletouLevel()
    {
        //Dizer que o jogo acabou
        fimDeJogo = true;

        //Congelar o player
        playerControlador.MovimentarPlayer.CongelarPlayer();

        //Exibir a tela final do level
        StartCoroutine(ExibirTelaDoLevelCompletado());
    }

    /// <summary>
    /// Contar o tempo para poder exibir o painel do level completado e fazer a contagem das frutas
    /// </summary>
    IEnumerator ExibirTelaDoLevelCompletado()
    {
        //Aguardar 3 segundos
        yield return new WaitForSeconds(3f);

        //Calcular a medalha do level
        CalcularMedalhaLevel();

        //Salvar os dados do level na memória
        DBMng.SalvarDadosLevel(idLevel, totalItensColetados, idMedalha);

        //Exibir a tela de level completado
        pnlTopo.SetActive(false);
        pnlLevelCompletado.SetActive(true);

        //Fazer uma contagem dos itens coletados
        int contagem = 0;
        while(contagem < totalItensColetados)
        {
            //Incrementar a contagem
            contagem++;

            //Exibir a contagem no texto
            txtTotalItensColetadosFinal.text = $"x{contagem}";

            //aguardo 0.1 segundo para reiniciar o loop do comando while
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Método para descobrir qual a melhada que o jogador consegui no level
    /// </summary>
    private void CalcularMedalhaLevel()
    {
        //Definir a porcentagem
        float porcentagem = (totalItensColetados / qtdDeItensColetaveisNoLevel) * 100;

        //Definir qual medalha foi conquistada com base na porcentagem
        idMedalha = porcentagem < 50 ? 1 : porcentagem >= 50 && porcentagem < 100 ? 2 : 3;

        //Atribuo a medalha na imagem do icone
        imgIconeMedalha.sprite = sptsMedalhas[idMedalha];
    }

    /// <summary>
    /// Método para ir para o próximo level
    /// </summary>
    public void ProximoLevel()
    {
        //Exibir o painel de carregamento
        CanvasLoadingMng.Instance.ExibirPainelLoading();

        //Carregar a proxima cena
        SceneManager.LoadScene(idLevel + 1);
    }

    /// <summary>
    /// Método para retornar a cena menu
    /// </summary>
    public void VoltarAoMenu()
    {
        //Exibir o painel de carregamento
        CanvasLoadingMng.Instance.ExibirPainelLoading();

        SceneManager.LoadScene(0);
    }
}
