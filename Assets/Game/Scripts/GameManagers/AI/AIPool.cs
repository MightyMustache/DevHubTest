using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AIPool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 10;
    private DataLoader _dataLoader;
    private  Queue<GameObject> _pool = new Queue<GameObject>();
    

    [Inject]
    public void Constract(DataLoader dataLoader)
    {
        _dataLoader = dataLoader;
    }

    private void Start()
    {

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_dataLoader.AIPrefab, transform.position, _dataLoader.AIPrefab.transform.rotation);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (_pool.Count > 0)
        {
            var obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instantiate(_dataLoader.AIPrefab, transform);
            return newObj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
