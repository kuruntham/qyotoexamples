using System;
using QtCore;
using QtGui;

namespace ButtonsStretch
{
    class MyWidget : QWidget
    {
        public MyWidget()
        {
            WindowTitle = "Button Stretch Example";
            InitiUI();
            Resize(300, 200);
            Move(300, 300);
            Show();
        }

        private void InitiUI()
        {
            QPushButton okBtn = new QPushButton("Ok", this);
            QPushButton cancelBtn = new QPushButton("Cancel", this);

            QVBoxLayout vBoxLayout = new QVBoxLayout(this);
            QHBoxLayout hBoxLayout = new QHBoxLayout();

            hBoxLayout.AddStretch(1);
            hBoxLayout.AddWidget(okBtn);            
            hBoxLayout.AddWidget(cancelBtn);

            vBoxLayout.AddStretch(1);
            vBoxLayout.AddLayout(hBoxLayout);
            
        }

        public static int Main(string[] args)
        {
            new QApplication(args);
            new MyWidget();
            return QApplication.Exec();
        }
    }
}