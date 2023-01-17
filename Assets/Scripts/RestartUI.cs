using TMPro;
using UnityEngine;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _attemptText;
    [SerializeField] private TextMeshProUGUI _lastedTimeText;
    

    private void Start()
    {
        Player.OnPlayerDiedEvent += ShowUI;
    }

    private void OnDisable()
    {
        Player.OnPlayerDiedEvent -= ShowUI;
    }

    public void ShowUI()
    {
        _panel.gameObject.SetActive(true);
        _attemptText.text = $"Attempt № {GameManager.Instance.AttemptCount}";
        _lastedTimeText.text = $"You lasted {GameManager.Instance.LastedTime} sec.";
    }

    public void HideUI()
    {
        _panel.gameObject.SetActive(false);
    }
}