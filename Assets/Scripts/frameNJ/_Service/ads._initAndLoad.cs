using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public partial class ads : nj.Singleton<ads>
{
    const string _testBannerId = "ca-app-pub-3940256099942544/6300978111";
    const string _testInterId = "ca-app-pub-3940256099942544/1033173712";
    const string _testReawrd1Id = "ca-app-pub-3940256099942544/5224354917";
    const string _testReawrd10Id = "ca-app-pub-3940256099942544/5224354917";
    //const string _testDeviceUniqId = "52e23f2c02d9ef996ca4661316adca58";

    public enum rewardType { none, x1, x10 }
    public rewardType RewardType = rewardType.none;
    NetworkReachability _netReach;
    
    public void Init()
    {
        MobileAds.Initialize((initStatus) =>
        {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                case AdapterState.NotReady:
                    // The adapter initialization did not complete.
                    MonoBehaviour.print("Adapter: " + className + " not ready.");
                    break;
                case AdapterState.Ready:
                    // The adapter was successfully initialized.
                    Debug.Log("Adapter: " + className + " is initialized.");
                    break;
                }
            }
        });

        _netReach = Application.internetReachability;
    }

    public void StartLoad()
    {
        if(_netReach == NetworkReachability.NotReachable && Application.internetReachability != NetworkReachability.NotReachable)
            MobileAds.Initialize(initStatus => { 
                LoadRewardX10();
                LoadBanner();});
        else
        {
            LoadRewardX10();
            LoadBanner();
        }
            
    }
    
    public void PreLoad(int stageIdx)
    {
        if(_netReach == NetworkReachability.NotReachable && Application.internetReachability != NetworkReachability.NotReachable)
            MobileAds.Initialize(initStatus => { });
            
        switch(stageIdx % 5 )  {
            case 1: case 3: LoadInterstitial(); break;
            case 2: LoadRewardX1(); break;
            case 4: LoadRewardX10(); break;
        }
        LoadBanner();
        _netReach = Application.internetReachability;
    }

    public void Check_ForNextStage(int stageIdx)
    {
        _countHp0ForInterstitial = 0;
        if(_netReach == NetworkReachability.NotReachable && Application.internetReachability != NetworkReachability.NotReachable)
            MobileAds.Initialize(initStatus => {});

        switch(stageIdx % 5 )  {
            case 1: case 3: ShowInterstitial(); break;
        }
        
        ShowBanner();
        _netReach = Application.internetReachability;
    }

    AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
          //  .AddTestDevice(AdRequest.TestDeviceSimulator)
         //   .AddTestDevice(_testDeviceUniqId)
            .AddKeyword("game")
            .AddKeyword("puzzle")
            .AddKeyword("action")
            .AddKeyword("adventure")
            .AddKeyword("zombie")
            .AddKeyword("maze")
            //.SetGender(Gender.Male)
            //.SetBirthday(new DateTime(1985, 2, 29))
            .TagForChildDirectedTreatment(false)
            .AddExtra("max_ad_content_rating", "G")
            .AddExtra("color_bg", "000D29")
            .Build();
#if UNITY_EDITOR
#elif UNITY_ANDROID
#elif UNITY_IPHONE
#else
#endif
    }

    class network
    {
        bool _isConnected;
        public void Check()
        {

        }

    }
}
