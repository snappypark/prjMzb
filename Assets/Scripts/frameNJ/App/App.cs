using UnityEngine;

namespace nj
{ 
    public class App : MonoSingleton<App>
    {
        public static Layer TouchPlan;

        const int frameRate = 32;//42;// 32;
        public static scBool IsTest = new scBool();

        void Awake()
        {
            Scr.Awake();
            Application.targetFrameRate = frameRate;

#if UNITY_EDITOR
            IsTest = true;
#elif UNITY_ANDROID
            if(!IsVendingApk())
                Application.Quit();
#else
            IsTest = false;
#endif
            UnityEngineEx.RandEx.InitSeedWithTick();
            TouchPlan = new Layer("TouchPlane");
        }


        public static bool IsVendingApk()
        {
            return Application.installerName.Equals("com.android.vending");
        }
        /*
    #region GUI
    enum eGUIType
    {
        NotUse = -1,
        JSonTool = 0,
        TestMode2,
        TestMode3,
    }

    [SerializeField] eGUIType _guiType = eGUIType.NotUse;
    string[] _toolbarStrs = new string[] { "JSonTool", "TestMode2", "TestMode3" };
    string[] _fpEditStrs = new string[] { "Move", "Draw", "Delete" };
    int _fpEditInt = 0;

    void OnGUI()
    {
        if (_guiType == eGUIType.NotUse)
            return;

        _dt += (Time.deltaTime - _dt) * 0.1f;
        GUILayout.BeginArea(new Rect(4, 4, Screen.width - 8, Screen.height));

        GUIStyle guiStyle = new GUIStyle(GUI.skin.button) { fontSize = Screen.height >> 5 };
        GUILayoutOption[] guiOptions0 = new[] { GUILayout.Width(Screen.width), GUILayout.Height(Screen.height >> 5) };
        GUILayoutOption[] guiOptions = new[] { GUILayout.Width(Screen.width * 0.3f), GUILayout.Height(Screen.height >> 4) };

        GUILayout.Label(string.Format("fps:{0:0.00} ", 1.0f / _dt), guiStyle, guiOptions0);
        _guiType = (eGUIType)GUILayout.Toolbar((int)_guiType, _toolbarStrs, GUILayout.Width(Screen.width));

        switch (_guiType)
        {
            case eGUIType.JSonTool:
                {
                    if (GUILayout.Button("New ", guiStyle, guiOptions))
                    {
                    }
                    if (GUILayout.Button("Save ", guiStyle, guiOptions))
                    {
                   //     string jsonStr = JsonUtility.ToJson(GraphMgr.Inst.graphForEdit);
                   //     FileIO.Local.Write("g_Tmp", jsonStr, "txt");
                    }
                    if (GUILayout.Button("Load ", guiStyle, guiOptions))
                    {
                        //string jsonStr  = FileIO.Local.Read("g_Tmp", "txt");
                      //  JsonUtility.FromJsonOverwrite(jsonStr, GraphMgr.Inst.graphForEdit);
                        //App.Inst.JSoner.PhaseVuArtInfo = JsonUtility.FromJson<JSoner_VuArtInfo>(jsonStr);
                        //                        ARs.Inst.LoadByJsonStr_ArtMetaInfo(jsonStr_Fp);
                    }
                }
                break;
            case eGUIType.TestMode2:
                {
                    _fpEditInt = GUILayout.Toolbar(_fpEditInt, _fpEditStrs, guiOptions);
                }
                break;
            case eGUIType.TestMode3:
                {
                }
                break;
        }

        //GUILayout.BeginHorizontal();
        //GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    #endregion*/


        public struct Layer
        {
            public int Mask;
            public Layer(string name)
            {
                Mask = 1<<LayerMask.NameToLayer(name);
            }
        }

        public class Scr
        {
            public static float AspectMin = 1.5f;  //  for height
            public static float Aspect3_2 = 0.66666666667f;  //  for height
            public static float AspectW_H = 0.0f;  // for width
            public static float AspectH_W = 0.0f;  //  for height

            public static int Width = 0;
            public static int Height = 0;
            public static float OverWidth = 0;
            public static float OverHeight = 0;
            public static float HalfWidth = 0;
            public static float HalfHeight = 0;
            public static float HalfOverWidth = 0;
            public static float HalfOverHeight = 0;


            const int scrCommonWidth = 980;//920;//860// 960;//980;//1080;// 1280
#if UNITY_ANDROID || UNITY_IOS
            static int scrWidth = scrCommonWidth;//890// 960;//980;//1080;// 1280//1600
#else
            static int scrWidth = scrCommonWidth;
#endif
            public static void Awake()
            {
                AspectW_H = (float)Screen.width / (float)Screen.height;
                AspectH_W = (float)Screen.height / (float)Screen.width;
#if UNITY_EDITOR
                Width = Screen.width;
#elif UNITY_ANDROID || UNITY_IOS
                Width = Mathf.Min(Screen.currentResolution.width, scrWidth);
#else
                Width = Screen.width ;
#endif
                //  Width = Screen.width;
                //  Height = Screen.height;
                Height = (int)(AspectH_W * Width);
                //Debug.Log("1Screen.Height " + Height);
                //   Debug.Log("Screen.width " + Width);
                Screen.SetResolution(Width, Height, true);

                OverWidth = 1 / Width;
                OverHeight = 1 / Height;
                HalfWidth = Screen.width * 0.5f;
                HalfHeight = Screen.height * 0.5f;
                HalfOverWidth = 1 / HalfWidth;
                HalfOverHeight = 1 / HalfHeight;

            }

            public static bool HasGap_FromCenter()
            {
                Vector3 gap = new Vector3(Input.mousePosition.x - HalfWidth, 0, Input.mousePosition.y - HalfHeight);
                return gap.sqrMagnitude > 1;
            }

            public static Vector3 GetNorXZ_FromCenter()
            {
                Vector3 gap = new Vector3(Input.mousePosition.x - HalfWidth, 0, Input.mousePosition.y - HalfHeight);
                return gap.normalized;
            }

        }
    }

    
}