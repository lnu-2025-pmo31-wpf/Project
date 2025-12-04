namespace Project_kindergarten
{
    partial class admin_kinder
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
            back_button = new Button();
            kinder_label = new Label();
            exit_button = new Button();
            SuspendLayout();
            // 
            // back_button
            // 
            back_button.Location = new Point(12, 12);
            back_button.Name = "back_button";
            back_button.Size = new Size(94, 29);
            back_button.TabIndex = 22;
            back_button.Text = "Назад";
            back_button.UseVisualStyleBackColor = true;
            // 
            // kinder_label
            // 
            kinder_label.AutoSize = true;
            kinder_label.Font = new Font("Segoe UI", 20F);
            kinder_label.Location = new Point(231, 12);
            kinder_label.Name = "kinder_label";
            kinder_label.Size = new Size(326, 46);
            kinder_label.TabIndex = 21;
            kinder_label.Text = "Управління дітьми  ";
            // 
            // exit_button
            // 
            exit_button.Location = new Point(694, 12);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(94, 29);
            exit_button.TabIndex = 20;
            exit_button.Text = "Вийти ";
            exit_button.UseVisualStyleBackColor = true;
            // 
            // admin_kinder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(back_button);
            Controls.Add(kinder_label);
            Controls.Add(exit_button);
            Name = "admin_kinder";
            Text = "admin_kinder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button back_button;
        private Label kinder_label;
        private Button exit_button;
    }
}