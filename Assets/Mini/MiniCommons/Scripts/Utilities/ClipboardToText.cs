using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Mini.Common.Utilities
{
    public class ClipboardToText : MonoBehaviour
    {
        private string _text;
        public UnityEvent<string> TextChanged = new UnityEvent<string>();
        public string Text 
        { 
            get => _text; 
            set 
            {
                if (_text == value)
                {
                    return;
                }
                _text = value; 
                TextChanged?.Invoke(_text); 
            } 
        }

        // Update is called once per frame
        void Update()
        {
            var text = GUIUtility.systemCopyBuffer;
            if (!string.IsNullOrEmpty(text))
            {
                Text = text;
            }
        }
    }
}
