using LOK1game;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPTextLocaliserUI : MonoBehaviour, ILocalizer
{
    private TMP_Text _textField;

    [SerializeField] private string _key;

    private void Start()
    {
        if(_key == null && _textField.text != null)
        {
            Localize(_textField.text);
        }
        else
        {
            Localize(_key);
        }   
    }

    public void Localize(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            _textField = GetComponent<TMP_Text>();

            var value = LocalisationSystem.GetLocalisedValue(key);

            _textField.text = value;
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Update Text")]
    public void EditorUpdate()
    {
        Localize(_key);
    }

#endif
}
