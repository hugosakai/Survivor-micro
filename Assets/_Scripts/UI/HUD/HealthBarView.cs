using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private SpriteRenderer _fillBar;
    [SerializeField] private Health _health;
    [SerializeField] private Color _healthColor;
    private float _fillAmount;
    private MaterialPropertyBlock mpb;

    private void OnEnable()
    {
        mpb = new MaterialPropertyBlock();

        // 1) puxa estado atual (opcional, mas bom pra preservar outros props)
        _fillBar.GetPropertyBlock(mpb);

        // 2) grava a cor no MPB
        mpb.SetColor("_Color", _healthColor);

        // 3) APLICA no renderer (você estava chamando Get de novo aqui)
        _fillBar.SetPropertyBlock(mpb);

        if (_health == null) _health = GetComponentInParent<Health>();
        if (_health == null)
        {
            Debug.LogError($"{name} sem referência de Health!");
            enabled = false;
            return;
        }

        _health.OnHealthChanged += UpdateHealth;

        UpdateHealth(_health.CurrentHealth, _health.MaxHealth);
    }

    private void OnDisable()
    {
        if (_health != null) _health.OnHealthChanged -= UpdateHealth;
        if (_fillBar != null) _fillBar.SetPropertyBlock(null); // limpa overrides
    }

    private void UpdateHealth(float current, float max)
    {
        _fillBar.GetPropertyBlock(mpb);

        // calcula proporção
        float percent = current / max;

        // arredonda para baixo
        percent = Mathf.Floor(percent * 1000f) / 1000f; // 3 casas decimais
        // ou mais agressivo:
        // percent = Mathf.FloorToInt(percent * 100f) / 100f; // 2 casas

        // garante 0 se a vida acabou
        if (current <= 0f) percent = 0f;

        mpb.SetFloat("_Fill", percent);
        _fillBar.SetPropertyBlock(mpb);
    }
}
