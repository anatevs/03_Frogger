using UnityEngine;

namespace UI
{
    public class LifesPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _views;

        public void ShowLifes(int lifes)
        {
            HideAll();

            for (int i = 0; i < lifes; i++)
            {
                _views[i].SetActive(true);
            }
        }

        public void HideLife(int index)
        {
            _views[index].SetActive(false);
        }

        private void HideAll()
        {
            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].SetActive(false);
            }
        }
    }
}