# blazor-mini

---

# 프로젝트 소개

Blazor를 이용해 만든 채팅과 방명록입니다.

개발기간: 2024.05.31 ~ 2024.06.04

---
### 방명록


![image](https://github.com/briiiqtt/blazor-mini/assets/89250347/8f4c57c4-85fd-4793-9105-c67191c7c577)
방명록은 MySQL과의 연결 과정과 Blazor단에서의 처리를 보여드리는 것을 목적으로 간단하게 구현하였습니다.

### 채팅

[시연 영상(0분22초)](https://drive.google.com/file/d/1sJI_9ls1SjHEM1f7-dt5AdNAYWOCKcsG/view?usp=sharing)


채팅은 다음 내용으로 구성되어 있습니다.
- signalR 클라이언트에서 서버의 허브에 연결과 콜백을 설정
- 허브에서 이벤트별 처리를 구현하고 허브를 등록
- 선택할 수 있는 그룹별로 분리된 채팅방 제공

### 
