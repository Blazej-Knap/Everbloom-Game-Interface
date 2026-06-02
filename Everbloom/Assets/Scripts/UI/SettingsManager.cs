using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public Toggle muzykaToggle;
    public Toggle dzwiekiToggle;
    public Slider glosnoscSlider;

    private const string MuzykaKey = "Settings_Muzyka";
    private const string DzwiekiKey = "Settings_Dzwieki";
    private const string GlosnoscKey = "Settings_Glosnosc";

    private void Start()
    {
        LoadSettings();
        
        // Add listeners
        if (muzykaToggle != null) muzykaToggle.onValueChanged.AddListener(OnMuzykaChanged);
        if (dzwiekiToggle != null) dzwiekiToggle.onValueChanged.AddListener(OnDzwiekiChanged);
        if (glosnoscSlider != null) glosnoscSlider.onValueChanged.AddListener(OnGlosnoscChanged);
    }

    private void LoadSettings()
    {
        if (muzykaToggle != null) muzykaToggle.isOn = PlayerPrefs.GetInt(MuzykaKey, 1) == 1;
        if (dzwiekiToggle != null) dzwiekiToggle.isOn = PlayerPrefs.GetInt(DzwiekiKey, 1) == 1;
        if (glosnoscSlider != null) glosnoscSlider.value = PlayerPrefs.GetFloat(GlosnoscKey, 0.75f);
    }

    private void OnMuzykaChanged(bool isOn)
    {
        PlayerPrefs.SetInt(MuzykaKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log($"Muzyka: {isOn}");
        GlobalInputManager.ApplyAudioSettings();
    }

    private void OnDzwiekiChanged(bool isOn)
    {
        PlayerPrefs.SetInt(DzwiekiKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log($"Dźwięki: {isOn}");
        GlobalInputManager.ApplyAudioSettings();
    }

    private void OnGlosnoscChanged(float value)
    {
        PlayerPrefs.SetFloat(GlosnoscKey, value);
        PlayerPrefs.Save();
        Debug.Log($"Głośność: {value}");
        GlobalInputManager.ApplyAudioSettings();
    }
}
