using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 10;
    [Range(0f, 1f)]
    public float armor = 0.5f;
    public Text healthText;

    public DeathEvent OnDeathEvent { get; private set; }

    public bool IsDead => health < 0;

    private void Awake()
    {
        OnDeathEvent = new DeathEvent();
    }

    private void Start()
    {
        SetUiHealth();
    }

    public void Hurt(float damage)
    {
        health -= damage * (1f - armor);
        SetUiHealth();
        if (IsDead)
        {
            OnDeathEvent.Dispatch();
            gameObject.SetActive(false);
        }
    }

    private void SetUiHealth()
    {
        healthText.text = health.ToString();
    }
}
