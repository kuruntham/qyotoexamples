using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QtCore;
using QtGui;

namespace TableWidget
{
    class TableWidget : QWidget
    {
        private QTableWidget tableWidget;
        private QLabel lblSelectedCode;
        private QLabel lblSelectedName;
        
        public TableWidget()
        {
            Resize(500, 600);
            InitUI();
            Show();
        }

        public void InitUI()
        {
            
            QVBoxLayout vBox = new QVBoxLayout(this);
            tableWidget = new QTableWidget();
            
            QTableWidgetItem codeHeader = new QTableWidgetItem();
            codeHeader.Text = "Code";

            QTableWidgetItem nameHeader = new QTableWidgetItem();
            nameHeader.Text = "Product ";
            
            tableWidget.ColumnCount = 2;
            tableWidget.SetHorizontalHeaderItem(0, codeHeader);
            tableWidget.SetHorizontalHeaderItem(1, nameHeader);
            tableWidget.HorizontalHeader.StretchLastSection = true;
            tableWidget.VerticalHeader.Hide();
            tableWidget.selectionBehavior = QAbstractItemView.SelectionBehavior.SelectRows;
            tableWidget.EditTriggers = QAbstractItemView.EditTrigger.NoEditTriggers;

            tableWidget.ItemSelectionChanged += OnItemSelectionChanged;            
            
            this.KeyReleaseEvent += OnWidgetKeyReleaseEvent;

            QHBoxLayout hBox1 = new QHBoxLayout();
            QHBoxLayout hBox2 = new QHBoxLayout();

            QLabel lbl1 = new QLabel("Selected Code : ", this);
            lblSelectedCode = new QLabel("",this);
            
            QLabel lbl2 = new QLabel("Selected Product : ", this);
            lblSelectedName = new QLabel("",this);

            hBox1.AddWidget(lbl1);
            hBox1.AddWidget(lblSelectedCode);

            hBox2.AddWidget(lbl2);
            hBox2.AddWidget(lblSelectedName);
            
            PopulateTable();

            vBox.AddWidget(tableWidget);
            vBox.AddItem(hBox1);
            vBox.AddItem(hBox2);

            List<QTableWidgetItem> items = tableWidget.FindItems("Pro", MatchFlag.MatchStartsWith);

            foreach(QTableWidgetItem item in items)
            {
                
                
            }
        }

        private void OnWidgetKeyReleaseEvent(object sender, QEventArgs<QKeyEvent> evtArgs)
        {
            if (tableWidget.Focus)
            {
                if (evtArgs.Event.Key == (int)Key.Key_Return || evtArgs.Event.Key == (int)Key.Key_Enter)
                {                   
                    var row = tableWidget.CurrentRow;
                    QTableWidgetItem code = tableWidget.Item(row, 0);
                    QTableWidgetItem productName = tableWidget.Item(row, 1);

                    lblSelectedCode.Text = code.Text;
                    lblSelectedName.Text = productName.Text;
                }
            }
        }

        [Q_SLOT]
        private void OnItemSelectionChanged()
        {
           
        }
        
        private void PopulateTable()
        {
            var code = 1000;            
            for (var i = 0; i < 1000; i++)
            {
                code++;                
                tableWidget.InsertRow(i);

                QTableWidgetItem codeItem = new QTableWidgetItem();
                codeItem.Text = code.ToString();

                QTableWidgetItem productItem = new QTableWidgetItem();
                productItem.Text = "Product " + i.ToString();

                tableWidget.SetItem(i, 0, codeItem);
                tableWidget.SetItem(i, 1, productItem);
            }
            
        }


        [STAThread]
        public static int Main(string[] args)
        {
            new QApplication(args);
            new TableWidget();
            return QApplication.Exec();
        }
    }
}
