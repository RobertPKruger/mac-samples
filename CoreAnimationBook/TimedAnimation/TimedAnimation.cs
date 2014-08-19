using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Foundation;
using AppKit;
using CoreAnimation;
using CoreGraphics;

namespace TimedAnimation {

	public partial class TimedAnimation : AppKit.NSView {
		NSImageView photo1;
		NSImageView photo2;
		
		public TimedAnimation (IntPtr handle) : base(handle) {}

		[Export("initWithCoder:")]
		public TimedAnimation (NSCoder coder) : base(coder) {}
		
		[Export("initWithFrame:")]
		public TimedAnimation (CGRect frame) : base(frame)
		{
			var xInset = frame.Width / 3;
			var yInset = frame.Height / 3;
			
			CGRect aniFrame = frame.Inset (xInset, yInset);
			
			// photo1 starts and the left edge
			var origin = frame.Location;
			origin.X = 0.0f;
			origin.Y = Bounds.GetMidY () - aniFrame.Height / 2;
			aniFrame.Location = origin;
			
			photo1 = new NSImageView (aniFrame);
			photo1.ImageScaling = NSImageScale.AxesIndependently;
			photo1.Image = NSImage.ImageNamed ("photo1.jpg");
			AddSubview (photo1);
			
			// photo2 starts in the center
			origin.X = Bounds.GetMidX () - aniFrame.Width / 2;
			aniFrame.Location = origin;
			photo2 = new NSImageView (aniFrame);
			photo2.ImageScaling = NSImageScale.AxesIndependently;
			photo2.Image = NSImage.ImageNamed ("photo2.jpg");
			AddSubview (photo2);
		}
		
		public override bool AcceptsFirstResponder ()
		{
			return true;
		}
		
		public override void KeyDown (NSEvent theEvent)
		{
			if ((NSKey)(theEvent.Characters [0]) == NSKey.RightArrow)
				right ();
			else if (theEvent.Characters [0] == 'r')
				reset ();
			else
				base.KeyDown (theEvent);
		}
		
		CABasicAnimation basicAnimationNamed (string name, float duration)
		{
			CABasicAnimation animation = new CABasicAnimation ();
			animation.Duration = duration;
			animation.SetValueForKey ((NSString)name, (NSString)"name");
			return animation;
		}
		
		void right () 
		{
			// photo1 is going to move to where photo2 is
			CGPoint newOrigin = photo2.Frame.Location;
			CABasicAnimation animation = basicAnimationNamed ("photo1",1.0f);
			animation.AnimationStopped += HandleAnimationStopped;
			photo1.Animations = NSDictionary.FromObjectAndKey (animation, (NSString)"frameOrigin");
			((NSView)photo1.Animator).SetFrameOrigin (newOrigin);
		}
		
		void reset ()
		{
			// NOTE - fix the animations to allow null values
			photo1.Animations = new NSDictionary ();
			photo2.Animations = new NSDictionary ();
			
			CGPoint newPhoto1Origin = new CGPoint (0, Frame.GetMidY () - photo1.Bounds.Height / 2);
			CGPoint newPhoto2Origin = new CGPoint (Frame.GetMidX () - photo2.Bounds.Width / 2,
			                                     Frame.GetMidY () - photo1.Bounds.Height / 2);
			((NSView)photo1.Animator).SetFrameOrigin (newPhoto1Origin);
			((NSView)photo2.Animator).SetFrameOrigin (newPhoto2Origin);
		}
		
		void HandleAnimationStopped (object sender, CAAnimationStateEventArgs e)
		{
			//Console.WriteLine("stopped" + ((CAAnimation)sender).ValueForKey((NSString)"name"));
			var animation = (CAAnimation)sender;
			string animationValue = ((CAAnimation)sender).ValueForKey((NSString)"name").ToString();
			if (e.Finished && animationValue == "photo1"){
				CABasicAnimation photo2Animation = basicAnimationNamed ("photo2", (float)animation.Duration);
				photo2.Animations = NSDictionary.FromObjectAndKey (photo2Animation, (NSString)"frameOrigin");
				CGPoint newPhoto2Origin = new CGPoint(Frame.GetMaxX () - photo2.Frame.Size.Width, photo2.Frame.Location.Y);
				((NSView)photo2.Animator).SetFrameOrigin(newPhoto2Origin);
			}
		}
	}
}

