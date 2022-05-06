using System;
using UnityEngine;
using UnityEngine.Purchasing;

using Beebyte.Obfuscator;

public class InApp : MonoBehaviour, IStoreListener
{
    public static InApp Inst;
    static IStoreController _storeController = null;
    static IExtensionProvider _exProvider = null;

    #region poduct id
    public const string productId1 = "noads";
    #endregion

    void Start()
    {
        Inst = this;
        InitializePurchasing();
    }

    private bool IsInitialized()
    {
        return (_storeController != null && _exProvider != null);
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var module = StandardPurchasingModule.Instance();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(productId1, ProductType.Consumable, new IDs
        {
            { productId1, AppleAppStore.Name },
            { productId1, GooglePlay.Name },
        });
        
        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProductID()
    {
        try
        {
            if (IsInitialized())
            {
                Product p = _storeController.products.WithID(productId1);

                if (p != null && p.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
                    _storeController.InitiatePurchase(p);
                }
                else
                    uis.pops.Show_Ok("Purchase Failed: 0311");
            }
            else
                uis.pops.Show_Ok("Purchase Failed: 0312 Not initialized. sorry");
        }
        catch (Exception e)
        {
                uis.pops.Show_Ok("Purchase Failed: 0313 " + e);
        }
    }

    public void RestorePurchase()
    {
        if (!IsInitialized())
            return;
            
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = _exProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions
                (
                    (result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
                );
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    [SkipRename]
    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
    {
        Debug.Log("OnInitialized : PASS");

        _storeController = sc;
        _exProvider = ep;
    }

    [SkipRename]
    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
    }

    [SkipRename]
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
       // Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        switch (args.purchasedProduct.definition.id)
        {
            case productId1:
            dUser.OnPurchase_NoAds();
            ads.Inst.HideBanner();
            
            uis.pops.Show_Ok("Success: Ads will not show.");
            uis.outgam.menu.Refresh();
            break;
                
        }

        return PurchaseProcessingResult.Complete;
    }

    [SkipRename]
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        uis.pops.Show_Ok("Purchase Failed:" + failureReason);

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
