using UnityEditor;
using UnityEngine;

// 💡 유니티 에디터가 켜질 때 이 스크립트를 자동으로 메모리에 올리라는 뜻입니다.
[InitializeOnLoad]
public class SpawnNodePainter
{
    // 배치 모드가 켜져있는지 확인하는 스위치
    static bool isPainting = false;

    // 생성자: 씬 뷰가 화면을 그릴 때마다 우리가 만든 함수를 실행하도록 연결(구독)합니다.
    static SpawnNodePainter()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    // 💡 유니티 상단 메뉴에 툴을 켜고 끄는 버튼을 만듭니다.
    [MenuItem("Tools/스폰 노드 배치 모드 (ON/OFF)")]
    static void TogglePainter()
    {
        isPainting = !isPainting;
        Debug.Log("스폰 노드 배치 모드: " + (isPainting ? "🟢 켜짐" : "🔴 꺼짐"));
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        // 배치 모드가 꺼져있으면 아무 일도 하지 않고 돌아갑니다.
        if (!isPainting) return; 

        // 현재 마우스/키보드 입력 상태를 가져옵니다.
        Event e = Event.current; 

        // 💡 Shift 키를 누른 상태로 마우스 왼쪽(0) 버튼을 클릭했다면?
        if (e.type == EventType.MouseDown && e.button == 0 && e.shift)
        {
            // 2D 모니터 화면의 마우스 위치를 3D 월드의 가상 레이저(Ray)로 변환합니다.
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            
            // 바닥(콜라이더가 있는 맵)에 레이저가 맞았다면
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 1. 빈 게임오브젝트를 생성합니다.
                GameObject newNode = new GameObject("SpawnNode_Point");
                
                // 2. 레이저가 맞은 바닥 위치로 오브젝트를 이동시킵니다.
                newNode.transform.position = hit.point;
                
                // 3. 우리가 만든 SpawnNode 스크립트를 부착합니다.
                newNode.AddComponent<SpawnBase>();

                // 💡 필수 소양: Ctrl + Z (실행 취소)를 눌렀을 때 지워지도록 에디터 역사에 기록합니다!
                Undo.RegisterCreatedObjectUndo(newNode, "스폰 노드 생성");

                // 4. 유니티가 이 클릭 이벤트를 다른 용도(예: 뒷배경 오브젝트 선택)로 쓰지 못하게 꿀꺽 삼켜버립니다.
                e.Use();
            }
        }
    }
}
