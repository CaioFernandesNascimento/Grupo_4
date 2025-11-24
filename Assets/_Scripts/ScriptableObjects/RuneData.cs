using UnityEngine;


[CreateAssetMenu(fileName = "NewRuneData", menuName = "Nome do Game/Rune Data")]
public class RuneData : ScriptableObject
{
    // Todos os dados que uma runa precisa ter.
    public RuneType runeType;
    public Sprite runeSprite;

}