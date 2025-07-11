using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AIManager : MonoBehaviour
{

    [SerializeField] private float _minDistanceFromPlayer = 0.3f;
    [SerializeField] private float _radiusOffset = 1f;
    public readonly List<GameObject> AIOnscene = new List<GameObject>();

    private AIPool _aIPool;
    private GameObject _player;

    [Inject(Id = "MidArea")]
    private GameObject _spawnAreaGO;

    [Inject(Id = "LeftArea")]
    private GameObject _leftAreaGO;

    [Inject(Id = "RightArea")]
    private GameObject _rightAreaGO;



    [Inject]
    public void Constract(AIPool aIPool, PlayerMovement playerMovement)
    {
        _aIPool = aIPool;
        _player = playerMovement.gameObject;
    }

    private float GetWorldRadius(SphereCollider spherecollider)
    {
        return spherecollider.radius * Mathf.Max(
        spherecollider.transform.lossyScale.x,
        spherecollider.transform.lossyScale.z
        );
    }

    private Vector3 GetRandomPosInCircle(SphereCollider spherecollider)
    {
        Vector3 worldCenter = spherecollider.transform.position + spherecollider.center;
        Vector2 randomCircle = Random.insideUnitCircle * (GetWorldRadius(spherecollider) - _radiusOffset);
        return new Vector3(worldCenter.x + randomCircle.x, 0, worldCenter.z + randomCircle.y);
    }

    public void AISpawn(int amount)
    {
        SphereCollider spawnArea = _spawnAreaGO.GetComponent<SphereCollider>();

        for (int i = 0; i < amount; i++)
        {
            GameObject gotospawn = _aIPool.Get();
            AIOnscene.Add(gotospawn);
            Vector3 spawnPosition = GetRandomPosInCircle(spawnArea);

            Vector3 toPlayer = spawnPosition - _player.transform.position;
            float distance = toPlayer.magnitude;

            if (distance < _minDistanceFromPlayer)
            {
                Vector2 randomDir2D = Random.insideUnitCircle.normalized;
                Vector3 randomDir = new Vector3(randomDir2D.x, 0f, randomDir2D.y);

                spawnPosition = _player.transform.position + randomDir * _minDistanceFromPlayer;
            }

            gotospawn.transform.position = spawnPosition;
        }
    }

    public void AIMoveInPosition()
    {
        SphereCollider leftArea = _leftAreaGO.GetComponent<SphereCollider>();
        SphereCollider rightArea = _rightAreaGO.GetComponent<SphereCollider>();

        for (int i = 0; i < AIOnscene.Count; i++)
        {
            GameObject ai = AIOnscene[i];
            SphereCollider targetArea = (i % 2 == 0) ? leftArea : rightArea;

            Vector3 posToMove = GetRandomPosInCircle(targetArea);
            ai.GetComponent<AIBehaviour>().MoveToLocation(posToMove);
        }
    }

    public void AIAllSceneClear()
    {
        foreach (var go in AIOnscene)
        {
            _aIPool.ReturnToPool(go);
        }
        AIOnscene.Clear();
    }
}
