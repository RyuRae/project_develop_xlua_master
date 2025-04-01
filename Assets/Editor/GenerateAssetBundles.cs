using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using FileTools;
using System;
using System.Text;

public class PackingAgent
{
    #region 配置
    //public static bool isIncrement = false; //是否是增量打包 如果是要在打包前保留之前所有已打包的bundle
    public static string mainManifestName = FileHelper.GetMainManifestName(); 
    public static string sourceFolderAbsolutePath = Application.dataPath + "/ABPrefabs/";
    public static string tempForPackingFolderAbsolutePath = new StringBuilder().AppendFormat("{0}/{1}/", Application.streamingAssetsPath, mainManifestName).ToString();
    public static string tempForPackingFolderRelativePath = "Assets" + tempForPackingFolderAbsolutePath.Replace(Application.dataPath, "");

    public static string releaseParentFolderAbsolutePath = "C:/Users/liulei/Desktop/release/";//C:\Users\liulei\Desktop\exam_practice_platform
    public static string releaseBundleFolderAbsolutePath = releaseParentFolderAbsolutePath + FileTools.StrConst.PATH_BUNDLE_FOLDER;
    public static string releaseAudioFolderAbsolutePath = releaseParentFolderAbsolutePath + FileTools.StrConst.PATH_AUDIO_FOLDER;
    public static string releaseTextrueFolderAbsolutePath = releaseParentFolderAbsolutePath + FileTools.StrConst.PATH_TEXTRUE_FOLDER;
    public static string releaseConfigFolderAbsolutePath = releaseParentFolderAbsolutePath + FileTools.StrConst.PATH_CONFIG_FOLDER;
    public static string releaseLuaFolderAbsolutePath = releaseParentFolderAbsolutePath + FileTools.StrConst.PATH_LUAASSET_FOLDER;

    //public static string releaseARMarkFolderAbsolutePath = releaseParentFolderAbsolutePath + StrConst.PATH_ARMARK_FOLDER;
    public static Dictionary<RuntimeAssetType, string> releaseFolderAbsolutePaths = new Dictionary<RuntimeAssetType, string>
    {
        { RuntimeAssetType.MANIFEST,  releaseBundleFolderAbsolutePath },
        { RuntimeAssetType.BUNDLE_PREFAB,  releaseBundleFolderAbsolutePath },
        { RuntimeAssetType.BUNDLE_SCENE,  releaseBundleFolderAbsolutePath },
        { RuntimeAssetType.AUDIO,  releaseAudioFolderAbsolutePath },
        { RuntimeAssetType.TEXTRUE,  releaseTextrueFolderAbsolutePath },
        { RuntimeAssetType.TEXT,  releaseConfigFolderAbsolutePath },
        { RuntimeAssetType.LUAASSET,  releaseLuaFolderAbsolutePath },
    };

    public static string packingRecordFileFolderAbsolutePath = releaseParentFolderAbsolutePath;
    public static string packingRecordFileName = FileTools.StrConst.CONF_ASSET_RECORD_INFO + FileTools.StrConst.DOT_CON;
    public static string packingRecordFileAbsolutePath = packingRecordFileFolderAbsolutePath + packingRecordFileName;

    //public static HashSet<string> ignoreToClearFiles = new HashSet<string>
    //{
    //    packingRecordFileName,
    //    StrConst.CONF_WORD_LIB + StrConst.DOT_CSV,
    //};

    public static HashSet<string> ignoreToClearFileExtensions = new HashSet<string>
    {
        FileTools.StrConst.DOT_CON,
        FileTools.StrConst.DOT_CSV,
        FileTools.StrConst.DOT_MP3,
        FileTools.StrConst.DOT_WAV,
        FileTools.StrConst.DOT_PNG,
        FileTools.StrConst.DOT_JPEG,
        FileTools.StrConst.DOT_TXT,
        FileTools.StrConst.DOT_XML,
        FileTools.StrConst.DOT_LUA,
        FileTools.StrConst.DOT_JSON,
        FileTools.StrConst.DOT_ICO,
        FileTools.StrConst.DOT_HTML,
        FileTools.StrConst.DOT_CSS,
        FileTools.StrConst.DOT_TTF,
        FileTools.StrConst.DOT_WOFF,
        FileTools.StrConst.DOT_JS
    };

    private HashSet<string> convertToBundleFileExtensions = new HashSet<string>
    {
        FileTools.StrConst.DOT_PREFAB,
        FileTools.StrConst.DOT_UNITY,
    };

    public static HashSet<string> toBeRecordedExtensionsFilter = new HashSet<string>
    {
        FileTools.StrConst.DOT_AB,
        FileTools.StrConst.DOT_CON,
        FileTools.StrConst.DOT_CSV,
        FileTools.StrConst.DOT_MP3,
        FileTools.StrConst.DOT_WAV,
        FileTools.StrConst.DOT_PNG,
        FileTools.StrConst.DOT_JPEG,
        FileTools.StrConst.DOT_TXT,
        FileTools.StrConst.DOT_XML,
        FileTools.StrConst.DOT_LUA,
        FileTools.StrConst.DOT_JSON
    };

    #endregion
    // ------------------------------------------------------------------------------------------------------------------------------
    #region 辅助打包
    public bool ShouldBeBundle(FileInfo fileInfo)
    {
        fileInfo.Refresh();
        return fileInfo.Exists && convertToBundleFileExtensions.Contains(fileInfo.Extension.ToLower());
    }

    public List<FileInfo> GetFileInfoListForPacking()
    {
        List<FileInfo> fileInfoList = new List<FileInfo>();
        DirectoryInfo dir = new DirectoryInfo(sourceFolderAbsolutePath);            //获取目录信息
        FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);  //获取所有的文件信息
        var it = files.GetEnumerator();
        while (it.MoveNext())
        {
            FileInfo fileInfo = (FileInfo)it.Current;
            if (ShouldBeBundle(fileInfo))
                fileInfoList.Add(fileInfo);
        }
        return fileInfoList;
    }

    public string GetBundleName(FileInfo fileInfo)
    {
        return AssetRecord.GetBundleName(fileInfo);
    }

    public string[] GetAssetNames(FileInfo fileInfo)
    {
        string resourceRelativePath = "Assets" + fileInfo.FullName.Replace("\\", "/").Replace(Application.dataPath, "");
        return new string[] { resourceRelativePath };
    }

    public bool ShouldBeInReleaseFolder(FileInfo fileInfo)
    {
        fileInfo.Refresh();
        return fileInfo.Exists && (fileInfo.Name.EndsWith(FileTools.StrConst.DOT_AB) || fileInfo.Name.Equals(mainManifestName));
    }

    public bool ShouldBeRecorded(FileInfo fileInfo)
    {
        fileInfo.Refresh();
        return fileInfo.Exists && toBeRecordedExtensionsFilter.Contains(fileInfo.Extension.ToLower());

    }
    #endregion
    // ------------------------------------------------------------------------------------------------------------------------------
    #region 生成打包信息

    public void GeneratePackingRecord()
    {
        CheckPackingRecordFileAbsolutePath();
        AssetRecordsInfo oldInfo = GetOldPackingRecord();
        AssetRecordsInfo newInfo = GetNewPackingRecord();
        AssetRecordsInfo finalInfo = GetFinalPackingRecord(oldInfo, newInfo);
        WritePackingRecord(finalInfo);
    }

    private void CheckPackingRecordFileAbsolutePath()
    {
        if (!Directory.Exists(packingRecordFileFolderAbsolutePath))
        {
            Directory.CreateDirectory(packingRecordFileFolderAbsolutePath);
        }
    }

    private AssetRecordsInfo GetOldPackingRecord()
    {
        AssetRecordsInfo oldInfo = new AssetRecordsInfo();
        FileHelper.ReadFromBinaryFile(packingRecordFileAbsolutePath, oldInfo.ReadByBinaryReader_Summary);
        if (string.IsNullOrEmpty(oldInfo.IndexName))
        {
            oldInfo = new AssetRecordsInfo(new FileInfo(packingRecordFileAbsolutePath));
        }
        return oldInfo;
    }

    private AssetRecordsInfo GetNewPackingRecord()
    {
        AssetRecordsInfo newInfo = new AssetRecordsInfo();
        Action<AssetRecord> onRecordCreated = null; //为类似生成文件后记录MD5的操作 预留
        long totalSize = 0L;
        HashSet<string> checkedFolderPaths = new HashSet<string>();
        var it = releaseFolderAbsolutePaths.Values.GetEnumerator();
        while (it.MoveNext())
        {
            string folderPath = it.Current;
            if (!checkedFolderPaths.Contains(folderPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
                if (dirInfo.Exists && !checkedFolderPaths.Contains(folderPath))
                {
                    SetRecords(dirInfo, newInfo, onRecordCreated, ref totalSize);
                    checkedFolderPaths.Add(folderPath);
                }
            }
        }
        newInfo.UpdateInfoAndCorrectionData(mainManifestName, totalSize);
        return newInfo;
    }

    private void SetRecords(DirectoryInfo dirInfo,
                            AssetRecordsInfo assetRecordsInfo,
                            Action<AssetRecord> onRecordCreated,
                            ref long totalSize)
    {
        FileInfo[] fileInfoArray = dirInfo.GetFiles("*", SearchOption.AllDirectories);
        var it = fileInfoArray.GetEnumerator();
        while (it.MoveNext())
        {
            FileInfo fileInfo = (FileInfo)it.Current;
            if (fileInfo.Name.Equals(mainManifestName) || ShouldBeRecorded(fileInfo))
            {
                AssetRecord record = new AssetRecord(fileInfo, mainManifestName);
                RuntimeAssetType type = FileHelper.GetRuntimeAssetType((FileExtension)record.OrigExt);
                Dictionary<string, AssetRecord> records = assetRecordsInfo.GetRecordsDic(type);
                if (records != null && !records.ContainsKey(record.IndexName))
                {
                    records.Add(record.IndexName, record);
                    if (onRecordCreated != null)
                        onRecordCreated(record);
                    totalSize += record.Size;
                }
            }
        }
    }

    private AssetRecordsInfo GetFinalPackingRecord(AssetRecordsInfo oldInfo, AssetRecordsInfo newInfo)
    {
        // compare
        return newInfo;
    }


    private void WritePackingRecord(AssetRecordsInfo assetRecordsInfo)
    {
        //写入记录文件
        FileHelper.WriteToBinaryFile(packingRecordFileAbsolutePath, assetRecordsInfo.WriteByBinaryWriter);
        assetRecordsInfo.PrintInfo();
    }

    #endregion
    // ------------------------------------------------------------------------------------------------------------------------------
}

public class GenerateAssetBundles
{
    [MenuItem("AssetBundle/GenerateAllAssetBundles")]
    static void GenerateAllAssetBundles()
    {
        PackingAgent agent = new PackingAgent();

        //检查资源路径
        if (!CheckSourceFolderAbsolutePath(PackingAgent.sourceFolderAbsolutePath)) return;

        //先清除之前设置过的AssetBundleName，避免产生不必要的资源也打包
        ClearOldAssetBundleNames();

        //设置临时输出路径
        CheckTempForPackingFolderAbsolutePath(PackingAgent.tempForPackingFolderAbsolutePath);

        //设置打包的文件队列
        AssetBundleBuild[] buildArray = GetBuildList(agent.GetFileInfoListForPacking(), 
                                                     agent.GetBundleName, agent.GetAssetNames).ToArray();

        //设置打包选项
        BuildAssetBundleOptions buildOptions = BuildAssetBundleOptions.DeterministicAssetBundle;
        //buildOptions |= BuildAssetBundleOptions.ChunkBasedCompression; //LZM4压缩方式

        //打包
        BuildPipeline.BuildAssetBundles(PackingAgent.tempForPackingFolderRelativePath, 
                                        buildArray,
                                        buildOptions, 
                                        EditorUserBuildSettings.activeBuildTarget);

        //清除发布目录 为新的bundle移入做准备
        CheckAndClearReleaseAbsolutePath(PackingAgent.releaseFolderAbsolutePaths.Values, PackingAgent.ignoreToClearFileExtensions);

        //将打包好的bundle以及主manifest文件移动到发布目录
        MoveBundleFilesToReleaseFolder(PackingAgent.tempForPackingFolderAbsolutePath,
                                       PackingAgent.releaseBundleFolderAbsolutePath,
                                       agent.ShouldBeInReleaseFolder);

        //生成打包记录文件
        agent.GeneratePackingRecord();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Popup("All Generated (￣▽￣) !");
    }

    // -----------------------------------------------------------------------------------
    #region functions

    static bool CheckSourceFolderAbsolutePath(string sourceFolderAbsolutePath)
    {
        // 检查 资源路径
        if (!Directory.Exists(sourceFolderAbsolutePath))
        {
            Popup("The target resource is not exist");
            return false;
        }

        return true;
    }

    static void CheckTempForPackingFolderAbsolutePath(string tempForPackingFolderAbsolutePath)
    {
        // 检查 输出路径
        if (Directory.Exists(tempForPackingFolderAbsolutePath))
        {
            Directory.Delete(tempForPackingFolderAbsolutePath, true);
        }

        if (!Directory.Exists(tempForPackingFolderAbsolutePath))
        {
            Directory.CreateDirectory(tempForPackingFolderAbsolutePath);
        }
    }

    static void ClearOldAssetBundleNames()
    {
        string[] oldAssetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < oldAssetBundleNames.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[i], true);
        }
        AssetDatabase.Refresh();
    }

    static List<AssetBundleBuild> GetBuildList(List<FileInfo> fileInfoListForPacking, 
                                               Func<FileInfo, string> getBundleName, 
                                               Func<FileInfo, string[]> getAssetName)
    {
        List<AssetBundleBuild> assetBundleBuildList = new List<AssetBundleBuild>();
        if (fileInfoListForPacking != null)
        {
            var it = fileInfoListForPacking.GetEnumerator();
            while (it.MoveNext())
            {
                FileInfo fileInfo = it.Current;
                string assetBundleName = getBundleName(fileInfo);
                if (!string.IsNullOrEmpty(assetBundleName))
                {
                    AssetBundleBuild build = new AssetBundleBuild();
                    build.assetBundleName = assetBundleName;
                    build.assetNames = getAssetName(fileInfo);
                    assetBundleBuildList.Add(build);
                }
                else
                {
                    Debug.Log("资源 : " + fileInfo.Name + "记录失败");
                }
            }
        }
        return assetBundleBuildList;
    }

    static void CheckAndClearReleaseAbsolutePath(Dictionary<RuntimeAssetType, string>.ValueCollection paths, 
                                                 HashSet<string> ignoreToClearFileExtensions)
    {
        var it_folder = paths.GetEnumerator();
        while (it_folder.MoveNext())
        {
            string releaseFolderAbsolutePath = it_folder.Current;
            if (!Directory.Exists(releaseFolderAbsolutePath))
            {
                Directory.CreateDirectory(releaseFolderAbsolutePath);
                continue;
            }

            DirectoryInfo dir = new DirectoryInfo(releaseFolderAbsolutePath);
            FileInfo[] fileInfoArray = dir.GetFiles("*", SearchOption.AllDirectories);
            var it_fileInfo = fileInfoArray.GetEnumerator();
            while (it_fileInfo.MoveNext())
            {
                FileInfo fi = (FileInfo)it_fileInfo.Current;
                if (!ignoreToClearFileExtensions.Contains(fi.Extension.ToLower()))
                {
                    File.Delete(@fi.FullName.Replace("\\", "/"));
                }
            }
        }
    }

    static void MoveBundleFilesToReleaseFolder(string tempForPackingFolderAbsolutePath, 
                                               string releaseBundleFolderAbsolutePath, 
                                               Func<FileInfo, bool> shouldBeInReleaseFolder)
    {
        DirectoryInfo dir = new DirectoryInfo(tempForPackingFolderAbsolutePath);
        FileInfo[] files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);  //获取所有的文件信息
        for (var i = 0; i < files.Length; ++i)
        {
            FileInfo fileInfo = files[i];
            if (shouldBeInReleaseFolder(fileInfo))
            {
                string copyFromName = fileInfo.FullName.Replace("\\", "/");
                string copyToName = releaseBundleFolderAbsolutePath + fileInfo.Name;
                File.Copy(copyFromName, copyToName, true);
            }
            //File.Delete(sourceFileName);
        }
        Directory.Delete(tempForPackingFolderAbsolutePath, true);
    }

    #endregion
    // -----------------------------------------------------------------------------------

    #region helper functions

    const string STR_TITLE_SETNAME = "设置AssetName名称";
    const string STR_CONTENT_SETNAME = "正在设置AssetName名称中...";
    const string STR_TITLE_WARNNING = "注意 注意 注意 重要的的事情吼三遍 吼 吼";
    const string STR_BUTTON_CONFIRM = "Got it !";
    //显示进程加载条
    static void ProgressBar(float progress, string title, string content, bool isOpen)
    {
        if (isOpen)
        {
            EditorUtility.DisplayProgressBar(title, content, 0f);
        }
        else
        {
            EditorUtility.ClearProgressBar();   //清除进度条
        }
    }

    static void Popup(string content)
    {
        EditorUtility.DisplayDialog(STR_TITLE_WARNNING, content, STR_BUTTON_CONFIRM);
    }

    #endregion

    // -----------------------------------------------------------------------------------
    //[MenuItem("TestFunction/Operate")]
    //static void TestFunction()
    //{
    //    //Debug.Log("Test ； " + Enum.IsDefined(typeof(FileExtension), "PREFAB"));
    //}
}