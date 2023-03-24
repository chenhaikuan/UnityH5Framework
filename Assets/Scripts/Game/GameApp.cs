using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : UnitySingleton<GameApp> {

    public void InitGame() {
        this.EnterMainScene();
    }

    public void EnterMainScene() {
        // 主场景， 背景，3D场景，UI
        // 配置文件里面读取我们的关卡的数据---》关卡资源名字
        // string mapName = "Maps/Game.prefab";
        // GameObject mapPrefab = ResMgr.Instance.GetAssetCache<GameObject>(mapName);
        // GameObject map = GameObject.Instantiate(mapPrefab);
        // map.name = mapPrefab.name;
        // 挂游戏管理类的代码了GameMgr;
        // end

        // 释放我们的UI
        UIMgr.Instance.ShowUI("Loading",null,(ctrl)=>{});
        // end 
    }
}
