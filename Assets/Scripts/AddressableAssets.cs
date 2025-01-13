using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AddressableAssets : MonoBehaviour
{
    void Update(){
        if (Input.GetKeyDown(KeyCode.K)){
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Cube.prefab");

            asyncOperationHandle.Completed += AsyncOperationHandleComplete;
        }       
    }

    private void AsyncOperationHandleComplete(AsyncOperationHandle<GameObject> asyncOperationHandle){
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded){
            Instantiate(asyncOperationHandle.Result);
        }
        else{
            Debug.LogError("Fail to Load");
        }
    }
}
