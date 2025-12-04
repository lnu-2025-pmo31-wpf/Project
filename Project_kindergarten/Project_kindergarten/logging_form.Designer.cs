namespace Project_kindergarten
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            kinder_lebel = new Label();
            loggging_label = new Label();
            password_label = new Label();
            logging_textBox = new TextBox();
            password_textBox = new TextBox();
            role_label = new Label();
            role_comboBox = new ComboBox();
            enter_button = new Button();
            data_panel = new Panel();
            data_panel.SuspendLayout();
            SuspendLayout();
            // 
            // kinder_lebel
            // 
            kinder_lebel.AutoSize = true;
            kinder_lebel.Font = new Font("Segoe UI", 20F);
            kinder_lebel.Location = new Point(70, 34);
            kinder_lebel.Name = "kinder_lebel";
            kinder_lebel.Size = new Size(444, 46);
            kinder_lebel.TabIndex = 0;
            kinder_lebel.Text = "Kinder Батьківський портал";
            // 
            // loggging_label
            // 
            loggging_label.AutoSize = true;
            loggging_label.Font = new Font("Segoe UI", 15F);
            loggging_label.Location = new Point(46, 44);
            loggging_label.Name = "loggging_label";
            loggging_label.Size = new Size(82, 35);
            loggging_label.TabIndex = 1;
            loggging_label.Text = "Логін:";
            // 
            // password_label
            // 
            password_label.AutoSize = true;
            password_label.Font = new Font("Segoe UI", 15F);
            password_label.Location = new Point(46, 97);
            password_label.Name = "password_label";
            password_label.Size = new Size(107, 35);
            password_label.TabIndex = 2;
            password_label.Text = "Пароль:";
            // 
            // logging_textBox
            // 
            logging_textBox.Font = new Font("Segoe UI", 15F);
            logging_textBox.Location = new Point(163, 38);
            logging_textBox.Name = "logging_textBox";
            logging_textBox.Size = new Size(263, 41);
            logging_textBox.TabIndex = 3;
            // 
            // password_textBox
            // 
            password_textBox.Font = new Font("Segoe UI", 15F);
            password_textBox.Location = new Point(163, 91);
            password_textBox.Name = "password_textBox";
            password_textBox.Size = new Size(263, 41);
            password_textBox.TabIndex = 4;
            // 
            // role_label
            // 
            role_label.AutoSize = true;
            role_label.Font = new Font("Segoe UI", 15F);
            role_label.Location = new Point(46, 158);
            role_label.Name = "role_label";
            role_label.Size = new Size(75, 35);
            role_label.TabIndex = 5;
            role_label.Text = "Роль:";
            role_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // role_comboBox
            // 
            role_comboBox.Font = new Font("Segoe UI", 15F);
            role_comboBox.FormattingEnabled = true;
            role_comboBox.Items.AddRange(new object[] { "Адміністратор", "Вихователь", "Батьки" });
            role_comboBox.Location = new Point(163, 150);
            role_comboBox.Name = "role_comboBox";
            role_comboBox.Size = new Size(263, 43);
            role_comboBox.TabIndex = 6;
            role_comboBox.Text = " ";
            // 
            // enter_button
            // 
            enter_button.Font = new Font("Segoe UI", 15F);
            enter_button.Location = new Point(244, 414);
            enter_button.Name = "enter_button";
            enter_button.Size = new Size(113, 44);
            enter_button.TabIndex = 7;
            enter_button.Text = "Увійти";
            enter_button.UseVisualStyleBackColor = true;
            enter_button.Click += enter_button_Click;
            // 
            // data_panel
            // 
            data_panel.BackColor = SystemColors.ControlLightLight;
            data_panel.Controls.Add(loggging_label);
            data_panel.Controls.Add(logging_textBox);
            data_panel.Controls.Add(role_label);
            data_panel.Controls.Add(role_comboBox);
            data_panel.Controls.Add(password_label);
            data_panel.Controls.Add(password_textBox);
            data_panel.Location = new Point(61, 142);
            data_panel.Name = "data_panel";
            data_panel.Size = new Size(511, 238);
            data_panel.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 553);
            Controls.Add(data_panel);
            Controls.Add(enter_button);
            Controls.Add(kinder_lebel);
            Name = "Form1";
            Text = "logging_form";
            Load += Form1_Load;
            data_panel.ResumeLayout(false);
            data_panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label kinder_lebel;
        private Label loggging_label;
        private Label password_label;
        private TextBox logging_textBox;
        private TextBox password_textBox;
        private Label role_label;
        private ComboBox role_comboBox;
        private Button enter_button;
        private Panel data_panel;
    }
}
