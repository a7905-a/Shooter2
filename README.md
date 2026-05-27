[![Unity](https://img.shields.io/badge/Unity-6000.0.40f1-000000?style=flat-square&logo=Unity&logoColor=white)](#)
[![language](https://img.shields.io/badge/language-C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)](#)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?style=flat-square&logo=windows&logoColor=white)](#)

⭐ 3인칭 3D 유니티 프로젝트입니다.

## ⭐ 주요 기능 및 아키텍처

### A. 사용자 입력 처리 및 동적 뷰 관리


- **New Input System 적용:** `Invoke Unity Events` 방식을 채택하여 인스펙터 상에서 직관적으로 이벤트를 연결했습니다. 이를 통해 입력 로직의 하드코딩을 지양하고 휴먼 에러를 최소화하여 유지보수성을 높였습니다.
- **물리 간섭 없는 이동 (`CharacterController`):** 사용자 입력에 대한 즉각적인 피드백, 객체 제어가 요구되는 환경에 맞게 물리 엔진(Rigidbody)의 관성 관섭을 배제하고 CharacterController를 채택하여 직관적이고 안정적인 이동 시스템을 구축했습니다.
<img src="https://github.com/user-attachments/assets/eec45f93-3072-497c-8ae9-3d308583fa51" width="500" alt="데모 GIF" />


- **동적 시점 전환 및 IK 기반 실시간 트래킹:** 특정 상호작용 상태 진입 시, Cinemachine의 시야각(FOV) 제어와 역운동학(Animation Rigging, IK) 기술을 동기화했습니다. 3D 객체의 자세와 시선이 화면의 타겟 포인트를 실시간으로 정확하게 추적하도록 연동하여, 사용자와 객체 간의 자연스러운 상호작용 및 몰입도 높은 조작 환경을 구현했습니다.
<img src="https://github.com/user-attachments/assets/71fef215-09ac-432b-991a-4a87a28f6a3e" width="500" alt="데모 GIF" />

### B. 데이터 주도 설계 기반의 유연한 객체 상태 관리


- **Scriptable Object(SO) 활용해 데이터, 로직 분리:** 객체의 고유 속성(기능적 파라미터, 성능 지표등)을 하드코딩하지 않고 Scriptable Object로 모듈화하여, 데이터 수정에 따른 코드 의존성을 낮추고 유지보수성을 향상시켰습니다.
- **경량화된 런타임 데이터 주입 구조:** 3D환경에서 객체와 상호작용 시 무거운 스크립트를 새로 생성하지 않고, 핵심 데이터(SO)만 사용자 시스템이 참조로 전달합니다. 이를 통해 3D 렌더링 모델 교체, 애니메이션 상태(Layer) 변이, IK 트래킹 재설정 등 복합적인 상태 변화가 즉각적으로 처리되는 확장성 높은 시스템을 구축했습니다.

<img src="https://github.com/user-attachments/assets/b903eaab-5442-4d49-980c-0402169d1989" width="500" alt="데모 GIF" />



### C. 옵저버 패턴 기반 이벤트 구동 아키텍처 : 로직의 디커플링
- **로직과 데이터의 분리:** 객체의 행동 제어 모듈과 상태 변화 데이터를 분리하여 모듈 간의 독립성을 확보했습니다.
- **결합도 최소화:** 객체의 생성, 외부 상호작용에 의한 상태 업데이트, 그리고 메모리 해제(소멸)에 이르는 전체 생명주기를 이벤트 기반으로 설계하여, 각 시스템 간의 코드 의존성을 낮추고 유지보수성을 향상시켰습니다.

<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/916fc455-dc70-4e57-842b-13acbf8db7b5" />
<img width="400" height="350" alt="image" src="https://github.com/user-attachments/assets/7c6322b9-a3fd-452e-b846-3a990ce5ef81" />


- **유연한 모듈 구조:** 객체의 핵심 제어 로직이 시각적 트랜지션(애니메이션) 처리 모듈을 직접 호출하지 않고 이벤트를 발행하도록 구성하여, 그 계층 간의 결합도를 최소화하고 유지보수성을 향상시켰습니다.
  
  <img width="450" height="400" alt="image" src="https://github.com/user-attachments/assets/82175eba-bbb7-4ba3-bc6d-4f504cec86e3" />
  <img width="500" height="400" alt="image" src="https://github.com/user-attachments/assets/df4216aa-3aec-494f-8657-12734cfd15bd" />





### D. 오브젝트 풀링 기반 메모리 및 성능 최적
- **메모리 단편화 및 GC 오버헤드 방지:** 단발성 시각 피드백(파티클 등)이나 이벤트 연출 등 짧은 주기로 빈번하게 생성 및 소멸되는 객체들에 풀링 기법을 적용했습니다.
- - **런타임 성능 안정화:** 런타임 중의 동적 메모리 할당과 해체(Instantiate, Destroy)를 최소화하고, 사전 할당된 인스턴스의 상태값(활성, 비활성)만 제어하여 재사용함으로써 가비지 컬렉션 스파이크로 인한 처리 지연과 렌더링 성능 저하를 방지했습니다.
<img width="500" height="700" alt="image" src="https://github.com/user-attachments/assets/37e1ad9f-f5ea-412d-b27b-7001f082532c" />
<img src="https://github.com/user-attachments/assets/a6e22fbc-5e4a-4af7-af4d-92a9b93210e0" width="500" alt="데모 GIF" />

