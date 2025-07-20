using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject introPanel;   // parent of BtnEnter/Guide/Quit
    public GameObject guidePanel;   // a 3D text object you’ll create
    public GameObject endPanel;     // parent of Restart/Quit end buttons

    public void EnterVR()
    {
        introPanel.SetActive(false);
    }
    public void ShowGuide()
    {
        guidePanel.SetActive(true);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void Restart()
    {
        endPanel.SetActive(false);
        introPanel.SetActive(true);
    }
}
