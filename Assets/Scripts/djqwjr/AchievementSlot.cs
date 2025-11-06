using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AchievementSlot : MonoBehaviour
{
    [Header("UI References")]
    public Image iconlmage;
    public Text nameText;
    public Text descriptionText;
    public Text progressText;
    public Slider progressSlider;

    public void SetAchievement(AchievementData achievement, float progress)
    {
        if (nameText != null)
            nameText.text = achievement.achivevmentName;

        if (descriptionText != null)
            descriptionText.text = achievement.description;

        if (iconlmage != null && achievement.icon != null)
            iconlmage.sprite = achievement.icon;

        if (progressSlider != null)
            progressSlider.value = achievement.isUnlocked ? 1f : progress;
        if (progressText != null)
        {
            if (achievement.isUnlocked)
            {
                progressText.text = "¿Ï·á!";
            }
            else
            {
                int current = Mathf.FloorToInt(progress * achievement.requiredAmount);
                progressText.text = current + "/"+ achievement.requiredAmount;
            }
        }
    }
}
