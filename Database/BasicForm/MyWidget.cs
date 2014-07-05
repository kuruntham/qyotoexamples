using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

using QtCore;
using QtGui;

namespace BasicForm
{
    class MyWidget:QWidget
    {
        private QLineEdit txtCode;
        private QLineEdit txtName;
        private QComboBox cmbUnit;
        private QLineEdit txtPrice;

        //Error Labels
        private QLabel labelErrorCode;
        private QLabel labelErrorName;
        private QLabel labelErrorUnit;
        private QLabel labelErrorPrice;
        
        public MyWidget()
        {
            Resize(300, 400);
            InitUI();
            Show();
        }

        private void InitUI()
        {
            QLabel labelCode = new QLabel("Code",this);
            QLabel labelName = new QLabel("Name", this);
            QLabel labelUnit = new QLabel("Price", this);
            QLabel labelPrice = new QLabel("Unit Price", this);
            
            labelErrorCode = new QLabel("", this);
            labelErrorCode.ObjectName = "errorCode";

            labelErrorName = new QLabel("", this);
            labelErrorName.ObjectName = "errorName";

            labelErrorUnit = new QLabel("", this);
            labelErrorUnit.ObjectName = "errorUnit";

            labelErrorPrice = new QLabel("", this);
            labelErrorPrice.ObjectName = "errorPrice";

            StyleSheet = @"QLabel#errorCode{color:#ff0000} 
                QLabel#errorName{color:#ff0000}
                QLabel#errorUnit{color:#ff0000}  
                QLabel#errorPrice{color:#ff0000}  
            ";

            txtCode = new QLineEdit(this);
            txtName = new QLineEdit(this);
            cmbUnit = new QComboBox(this);
            txtPrice = new QLineEdit(this);

            txtCode.TextEdited += OnTextCodeEdited;
            txtName.TextEdited += OnTextNameEdited;
            txtPrice.TextEdited += OnTextPriceEdited;

            txtCode.FocusOutEvent += OnTextCodeFocusOut;
            txtName.FocusOutEvent += OnTextNameFocusOut;
            txtPrice.FocusOutEvent += OnTextPriceFocusOut;

            QPushButton btnSave = new QPushButton("Save", this);
            QPushButton btnReset = new QPushButton("Reset", this);

            btnSave.Clicked += OnBtnSaveClicked;
            btnReset.Clicked += OnBtnResetClicked;
            
            //Layouts
            QVBoxLayout topVLayout = new QVBoxLayout(this);
            QHBoxLayout topHLayout = new QHBoxLayout();
            
            QFormLayout formLayout = new QFormLayout();            

            QVBoxLayout vBoxCode = new QVBoxLayout();
            QVBoxLayout vBoxName = new QVBoxLayout();
            QVBoxLayout vBoxUnit = new QVBoxLayout();            
            QVBoxLayout vBoxPrice = new QVBoxLayout();

            QHBoxLayout btnHLayout = new QHBoxLayout();

            QGroupBox groupBox = new QGroupBox("Product",this);
            groupBox.MinimumWidth = 250;

            QVBoxLayout grpBoxVLayout = new QVBoxLayout();

            vBoxCode.AddWidget(txtCode);
            vBoxCode.AddWidget(labelErrorCode);

            vBoxName.AddWidget(txtName);
            vBoxName.AddWidget(labelErrorName);

            vBoxUnit.AddWidget(cmbUnit);
            vBoxUnit.AddWidget(labelErrorUnit);

            vBoxPrice.AddWidget(txtPrice);
            vBoxPrice.AddWidget(labelErrorPrice);

            formLayout.HorizontalSpacing = 5;
            formLayout.VerticalSpacing = 10;
            formLayout.AddRow(labelCode, vBoxCode);
            formLayout.AddRow(labelName, vBoxName);
            formLayout.AddRow(labelUnit, vBoxUnit);
            formLayout.AddRow(labelPrice, vBoxPrice);            

            btnHLayout.AddStretch(1);
            btnHLayout.AddWidget(btnSave);
            btnHLayout.AddWidget(btnReset);

            grpBoxVLayout.AddItem(formLayout);
            grpBoxVLayout.AddItem(btnHLayout);

            cmbUnit.AddItem("Litre", "L");
            cmbUnit.AddItem("Kilogram","K");
            cmbUnit.AddItem("Gram", "G");
            cmbUnit.AddItem("Packet", "P");

            groupBox.Layout = grpBoxVLayout;

            topHLayout.AddStretch(1);
            topHLayout.AddWidget(groupBox);
            topHLayout.AddStretch(1);

            topVLayout.AddStretch(1);
            topVLayout.AddItem(topHLayout);            
            topVLayout.AddStretch(1);

        }

        
        [Q_SLOT]
        private void OnBtnSaveClicked()
        {
            if ( ValidateForm() )
            {
                Console.WriteLine("Coming");
                var cs = "Data Source=basciform.sqlite;";
                SQLiteConnection con = new SQLiteConnection(cs);
                con.Open();

                var sql = "INSERT INTO Product (Code,Name,Unit,UnitPrice) values (@code,@name,@unit,@unitPrice)";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);                
                cmd.Parameters.AddWithValue("@code",txtCode.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@unit", cmbUnit.ItemData(cmbUnit.CurrentIndex));                
                cmd.Parameters.AddWithValue("@unitPrice", txtPrice.Text);
                
                try
                {
                    cmd.ExecuteNonQuery();
                    ResetFields();
                    txtCode.SetFocus();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                con.Close();
            }
        }

        private void ResetFields()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            cmbUnit.CurrentIndex = 0;

            labelErrorCode.Text = "";
            labelErrorName.Text = "";
            labelErrorPrice.Text = "";
            labelErrorUnit.Text = "";
        }

        [Q_SLOT]
        private void OnBtnResetClicked()
        {
            ResetFields();
        }

        private Boolean ValidateForm()
        {
            var isCodeValidated = !ValidateCode();
            var isNameValidated = !ValidateName();
            var isUnitPriceValidated = !ValidateUnitPrice();

            return (isCodeValidated && isNameValidated && isUnitPriceValidated);
        }

        private Boolean ValidateCode()
        {
            
            var code = txtCode.Text.Trim();
            var error = (code == String.Empty);
            
            var errorMsg = "";
            if (error == true)
            {
                errorMsg = "Code cannot be blank.";                
            }

            labelErrorCode.Text = errorMsg;

            return error;

        }
        

        private Boolean ValidateName()
        {
            var nme = txtName.Text.Trim();
            var error = (nme == String.Empty);

            var errorMsg = "";
            if (error == true)
            {
                errorMsg = "Name cannot be blank.";
            }

            labelErrorName.Text = errorMsg;

            return error;
        }

        private Boolean ValidateUnitPrice()
        {
            var price = txtPrice.Text.Trim();
            var error = (price == String.Empty);

            var errorMsg = "";
            if (error == true)
            {
                errorMsg = "Unit Price cannot be blank.";
            }

            labelErrorPrice.Text = errorMsg;

            return error;
        }
        
        private void OnTextCodeFocusOut(object sender, QEventArgs<QFocusEvent> evt)
        {
            ValidateCode();
        }

        private void OnTextNameFocusOut(object sender, QEventArgs<QFocusEvent> evt)
        {
            ValidateName();
        }

        private void OnTextPriceFocusOut(object sender, QEventArgs<QFocusEvent> evt)
        {
            ValidateUnitPrice();
        }
        
        [Q_SLOT]
        private void OnTextCodeEdited(String str)
        {
            ValidateCode();
        }

        [Q_SLOT]
        private void OnTextNameEdited(String str)
        {
            ValidateName();
        }

        [Q_SLOT]
        private void OnTextPriceEdited(String str)
        {
            ValidateUnitPrice();
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
