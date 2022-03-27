using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, ISelectable, ICharacterInteractable
{
    [SerializeField] private List<Path> _paths;
    [SerializeField] private Renderer _renderer;

    private Character _currentCharacter;
    private Color _normalColor;
    private Color _selectedColor;

    private void Start()
    {
        _normalColor = _renderer.material.color;
        _selectedColor = Color.red;
    }

    public Character GetCurrentCharacter() { return _currentCharacter; }

    public void SetCurrentCharacter(Character current) { _currentCharacter = current; }

    public List<Path> GetPathList() { return _paths; }

    public void SetUpPath(Path path) { if (!_paths.Contains(path)) { _paths.Add(path); } }

    public void RemovePath(Path path) { if (_paths.Contains(path)) { _paths.Remove(path); } }

    public void Select() { _renderer.material.color = _selectedColor; }

    public void Deselect() { _renderer.material.color = _normalColor; }
}