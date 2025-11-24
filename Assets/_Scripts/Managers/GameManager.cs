using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GolemController golem;
    private ProgrammingPanelController programmingPanel;
    private ExecutionManager executionManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        FindSceneReferences();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSceneReferences();
    }

    void FindSceneReferences()
    {
        golem = FindFirstObjectByType<GolemController>();
        programmingPanel = FindFirstObjectByType<ProgrammingPanelController>();
        executionManager = FindFirstObjectByType<ExecutionManager>();

        if (executionManager == null) Debug.LogError("GameManager não encontrou o ExecutionManager na cena!");
        if (golem == null) Debug.LogError("GameManager não encontrou o GolemController na cena!");
        if (programmingPanel == null) Debug.LogError("GameManager não encontrou o ProgrammingPanelController na cena!");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public GolemController GetGolem()
    {
        return golem;
    }

    public ProgrammingPanelController GetProgrammingPanel()
    {
        return programmingPanel;
    }
    
    public ExecutionManager GetExecutionManager()
    {
        return executionManager;
    }
}