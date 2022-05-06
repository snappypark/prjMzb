using UnityEngine;

public class nets : nj.MonoSingleton<nets>
{
    public bolts bolts;

    public static bool checkNotReachable()
    {
        return Application.internetReachability == NetworkReachability.NotReachable;
    }
}
