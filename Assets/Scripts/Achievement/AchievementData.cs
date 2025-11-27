using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Achievement", menuName = "Achivevment/Achievement Data")]
public class AchievementData : ScriptableObject
{
    public string achivevmentName;
    public string description;
    public AchievementType achievementType;
    public int requiredAmount;      //필요수량(예 : 코인 10개)
    public int rewardCoins;         //보상코인
    public bool isUnlocked;         //달성여부
    public Sprite icon;             //업적 아이콘
}
