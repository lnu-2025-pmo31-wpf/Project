namespace Project_kindergarten
{
    public class TeacherData
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Посада (наприклад: "Вихователь", "Помічник вихователя")
        /// </summary>
        public string Position { get; set; } = "Вихователь";

        /// <summary>
        /// Основний вихователь групи (для відображення у списку груп)
        /// </summary>
        public bool IsPrimary { get; set; }

        public int GroupId { get; set; }
        public GroupData? Group { get; set; }
    }
}
