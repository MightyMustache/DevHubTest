using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _playerAnimator;
    public static readonly int VelocityHash = Animator.StringToHash("Velocity");

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void ChangeVelocityParametr(float velocity)
    {
        _playerAnimator.SetFloat(VelocityHash, velocity);
    }
}
