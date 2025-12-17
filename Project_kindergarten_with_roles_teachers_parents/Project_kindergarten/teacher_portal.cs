using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Project_kindergarten.Data;

namespace Project_kindergarten
{
    public sealed class teacher_portal : Form
    {
        private readonly int _teacherId;

        private Label _lblHeader = null!;
        private DataGridView _groupsGrid = null!;
        private DataGridView _kidsGrid = null!;
        private Button _btnLogout = null!;
        private Button _btnEditNote = null!;

        public teacher_portal(int teacherId)
        {
            _teacherId = teacherId;

            Text = "Портал вихователя";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 600);

            BuildUi();
            Load += (_, __) => LoadData();
        }

        private void BuildUi()
        {
            _lblHeader = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(20, 15),
                Text = "Портал вихователя"
            };
            Controls.Add(_lblHeader);

            _btnLogout = new Button
            {
                Text = "Вийти",
                Size = new Size(100, 35),
                Location = new Point(860, 15)
            };
            _btnLogout.Click += (_, __) =>
            {
                var login = new Form1();
                login.Show();
                Hide();
            };
            Controls.Add(_btnLogout);

            var lblGroups = new Label
            {
                Text = "Мої групи:",
                AutoSize = true,
                Location = new Point(20, 60)
            };
            Controls.Add(lblGroups);

            _groupsGrid = new DataGridView
            {
                Location = new Point(20, 85),
                Size = new Size(940, 140),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _groupsGrid.SelectionChanged += (_, __) => LoadKidsForSelectedGroup();
            Controls.Add(_groupsGrid);

            var lblKids = new Label
            {
                Text = "Діти у вибраній групі:",
                AutoSize = true,
                Location = new Point(20, 240)
            };
            Controls.Add(lblKids);

            _kidsGrid = new DataGridView
            {
                Location = new Point(20, 265),
                Size = new Size(940, 250),
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            Controls.Add(_kidsGrid);

            _btnEditNote = new Button
            {
                Text = "Примітка батькам",
                Size = new Size(160, 35),
                Location = new Point(20, 525)
            };
            _btnEditNote.Click += (_, __) => EditNoteForSelectedChild();
            Controls.Add(_btnEditNote);
        }

        private void LoadData()
        {
            using var db = new KindergartenDbContext();

            var teacher = db.Teachers
                .Include(t => t.Group)
                .FirstOrDefault(t => t.Id == _teacherId);

            if (teacher == null)
            {
                MessageBox.Show("Вихователя не знайдено.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _lblHeader.Text = $"Вихователь: {teacher.FullName}";

            // У поточній моделі TeacherData має одну GroupId.
            // Якщо зробиш багато-груп — тут просто зміниш запит.
            var groups = db.Teachers
                .Where(t => t.Id == _teacherId)
                .Include(t => t.Group)
                .Select(t => t.Group!)
                .ToList();

            var rows = groups.Select(g => new
            {
                g.Id,
                g.Name,
                g.AgeCategory,
                Children = $"{g.CurrentChildren}/{g.MaxChildren}",
                Teachers = g.Teacher,
                g.Room
            }).ToList();

            _groupsGrid.DataSource = rows;
            if (_groupsGrid.Columns["Id"] != null) _groupsGrid.Columns["Id"].HeaderText = "ID";
            if (_groupsGrid.Columns["Name"] != null) _groupsGrid.Columns["Name"].HeaderText = "Група";
            if (_groupsGrid.Columns["AgeCategory"] != null) _groupsGrid.Columns["AgeCategory"].HeaderText = "Вік";
            if (_groupsGrid.Columns["Children"] != null) _groupsGrid.Columns["Children"].HeaderText = "Діти";
            if (_groupsGrid.Columns["Teachers"] != null) _groupsGrid.Columns["Teachers"].HeaderText = "Вихователі";
            if (_groupsGrid.Columns["Room"] != null) _groupsGrid.Columns["Room"].HeaderText = "Кімната";

            if (_groupsGrid.Rows.Count > 0)
                _groupsGrid.Rows[0].Selected = true;

            LoadKidsForSelectedGroup();
        }

        private int? SelectedGroupId()
        {
            if (_groupsGrid.CurrentRow == null) return null;
            if (_groupsGrid.CurrentRow.Cells["Id"]?.Value is int id) return id;
            if (int.TryParse(_groupsGrid.CurrentRow.Cells["Id"]?.Value?.ToString(), out int parsed)) return parsed;
            return null;
        }

        private void LoadKidsForSelectedGroup()
        {
            int? groupId = SelectedGroupId();
            if (groupId == null)
            {
                _kidsGrid.DataSource = null;
                return;
            }

            using var db = new KindergartenDbContext();

            var kids = db.Children
                .Where(c => c.GroupId == groupId.Value)
                .OrderBy(c => c.FullName)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    BirthDate = c.BirthDate.ToString("yyyy-MM-dd"),
                    Parent = c.ParentFullName,
                    Phone = c.ParentPhone,
                    Notes = c.NotesForParents
                })
                .ToList();

            _kidsGrid.DataSource = kids;

            if (_kidsGrid.Columns["Id"] != null) _kidsGrid.Columns["Id"].HeaderText = "ID";
            if (_kidsGrid.Columns["FullName"] != null) _kidsGrid.Columns["FullName"].HeaderText = "ПІБ";
            if (_kidsGrid.Columns["BirthDate"] != null) _kidsGrid.Columns["BirthDate"].HeaderText = "Дата нар.";
            if (_kidsGrid.Columns["Parent"] != null) _kidsGrid.Columns["Parent"].HeaderText = "Батьки";
            if (_kidsGrid.Columns["Phone"] != null) _kidsGrid.Columns["Phone"].HeaderText = "Телефон";
            if (_kidsGrid.Columns["Notes"] != null) _kidsGrid.Columns["Notes"].HeaderText = "Примітки батькам";
        }

        private int? SelectedChildId()
        {
            if (_kidsGrid.CurrentRow == null) return null;
            if (_kidsGrid.CurrentRow.Cells["Id"]?.Value is int id) return id;
            if (int.TryParse(_kidsGrid.CurrentRow.Cells["Id"]?.Value?.ToString(), out int parsed)) return parsed;
            return null;
        }

        private void InitializeComponent()
        {

        }

        private void EditNoteForSelectedChild()
        {
            int? childId = SelectedChildId();
            if (childId == null)
            {
                MessageBox.Show("Оберіть дитину.", "Увага",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new KindergartenDbContext();
            var child = db.Children.FirstOrDefault(c => c.Id == childId.Value);
            if (child == null) return;

            using var dlg = new NotesDialog(child.NotesForParents);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            child.NotesForParents = dlg.NotesText.Trim();
            db.SaveChanges();

            LoadKidsForSelectedGroup();
        }

        private sealed class NotesDialog : Form
        {
            private readonly TextBox _txt = new();
            private readonly Button _ok = new();
            private readonly Button _cancel = new();

            public string NotesText => _txt.Text;

            public NotesDialog(string current)
            {
                Text = "Примітка для батьків";
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                ClientSize = new Size(520, 260);

                _txt.Multiline = true;
                _txt.ScrollBars = ScrollBars.Vertical;
                _txt.Location = new Point(15, 15);
                _txt.Size = new Size(490, 170);
                _txt.Text = current ?? string.Empty;

                _ok.Text = "Зберегти";
                _ok.Location = new Point(300, 205);
                _ok.Size = new Size(95, 35);
                _ok.DialogResult = DialogResult.OK;

                _cancel.Text = "Скасувати";
                _cancel.Location = new Point(410, 205);
                _cancel.Size = new Size(95, 35);
                _cancel.DialogResult = DialogResult.Cancel;

                Controls.AddRange(new Control[] { _txt, _ok, _cancel });

                AcceptButton = _ok;
                CancelButton = _cancel;
            }
        }
    }
}
