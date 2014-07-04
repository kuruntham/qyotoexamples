using System;
using QtCore;
using QtGui;

namespace Tooltip
{
	public class MyWidget : QWidget
	{
		public MyWidget ()
		{
			WindowTitle = "Tooltip";
			ToolTip = "This is QWidget";
			Resize (250, 150);
			Move (300, 300);
			Show ();
		}

		[STAThread]
		public static int Main(string[] args)
		{
			new QApplication (args);
			new MyWidget ();
			return QApplication.Exec ();
		}
	}
}

