using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Animateurs de panels")]
    [SerializeField] private InventoryReadMenu   inventoryPanel;
    [SerializeField] private QuestMenuAnimator   questPanel;
    [SerializeField] private OptionsMenuAnimator optionsPanel;

    [Header("Boutons — délégué à InvRead")]
    [SerializeField] private InvRead invRead;

    private enum ActivePanel { None, Inventory, Quest, Options }
    private ActivePanel _current = ActivePanel.None;

    void OnEnable()
    {
        _current = ActivePanel.None;
    }

    // ── Boutons ────────────────────────────────────────────────────────────

    public void OnInventoryClick() => Toggle(ActivePanel.Inventory);
    public void OnQuestClick()     => Toggle(ActivePanel.Quest);
    public void OnOptionsClick()   => Toggle(ActivePanel.Options);
    public void Quitter()          => Application.Quit();

    // ── Logique ────────────────────────────────────────────────────────────

    void Toggle(ActivePanel panel)
    {
        if (_current == panel)
        {
            ForceCloseCurrentPanel();
            _current = ActivePanel.None;
            invRead.SlideButtonsIn();
            return;
        }

        bool wasNone = _current == ActivePanel.None;

        ForceCloseCurrentPanel();

        _current = panel;

        if (wasNone)
            invRead.SlideButtonsOut();

        OpenCurrentPanel();
    }

    void ForceCloseCurrentPanel()
    {
        switch (_current)
        {
            case ActivePanel.Inventory: inventoryPanel.ForceClose(); break;
            case ActivePanel.Quest:     questPanel.ForceClose();     break;
            case ActivePanel.Options:   optionsPanel.ForceClose();   break;
        }
    }

    void OpenCurrentPanel()
    {
        switch (_current)
        {
            // ButtonClickManaged → pas de slide boutons dans InvRead
            case ActivePanel.Inventory: inventoryPanel.ButtonClickManaged(); break;
            case ActivePanel.Quest:     questPanel.Open();                   break;
            case ActivePanel.Options:   optionsPanel.Open();                 break;
        }
    }

    /// <summary>Appelé par InventoryUIPause à la fermeture du menu.</summary>
    public void ResetAll()
    {
        ForceCloseCurrentPanel();
        _current = ActivePanel.None;
        invRead.SlideButtonsIn();
    }
}