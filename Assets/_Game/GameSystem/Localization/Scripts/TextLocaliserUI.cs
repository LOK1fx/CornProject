using LOK1game;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLocaliserUI : MonoBehaviour, ILocalizer
{
    private Text _textField;

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
            _textField = GetComponent<Text>();

            string value = LocalisationSystem.GetLocalisedValue(key);

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
