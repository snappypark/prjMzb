using UnityEngine;

public partial class zone
{
    [HideInInspector] public sights.eState sight = sights.eState.ByCtrlUnit;

    bool _hasMeteor = false;

    public enum Option
    {
        Meteor,
    }

    public void AddOption(Option option)
    {
        switch (option)
        {
            case Option.Meteor:
                int stageIdx = dStage.PlayingIdx;
                int i100 = stageIdx % 100;
                int x100 = stageIdx / 100;
                sMeteor.timeGap = 1.7f - i100*0.01f; // 100/50 = 1.0
                sMeteor.numGap = 3 + (int)(i100 * 0.01f) + (int)(x100 * 0.01f);
                sMeteor.speed = 3.0f + i100 * 0.01f + x100 * 0.01f;
                _hasMeteor = true;
                break;
        }
    }

}
