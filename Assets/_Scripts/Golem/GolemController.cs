using System.Collections;
using UnityEngine;

// Enum para representar as direções de forma clara e legível.
public enum Direction { North, East, South, West }

public class GolemController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;

    [Header("Collision Settings")]
    [Tooltip("Selecione a camada (Layer) que contém as paredes e obstáculos.")]
    [SerializeField] private LayerMask wallLayer;

    private bool _isMoving = false;
    public bool IsMoving => _isMoving;

    private Direction currentDirection = Direction.North;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        StopAllCoroutines();
        _isMoving = false;
    }

    public void Walk()
    {
        if (_isMoving) return;

        Vector3 direction = GetDirectionVector(); 
        Vector3 targetPosition = transform.position + direction;

        Collider2D wallCheck = Physics2D.OverlapCircle(targetPosition, 0.2f, wallLayer);

        if (wallCheck != null)
        {
            StartCoroutine(BonkAndResetCoroutine(direction));
        }
        else
        {
            StartCoroutine(MoveCoroutine(targetPosition));
        }
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        _isMoving = true;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        _isMoving = false;
    }

    private IEnumerator BonkAndResetCoroutine(Vector3 direction)
    {
        _isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 bonkPos = startPos + direction * 0.3f; 

        float timer = 0f;
        while (timer < 0.1f)
        {
            transform.position = Vector3.Lerp(startPos, bonkPos, timer / 0.1f);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < 0.1f)
        {
            transform.position = Vector3.Lerp(bonkPos, startPos, timer / 0.1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.position = startPos;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.RestartLevel();
        _isMoving = false;
    }

    public void TurnRight()
    {
        if (_isMoving) return;

        // Atualiza a enum da direção
        switch (currentDirection)
        {
            case Direction.North: currentDirection = Direction.East; break;
            case Direction.East:  currentDirection = Direction.South; break;
            case Direction.South: currentDirection = Direction.West; break;
            case Direction.West:  currentDirection = Direction.North; break;
        }
        
        StartCoroutine(RotateCoroutine(-90)); // Gira para a direita (sentido horário)
    }

    public void TurnLeft()
    {
        if (_isMoving) return;

        // Atualiza a enum da direção
        switch (currentDirection)
        {
            case Direction.North: currentDirection = Direction.West; break;
            case Direction.West:  currentDirection = Direction.South; break;
            case Direction.South: currentDirection = Direction.East; break;
            case Direction.East:  currentDirection = Direction.North; break;
        }

        StartCoroutine(RotateCoroutine(90)); // Gira para a esquerda (sentido anti-horário)
    }

    private IEnumerator RotateCoroutine(float angle)
    {
        _isMoving = true;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, angle);

        float t = 0;
        while (t < 1.0f)
        {
            // Usamos Slerp para uma rotação mais suave
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            // Time.deltaTime normalizado pelo tempo total da rotação para velocidade constante
            t += Time.deltaTime / (90f / rotationSpeed); 
            yield return null;
        }
        
        transform.rotation = targetRotation;
        _isMoving = false; 
    }

    private Vector3 GetDirectionVector()
    {
        switch (currentDirection)
        {
            case Direction.North: return Vector3.up;
            case Direction.East:  return Vector3.right;
            case Direction.South: return Vector3.down;
            case Direction.West:  return Vector3.left;
            default:              return Vector3.zero;
        }
    }
}