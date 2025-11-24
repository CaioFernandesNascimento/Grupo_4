using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTileLogic : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Golem"))
        {
            Debug.Log("Golem colidiu com Bloco de Reinício! Reiniciando fase...");
            GameManager.Instance.RestartLevel();
        }
    }
}