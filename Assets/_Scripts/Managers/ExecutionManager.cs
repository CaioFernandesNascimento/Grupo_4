using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionManager : MonoBehaviour
{
    public static ExecutionManager Instance { get; private set; }

    [Header("Execution Settings")]
    [Tooltip("Pequena pausa em segundos entre cada ação para melhor feedback visual.")]
    [SerializeField] private float delayBetweenActions = 0.2f;
    [SerializeField] private Color highlightColor = Color.yellow;

    [SerializeField] private GolemController golem;
    [SerializeField] private ProgrammingPanelController programmingPanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ExecuteProgram()
    {
        golem = GameManager.Instance.GetGolem();
        programmingPanel = GameManager.Instance.GetProgrammingPanel();
        if (golem == null || programmingPanel == null)
        {
            golem = GameManager.Instance.GetGolem();
            programmingPanel = GameManager.Instance.GetProgrammingPanel();
            return;
        }

        List<RuneUI> runesToExecute = programmingPanel.GetProgramRunes();
        if (runesToExecute.Count > 0)
        {
            StartCoroutine(ExecuteSequenceCoroutine(runesToExecute, golem));
        }
    }

    private IEnumerator ExecuteSequenceCoroutine(List<RuneUI> runesToExecute, GolemController golem)
    {

        foreach (RuneUI currentRune in runesToExecute)
        {
            yield return new WaitUntil(() => !golem.IsMoving);
            if (delayBetweenActions > 0) yield return new WaitForSeconds(delayBetweenActions);

            currentRune.Highlight(highlightColor);

            RuneType command = currentRune.GetRuneType();

            switch (command)
            {
                case RuneType.Walk: golem.Walk(); break;
                case RuneType.TurnLeft: golem.TurnLeft(); break;
                case RuneType.TurnRight: golem.TurnRight(); break;
            }

            yield return new WaitUntil(() => !golem.IsMoving);
            currentRune.Unhighlight();
        }

        Debug.Log("--- EXECUTION FINISHED ---");
    }
    
    public ExecutionManager GetExecutionManager()
    {
        return this;
    }
}