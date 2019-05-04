using System.Collections;
using UnityEngine;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] private Transform _bar;
//    [SerializeField] private Transform _redBar;
    
    bool loweringBar = false;
    private float _maxHP;
    private float _currentHP;
    private Coroutine _activeCoroutine;

    public static OxygenBar instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init(100f);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("DestroyedGameBar");
        instance = null;
    }

    public void Init(float maxHealth)
    {
        _maxHP = maxHealth;
        _currentHP = maxHealth;
    }
    
    public void UpdateHealth(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            _bar.localScale = new Vector2(0, 1f);
//            _redBar.localScale = new Vector2(0, 1f);
            return;
        }
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
        }
//        _activeCoroutine = StartCoroutine(AdjustRedBar(_currentHP, currentHealth));
        _currentHP = currentHealth;
        _bar.localScale = new Vector2(_currentHP / _maxHP, 1f);
    }

//    private IEnumerator AdjustRedBar(float startingHealth, float currentHealth)
//    {
//        Vector2 startScale = new Vector2(startingHealth / _maxHP, 1f);
//        Vector2 finalScale = new Vector2(currentHealth / _maxHP, 1f);
//        if (!loweringBar)
//        {
//            yield return new WaitForSeconds(0.4f);
//        }
//        loweringBar = true;
//        float t = 0;
//        while (t < 1f)
//        {
//            t += Time.deltaTime;
////            _redBar.localScale = Vector2.Lerp(startScale, finalScale, t);
//            yield return null;
//        }
//        loweringBar = false;
////        _redBar.localScale = finalScale;
//    }
}