using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Project_kindergarten.Data;

namespace Project_kindergarten
{
    public sealed class parent_portal : Form
    {
        private readonly int _childId;

        private Label _lblTitle = null!;
        private Button _btnLogout = null!;
        private TextBox _txtInfo = null!;

        public parent_portal(int childId)
        {
            _childId = childId;

            Text = "Портал батьків";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 500);

            BuildUi();
            Load += (_, __) => LoadData();
        }

        private void BuildUi()
        {
            _lblTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(20, 15),
                Text = "Інформація про дитину"
            };
            Controls.Add(_lblTitle);

            _btnLogout = new Button
            {
                Text = "Вийти",
                Size = new Size(100, 35),
                Location = new Point(660, 15)
            };
            _btnLogout.Click += (_, __) =>
            {
                var login = new Form1();
                login.Show();
                Hide();
            };
            Controls.Add(_btnLogout);

            _txtInfo = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(740, 370),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(_txtInfo);
        }

        private void LoadData()
        {
            using var db = new KindergartenDbContext();

            var child = db.Children
                .Include(c => c.Group)
                .FirstOrDefault(c => c.Id == _childId);

            if (child == null)
            {
                MessageBox.Show("Дитину не знайдено.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string groupName = child.Group?.Name ?? "—";
            string teachers = child.Group?.Teacher ?? "—";

            _lblTitle.Text = $"Дитина: {child.FullName}";

            _txtInfo.Text =
                $"ПІБ: {child.FullName}\r\n" +
                $"Дата народження: {child.BirthDate:yyyy-MM-dd}\r\n" +
                $"Група: {groupName}\r\n" +
                $"Вихователі: {teachers}\r\n" +
                "\r\n" +
                $"Контакти батьків (у базі): {child.ParentFullName} / {child.ParentPhone}\r\n" +
                $"Адреса: {child.Address}\r\n" +
                "\r\n" +
                $"Примітки для батьків:\r\n{(string.IsNullOrWhiteSpace(child.NotesForParents) ? "—" : child.NotesForParents)}\r\n";
        }
    }
}
