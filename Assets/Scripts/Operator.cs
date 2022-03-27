using ProjectA.Game;
using UnityEngine;

public class Operator : MonoBehaviour
{
    private Camera _camera;
    private MouseTracker _mouseTracker;
    private Transform _characterSection;
    private ISelectable _currentSelected;
    private ISelectable _startSelected;
    private ISelectable _endSelected;

    private OperatorHelper kostil; //=

    private void Start()
    {
        _camera = Camera.main;
        _mouseTracker = new MouseTracker();
        kostil = new OperatorHelper();
    }

    private void OnDrawGizmos()
    {
        if (_startSelected != null)
        {
            Vector3 mouseWorldPosition = _mouseTracker.MouseWorldPosition(Input.mousePosition, 10);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(mouseWorldPosition, 0.2f);
        }
    }

    private void Update()
    {
        GetCurrentSelected();

        CheckForMove();

        CheckForDivide();
    }

    private void CheckForMove()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _startSelected = _currentSelected;
            _endSelected = null;

            EventManager.selectionBegin?.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _endSelected = _currentSelected;
            
            Path availablePath = kostil.TryGetAvailablePath(_startSelected, _endSelected);
            if (availablePath != null)
            {
                Character currentCharactter = _startSelected as Character;
                currentCharactter.StartMoving(availablePath);
            }

            _endSelected = null;
            _startSelected = null;

            EventManager.selectionEnd?.Invoke();
        }
    }

    private void CheckForDivide()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _startSelected = _currentSelected;
            _endSelected = null;

            EventManager.selectionBegin?.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _endSelected = _currentSelected;

            Path availablePath = kostil.TryGetAvailablePath(_startSelected, _endSelected);
            if (availablePath != null)
            {
                Character currentCharactter = _startSelected as Character;
                currentCharactter.HalveMass();
                Character dividedCharacter = Instantiate(currentCharactter, _characterSection);
                dividedCharacter.StartMoving(availablePath);
            }

            _endSelected = null;
            _startSelected = null;

            EventManager.selectionEnd?.Invoke();
        }
    }

    private void GetCurrentSelected()
    {
        Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hitObjecet))
        {
            ISelectable selectedObject = hitObjecet.transform.GetComponent<ISelectable>();
            if (selectedObject != null)
            {
                if (_currentSelected == null)
                {
                    _currentSelected = selectedObject;
                    _currentSelected.Select();
                }
                else if (_currentSelected != selectedObject)
                {
                    _currentSelected.Deselect();
                    _currentSelected = selectedObject;
                    _currentSelected.Select();
                }
            }
        }
        else if (_currentSelected != null)
        {
            _currentSelected.Deselect();
            _currentSelected = null;
        }
    }

    public void SetCharactersParent(Transform section) { _characterSection = section; }
}