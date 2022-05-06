using System;
using GoogleMobileAds.Api;

public partial class ads
{
    BannerView _banner;
    bool _isLoaded = false;
    
    bool _isShown = false;
    public void LoadBanner()
    {
        if(dUser.NoAds)
            return;
        if (_banner != null)
           return;
        _isShown = true;
        _banner = new BannerView("ca-app-pub-9839048061492395/5489957602", AdSize.Banner, AdPosition.Bottom);
        _banner.OnAdLoaded += this.HandleAdLoaded;
        _banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        _banner.OnAdLeavingApplication += this.HandleAdLeftApplication;
        _banner.LoadAd(createAdRequest());
        
    }

    public void ShowBanner()
    {
        if(dUser.NoAds)
            return;
        if(_isShown)
            return;
        _isShown = true;
        
        if (_banner != null && _isLoaded)
        {
            _banner.Show();
            uis.ad.Banner.SetActive(false);
        }
        else
            uis.ad.Banner.SetActive(true);
    }
    
    public void HideBanner()
    {
        if(!_isShown)
            return;
        _isShown = false;

        if (_banner != null && _isLoaded)
            _banner.Hide();
        uis.ad.Banner.SetActive(false);
    }

    public void HandleAdLoaded(object s, EventArgs a) {
        _isLoaded = true;
        uis.ad.Banner.SetActive(false);
    }

    public void HandleAdFailedToLoad(object s, AdFailedToLoadEventArgs a) {
        destoryBanner();
        uis.ad.Banner.SetActive(true);
    }

    public void HandleAdLeftApplication(object s, EventArgs a) {
        //destoryBanner();
    }

    void destoryBanner()
    {
        if (_banner != null)
            _banner.Destroy();
        uis.ad.Banner.SetActive(false);
        _banner = null;
        _isLoaded = false;
    }
}
