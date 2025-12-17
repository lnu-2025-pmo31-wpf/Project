using System;
using System.Linq;
using System.Windows.Forms;
using Project_kindergarten.Data;

namespace Project_kindergarten
{
    public partial class admin_groups : Form
    {
        public admin_groups()
        {
            InitializeComponent();
        }

        private void admin_groups_Load(object sender, EventArgs e)
        {
            LoadGroupsToGrid();
        }

        private void LoadGroupsToGrid()
        {
            using var db = new KindergartenDbContext();

            // На старті краще оновити кеш-значення (Teacher + CurrentChildren)
            DbInitializer.RefreshGroupCache(db);
            db.SaveChanges();

            var groups = db.Groups
                .OrderBy(g => g.Id)
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    g.AgeCategory,
                    ChildrenCount = $"{g.CurrentChildren}/{g.MaxChildren}",
                    Teacher = g.Teacher,
                    g.Room
                })
                .ToList();

            dataGridView1.DataSource = groups;
        }

        // === НАЗАД → administrator_form ===
        private void back_button_Click(object sender, EventArgs e)
        {
            administrator_form admin = new administrator_form();
            admin.Show();
            this.Hide();
        }

        // === ВИЙТИ → Form1 (логін) ===
        private void exit_button_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        // === ДОДАТИ ГРУПУ admin_add_groups ===
        private void add_groups_button_Click(object sender, EventArgs e)
        {
            admin_add_groups add = new admin_add_groups();
            add.Show();
            this.Hide();
        }

        // === Редагування групи admin_edit_groups ===
        private void edit_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            var row = dataGridView1.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            using var db = new KindergartenDbContext();
            var group = db.Groups.FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                MessageBox.Show("Групу не знайдено в базі даних.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadGroupsToGrid();
                return;
            }

            admin_edit_groups editForm = new admin_edit_groups(group);
            editForm.Show();
            this.Hide();
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            var row = dataGridView1.SelectedRows[0];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int id)) return;

            var confirm = MessageBox.Show(
                "Ви точно хочете видалити цю групу?\n\nУВАГА: будуть видалені і діти/вихователі цієї групи.",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            using var db = new KindergartenDbContext();
            var group = db.Groups.FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                MessageBox.Show("Групу не знайдено в базі даних.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadGroupsToGrid();
                return;
            }

            db.Groups.Remove(group);
            db.SaveChanges();

            LoadGroupsToGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            string id = row.Cells["Id"].Value?.ToString() ?? "";
            string name = row.Cells["Name"].Value?.ToString() ?? "";
            string age = row.Cells["AgeCategory"].Value?.ToString() ?? "";
            string count = row.Cells["ChildrenCount"].Value?.ToString() ?? "";
            string teacher = row.Cells["Teacher"].Value?.ToString() ?? "";
            string room = row.Cells["Room"].Value?.ToString() ?? "";

            MessageBox.Show(
                $"ID: {id}\n" +
                $"Назва групи: {name}\n" +
                $"Вікова категорія: {age}\n" +
                $"Діти: {count}\n" +
                $"Вихователі: {teacher}\n" +
                $"Кабінет: {room}",
                "Інформація про групу",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
