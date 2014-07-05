using System;
using QtCore;
using QtGui;

namespace AbsolutePositioning
{
	public class MyWidget : QWidget
	{
		public MyWidget ()
		{
			WindowTitle = "Absolite Positioning";
			InitUI ();
			Resize (300, 280);
			Move (300, 300);
			Show ();
		}

		private void InitUI()
		{
			StyleSheet = "QWidget {background-color:#414141}";

			QPixmap img1 = new QPixmap ("img1.jpg");
			QPixmap img2 = new QPixmap ("img2.jpg");
			QPixmap img3 = new QPixmap ("img3.jpg");

			QLabel label1 = new QLabel (this);
			label1.Pixmap = img1;
			label1.Move (20, 20);

			QLabel label2 = new QLabel (this);
			label2.Pixmap = img2;
			label2.Move (40	, 160);

			QLabel label3 = new QLabel (this);
			label3.Pixmap = img3;
			label3.Move (170, 50);

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

