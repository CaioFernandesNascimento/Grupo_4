using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalMechanic : MonoBehaviour
{

    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");

        if (other.tag == "Player")
        {
            Debug.Log("Mudança de scene");
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }




}
