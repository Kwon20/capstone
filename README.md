# capstone

### 파일별 설명

1.Door  : 던전 방의 문(상하좌우)  
2.DungeonCrawler : (0,0)에서 부터 상하좌우 1칸씩 움직이며 좌표를 리턴하는 객체  
3.DungeonCrawlerController : 던전 크롤러가 가져온 좌표가 겹치지 않게 저장  
4.DungeonGeneratorData : 랜덤맵 생성 시 크롤러의 갯수와 방의 최대 최소 갯수  
5.DungeonGenerator : 방 번호를 셔플 후 좌표별로 생성 될 방 번호 부여  
6.Room : 던전 방(상하좌우 방이 있는 지 확인/문 열고 닫기)  
7.RoomController : 방의 씬을 불러온 후 지정된 좌표로 이동
