using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Polyriser {
	static class TrayAnimation {
		const int IDANI_OPEN = 1;
		const int IDANI_CLOSE = 2;
		const int IDANI_CAPTION = 3;

		struct RECT {
			int left, top, right, bottom;

			public RECT(int left, int top, int right, int bottom) {
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			public RECT(Rectangle rect) {
				left = rect.X;
				top = rect.Y;
				right = rect.Right;
				bottom = rect.Bottom;
			}
		}

		[DllImport("user32")]
		static extern int DrawAnimatedRects(IntPtr hwnd, int idAni, ref RECT lprcFrom, ref RECT lprcTo);
		[DllImport("user32")]
		static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
		[DllImport("user32")]
		static extern int GetWindowRect(IntPtr hwnd, out RECT lpRect);


		static RECT FindTrayBounds() {
			var taskbarWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
			if(taskbarWnd == null)
				goto FallbackDefaults;
			var trayWnd = FindWindowEx(taskbarWnd, IntPtr.Zero, "TrayNotifyWnd", null);
			if(trayWnd == null)
				goto FallbackDefaults;
			RECT ret;
			if(GetWindowRect(trayWnd, out ret) == 0)
				goto FallbackDefaults;
			return ret;

		FallbackDefaults:
			var screen = Screen.PrimaryScreen.Bounds;
			return new RECT(screen.Right - 64, screen.Bottom - 16, screen.Right, screen.Bottom);
		}


		public static void ToTray(Control window) {
			var rcWindow = new RECT(window.Bounds);
			var rcTray = FindTrayBounds();
			DrawAnimatedRects(window.Handle, IDANI_CAPTION, ref rcWindow, ref rcTray);
		}

		public static void FromTray(Control window) {
			var rcWindow = new RECT(window.Bounds);
			var rcTray = FindTrayBounds();
			DrawAnimatedRects(window.Handle, IDANI_CAPTION, ref rcTray, ref rcWindow);
		}
	}
}