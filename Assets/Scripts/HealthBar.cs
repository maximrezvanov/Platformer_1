using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image health;
    [SerializeField] private float delta;
    private float healthBarValue;
    private Player player;
    private float currentHealth;

    void Start()
    {
        player = FindObjectOfType<Player>();
        healthBarValue = player.Health.CurrentHealth / 100.0f;
    }

    void Update()
    {
        currentHealth = player.Health.CurrentHealth / 100.0f;

        if (currentHealth > healthBarValue)
            healthBarValue += delta;
        if (currentHealth < healthBarValue)
            healthBarValue -= delta;

        if (currentHealth < delta)
            healthBarValue = currentHealth;

        health.fillAmount = healthBarValue;
    }
}
