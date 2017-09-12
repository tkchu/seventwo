using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

[InitializeOnLoad]
public class IMEAutoChanger
{
	private static FieldInfo s_ActiveEditorField;
	private static FieldInfo s_TextEditorHasFocusField;

	static IMEAutoChanger()
	{
		System.Type textEditorType = typeof(TextEditor);
		s_TextEditorHasFocusField = textEditorType.GetField( "m_HasFocus",BindingFlags.NonPublic | BindingFlags.Instance );
		
		System.Type editorGUIType = typeof(EditorGUI);
		s_ActiveEditorField = editorGUIType.GetField ( "activeEditor",BindingFlags.NonPublic | BindingFlags.Static );

		EditorApplication.update += Update;
	}

	private static TextEditor activeEditor
	{
		get
		{
			return (TextEditor)s_ActiveEditorField.GetValue(null);
		}
	}
	
	private static bool HasFocus( TextEditor textEditor )
	{
		return textEditor != null && GUIUtility.keyboardControl != 0 && GUIUtility.keyboardControl == textEditor.controlID && (bool)s_TextEditorHasFocusField.GetValue( textEditor );
	}

	static bool m_Changed = false;
	static IMECompositionMode m_CacheCompositionMode;

	static void Update()
	{
		if( EditorGUIUtility.editingTextField || HasFocus( activeEditor ) )
		{
			if (!m_Changed)
			{
				m_Changed = true;
				m_CacheCompositionMode = Input.imeCompositionMode;
				Input.imeCompositionMode = IMECompositionMode.On;
			}
		}
		else if( m_Changed )
		{
			m_Changed = false;
			Input.imeCompositionMode = m_CacheCompositionMode;
		}
	}
}
