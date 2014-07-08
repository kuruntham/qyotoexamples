using System;
using System.Collections.Generic;

using QtCore;
using QtGui;

namespace AutoComplete
{
    class MyWidget:QWidget
    {   
        private QCompleter completer;
        private TableModel tableModel;
        private QTableView tableView;
        
        public MyWidget()
        {
            Resize(300, 300);
            QVBoxLayout vBox1 = new QVBoxLayout(this);
            QHBoxLayout hBox = new QHBoxLayout();
            vBox1.AddLayout(hBox);
            
            QLineEdit txtCode = new QLineEdit(this);
            QLineEdit txtName = new QLineEdit(this);            

            hBox.AddWidget(txtCode);
            hBox.AddWidget(txtName);

            tableView = new QTableView();
            tableView.MinimumHeight = 200;
            tableView.MinimumWidth = 300;
            tableModel = new TableModel();
            completer = new QCompleter(tableModel);
            completer.Popup = tableView;
            completer.CompletionColumn = 0;
            completer.MaxVisibleItems = 5;            

            tableView.HorizontalHeader.StretchLastSection = false;
            tableView.HorizontalHeader.Hide();
            tableView.VerticalHeader.Hide();
            tableView.VerticalHeader.SetResizeMode(QHeaderView.ResizeMode.Fixed);
            tableView.VerticalHeader.DefaultSectionSize = 24;
            tableView.EditTriggers = QAbstractItemView.EditTrigger.NoEditTriggers;
            tableView.HorizontalHeader.SetResizeMode(0, QHeaderView.ResizeMode.ResizeToContents);
            tableView.HorizontalHeader.SetResizeMode(1, QHeaderView.ResizeMode.Stretch);
            tableView.selectionBehavior = QAbstractItemView.SelectionBehavior.SelectRows;

            txtCode.Completer = completer;
            
            Show();
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
