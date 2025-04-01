using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldSubmit : MonoBehaviour
{
    [Serializable]
    public class OnSubmitEvent : UnityEvent<InputFieldSubmit, string> { }

    /// <summary>输入框提交事件</summary>
    public OnSubmitEvent onSubmit = null;

    private InputField inputField;

    void Awake()
    {
        inputField = GetComponent<InputField>();
        inputField.lineType = InputField.LineType.MultiLineNewline;
    }

    void OnEnable()
    {
        inputField.onValidateInput += CheckForEnter;
    }

    private char CheckForEnter(string text, int charIndex, char addedChar)
    {
        if (addedChar == '\n' && onSubmit != null)
        {
            onSubmit.Invoke(this, text);
            return '\0';
        }
        return addedChar;
    }

    void OnDisable()
    {
        inputField.onValidateInput -= CheckForEnter;
    }
}
