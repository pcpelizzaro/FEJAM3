using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenuScript : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject upgradesMenuUI;
    public GameObject textUI;
    public GameScript gameScript;

    [Header("Status")]
    private bool isMenuOpen = false;
    public bool IsMenuOpen => isMenuOpen; // Public read-only property for other scripts

    /// <summary>
    /// Call this from external scripts to open the Upgrades Menu.
    /// </summary>
    void Start()
    {
        upgradesMenuUI.SetActive(false);
    }
    public void OpenMenu()
    {
        if (isMenuOpen) return;

        upgradesMenuUI.SetActive(true);
        isMenuOpen = true;

        // Unlock and show mouse cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Call this to close the menu (wire this to a "Close" button or call externally).
    /// </summary>
    public void CloseMenu()
    {
        if (!isMenuOpen) return;

        upgradesMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume gameplay
        isMenuOpen = false;

        // Lock and hide mouse cursor back for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //gameScript.reset();
        gameScript.IsPlayerStarting = true;
    }

    /// <summary>
    /// Example method for purchasing an upgrade. Wire this to your upgrade buttons.
    /// </summary>
    public void UpgradeHealth()
    {
        Debug.Log("UpgradeHealth pressed");
    }
    public void UpgradeMobility()
    {
        Debug.Log("UpgradeMobility pressed");
    }
    public void UpgradeRocket()
    {
        Debug.Log("UpgradeRocket pressed");
    }
    public void ReturnToGame()
    {
        Debug.Log("ReturnToGame pressed");
        CloseMenu();
    }
}
