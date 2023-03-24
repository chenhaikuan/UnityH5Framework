using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLanch : MonoBehaviour
{
    private void Awake() {
        // ��ʼ����Ϸ���
        this.InitFramework();
        // end 

        // �����Դ����
        this.CheckHotUpdate();
        // end

        // ��ʼ����Ϸ�߼�
        this.InitGameLogic();
        // end
    }

    private void CheckHotUpdate() {
        // ��ȡ��������Դ+�ű�����İ汾
        // end
        // ��ȡ�����б�
        // end
        // ���ظ�����Դ������
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
