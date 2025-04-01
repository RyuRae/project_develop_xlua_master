using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using Common;
using System.Collections.Generic;
using Uitls;

namespace FileTools
{
    public class StrConst : Singleton<StrConst>
    {
        //-------------------------------------------------------------------------------------------
        #region 路径
        public const string PATH_BUNDLE_FOLDER = "ab/";//"assetbundles/"
        public const string PATH_CONFIG_FOLDER = "cf/";//"configfiles/
        public const string PATH_AUDIO_FOLDER = "af/";//"audiofiles/"
        public const string PATH_TEXTRUE_FOLDER = "ttf/";//"textruefiles/"
        public const string PATH_LUAASSET_FOLDER = "la/";

        private string _LocalFileAddress;
        public string LocalFileAddress
        {
            get
            {
                if (_LocalFileAddress == null)
                {
                    _LocalFileAddress =
#if UNITY_EDITOR || UNITY_STANDALONE
                        Application.persistentDataPath + "/exam_practice_platform/release/";//Application.streamingAssetsPath + "/Res/";
#elif UNITY_ANDROID
                        Application.persistentDataPath + "/exam_practice_platform/release/";//"jar:file://" + Application.dataPath + "!/assets/" + "Res/";
#else
                        Application.persistentDataPath + "/exam_practice_platform/release/";
#endif
                }

                return _LocalFileAddress;
            }
        }

        public void SetLocalFileAddress(string folder)
        {
            _LocalFileAddress =
#if UNITY_EDITOR || UNITY_STANDALONE
            Application.persistentDataPath + "/exam_practice_platform/release/" + folder + "/";
#elif UNITY_ANDROID
            Application.persistentDataPath + "/exam_practice_platform/release/" + folder + "/";
#else
            Application.persistentDataPath + "/exam_practice_platform/release/" + folder + "/";
#endif

        }

        public static string localPath = Instance.LocalFileAddress + PATH_AUDIO_FOLDER;

        private string _LocalTestFileAddress;

        public string LocalTestFileAddress
        {
            get
            {
                if (_LocalTestFileAddress == null)
                {
                    _LocalTestFileAddress =
                        //#if UNITY_EDITOR || UNITY_STANDALONE
                        Application.streamingAssetsPath + "/LUA/";

                }

                return _LocalTestFileAddress;
            }
        }

        private string _ServerFileAddress;
        public string ServerFileAddress
        {
            get
            {
                if (_ServerFileAddress == null)
                {
                    // _ServerFileAddress = "http://service.airguru.cn/file/downloadBundles/";
                    //http://service.airguru.cn:8080/file/downloadBundles/ ---http://10.0.1.52:8080/Air_ARToolkit/release/

                    //  _ServerFileAddress = "http://127.0.0.1:8080/Air_ARToolkit/release/";

                    _ServerFileAddress = "http://127.0.0.1:8080/HomeBoxResources/";
                    //var content = Util.DataProcess.LoadCSV(Util.Path.GetStreamingAssetsPath("Config/ServerIP.csv"));
                    //Debug.Log(content[1]);
                    //string[] arr = content[1].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);

                    //_ServerFileAddress = string.Format("http://{0}:{1}/HomeBoxResources/", arr[0], arr[1]);
                    Debug.Log(_ServerFileAddress);
                    //"http://10.0.1.52:8080/Air_ARToolkit/release/";http://service.airguru.cn/file/downloadAssets/
                }

                return _ServerFileAddress;
            }
        }

        private string _DownloadTempAddress;
        public string DownloadTempAddress
        {
            get
            {
                if (_DownloadTempAddress == null)
                {
                    _DownloadTempAddress =
#if UNITY_EDITOR || UNITY_STANDALONE
                        Application.persistentDataPath + "/temprelease/";
#elif UNITY_ANDROID
                        Application.persistentDataPath + "/temprelease/";
#else
                        Application.persistentDataPath + "/temprelease/";
#endif
                }

                return _DownloadTempAddress;
            }
        }

#endregion
        //-------------------------------------------------------------------------------------------
#region 字符串
        public const string ANDROID = "Android";
        public const string IOS = "IOS";
        public const string WEBGL = "WebGL";
        public const string WINDOWS = "Windows";
        public const string OSX = "OSX";

        public const string DOT_PREFAB = ".prefab";
        public const string DOT_UNITY = ".unity";
        public const string DOT_AB = ".ab";
        public const string DOT_CON = ".con";
        public const string DOT_TXT = ".txt";
        public const string DOT_CSV = ".csv";
        public const string DOT_JPEG = ".jpeg";
        public const string DOT_PNG = ".png";
        public const string DOT_MP3 = ".mp3";
        public const string DOT_WAV = ".wav";
        public const string DOT_DAT = ".dat";
        public const string DOT_XML = ".xml";
        public const string DOT_LUA = ".lua";
        public const string DOT_JSON = ".json";
        public const string DOT_CSS = ".css";
        public const string DOT_ICO = ".ico";
        public const string DOT_HTML = ".html";
        public const string DOT_TTF = ".ttf";
        public const string DOT_WOFF = ".woff";
        public const string DOT_JS = ".js";


        public static string FILE_PREFIX =
#if UNITY_EDITOR || UNITY_STANDALONE
        "file:///";
#elif UNITY_ANDROID
        "file:///";
#else
        "file:///";
#endif

#endregion
        //-------------------------------------------------------------------------------------------
#region 配置文件名

        public const string CONF_ASSET_RECORD_INFO = "assetrecordsinfo";
        public const string CONF_WORD_LIB = "word_lib";

#endregion

        /// <summary>
        /// 是否按照hash值命名
        /// </summary>
        public static bool IsNameByHash = true;

        //-------------------------------------------------------------------------------------------
    }

    //-----------------------------------------------------------------------------------------------

   

    public enum LoadMethod { WWW, BUNDLE_FILE, HTTP_WEB_REQUEST }

    public enum FileExtension
    {
        NULL = 0,
        EMPTY = 1,
        PREFAB = 2,
        UNITY = 3,
        AB = 4,
        CON = 5,
        TXT = 6,
        CSV = 7,
        JPEG = 8,
        PNG = 9,
        MP3 = 10,
        FSET = 11,
        FSET3 = 12,
        ISET = 13
    }

    public enum RuntimeAssetType
    {
        UNKNOW = 0,
        MANIFEST = 1,
        BUNDLE_SCENE = 2,
        BUNDLE_PREFAB = 3,
        TEXT = 4,
        TEXTRUE = 5,
        AUDIO = 6,
        ARMARK = 7,
        LUAASSET = 8
    }

    public enum FileAddressType { NULL, SERVER, LOCAL, DOWNLOAD_TEMP }

    public enum LoadBehaviour
    {
        Null = 0,
        DownloadFile_WWW = 2,
        ContentLoadFromServer_WWW = 4,
        ContentLoadFromLoacal_WWW = 8,
        ContentLoadFromLoacal_LoadBundleFile = 16,
        DownloadFile_ResumeBrokenTransfer_HttpWebRequest = 32,
    }

}
