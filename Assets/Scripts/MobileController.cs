using UnityEngine;

public class MobileController : MonoBehaviour
{
    public GameObject mobileControls;
    [SerializeField] private bool forceEnableInEditor = false;

    public void EnableMobileControls()
    {
#if UNITY_EDITOR
        mobileControls.SetActive(forceEnableInEditor);
#else
        mobileControls.SetActive(Application.isMobilePlatform);
#endif
    }
    public void DisableMobileControls()
    {
        mobileControls.SetActive(false);
    }
}
