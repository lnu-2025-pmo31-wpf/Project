namespace Project_kindergarten
{
    partial class admin_personnelcs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            exit_button = new Button();
            educators_label = new Label();
            dataGridView1 = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            Name = new DataGridViewTextBoxColumn();
            Group = new DataGridViewTextBoxColumn();
            Phone = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            back_button = new Button();
            add_button = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // exit_button
            // 
            exit_button.Location = new Point(746, 12);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(94, 29);
            exit_button.TabIndex = 5;
            exit_button.Text = "Вийти ";
            exit_button.UseVisualStyleBackColor = true;
            // 
            // educators_label
            // 
            educators_label.AutoSize = true;
            educators_label.Font = new Font("Segoe UI", 20F);
            educators_label.Location = new Point(303, 48);
            educators_label.Name = "educators_label";
            educators_label.Size = new Size(188, 46);
            educators_label.TabIndex = 6;
            educators_label.Text = "Вихователі";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { id, Name, Group, Phone, Email });
            dataGridView1.Location = new Point(12, 108);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(826, 153);
            dataGridView1.TabIndex = 7;
            // 
            // id
            // 
            id.HeaderText = "ID";
            id.MinimumWidth = 6;
            id.Name = "id";
            id.ReadOnly = true;
            id.Width = 50;
            // 
            // Name
            // 
            Name.HeaderText = "ПІБ";
            Name.MinimumWidth = 6;
            Name.Name = "Name";
            Name.Width = 250;
            // 
            // Group
            // 
            Group.HeaderText = "Група";
            Group.MinimumWidth = 6;
            Group.Name = "Group";
            Group.Width = 120;
            // 
            // Phone
            // 
            Phone.HeaderText = "Телефон";
            Phone.MinimumWidth = 6;
            Phone.Name = "Phone";
            Phone.Width = 150;
            // 
            // Email
            // 
            Email.HeaderText = "Email";
            Email.MinimumWidth = 6;
            Email.Name = "Email";
            Email.Width = 200;
            // 
            // back_button
            // 
            back_button.Location = new Point(12, 12);
            back_button.Name = "back_button";
            back_button.Size = new Size(94, 29);
            back_button.TabIndex = 8;
            back_button.Text = "Назад";
            back_button.UseVisualStyleBackColor = true;
            // 
            // add_button
            // 
            add_button.Font = new Font("Segoe UI", 15F);
            add_button.Location = new Point(303, 305);
            add_button.Name = "add_button";
            add_button.Size = new Size(257, 58);
            add_button.TabIndex = 9;
            add_button.Text = "Додати вихователя";
            add_button.UseVisualStyleBackColor = true;
            // 
            // admin_personnelcs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(852, 453);
            Controls.Add(add_button);
            Controls.Add(back_button);
            Controls.Add(dataGridView1);
            Controls.Add(educators_label);
            Controls.Add(exit_button);
            Name = "admin_personnelcs";
            Text = "admin_personnelcs";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button exit_button;
        private Label educators_label;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn Group;
        private DataGridViewTextBoxColumn Phone;
        private DataGridViewTextBoxColumn Email;
        private Button back_button;
        private Button add_button;
    }
}