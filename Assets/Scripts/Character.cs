using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour, ISelectable
{
    [SerializeField] private CharacterSettings _characterSettings;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshPro _massText;

    private ICharacterInteractable _currentInteractable; //=
    private List<Vector3> _wayPoints;
    private int _currentWayPoint;
    private bool _isMoving;

    private Color _normalColor;
    private Color _selectionColor;

    [SerializeField] private float _mass = 1000f; //=

    private void Start()
    {
        EventManager.selectionBegin += MakeMassVisible;
        EventManager.selectionEnd += HideMass;

        _normalColor = _renderer.material.color;
        _selectionColor = Color.red;

        HideMass();
        ChangeMass(_mass);
    }

    private void Update()
    {
        if (_isMoving)
        {
            Move();
        }
    }

    private void OnDestroy()
    {
        EventManager.selectionBegin -= MakeMassVisible;
        EventManager.selectionEnd -= HideMass;
    }

    public void StartMoving(Path targetPath)
    {
        SetInteractable(targetPath);

        _wayPoints = targetPath.GetPathPoints();
        SetPosition(_wayPoints[0]);

        _currentWayPoint = 0;
        _isMoving = true;
    }

    private void Move()
    {
        Vector3 currentPoint = _wayPoints[_currentWayPoint];
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = Vector3.MoveTowards(currentPosition, currentPoint, _characterSettings.moveSpeed * Time.deltaTime);
        SetPosition(newPosition);

        float distanceToPoint = Vector3.Distance(newPosition, currentPoint);
        if (distanceToPoint < _characterSettings.deadDistance)
        {
            _currentWayPoint++;

            if (_currentWayPoint >= _wayPoints.Count)
            {
                EndMoving();
            }
        }
    }

    private void EndMoving()
    {
        _isMoving = false;

        Path currentPath = _currentInteractable as Path;
        Node newNode = currentPath.GetTargetNode();
        SetInteractable(newNode);
    }

    private void MakeMassVisible() { _massText.renderer.enabled = true; }

    private void HideMass() => _massText.renderer.enabled = false; // inline method

    public float GetMass() { return _mass; }

    public void HalveMass() { ChangeMass(_mass / 2); }

    public void ChangeMass(float mass)
    {
        _mass = mass;
        _massText.text = _mass.ToString();

        ChangeScaleByMass(_mass);
    }

    private void ChangeScaleByMass(float mass)
    {
        float scaleFactor = mass / _characterSettings.massToScaleMultiplier;
        scaleFactor = Mathf.Clamp(scaleFactor, _characterSettings.massMinRange, _characterSettings.massMaxRange);

        _renderer.transform.localScale = Vector3.one * scaleFactor;
    }

    private void MergeWithCharacter(Character anotherCharacter)
    {
        float newMass = _mass + anotherCharacter.GetMass();
        anotherCharacter.ChangeMass(newMass);
    }

    public void SetInteractable(ICharacterInteractable interactable) //=
    {
        if (_currentInteractable is Node)
        {
            Node currentNode = (Node)_currentInteractable; //=
            currentNode.SetCurrentCharacter(null);
        }

        _currentInteractable = interactable;

        if (interactable is Node)
        {
            Node currentNode = _currentInteractable as Node;
            Character anotherCharacter = currentNode.GetCurrentCharacter();

            if (anotherCharacter == null)
            {
                currentNode.SetCurrentCharacter(this);
            }
            else
            {
                MergeWithCharacter(anotherCharacter);
                Destroy(gameObject);
            }
        }
    }

    public ICharacterInteractable GetInteractable() { return _currentInteractable; }

    public void SetPosition(Vector3 position) { transform.position = position; }

    public void Select() { _renderer.material.color = _selectionColor; }

    public void Deselect() { _renderer.material.color = _normalColor; }
}