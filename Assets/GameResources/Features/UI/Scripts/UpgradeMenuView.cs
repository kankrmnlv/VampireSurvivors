namespace GameResources.Features.UI.Scripts
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Вьюшка апгрейдов
    /// </summary>
    public sealed class UpgradeMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _root = default;

        [SerializeField] private Button _option1Button = default;
        [SerializeField] private Button _option2Button = default;
        [SerializeField] private Button _option3Button = default;

        [SerializeField] private Text _option1Title = default;
        [SerializeField] private Text _option2Title = default;
        [SerializeField] private Text _option3Title = default;

        [SerializeField] private Text _option1Description = default;
        [SerializeField] private Text _option2Description = default;
        [SerializeField] private Text _option3Description = default;

        /// <summary>
        /// Вкл/выкл вьюхи
        /// </summary>
        /// <param name="visible"></param>
        public void SetVisible(bool visible)
        {
            if (_root != null)
            {
                _root.SetActive(visible);
                return;
            }

            gameObject.SetActive(visible);
        }

        /// <summary>
        /// Принимаем и выставляем данные
        /// </summary>
        public void SetOption(int index, string title, string description)
        {
            if (index == 0)
            {
                _option1Title.text = title;
                _option1Description.text = description;
                return;
            }

            if (index == 1)
            {
                _option2Title.text = title;
                _option2Description.text = description;
                return;
            }

            if (index == 2)
            {
                _option3Title.text = title;
                _option3Description.text = description;
                return;
            }
        }

        /// <summary>
        /// Для подвязки логики по нажатию кнопки
        /// </summary>
        public void SetOptionHandler(int index, UnityEngine.Events.UnityAction handler)
        {
            Button button = GetButton(index);
            if (button == null)
            {
                return;
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(handler);
        }

        private Button GetButton(int index)
        {
            if (index == 0)
            {
                return _option1Button;
            }

            if (index == 1)
            {
                return _option2Button;
            }

            if (index == 2)
            {
                return _option3Button;
            }

            return null;
        }
    }
}