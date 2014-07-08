using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

using QtCore;
using QtGui;



namespace AutoComplete
{
    class TableModel:QAbstractTableModel
    {
        private List<string> list;
        private List<string> list1;

        public TableModel(QObject parent = null):base(parent)
        {
            list = new List<string>();
            list1 = new List<string>();
            using (var context = new SqliteContext())
            {
                var products = from p in context.Products
                               select p;
                
                foreach (var product in products)
                {
                    list1.Add(product.Code);
                    list.Add(product.Name);                  
                }

            }        
            
        }
        
        public override int ColumnCount(QModelIndex parent)
        {
            return 2;
        }

        public override object Data(QModelIndex index, int role = 0)
        {            
            if (role == (int)Qt.ItemDataRole.DisplayRole || role == (int)Qt.ItemDataRole.EditRole)
            {
                
                if (index.Column == 0)
                {
                    return list1[index.Row];
                }
                else if(index.Column == 1)
                {
                    return list[index.Row];
                }
            }

            return null;
        }

        public override QModelIndex Parent(QModelIndex child)
        {   
            return CreateIndex(-1, -1);
        }

        public override int RowCount(QModelIndex parent)
        {
            return list.Count;
        }
    }
}
