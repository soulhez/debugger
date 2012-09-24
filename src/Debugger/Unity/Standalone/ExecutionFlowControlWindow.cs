using CodeEditor.Composition;
using CodeEditor.Debugger.Unity.Engine;
using Mono.Debugger.Soft;
using UnityEngine;

namespace CodeEditor.Debugger.Unity.Standalone
{
	[Export(typeof(IDebuggerWindow))]
	internal class ExecutionFlowControlWindow : DebuggerWindow
	{
		private readonly IDebuggerSession _debuggingSession;

		[ImportingConstructor]
		public ExecutionFlowControlWindow(IDebuggerSession debuggingSession)
		{
			_debuggingSession = debuggingSession;
		}

		public override void OnGUI()
		{
			GUI.enabled = _debuggingSession.Suspended;// && !_debuggingSession.WaitingForResponse;
			if (GUILayout.Button("Continue"))
				_debuggingSession.SafeResume();
			if (GUILayout.Button("Step Over"))
				_debuggingSession.SendStepRequest(StepDepth.Over);
			if (GUILayout.Button("Step In"))
				_debuggingSession.SendStepRequest(StepDepth.Into);
			if (GUILayout.Button("Step Out"))
				_debuggingSession.SendStepRequest(StepDepth.Out);

			GUI.enabled = !_debuggingSession.Suspended;// && !_debuggingSession.WaitingForResponse;
			if (GUILayout.Button("Break"))
				_debuggingSession.Break();

			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Connect"))
				_debuggingSession.Connect();
			GUI.enabled = true;
		}

		public string Title
		{
			get { return "Controls"; }
		}
	}
}