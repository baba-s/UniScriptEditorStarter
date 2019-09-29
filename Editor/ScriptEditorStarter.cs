using System.Reflection;
using UnityEditor;
using UnityEngine.Events;

namespace KoganeUnityLib
{
	[InitializeOnLoad]
	internal static class EditorApplicationUtility
	{
		private const BindingFlags BINDING_ATTR = 
			BindingFlags.Static | 
			BindingFlags.Instance | 
			BindingFlags.NonPublic;

		private static readonly FieldInfo m_info = typeof( EditorApplication )
			.GetField( "projectWasLoaded", BINDING_ATTR );

		public static UnityAction projectWasLoaded
		{
			get => m_info.GetValue( null ) as UnityAction;
			set
			{
				var functions = m_info.GetValue( null ) as UnityAction;
				functions += value;
				m_info.SetValue( null, functions );
			}
		}
	}

	internal static class ScriptEditorStarter
	{
		[InitializeOnLoadMethod]
		private static void Initialize()
		{
			EditorApplicationUtility.projectWasLoaded += () =>
			{
				EditorApplication.ExecuteMenuItem( "Assets/Open C# Project" );
			};
		}
	}
}