using System.Windows.Forms;


namespace Polyriser {
	class MouseOnlyButton : Button {
		public MouseOnlyButton() {
			SetStyle(ControlStyles.Selectable, false);
		}
	}
}