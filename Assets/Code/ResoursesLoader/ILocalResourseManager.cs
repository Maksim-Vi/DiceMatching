using System;
using System.Threading.Tasks;
using UnityObject = UnityEngine.Object;

public interface ILocalResourseManager : IResourseManager
{
    Task<T> LoadAssetByPathAsync<T>(string path) where T : UnityObject;
    T LoadAssetByPath<T>(string path) where T : UnityObject;
    void LoadAllAsset<T>(string path, Action<T[]> onSuccess, Action onFailed = null) where T : UnityObject;
}

