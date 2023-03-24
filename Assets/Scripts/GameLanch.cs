using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLanch : MonoBehaviour
{
    private void Awake() {
        // 初始化游戏框架
        this.InitFramework();
        // end 

        // 检查资源更新
        this.CheckHotUpdate();
        // end

        // 初始化游戏逻辑
        this.InitGameLogic();
        // end
    }

    private void CheckHotUpdate() {
        // 获取服务器资源+脚本代码的版本
        // end
        // 拉取下载列表
        // end
        // 下载更新资源到本地
        // end
    }

    private void InitFramework() {
        this.gameObject.AddComponent<ResMgr>();
        this.gameObject.AddComponent<UIMgr>();
    }

    private void InitGameLogic() {
        this.gameObject.AddComponent<GameApp>();
        GameApp.Instance.InitGame();
    }
}
