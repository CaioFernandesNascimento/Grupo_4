using UnityEngine;

public class ExecuteButtonForceReference : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private ExecutionManager executionManager;
    
    void Awake()
    {
        executionManager = ExecutionManager.Instance.GetExecutionManager();
        if (executionManager == null)
        {
            executionManager = ExecutionManager.Instance.GetExecutionManager();
        }
        
        if (executionManager == null)
        {
            Debug.LogError("ExecuteButtonForceReference não encontrou o ExecutionManager na cena!");
        }
    }

    void Start()
    {
        executionManager = ExecutionManager.Instance.GetExecutionManager();
        if (executionManager == null)
        {
            executionManager = ExecutionManager.Instance.GetExecutionManager();
        }
    }
    
    // Método público para ser chamado pelo OnClick do botão
    public void ExecuteProgram()
    {
        executionManager = ExecutionManager.Instance.GetExecutionManager();
        if (executionManager != null)
        {
            executionManager.ExecuteProgram();
        }
        else
        {
            Debug.LogError("ExecutionManager não está disponível!");
        }
    }
}
