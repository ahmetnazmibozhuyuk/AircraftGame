using UnityEngine;
using TMPro;

namespace Aircraft
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private string TextString;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            _text.SetText(TextString);
            Destroy(gameObject, 1);
        }
        private void Update()
        {
            transform.localScale -= Vector3.one * 0.01f;
        }
    }
}
