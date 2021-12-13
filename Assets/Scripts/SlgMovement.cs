using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlgMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _mousePos;
    private Vector3 _lastMousePos;
    private Vector3 _clickedPosition;
    private Vector3 _rootClicked;
    [SerializeField] private Transform _root;
    [SerializeField] private bool _isClicked;
    [SerializeField] private PlayerMovementData _moveData;
    private float _bodyLength;
    private float _dragLength = 3f;
    private float _projection;
    private float _exitLength = 0.5f;
    private float _jumpHeight = 2f;
    private float _rootToEndDistance;

    private void Start()
    {
        _bodyLength = _moveData.BodyLength;
        _dragLength = _moveData.DragLength;
        _exitLength = _moveData.MinRootToEnd;
        _jumpHeight = _moveData.JumpHeight;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isClicked = true;
            _clickedPosition =_mousePos;
            _rootClicked = _root.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isClicked = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            _mousePos.x = hit.point.x;
            _mousePos.z = hit.point.z;
            Debug.DrawLine(ray.origin, _mousePos, Color.cyan);
        }

        if (!_isClicked)
        {
            //Raise the players head when holding the jump button
            if (Input.GetButtonDown("Jump"))
            {
                _mousePos.y = _root.position.y + _jumpHeight;
            }
            else
            {
                _mousePos.y = _root.position.y;
            }

            //clamp the target to the length of the players body
            if ((_mousePos - _root.position).sqrMagnitude>=_bodyLength*_bodyLength)
            {
                Vector3 clampedPos = (_mousePos - _root.position).normalized * _bodyLength + _root.position;
                _target.position = clampedPos;
            }
            else
            {
                _target.position = _mousePos;
            }
        }
        _rootToEndDistance = Vector3.Distance(_root.position, _target.position);
        if (_rootToEndDistance <= _exitLength)
        {
            return;
        }

        if (_isClicked)
        {
            float currentDrag = Vector3.Distance(_clickedPosition, _mousePos);

            Vector3 dragDirection = (_mousePos - _clickedPosition).normalized * _dragLength;
            Vector3 bestDirection = (_root.position- _clickedPosition).normalized * _dragLength;
            _projection = Vector3.Dot( bestDirection, dragDirection);
            Vector3 dir = (_target.position - _root.position).normalized;
            _root.position = Vector3.Lerp(_rootClicked, _clickedPosition - dir*0.5f , currentDrag / _dragLength);
        }
    }
}
