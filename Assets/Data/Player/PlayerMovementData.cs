using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player/MovementData")]
public class PlayerMovementData : ScriptableObject
{
    public float BodyLength => _bodyLength;
    [SerializeField] private float _bodyLength;
    public float MinRootToEnd=> _minRootToEnd;
    [SerializeField] private float _minRootToEnd;
    public float DragLength => _dragLength;
    [SerializeField] private float _dragLength;
    public float JumpHeight => _jumpHeight;
    [SerializeField] private float _jumpHeight;
}
