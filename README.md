<a name="top"></a>

[![Unity](https://img.shields.io/badge/Unity-6000.0.40f1-000000?style=flat-square&logo=Unity&logoColor=white)](#)
[![language](https://img.shields.io/badge/language-C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)](#)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?style=flat-square&logo=windows&logoColor=white)](#)


⭐ 데이터 주도 설계(Data-Driven Design)와 단일 책임 원칙(SRP)을 적용한 3인칭 슈팅(TPS) 게임 프로젝트입니다.

## 📑 목차
- 개요
- 주요 기능 및 아키텍처

## 🚀 개요

이 프로젝트는 단순한 기능 구현을 넘어, Scriptable Object를 활용하여 데이터 주도 설계을 적용했으며 이를 통한 '유지보수가 용이한 아키텍처 구축'에 중점을 두고 개발한 3D 프로젝트입니다. 

특히 기획 변경에 유연하게 대처할 수 있도록 Scriptable Object를 적극 활용하였으며, 객체 간의 결합도를 낮추기 위해 옵저버 패턴과 구조적 리팩토링을 진행했습니다.

## ✨ 주요 기능 및 아키텍처

### 1. 플레이어 컨트롤 및 동적 카메라 시스템 (Player & Camera)


- **New Input System 적용:** `Invoke Unity Events` 방식을 채택하여 인스펙터 상에서 직관적으로 이벤트를 연결했습니다. 이를 통해 입력 로직의 하드코딩을 지양하고 휴먼 에러를 최소화하여 유지보수성을 높였습니다.
- **물리 엔진 독립적 이동 (`CharacterController`):** 조작의 즉각성과 정교함이 생명인 슈팅 게임의 특성에 맞춰, Rigidbody 대신 `CharacterController`를 사용하여 미끄러짐 없는 쾌적한 TPS 조작감을 구현했습니다.
- **시네머신 & IK 기반 정밀 조준 연출:** 마우스 델타값을 활용한 부드러운 시점 변환을 바탕으로, 우클릭 조준 시 Cinemachine 카메라 줌인과 **Animation Rigging (IK)**을 연동했습니다. 캐릭터의 상체와 시선이 십자선(Crosshair)을 정확히 따라가도록 구현하여 조작의 몰입감을 극대화했습니다.

### 2. 데이터 주도 설계 기반의 무기 시스템 (Data-Driven Weapon System)


- **Scriptable Object(SO) 활용:** 수많은 무기의 스펙(데미지, 탄퍼짐 등)과 리소스(모델링, 애니메이션 레이어)를 `Scriptable Object`로 분리하여 데이터화했습니다. 
- **유연한 획득 및 장착 구조:** 필드에서 무기 아이템과 상호작용 시, 무기 스크립트를 새로 생성하지 않고 **해당 무기의 SO 데이터만 플레이어에게 전달**합니다. 이를 통해 즉각적으로 모델링 교체, 애니메이션 레이어 변경, IK 타겟 재설정이 이루어지는 모듈화된 장착 시스템을 구축했습니다.

### 3. 단일 책임 원칙(SRP)을 준수한 전투 및 웨이브 관리 (Battle & Enemy)
- **로직의 디커플링 (`EnemyHealth`):** 적의 AI나 이동 로직과 체력/피격 데이터를 철저히 분리하기 위해 전담 스크립트(`EnemyHealth`)를 설계했습니다. 플레이어의 무기는 대상의 구체적인 타입을 몰라도 `TakeDamage` 인터페이스만 호출하도록 결합도를 낮췄습니다.
- **트리거 기반 스폰 및 스테이지 제어:** 보이지 않는 `Collider Trigger`를 활용해 플레이어 진입 시 적이 스폰되도록 동적 환경을 구성했습니다. 해당 구역의 적 객체가 모두 파괴되면 다음 스테이지로 넘어가는 문(Portal)이 개방되는 이벤트 주도(Event-Driven) 형태의 진행을 구현했습니다.

### 4. 렌더링 및 메모리 최적화 (Optimization & UI)
- **오브젝트 풀링 (Object Pooling) 아키텍처:** 총구 화염(Muzzle Flash), 피격 이펙트, 탄알 궤적(Trail) 등 짧은 시간에 빈번하게 생성/파괴되는 오브젝트들에 풀링 시스템을 적용했습니다. 미리 생성된 객체를 활성/비활성화하여 재사용함으로써 가비지 컬렉터(GC) 호출을 억제하고 런타임 프레임 드랍을 방지했습니다.
- **빌보드(Billboard) 방식의 World UI:** World Space 캔버스를 활용하여 적의 체력 바를 구현했습니다. 플레이어의 카메라가 어느 방향에서 보더라도 체력 바가 항상 정면을 향해 렌더링 되도록 수학적(`LookRotation`)으로 처리하여 직관적인 피드백을 제공합니다.

# 2. Unity Hub를 실행합니다.
# 3. 'Add project from disk'를 클릭하여 클론한 폴더를 선택합니다.
# 4. Unity 버전을 2022.3.x (본인 버전 기입)로 맞춘 후 프로젝트를 엽니다.
# 5. [Assets/Scenes/MainScene]을 열고 Play 버튼을 눌러 게임을 실행합니다!
