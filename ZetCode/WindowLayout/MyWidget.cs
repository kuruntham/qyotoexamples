using System;
using QtCore;
using QtGui;

namespace WindowLayout
{
    class MyWidget : QWidget
    {
        public MyWidget()
        {
            WindowTitle = "Window Layout";
            Resize(350, 300);
            InitUI();
            Show();
        }

        private void InitUI()
        {
            //Main Vertical box
            QVBoxLayout vBox = new QVBoxLayout(this);

            //Row 1
            QLabel labelWind = new QLabel("Windows",this);
            vBox.AddWidget(labelWind);

            //Row2
            QHBoxLayout hBoxR2 = new QHBoxLayout();
            vBox.AddItem(hBoxR2);

            //Row2 Column 1
            QTextEdit textEdit = new QTextEdit(this);
            textEdit.Enabled = false;
            hBoxR2.AddWidget(textEdit);

            //Row2 Column 2
            QVBoxLayout vBox1 = new QVBoxLayout();            
            QPushButton btnActivate = new QPushButton("Activate", this);
            QPushButton btnClose = new QPushButton("Close", this);
            vBox1.ContentsMargins = new QMargins(5,0,5,5);
            vBox1.AddWidget(btnActivate);
            vBox1.AddWidget(btnClose);
            vBox1.AddStretch(1);
            hBoxR2.AddItem(vBox1);            
            
            //Row3
            QHBoxLayout hBoxR3 = new QHBoxLayout();
            vBox.AddItem(hBoxR3);
            QPushButton btnHelp = new QPushButton("Help", this);
            QPushButton btnOk = new QPushButton("Ok", this);
            hBoxR3.AddWidget(btnHelp);
            hBoxR3.AddStretch(1);
            hBoxR3.AddWidget(btnOk);

        }

        [STAThread]
        public static int Main(string[] args)
        {
            new QApplication(args);
            new MyWidget();
            return QApplication.Exec();
        }
    }
}
