using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid){

    }
}
public class AddressableAssets : MonoBehaviour
{   
    [SerializeField] private AssetReferenceGameObject assetReferenceObject; //reference to addressable prefab path
    [SerializeField] private AssetReferenceAudioClip assetReferenceAudioClip; //reference to addressable audioClip path
    [SerializeField] private AudioSource audioSource;
    private bool test;
    GameObject addressableGameObjectContainer;
    AudioClip addressableAudioContainer;

    private void Start()
    {
        //load audio clip from addressable
       assetReferenceAudioClip.LoadAssetAsync<AudioClip>().Completed +=
        (AsyncOperationHandleComplete) => {
            if (AsyncOperationHandleComplete.Status == AsyncOperationStatus.Succeeded){
                addressableAudioContainer = audioSource.clip = AsyncOperationHandleComplete.Result;
            }
            else{
                Debug.LogError("Failed To Load");
            }
        };

        assetReferenceObject.LoadAssetAsync<GameObject>().Completed +=
         (AsyncOperationHandleComplete1) =>
         {
             if (AsyncOperationHandleComplete1.Status == AsyncOperationStatus.Succeeded)
             {
                 addressableGameObjectContainer = AsyncOperationHandleComplete1.Result;
             }
             else
             {
                 Debug.LogError("Failed To Load");
             }
         };
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.K)){
            Instantiate(addressableGameObjectContainer);
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Backspace)){
            assetReferenceObject.ReleaseInstance(addressableGameObjectContainer);
            Destroy(addressableGameObjectContainer);
        }

    }
}
