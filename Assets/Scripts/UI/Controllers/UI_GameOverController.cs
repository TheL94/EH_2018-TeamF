using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI_GameOverController : MenuBase {

    public void Init()
    {
        GameManager.I.UIMng.CurrentMenu = this;
        FindISelectableObects();
        SelectableButtons[0].IsSelected = true;
    }

    public override void Select()
    {
        switch (CurrentIndexSelected)
        {
            case 0:
                //GameManager.I.ChangeFlowState(FlowState.Menu);
                SceneManager.LoadScene(0);
                break;
        }
    }
}
