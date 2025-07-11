E-mail: seunguk.gamja@gmail.com

# 고령층을 대상으로 한 로컬 4인 기능성 게임
대학교 과제로 제작한 프로젝트로, 고령층이 한개의 기기로 4인이 플레이할 수 있는 게임을 개발하는 것이 목표였습니다.

- 카드 짝 맞추기 게임
- 스네이크 게임

---
# 카드 짝 맞추기 게임
### 1. 개요
- 장르: 2D 퍼즐
- 개발인원: 1명
- 개발기간: 2025.03.06~2025.04.30 (약 2개월)
- 개발도구: Unity, Photoshop
- 설명: 로컬 4인 멀티게임인 카드 짝 맞추기 게임이다.
- 기획의도: 고령층이 멀티로 플레이를 해야하니 동시에 조작하는 것보다 순서대로 플레이를 하는 것이 좋을 것 같다고 생각을 하였으며, 이에 카드 짝 맞추기 게임을 떠올려 개발하게 되었습니다.

<br>

### 2. 기대효과
- 단기 기억력 향상
  - 같은 카드를 찾는 과정에서 기억력 향상 효과를 기대
  - 치매 예방 등 초기 예방에 도움
    
- 시지각 능력 훈련
  - 도형, 색상, 문양 등 시각적 패턴을 인식하고 매칭
  - 시각 인지 처리 속도 향상
  - 색맹/시력 저하 예방에도 간접 효과
    
- 주의 집중력 강화
  - 화면 전환 없이 반복되는 같은 화면 내 조작은 주의력과 실수 방지 능력을 높이는 데 효과적

<br>

### 3. 기능 구현
- 카드 생성
  - 값 설정: 카드의 아이디 값을 할당해주고, 스프라이트를 변경해준다.
  - 랜덤 위치: 카드 리스트의 순서를 섞은 다음 위치를 변경해준다.

- 카드 선택
  - 카드 뒤집기: 두트윈을 사용한 애니메이션 효과를 주어 카드가 뒤집히는 것처럼 보이도록 하다.
  - 카드 설정: 선택한 첫 번째와 두 번째 카드를 설정한다.
  
- 카드 매칭
  - 카드 비교하기: 두 카드의 아이디 값을 비교하여 같은 카드인지 비교한다.
  - 카드 판단하기: 같은 카드일 경우에는 카드를 제거하고 해당 플레이어의 점수가 증가한다. 다른 카드일 경우에는 두 카드를 다시 뒤집고 다음 플레이어 순서로 넘어간다.

- 시스템
    - 게임매니저: 카드와 플레이어, 게임의 상태를 관리한다.
    - 사운드매니저: 효과음을 재생한다.
    - 로비매니저: 로비화면에서 씬 이동, 판넬 활성화, 소리 조절 등 로비화면을 관리한다.
    - 플레이어프리팹: 설정에서 조절한 배경음악과 효과음의 값을 저장하고 불러온다.
  
---
# 스네이크 게임
### 1. 개요
- 장르: 3D 액션
- 개발인원: 1명
- 개발기간: 2025.05.07~2025.05.28 (약 3주)
- 개발도구: Unity
- 설명: 로컬 4인 멀티게임인 스네이크 게임이다.
- 기획의도: 고령층이 동시에 조작해도 무리가 없을 게임을 생각하다가 스네이크 게임을 떠올리게 되었으며, 게임의 컨셉을 고령층에게 보다 친숙할만한 장보기로 변경하여 제작하게 되었습니다.

<br>

### 2. 기대효과
- 순발력 및 반응 속도 개선
  - 좌우 회전 입력과 부스터는 순간 판단력과 즉각 반응 능력 훈련에 도움
  - 운전 반응 유지, 보행 중 방향 전환 능력 강화에 효과적
 
- 공간 지각 능력 강화
  - 화면 전체에서 자신의 위치 파악은 공간 인식 훈련에 도움

- 손과 눈의 협응 능력 유지
  - 버튼을 누르는 간단한 조작이지만, 화면을 보면서 방향을 전환하는 것은 눈과 손의 협응 훈련

<br>

### 3. 기능 구현
- 뱀
    - 이동: 일정 속도로 정면을 향해 이동한다.
    - 회전: 일정 속도로 왼쪽 혹은 오른쪽으로 회전한다.
    - 부스터: 속도를 증가시켰다가 일정 시간 후 원래 속도로 돌아온다.
    - 사망: 뱀의 꼬리 위치마다 먹이가 생성되고 리스폰하게 된다.
    - 리스폰: 일정 시간 후 무적과 이동제한이 풀리게 된다.

- 꼬리
    - 정보 저장: 꼬리가 어떤 먹이를 통해 생성되었는지 값을 저장한다.
    - 꼬리 이동: 각 꼬리 순서의 위치값으로 향해 이동한다.
    - 꼬리 추가: 정해둔 간격으로 꼬리가 생성된다.

- 먹이 생성
    - 생성: 일정 시간 이후 맵에 먹이가 생성된다.
    - 삭제: 일정 시간 이후 맵에서 먹이가 사라진다.
 
- 시스템
    - 오브젝트 풀링: 꼬리와 먹이를 오브젝트 풀링을 통해 생성하고 관리한다.
    - 버튼 터치: 해당 버튼을 터치하는 것으로 뱀을 조종한다. (회전, 부스터)
