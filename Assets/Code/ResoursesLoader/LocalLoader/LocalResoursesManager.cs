using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public class LocalResoursesManager : IResourseManager, ILocalResourseManager
{
    public void LoadAsset<T>(string path, Action<T> onSuccess, Action onFailed = null) where T : UnityObject
    { 
        var slicedUrl = path.Replace(".png", "").Replace(".jpg", "");

        T res = Resources.Load<T>(slicedUrl);

        if (res != null)
        {
            onSuccess(res);
        } else
        {
            if(onFailed != null)
                onFailed();
        }
    }    
    
    public void LoadAllAsset<T>(string path, Action<T[]> onSuccess, Action onFailed = null) where T : UnityObject
    { 
        var slicedUrl = path.Replace(".png", "").Replace(".jpg", "");

        T[] res =  Resources.LoadAll<T>(slicedUrl);

        if (res != null)
        {
            onSuccess(res);
        } else
        {
            if(onFailed != null)
                onFailed();
        }
    }

    public async Task<T> LoadAssetByPathAsync<T>(string path) where T : UnityObject
    {
        await Task.Yield();

        return Resources.Load<T>(path);
    }

    public T LoadAssetByPath<T>(string path) where T : UnityObject
    {
        return Resources.Load<T>(path);
    }

    public Sprite LoadEmpty()
    {
        return Resources.Load<Sprite>($"Collections/dice_unknown");
    }
}