using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;        //매니저 싱글톤화

    [Header("UI 요소들")]
    public GameObject questUI;      //퀘스트패널 ui
    public Text questTitleText;     //퀘스트 타이틀 텍스트
    public Text questDescriptionText;   //퀘스트 내용
    public Text questProgressText;      //진행 상태
    public Button completButton;        //완료버튼

    [Header("퀘스트 목록")]
    public QuestData[] availableQuests;     //내가가지고있는퀘스트 몫록

    private QuestData currentQuest;         //진행중인 ㅌ쿠ㅔ스트 데이터
    private int currentQuestIndex = 0;      //퀘스트 목록중에 진행중인 번호

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(availableQuests.Length > 0)
        {
            StartQuest(availableQuests[0]);     //시작시 가지고 있는 첫번쨰 배열의 퀘스트를 진행
        }
        if(completButton != null)
        {
            completButton.onClick.AddListener(CompleteCurrentQuest);        //완료 버튼을 완료 함수와 연결
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentQuest != null && currentQuest.isActive)      //퀘스트 진행중인지 체크후
        {
            CheckQuesrProgress();       //퀘스트 진행상테 함수호출
            UpdateQuestUI();        //퀘스트ui함수호추\ㄹ
        }
    }

    //UI 업데이트 (퀘스트진행상황 ui로 표시)
    void UpdateQuestUI()
    {
        if (currentQuest == null) return;

        if (questTitleText != null)
        {
            questTitleText.text = currentQuest.questTitle;
        }

        if (questDescriptionText != null)
        {
            questDescriptionText.text = currentQuest.description;
        }
        if (questProgressText != null)
        {
            questProgressText.text = currentQuest.GetProgressText();
        }
    }

    //퀘스트 시작
    public void StartQuest(QuestData quest)
    {
        if (quest == null) return;

        currentQuest = quest;
        currentQuest.Initalize();       //퀘
        currentQuest.isActive = true;

        Debug.Log("퀘스트 시작 : " + questTitleText);
        UpdateQuestUI();
        if(questUI != null)
        {
            questUI.SetActive(true);
        }
    }
    //배달 퀘스트 진랭 체크
    void CheckDeliveryProgress()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;       //유저으 ㅣ위치를 찾는다
        if (player == null) return;

        float distance = Vector3.Distance(player.position, currentQuest.deliveryPosition);  //유저와 도착지 거리를 계산한다

        if (distance <= currentQuest.deliveryRedius)                //유저의 거리가 도착범위 안쪽인지 검사
        {
            if(currentQuest.currentProgresss == 0)
            {
                currentQuest.currentProgresss = 1;                  //퀘스트 완료
            }
        }
        else
        {
            currentQuest.currentProgresss = 0;                      //도착하지못함
        }
    }

    //수집 퀘스트 진행(외부에서 호출)
    public void AddCollectProgress(string itemTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Collet && currentQuest.targetTag == itemTag)
        {
            currentQuest.currentProgresss++;
            Debug.Log("아이템 수집 : " + itemTag);
        }
    }

    //상호작용 퀘스트 진행 (외부에서 호출)
    public void AddInteracProgress(string objectTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Interect && currentQuest.targetTag == objectTag)
        {
            currentQuest.currentProgresss++;
            Debug.Log("상호작용완료: " + objectTag);
        }
    }

    //현재 퀘스트 완료
    public void CompleteCurrentQuest()
    {
        if (currentQuest == null || !currentQuest.isCompleted) return;

        Debug.Log("퀘스트완료 ! " + currentQuest.rewardMessage);

        //완료버튼 비활성화
        if (completButton != null)
        {
            completButton.gameObject.SetActive(false);
        }
        //다음 퀘스트가 있으면 시작
        currentQuestIndex++;
        if (currentQuestIndex < availableQuests.Length)
        {
            StartQuest(availableQuests[currentQuestIndex]);
        }
        else
        {
            currentQuest = null;
            if(questUI != null)
            {
                questUI.gameObject.SetActive(false);
            }
        }
    }

    //퀘스트 진행 체크
    void CheckQuesrProgress()
    {
        if(currentQuest.questType == QuestType.Delivery)
        {
            CheckDeliveryProgress();
        }
        //퀘스트 완료체크
        if (currentQuest.IsComplete() && !currentQuest.isCompleted)
        {
            currentQuest.isCompleted = true;

            //완료버튼 활성화
            if (completButton != null)
            {
                completButton.gameObject.SetActive(true);
            }
        }
    }
    
}
