using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public interface IResourseManager
{
    void LoadAsset<T>(string path, Action<T> onSuccess = null, Action onFailed = null) where T : UnityObject;
    Sprite LoadEmpty();
}

