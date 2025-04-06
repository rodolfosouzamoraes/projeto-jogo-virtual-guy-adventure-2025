using System.Collections;
using UnityEngine;

public class DanoSpike : MonoBehaviour
{
    private bool houveColisao;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && houveColisao == false)
        {
            //Diz que houve colis�o
            houveColisao = true;

            //Dano ao jogador
            collision.gameObject.GetComponent<DanoPlayer>().EfetuarDano();

            //Reabilitar o dano ao jogador
            StartCoroutine(ResetarColisao());
        }
    }

    private IEnumerator ResetarColisao()
    {
        //Espera 0.3 segundo para poder haver uma nova colis�o com o player
        yield return new WaitForSeconds(0.3f);
        houveColisao = false;
    }
}
