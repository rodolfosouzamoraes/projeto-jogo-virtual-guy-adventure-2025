using UnityEngine;

public class ZonaChefe : MonoBehaviour
{
    private ChefeControlador chefeControlador;
    // Start is called before the first frame update
    void Start()
    {
        chefeControlador = FindFirstObjectByType<ChefeControlador>();
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        if(colisao.gameObject.tag == "Player"){
            //Habilita a movimentacao do chefe
            chefeControlador.HabilitaMovimentacao();

            //Ativa audio do chefe
            AudioMng.Instance.PlayAudioChefe();

            //Destroi o objeti
            Destroy(gameObject);
        }
    }
}
