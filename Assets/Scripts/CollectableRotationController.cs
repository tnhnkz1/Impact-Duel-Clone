using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRotationController : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(ObjectSpawner.instance.transform);
    }

    void Update()
    {
        transform.DORotate(new Vector3(90, 360, 0), 5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    }
}
