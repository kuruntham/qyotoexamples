using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

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
         
            tableWidget.ColumnCount = 3;
            tableWidget.SetHorizontalHeaderItem(0, new QTableWidgetItem("Code"));
            tableWidget.SetHorizontalHeaderItem(1, new QTableWidgetItem("Product"));
            tableWidget.SetHorizontalHeaderItem(2, new QTableWidgetItem("Id"));
            tableWidget.SetColumnHidden(2, true);

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
            
            using (var context = new SqliteContext() )
            {
                var products = from p in context.Products
                               select p;

                var i = 0;
                foreach(var product in products)
                {
                    tableWidget.InsertRow(i);
                    tableWidget.SetItem(i, 0, new QTableWidgetItem(product.Code));
                    tableWidget.SetItem(i, 1, new QTableWidgetItem(product.Name));
                    tableWidget.SetItem(i, 2, new QTableWidgetItem(product.Id.ToString()));                    

                    i++;
                }
                               
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
