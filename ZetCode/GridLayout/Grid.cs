using System;
using QtCore;
using QtGui;


namespace GridLayout
{
    class Grid : QWidget
    {
        public Grid()
        {
            WindowTitle = "Gridlayout";
            
            InitUI();
            Resize(300, 300);
            Move(300, 300);
            Show();
        }

        private void InitUI()
        {
            QGridLayout grid = new QGridLayout(this);

            QLabel labelName = new QLabel("Name", this);
            QLineEdit lineEdit = new QLineEdit(this);
            QTextEdit textEdit = new QTextEdit(this);
            QPushButton btnOk = new QPushButton("Ok", this);
            QPushButton btnCancel = new QPushButton("Cancel", this);

            /*
             * In our scenario, the grid will have totally four columns and three rows. We would add 
             * and make the widgets to span in the grid as needed.
             */

            //Add Name label at row 1 - Column 1
            grid.AddWidget(labelName, 0, 0);

            //Add line Edit at row 1 - Column 2 with row span 1 and column span 4
            grid.AddWidget(lineEdit, 0, 1 , 1, 3);

            //Add textEdit at row2 - column 1 with row span 1 and column span 4
            grid.AddWidget(textEdit, 1, 0, 1, 4);

            //Add a stretch at row 3 - column 2 to make the btn move right
            grid.SetColumnStretch(1, 1);

            //Add Ok Btn at row 3 - Column 3
            grid.AddWidget(btnOk, 2, 2);

            //Add Cancel Btn at row 3 - Column 4
            grid.AddWidget(btnCancel, 2, 3);
            

        }

        [STAThread]
        public static int Main(string[] args)
        {
            new QApplication(args);
            new Grid();
            return QApplication.Exec();
        }
    }
}
