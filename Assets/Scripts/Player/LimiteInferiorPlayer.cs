using UnityEngine;

public class LimiteInferiorPlayer : LimitePlayer
{
    private PlayerControlador playerControlador;

    private void Start()
    {
        playerControlador = GetComponentInParent<PlayerControlador>();
    }
    private void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.layer == 6)
        {
            playerControlador.MovimentarPlayer.HabilitaPulo();
        }
    }
}
