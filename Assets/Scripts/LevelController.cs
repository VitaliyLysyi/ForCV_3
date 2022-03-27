using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<Character> _startCharacters;
    [SerializeField] private List<Node> _startNodes;
    [SerializeField] private Operator _operator; //=
    [SerializeField] private GameObject _characterParent; 

    private void Start()
    {
        _operator.SetCharactersParent(_characterParent.transform);

        for (int i = 0; i < _startCharacters.Count; i++)
        {
            _startCharacters[i].SetInteractable(_startNodes[i]); //=
            _startCharacters[i].SetPosition(_startNodes[i].transform.position); //=
        }
    }
}
