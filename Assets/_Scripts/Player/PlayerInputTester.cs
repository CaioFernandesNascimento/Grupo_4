using UnityEngine;

public class PlayerInputTester : MonoBehaviour
{
    public GolemController golem;

    void Update()
    {
        if (golem == null) return;
        
        // Chama a função Walk() quando a seta para cima é pressionada.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            golem.Walk();
        }
        
        // Chama a função TurnLeft() quando a seta para esquerda é pressionada.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            golem.TurnLeft();
        }
        
        // Chama a função TurnRight() quando a seta para direita é pressionada.
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            golem.TurnRight();
        }
    }
}