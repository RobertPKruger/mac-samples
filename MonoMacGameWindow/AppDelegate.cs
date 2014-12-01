using System;
using System.Drawing;
using Foundation;
using AppKit;
using ObjCRuntime;

namespace MonoMacGameView
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MonoMacGameWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MonoMacGameWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
		}
		
		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}
}

