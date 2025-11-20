using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float distanciaMovimento = 1f;   // Quanto o boneco anda
    public float velocidade = 5f;           // Velocidade do movimento
    public bool movendo = false;

    public void MoverParaCima()
    {
        if (!movendo)
            StartCoroutine(Mover(Vector3.up));
    }

    public void MoverParaBaixo()
    {
        if (!movendo)
            StartCoroutine(Mover(Vector3.down));
    }

    public void MoverParaDireita()
    {
        if (!movendo)
            StartCoroutine(Mover(Vector3.right));
    }

    public void MoverParaEsquerda()
    {
        if (!movendo)
            StartCoroutine(Mover(Vector3.left));
    }

    private IEnumerator Mover(Vector3 direcao)
    {
        movendo = true;

        Vector3 inicio = transform.position;
        Vector3 destino = inicio + direcao * distanciaMovimento;

        while (Vector3.Distance(transform.position, destino) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino,
                velocidade * Time.deltaTime
            );

            yield return null;
        }

        transform.position = destino;
        movendo = false;
    }
}
