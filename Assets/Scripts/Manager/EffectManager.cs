using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // [Serializable] ªÁøÎ¿ª ¿ß«ÿ √ﬂ∞°

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    // [System.Serializable]∑Œ ºˆ¡§«œø© Inspectorø°º≠ µ•¿Ã≈Õ ∏Ò∑œ¿ª ∫º ºˆ ¿÷∞‘ «’¥œ¥Ÿ.
    [System.Serializable]
    public class EffectData
    {
        public string effectName;       // ¿Ã∆Â∆Æ¿Ã∏ß
        public GameObject effectPrefabs;    // ¿Ã∆Â∆Æ «¡∏Æ∆’
        public float defaultDuration = 2f;  // ±‚∫ª ¡ˆº” Ω√∞£
    }

    [Header("¿Ã∆Â∆Æ ∏Ò∑œ")]
    [SerializeField] private List<EffectData> effectList = new List<EffectData>();

    private Dictionary<string, EffectData> effectDictionary = new Dictionary<string, EffectData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionary() // ∏ÆΩ∫∆Æ∏¶ µÒº≈≥ ∏Æ∑Œ ∫Ø»Ø
    {
        effectDictionary.Clear();
        foreach (var effect in effectList)
        {
            // effectName¿Ã null¿Ã∞≈≥™ ∫ÒæÓ¿÷¥¬¡ˆ »Æ¿Œ«œ¥¬ ∞Õ¿Ã ¡¡Ω¿¥œ¥Ÿ.
            if (string.IsNullOrEmpty(effect.effectName))
            {
                Debug.LogWarning("¿Ã∏ß¿Ã æ¯¥¬ ¿Ã∆Â∆Æ µ•¿Ã≈Õ∞° ¿÷Ω¿¥œ¥Ÿ.");
                continue;
            }

            if (!effectDictionary.ContainsKey(effect.effectName))
            {
                effectDictionary.Add(effect.effectName, effect);
            }
            else
            {
                Debug.LogWarning($"¡ﬂ∫πµ» ¿Ã∆Â∆Æ ¿Ã∏ß : {effect.effectName}");
            }
        }
    }

    // ∏ﬁº≠µÂ ¿Ã∏ß¿« ¿œ∞¸º∫¿ª ¿ß«ÿ PlayEffect∑Œ ≈Î«’«œ¥¬ ∞Õ¿ª ±«¿Â«’¥œ¥Ÿ.
    // ±‚¡∏ PlayerEffect∏¶ PlayEffect¿« ø¿πˆ∑ŒµÂ∑Œ ∫Ø∞Ê«’¥œ¥Ÿ.
    public GameObject PlayEffect(string effectName, Vector3 position, Quaternion rotation)
    {
        if (effectDictionary.TryGetValue(effectName, out EffectData data))
        {
            GameObject effect = Instantiate(data.effectPrefabs, position, rotation);
            Destroy(effect, data.defaultDuration);
            return effect;
        }
        else
        {
            Debug.LogWarning($"¿Ã∆Â∆Æ∏¶ √£¿ª ºˆ æ¯Ω¿¥œ¥Ÿ. : {effectName}");
            return null;
        }
    }

    public GameObject PlayEffect(string effectName, Vector3 position, Quaternion rotation, float duration)  //¿Ã∆Â∆Æ ¿Áª˝ ¡ˆº” Ω√∞£ º≥¡§¿ª ∞°¥…«œ∞‘«—¥Ÿ
    {
        if (effectDictionary.TryGetValue(effectName, out EffectData data))
        {
            GameObject effect = Instantiate(data.effectPrefabs, position, rotation);
            Destroy(effect, duration);
            return effect;
        }
        else
        {
            Debug.LogWarning($"¿Ã∆Â∆Æ∏¶ √£¿ª ºˆ æ¯Ω¿¥œ¥Ÿ. : {effectName}"); // "¿Ã∆Â∆ÆµÈ ØÇ¿ª ºˆ æ¯Ω¿¥œ¥Ÿ." ø¿≈∏ ºˆ¡§
            return null;
        }
    }

    // ≥™∏”¡ˆ ø¿πˆ∑ŒµÂ ∏ﬁº≠µÂ¥¬ ±◊¥Î∑Œ ¿Ø¡ˆ
    public GameObject PlayEffect(string effectName, Vector3 position)
    {
        return PlayEffect(effectName, position, Quaternion.identity);
    }

    public GameObject PlayEffect(string effectName, Vector3 position, float duration)
    {
        return PlayEffect(effectName, position, Quaternion.identity, duration);
    }

    // ƒ⁄∑Á∆æ Ω√¿€ ∏ﬁº≠µÂ: PlayEffectDelayed ƒ⁄∑Á∆æ¿ª Ω√¿€
    public void PlayEffectWithDelay(string effectName, Vector3 position, Quaternion rotation, float delay, float duration)
    {
        // PlayEffectDelayed ƒ⁄∑Á∆æ¿ª Ω√¿€«’¥œ¥Ÿ.
        StartCoroutine(PlayEffectDelayed(effectName, position, rotation, delay, duration));
    }

    // ƒ⁄∑Á∆æ ∏ﬁº≠µÂ ¿Ã∏ß ºˆ¡§: PlayerEffectDealyed -> PlayEffectDelayed
    private IEnumerator PlayEffectDelayed(string effectName, Vector3 position, Quaternion rotation, float delay, float duration) // 'duartion' ø¿≈∏ ºˆ¡§
    {
        yield return new WaitForSeconds(delay);

        // duration ø¿≈∏ ºˆ¡§ π◊ duration¿Ã 0∫∏¥Ÿ ≈´¡ˆ »Æ¿Œ«œø© ø¿πˆ∑Œµ˘µ» PlayEffect »£√‚
        if (duration > 0)
        {
            PlayEffect(effectName, position, rotation, duration);
        }
        else
        {
            PlayEffect(effectName, position, rotation);
        }
    }
}