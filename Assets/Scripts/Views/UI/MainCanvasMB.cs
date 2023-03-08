using Client;
using Leopotam.EcsLite;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
#region Serialized Classes
[Serializable]
public struct Canvases 
{
    public Canvas _permanentCanvas, _beforeStartCanvas, _playSystemsCanvas, _shopCanvas, _winCanvas, _loseCanvas, _tutorialCanvas;

    public List<Canvas> GetAllCanvases()
    {
        return new List<Canvas>() { _permanentCanvas, _beforeStartCanvas, _playSystemsCanvas, _shopCanvas, _winCanvas, _loseCanvas, _tutorialCanvas };
    }
}

[Serializable]
public struct Panels
{
    public GameObject _permanentPanel, _beforeStartPanel, _playSystemsPanel, _shopPanel, _winPanel, _losePanel, _tutorialPanel;
    public List<GameObject> GetAllPanels()
    {
        return new List<GameObject>() { _permanentPanel, _beforeStartPanel, _playSystemsPanel, _shopPanel, _winPanel, _losePanel, _tutorialPanel };
    }
}
[Serializable]
public struct PanelsMB
{
    public PanelPermanentMB _permanentPanelMB; 
    public PanelBeforeStartMB _beforeStartPanelMB; 
    public PanelPlaySystemsMB _playSystemsPanelMB; 
    public PanelShopMB _shopPanelMB; 
    public PanelWinMB _winPanelMB; 
    public PanelLoseMB _losePanelMB; 
    public PanelTutorialMB _tutorialPanelMB;
}
[Serializable]
public struct SettingsPanel
{
    public Image
        music_image,
        sound_image,
        vibro_image;
}
#endregion

public class MainCanvasMB : MonoBehaviour
{
    public bool NeverDisablePermanent;
    public Canvases canvases;
    public Panels panels;
    public PanelsMB panelsMB;
    public RectTransform settingsPanelRect;
    [Header("Settings Panel")]
    public SettingsPanel settingsPanel;
    public float settingsPanelSpeed = 20f;
    public Vector2 settingsPanelOffset;
    public Vector2 initialSettingsOffset;
    private bool isSettingsOpen = false;
    [Space]
    private List<Button> _allButtons = new List<Button>();
    private List<Canvas> _allCanvases = new List<Canvas>();
    private List<GameObject> _allPanels = new List<GameObject>();

    #region ECS + Init()
    private EcsWorld _world;
    private GameState _state;
    private EcsPool<InterfaceComponent> _interfacePool = default;

    public GameState State { get { return _state; } }
    public void Init(EcsWorld world, GameState state)
    {
        world = state.EcsWorld;
        _world = world;
        _state = state;
        _interfacePool = _world.GetPool<InterfaceComponent>();
        GetAllButtons();
        _allCanvases.Clear();
        _allPanels.Clear();
        _allCanvases = canvases.GetAllCanvases();
        _allPanels = panels.GetAllPanels();
        initialSettingsOffset = settingsPanelRect.anchoredPosition;
        DisablePanelsOnStart(panels._playSystemsPanel, panels._shopPanel, panels._winPanel, panels._losePanel, panels._tutorialPanel);
        DisableCanvasesOnStart(canvases._playSystemsCanvas, canvases._shopCanvas, canvases._winCanvas, canvases._loseCanvas, canvases._tutorialCanvas);
    }

    #endregion

    #region Panels
    private void EnableCanvas(Canvas canvas, bool disableOtherCanvases)
    {
        if (disableOtherCanvases)
        {
            DisableAllCanvasesExceptFor(canvas);
        }
        else
        {
            canvas.enabled = true;
        }
    }
    private void EnablePanel(GameObject panel, bool disableOtherPanels)
    {
        if (disableOtherPanels)
        {
            DisableAllPanelsExceptFor(panel);
        }
        else
        {
            panel.SetActive(true);
        }
    }
    public void EnableTutorialPanel(bool disableOtherPanels)
    {
        if (canvases._tutorialCanvas == null)
        {
            EnablePanel(panels._tutorialPanel, disableOtherPanels);
        }
        else
        {
            EnableCanvas(canvases._tutorialCanvas, disableOtherPanels);
        }
    }
    public void EnableLosePanel(bool disableOtherPanels)
    {
        if (canvases._loseCanvas == null)
        {
            EnablePanel(panels._losePanel, disableOtherPanels);
        }
        else
        {
            EnableCanvas(canvases._loseCanvas, disableOtherPanels);
        }
    }
    public void EnableWinPanel(bool disableOtherPanels)
    {
        if (canvases._winCanvas == null)
        {
            EnablePanel(panels._winPanel, disableOtherPanels);
        }
        else
        {
            EnableCanvas(canvases._winCanvas, disableOtherPanels);
        }
    }
    public void EnablePlaySystemsPanel(bool disableOtherPanels)
    {
        if(canvases._playSystemsCanvas == null)
        {
            EnablePanel(panels._playSystemsPanel, disableOtherPanels);
        }
        else
        {
            EnableCanvas(canvases._playSystemsCanvas, disableOtherPanels);
        }
        GameState.Get().GameMode = GameMode.play;
    }
    public void EnableBeforeStartPanel(bool disableOtherPanels)
    {
        if (canvases._beforeStartCanvas == null)
        {
            EnablePanel(panels._beforeStartPanel, disableOtherPanels);
        }
        else
        {
            EnableCanvas(canvases._beforeStartCanvas, disableOtherPanels);
        }
    }
    public void EnableShopPanel(bool disableOtherPanels)
    {
        if (canvases._shopCanvas == null)
        {
            EnablePanel(panels._shopPanel, disableOtherPanels);
        }
        else
        {
            EnableCanvas(canvases._shopCanvas, disableOtherPanels);
        }
    }
    #endregion

    #region Shared Methods
    private void GetAllButtons()
    {
        foreach (var button in transform.GetComponentsInChildren<Button>(true))
        {
            _allButtons.Add(button);
        }
    }

    public void SetActiveAllCanvases(bool state)
    {
        foreach (var canvas in canvases.GetAllCanvases())
        {
            canvas.enabled = state;
        }
        if (NeverDisablePermanent)
        {
            canvases._permanentCanvas.enabled = true;
        }
    }
    public void SetActiveAllPanels(bool state)
    {
        foreach (var panel in panels.GetAllPanels())
        {
            panel.SetActive(state);
        }
        if (NeverDisablePermanent)
        {
            panels._permanentPanel.SetActive(true);
        }
    }

    public void DisableAllPanelsExceptFor(GameObject panel)
    {
        SetActiveAllPanels(false);
        panel.SetActive(true);
    }
    public void DisableAllCanvasesExceptFor(Canvas canvas)
    {
        SetActiveAllCanvases(false);
        canvas.enabled = true;
    }

    public void SetInteractableAllButtons(bool state)
    {
        foreach (var button in _allButtons)
        {
            button.interactable = state;
        }
    }
    public void DisableAllButtonsExceptFor(Button button)
    {
        SetInteractableAllButtons(false);
        button.interactable = true;
    }

    private void DisablePanelsOnStart(params GameObject[] Panels)
    {
        foreach (GameObject panel in Panels)
        {
            if (canvases._playSystemsCanvas != null) return;
            panel.SetActive(false);
        }
    }
    private void DisableCanvasesOnStart(params Canvas[] canvases)
    {
        foreach (Canvas canvas in canvases)
        {
            if (canvas == null) return;
            canvas.enabled = false;
        }
    }

    public void ReloadScene()
    {
        _state.LoadScene(_state.SceneNumber);
    }
    public void LoadNextScene()
    {
        int nextSceneIndex = _state.LastSceneNumber - 1 < _state.SceneNumber + 1 ? 0 : _state.SceneNumber + 1;
        _state.LoadScene(nextSceneIndex);
    }
    public void LoadScene(int sceneNumber)
    {
        _state.LoadScene(sceneNumber);
    }

    #endregion

    #region Settings Panel
    public void SettingsClick()
    {
        StopAllCoroutines();
        if (isSettingsOpen) 
        {
            IEnumerator coroutine = MoveSettingsPanel(initialSettingsOffset);
            StartCoroutine(coroutine);
        }
        else
        {
            StartCoroutine(MoveSettingsPanel(settingsPanelOffset));
        }
        isSettingsOpen = !isSettingsOpen;
    }

    private IEnumerator MoveSettingsPanel(Vector2 endPos)
    {
        Start:
        if (settingsPanelRect.anchoredPosition != endPos)
        {
            settingsPanelRect.anchoredPosition = Vector2.Lerp(settingsPanelRect.anchoredPosition, endPos, Time.unscaledDeltaTime * settingsPanelSpeed);
            yield return new WaitForEndOfFrame();
            goto Start;
        }
        else
        {
            yield return null;
        }
    }

    public void SettingsMusicClick()
    {
        _state.Music = !_state.Music;
        settingsPanel.music_image.sprite = _state.Music ? _state.InterfaceConfig.music_on : _state.InterfaceConfig.music_off;
        //if (_state.Music == false) { ... }
    }
    public void SettingsSoundsClick()
    {
        _state.Sounds = !_state.Sounds;
        settingsPanel.sound_image.sprite = _state.Sounds ? _state.InterfaceConfig.sound_on : _state.InterfaceConfig.sound_off;
        //if (!_state.Sounds) { ... }
    }
    public void SettingsVibroClick()
    {
        _state.Vibration = !_state.Vibration;
        settingsPanel.vibro_image.sprite = _state.Vibration ? _state.InterfaceConfig.vibro_on : _state.InterfaceConfig.vibro_off;
        // ...
    }
    #endregion

    #region TEST WIN & LOSE SYSTEMS
    public void AddWinEvent()
    {
        _world.GetPool<EnableWinSystemsEvent>().Add(_world.NewEntity());
    }
    public void AddLoseEvent()
    {
        _world.GetPool<EnableLoseSystemsEvent>().Add(_world.NewEntity());
    }
    #endregion
}
