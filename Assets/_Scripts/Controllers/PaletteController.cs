using System.Collections.Generic;
using UnityEngine;

public class PaletteController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private GameObject runeUIPrefab;
    [SerializeField] private List<RuneData> availableRunes;

    void Awake()
    {
        Debug.Assert(runeUIPrefab != null, "ERRO: O 'Rune UI Prefab' NÃO FOI CONFIGURADO no Inspector do PaletteController!");
        PopulatePalette();
    }

    void PopulatePalette()
    {
        foreach (RuneData runeData in availableRunes)
        {
            GameObject newRune = Instantiate(runeUIPrefab, transform);
            RuneUI runeUIComponent = newRune.GetComponent<RuneUI>();
            
            if (runeUIComponent != null)
            {
                runeUIComponent.Setup(runeData);
                runeUIComponent.SetMasterPrefab(runeUIPrefab);
            }
        }
    }
}