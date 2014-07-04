using System;
using QtCore;
using QtGui;

namespace QuitButton
{
	public class MyWidget : QWidget
	{
		public MyWidget ()
		{
			WindowTitle = "Quit Button";
			InitUI ();
			Resize (250, 150);
			Move (300, 300);
			Show ();
		}

		public void InitUI()
		{
			QPushButton quit = new QPushButton ("Quit", this);
			//Connect (quit, SIGNAL("clicked()"), qApp, SLOT("quit()"));
			quit.Clicked += OnQuitButtonClicked;
			quit.SetGeometry (50, 40, 80, 30);
		}

		[Q_SLOT]
		private void OnQuitButtonClicked()
		{
			QApplication.Quit ();
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


