using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_kindergarten
{
    public partial class administrator_form : Form
    {
        public administrator_form()
        {
            InitializeComponent();
        }

        private void administrator_form_Load(object sender, EventArgs e)
        {

        }

        private void administrator_label_Click(object sender, EventArgs e)
        {

        }

        // ===== ПЕРЕХІД ДО ПЕРСОНАЛУ =====
        private void educator_button_Click(object sender, EventArgs e)
        {
            admin_personnelcs staff = new admin_personnelcs();
            staff.Show();
            this.Hide();
        }

        // ===== ПЕРЕХІД ДО ГРУП =====
        private void groups_button_Click(object sender, EventArgs e)
        {
            admin_groups groups = new admin_groups();
            groups.Show();
            this.Hide();
        }

        // ===== ПЕРЕХІД ДО ДІТЕЙ =====
        private void kinder_button_Click(object sender, EventArgs e)
        {
            admin_kinder kids = new admin_kinder();
            kids.Show();
            this.Hide();
        }

        // ===== ВИХІД → ПОВЕРНЕННЯ НА ФОРМУ ВХОДУ =====
        private void exit_button_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }
    }
}
