<a name="top"></a>

[![Unity](https://img.shields.io/badge/Unity-6000.0.40f1-000000?style=flat-square&logo=Unity&logoColor=white)](#)
[![language](https://img.shields.io/badge/language-C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)](#)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?style=flat-square&logo=windows&logoColor=white)](#)


⭐ 3인칭 3D 유니티 프로젝트입니다.


## ⭐ 주요 기능 및 아키텍처

### 1. 플레이어 컨트롤 및 동적 카메라 시스템 (Player & Camera)


- **New Input System 적용:** `Invoke Unity Events` 방식을 채택하여 인스펙터 상에서 직관적으로 이벤트를 연결했습니다. 이를 통해 입력 로직의 하드코딩을 지양하고 휴먼 에러를 최소화하여 유지보수성을 높였습니다.
- **물리 엔진 독립적 이동 (`CharacterController`):** 조작의 즉각성과 정교함이 생명인 슈팅 게임의 특성에 맞춰, Rigidbody 대신 `CharacterController`를 사용하여 미끄러짐 없는 쾌적한 TPS 조작감을 구현했습니다.
<img src="https://github.com/user-attachments/assets/eec45f93-3072-497c-8ae9-3d308583fa51" width="500" alt="데모 GIF" />


- **시네머신 & IK 기반 조준 연출:** 우클릭 조준 시 Cinemachine 카메라 줌인과 Animation Rigging (IK)을 연동했습니다. 캐릭터의 상체와 시선이 십자선(Crosshair)을 정확히 따라가도록 구현하여 조작의 몰입감을 올렸습니다.
<img src="https://github.com/user-attachments/assets/71fef215-09ac-432b-991a-4a87a28f6a3e" width="500" alt="데모 GIF" />

### 2. 데이터 주도 설계 기반의 무기 시스템


- **Scriptable Object(SO) 활용:** 무기의 스펙(데미지, 연사속도 등)을  `Scriptable Object`로 분리하여 데이터화했습니다. 
- **유연한 획득 및 장착 구조:** 필드에서 무기 아이템과 상호작용 시, 무기 스크립트를 새로 생성하지 않고 **해당 무기의 SO 데이터만 플레이어에게 전달**합니다. 이를 통해 즉각적으로 모델링 교체, 애니메이션 레이어 변경, IK 타겟 재설정이 이루어지는 모듈화된 장착 시스템을 구축했습니다.
<img src="https://github.com/user-attachments/assets/b903eaab-5442-4d49-980c-0402169d1989" width="500" alt="데모 GIF" />



### 3. 단일 책임 원칙(SRP)을 준수한 프로젝트의 구조 : 로직의 디커플링 (옵저버 패턴)
- **적의 구조:** 적의 AI나 이동 로직과 체력/피격 데이터를 철저히 분리하기 위해 옵저버 패턴을 디자인 했습니다. 적 소환, 피격, 파괴를 이벤트 형식을 사용해 로직들의 의존성을 낮췄습니다.
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/916fc455-dc70-4e57-842b-13acbf8db7b5" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/7c6322b9-a3fd-452e-b846-3a990ce5ef81" />


- **플레이어의 구조:** 플레이어가 사용중인 무기의 재장전, 조준, 사격의 행동을 옵저버 패턴으로 디자인해서 각 행동에 필요한 애니메이션을 분리해서 로직들의 의존성을 낮췄습니다.
  
  <img width="450" height="400" alt="image" src="https://github.com/user-attachments/assets/82175eba-bbb7-4ba3-bc6d-4f504cec86e3" />
  <img width="500" height="400" alt="image" src="https://github.com/user-attachments/assets/df4216aa-3aec-494f-8657-12734cfd15bd" />





### 4. 렌더링 및 메모리 최적화
- **오브젝트 풀링:** 총구 화염, 피격 이펙트등 짧은 시간에 빈번하게 생성/파괴되는 오브젝트들에 풀링 시스템을 적용했습니다. 미리 생성된 객체를 활성/비활성화하여 재사용함으로써 가비지 컬렉터의 호출을 줄여서 프레임 드랍을 방지했습니다.
<img width="500" height="700" alt="image" src="https://github.com/user-attachments/assets/37e1ad9f-f5ea-412d-b27b-7001f082532c" />
<img src="https://github.com/user-attachments/assets/a6e22fbc-5e4a-4af7-af4d-92a9b93210e0" width="500" alt="데모 GIF" />

