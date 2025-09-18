using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 20f;

   public void RotateCharacterTowards(Vector3 rotateDirecion)
    {
        if (rotateDirecion.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(rotateDirecion);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                _rotationSpeed * Time.deltaTime
            );
        }
    }    
}
