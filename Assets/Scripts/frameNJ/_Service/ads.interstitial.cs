using System;
using GoogleMobileAds.Api;

public partial class ads
{   
    int _countHp0ForInterstitial = 0;

    InterstitialAd _interstitial = null;

    public void LoadInterstitial(bool isTest = false)
    {
        if(dUser.NoAds)
            return;
        if(_interstitial == null)
        {
            _interstitial = new InterstitialAd("ca-app-pub-9839048061492395/4053301593");
            _interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
            _interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
            _interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;
            _interstitial.LoadAd(this.createAdRequest());
        }
    }

    public void ShowInterstitial()
    {
        if(dUser.NoAds)
            return;
        if(_interstitial != null && _interstitial.IsLoaded())
            _interstitial.Show();
        else
            uis.ad.ShowGeoBreaker();
    }
    
    public void ShowInterstitial_byHp0Count()
    {
        if(dUser.NoAds)
            return;
        if(++_countHp0ForInterstitial >= 2)
            ShowInterstitial();
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args) {
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        if (_interstitial != null)
            _interstitial.Destroy();
        _interstitial = null;
    }
    
    public void HandleInterstitialLeftApplication(object sender, EventArgs args) {
        if (_interstitial != null)
            _interstitial.Destroy();
        _interstitial = null;
    }

    #endregion
}
//https://developers.google.com/admob/unity/interstitial?hl=ko