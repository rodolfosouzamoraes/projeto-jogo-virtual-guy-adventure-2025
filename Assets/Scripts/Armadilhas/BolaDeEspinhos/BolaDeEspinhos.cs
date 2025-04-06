using UnityEngine;

public class BolaDeEspinhos : MonoBehaviour
{
    public float velocidade = 100;
    public bool rotacaoConstante; //Diz se quer que o objeto rotacione constantemente ou não
    private GameObject contato;

    private void Start()
    {
        contato = GetComponentInChildren<ContatoBolaDeEspinhos>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotacionar o objeto
        contato.transform.eulerAngles += Vector3.back * velocidade * Time.deltaTime;

        //Verificar se a rotação será constante ou não
        if(rotacaoConstante == false){
            //Verificar os limites para rotacionar
            if(contato.transform.eulerAngles.z >= 90 && contato.transform.eulerAngles.z <=270){
                //Inverter a velocidade
                velocidade *=-1;
            }
        }
    }
}
