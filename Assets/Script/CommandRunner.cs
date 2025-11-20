using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRunner : MonoBehaviour
{
    //variavel para achar a grid que eh a Using grid
    public Transform executionGrid;

    public PlayerController player;

    public void Run()
    {
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        foreach (Transform slot in executionGrid)
        {
            if (slot.childCount == 0)
                continue;  // slot vazio = ignora

            Transform bloco = slot.GetChild(0);

            var cmd = bloco.GetComponent<Command>();

            if (cmd != null)
            {
                ExecuteCommand(cmd.commandName);

                while (player.movendo)
                    yield return null;
            }
        }
    }

    void ExecuteCommand(string cmd)
    {
        switch (cmd)
        {
            case "MoverParaCima":
                player.MoverParaCima();
                break;

            case "MoverParaEsquerda":
                player.MoverParaEsquerda();
                break;

            case "MoverParaDireita":
                player.MoverParaDireita();
                break;

            case "MoverParaBaixo":
                player.MoverParaBaixo();
                break;
        }
    }
}
