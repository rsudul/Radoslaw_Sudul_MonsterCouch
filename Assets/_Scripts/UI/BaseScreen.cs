using UnityEngine;
using UnityEngine.UI;

namespace MonsterCouchTest.UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [Header("Screen Root")]
        [SerializeField] private GameObject _panelRoot;

        [Header("Focus")]
        [SerializeField] private Selectable _firstSelectable;

        public Selectable FirstSelectable => _firstSelectable;

        public virtual void Show()
        {
            if (_panelRoot != null)
            {
                _panelRoot.SetActive(true);
            }
        }

        public virtual void Hide()
        {
            if (_panelRoot != null)
            {
                _panelRoot.SetActive(false);
            }
        }
    }
}