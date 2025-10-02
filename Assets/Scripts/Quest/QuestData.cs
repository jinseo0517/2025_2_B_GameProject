using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Quest" , menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    [Header("기본 정보")]
    public string questTitle = "새로운 퀘스트";       //퀘스트제목

    [TextArea(2, 4)]
    public string description = "퀘스트 설명을 입력하시오";   //퀘스트내용
    public Sprite questIcon;            //퀘스트 아이콘

    [Header("퀘스트 설정")]        
    public QuestType questType;     //퀘스트 종ㄹㅠ
    public int targetAmount = 1;        //목ㅍㅛㅅㅜㄹㅑㅇ(수지ㅂㅇㅛㅇ)

    [Header("배달 퀘스트용 (Delivery")]
    public Vector3 deliveryPosition;
    public float deliveryRedius = 3f;

    [Header("수집/상호작용 퀘스트용")]
    public string targetTag = "";

    [Header("보상")]
    public int experienceReward = 100;
    public string rewardMessage = "퀘스트 완료";

    [Header("퀘스트 연결")]
    public QuestData nextQuest;     //다음연속퀘스트

    //런타임 데이터 (저장되지않음)
    [System.NonSerialized] public int currentProgresss = 0;
    [System.NonSerialized] public bool isActive = false;
    [System.NonSerialized] public bool isCompleted = false;

    //퀘스트 초기화
    public void Initalize()
    {
        currentProgresss = 0;
        isActive = false;
        isCompleted = false;
    }

    //퀘스트 완료체크
    public bool IsComplete()
    {
        switch (questType)
            {
            case QuestType.Delivery:
                return currentProgresss >= 1;
                
            case QuestType.Collet:
            case QuestType.Interect:
                return currentProgresss >= targetAmount;

            default:
                return false;
        }
    }

    //진행률 계산(0.0 ~ 1.0)
    public float GetProgressPercentage()
    {
        if (targetAmount <= 0) return 0;
        return Mathf.Clamp01((float)currentProgresss / targetAmount);
    }

    //진행상황 텍스트
    public string GetProgressText()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return isCompleted ? "배달완료" : "목적지로 이동하세요";
            case QuestType.Collet:
            case QuestType.Interect:
                return $"{currentProgresss} / {targetAmount}";
            default:
                return "";
        }
    }
}
