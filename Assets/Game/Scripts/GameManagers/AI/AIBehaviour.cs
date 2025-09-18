using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    private Coroutine _moveRoutine;
    private CharacterAnimation _characterAnimation;
    private CharacterRotation _characterRotation;


    private void Start()
    {
        _characterAnimation = GetComponent<CharacterAnimation>();
        _characterRotation = GetComponent<CharacterRotation>();
    }

    public void MoveToLocation(Vector3 targetPosition)
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);

        _moveRoutine = StartCoroutine(MoveTo(targetPosition));
    }

    private IEnumerator MoveTo(Vector3 targetPosition)
    {
        _characterAnimation.ChangeVelocityParametr(_moveSpeed);
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                _moveSpeed * Time.deltaTime
            );
            _characterRotation.RotateCharacterTowards(targetPosition);
            yield return null;
        }
        _characterAnimation.ChangeVelocityParametr(0);
        transform.rotation = Quaternion.identity;
        transform.position = targetPosition;
        _moveRoutine = null;
    }

}
