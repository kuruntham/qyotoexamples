using System;
using QtCore;
using QtGui;

namespace CenterWindow
{
	public class MyWidget : QWidget
	{
		const int Wdth = 250;
		const int Hght = 150;

		public MyWidget ()
		{
			WindowTitle = "Centering the window";

			Resize (Wdth, Hght);

			Center ();

			Show ();
		}

		private void Center() 
		{
			QDesktopWidget qdw = new QDesktopWidget ();

			int screenWidth = qdw.Width;
			int screenHeight= qdw.Height;

			int centerX = (screenWidth - Wdth) / 2;
			int centerY = (screenHeight - Hght) / 2;

			Move (centerX, centerY);
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

