using UnityEngine;

public class PlayerControlador : MonoBehaviour
{
    //Declarar as váriaveis que vão armazenar os códigos do player
    private MovimentarPlayer movimentarPlayer;
    private AnimacaoPlayer animacaoPlayer;
    private DanoPlayer danoPlayer;
    private FlipCorpoPlayer flipCorpo;
    private Rigidbody2D rigidbody2d;

    //Definir as propriedades de acesso aos códigos do player
    public MovimentarPlayer MovimentarPlayer
    {
        get { return movimentarPlayer; }
    }
    public AnimacaoPlayer AnimacaoPlayer
    {
        get { return animacaoPlayer; }
    }
    public DanoPlayer DanoPlayer
    {
        get { return danoPlayer; }
    }
    public FlipCorpoPlayer FlipCorpo
    {
        get { return flipCorpo; }
    }
    public Rigidbody2D Rigidbody2D
    {
        get { return rigidbody2d; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //Obter a referencia do movimentar player
        movimentarPlayer = GetComponent<MovimentarPlayer>();

        //Obter a referencia do animacaoPlayer
        animacaoPlayer = GetComponent<AnimacaoPlayer>();

        //Obter a refencia do danoPlayer
        danoPlayer = GetComponent<DanoPlayer>();

        //Obter a referencia do flipCorpo
        flipCorpo = GetComponent<FlipCorpoPlayer>();

        //Obter a referencia do rigidbody2d
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
}
