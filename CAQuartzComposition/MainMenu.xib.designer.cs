// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace CAQuartzComposition {
	
	
	// Should subclass AppKit.NSResponder
	[Foundation.Register("AppDelegate")]
	public partial class AppDelegate {
	}
	
	// Should subclass Foundation.NSObject
	[Foundation.Register("FirstResponder")]
	public partial class FirstResponder {
		
		#pragma warning disable 0169
		[Foundation.Export("filterSwitch:")]
		partial void filterSwitch (AppKit.NSMenuItem sender);

		[Foundation.Export("switchView:")]
		partial void switchView (AppKit.NSMenuItem sender);
}
}
