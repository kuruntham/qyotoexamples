using System;

using QtCore;
using QtGui;


namespace TabWidget
{
    class MyWidget : QWidget
    {
        QTabWidget tabWidget;

        public MyWidget()
        {

            QVBoxLayout vBoxLayout = new QVBoxLayout(this);
            
            tabWidget = new QTabWidget();
            tabWidget.TabsClosable = true;            
            vBoxLayout.AddWidget(tabWidget);
            
            QWidget wdgt = new QWidget();
            QVBoxLayout vBox2 = new QVBoxLayout(wdgt);
            QRadioButton btnRadio1 = new QRadioButton("Radio Button 1", wdgt);
            QRadioButton btnRadio2 = new QRadioButton("Radio Button 2", wdgt);
            QRadioButton btnRadio3 = new QRadioButton("Radio Button 3", wdgt);

            vBox2.AddWidget(btnRadio1);
            vBox2.AddWidget(btnRadio2);
            vBox2.AddWidget(btnRadio3);
            vBox2.AddStretch(1);

            tabWidget.AddTab(wdgt, "Home");

            QWidget wdgt1 = new QWidget();
            QVBoxLayout vBox3 = new QVBoxLayout(wdgt1);
            QCheckBox chkBox1 = new QCheckBox("Check Box 1", wdgt1);
            QCheckBox chkBox2 = new QCheckBox("Check Box 2", wdgt1);
            QCheckBox chkBox3 = new QCheckBox("Check Box 3", wdgt1);

            vBox3.AddWidget(chkBox1);
            vBox3.AddWidget(chkBox2);
            vBox3.AddWidget(chkBox3);
            vBox3.AddStretch(1);

            tabWidget.AddTab(wdgt1, "Tab 1");

            tabWidget.TabCloseRequested += OnTabCloseRequested;

            Resize(300, 200);
            Show();
        }

        [Q_SLOT]
        private void OnTabCloseRequested(int tabIndex)
        {
            if (tabWidget.Count > 1)
            {
                tabWidget.RemoveTab(tabIndex);
            }
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
