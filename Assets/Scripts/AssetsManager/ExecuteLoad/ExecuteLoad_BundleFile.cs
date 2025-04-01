using FileTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace FileTools
{
    public class ExecuteLoad_BundleFile : UnityExecuteLoad, ExecuteLoad
    {
        public ExecuteLoad_BundleFile(LoadFile loadFile) : base(loadFile) { }

        public IEnumerator Execute()
        {
            string loadFilePath = FileHelper.GetFilePath(loadFile.CorrelateRecord, FileAddressType.LOCAL);
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromFileAsync(loadFilePath);
            yield return createRequest;
            if (createRequest.isDone)
            {
                loadFile.IsLoadSuccess = true;
                if (loadFile.CorrelateRecord.IsBundle)
                {
                    yield return LoadBundleByRuntimeAssetType(createRequest.assetBundle);
                    //将unload释放在上一个方法中进行是不对的 可能会报错 因为两个bundle地址不同
                    createRequest.assetBundle.Unload(false);
                }
            }
        }

        //public IEnumerator LoadExcute()
        //{
        //    string loadFilePath = FileHelper.GetFilePath(loadFile.CorrelateRecord, FileAddressType.LOCAL);
     
        //    UnityWebRequest request = UnityWebRequest.GetAssetBundle(loadFilePath);
         
        //    yield return request.SendWebRequest();
        
        //    loadFile.IsLoadSuccess = true;
        //    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        //    yield return LoadBundleByRuntimeAssetType(bundle);



        //}
    }
}
