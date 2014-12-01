using System;
using System.Drawing;
using Foundation;
using AppKit;
using ObjCRuntime;

namespace DrawerMadness
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		ParentWindowController parentWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			parentWindowController = new ParentWindowController ();
			parentWindowController.Window.MakeKeyAndOrderFront (this);
		}
		
		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
		
	}
}

