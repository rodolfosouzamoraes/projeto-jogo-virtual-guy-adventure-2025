using UnityEngine;

public class ContatoBolaDeEspinhos : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag == "Player")
        {
            CanvasGameMng.Instance.GameOver();
        }
    }
}
