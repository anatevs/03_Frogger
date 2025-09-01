using UnityEngine;

namespace UI
{
    public class PlatformUIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _mobileUI;

        private void Awake()
        {
            ShowMobile(Application.isMobilePlatform);
        }

        private void ShowMobile(bool isMobile)
        {
            for (int i = 0; i < _mobileUI.Length; i++)
            {
                _mobileUI[i].SetActive(isMobile);
            }
        }
    }
}