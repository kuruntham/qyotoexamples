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
        private List<Product> list = new List<Product>();        

        public TableModel(QObject parent = null):base(parent)
        {  
            using (var context = new SqliteContext())
            {
                list = context.Products.ToList(); 
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
                    return list[index.Row].Code;
                }
                else if(index.Column == 1)
                {
                    return list[index.Row].Name;
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