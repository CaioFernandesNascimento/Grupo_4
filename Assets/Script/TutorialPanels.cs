using UnityEngine;

public class TutorialPanels : MonoBehaviour
{

    [SerializeField] private GameObject painelTutorialInicial;
    [SerializeField] private GameObject painelTutorialInicialBackground;

    [SerializeField] private GameObject painelTutorialVoce;
    [SerializeField] private GameObject painelTutorialVoceBackground;

    [SerializeField] private GameObject painelTutorialComandos;
    [SerializeField] private GameObject painelTutorialComandosBackground;

    [SerializeField] private GameObject painelTutorialBotaoRun;
    [SerializeField] private GameObject painelTutorialBotaoRunBackground;

    [SerializeField] private GameObject painelTutorialObjetivo;
    [SerializeField] private GameObject painelTutorialObjetivoBackground;

    [SerializeField] private GameObject playerImage;

    public void abrirTutorialVoce()
    {
        painelTutorialInicial.SetActive(false);
        painelTutorialInicialBackground.SetActive(false);

        painelTutorialVoce.SetActive(true);
        painelTutorialVoceBackground.SetActive(true);
    }

    public void abrirTutorialObjetivo()
    {
        painelTutorialVoce.SetActive(false);
        playerImage.SetActive(false);
        painelTutorialVoceBackground.SetActive(false);

        painelTutorialObjetivo.SetActive(true);
        painelTutorialObjetivoBackground.SetActive(true);
    }


    public void abrirTutorialComandos()
    {
        painelTutorialObjetivo.SetActive(false);
        painelTutorialObjetivoBackground.SetActive(false);

        painelTutorialComandos.SetActive(true);
        painelTutorialComandosBackground.SetActive(true);
    }

    public void abrirTutorialBotaoRun()
    {
        painelTutorialComandos.SetActive(false);
        painelTutorialComandosBackground.SetActive(false);


        painelTutorialBotaoRun.SetActive(true);
        painelTutorialBotaoRunBackground.SetActive(true);
    }

    public void fecharTutorial()
    {
        painelTutorialBotaoRun.SetActive(false);
        painelTutorialBotaoRunBackground.SetActive(false);
    }

}
