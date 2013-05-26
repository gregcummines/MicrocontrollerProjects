using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChickenCoopBaseStation
{
    public partial class ErrorStatusForm : Form
    {
        public string Error
        {
            set
            { 
                lblErrorStatus.Text = value;
                lblErrorStatus.Text += " Problem since: " + DateTime.Now.ToString();
            }
            get { return lblErrorStatus.Text; }
        }

        public ErrorStatusForm()
        {
            InitializeComponent();
        }
    }
}
