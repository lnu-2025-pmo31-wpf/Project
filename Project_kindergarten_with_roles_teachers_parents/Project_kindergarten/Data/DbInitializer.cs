using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project_kindergarten.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            using var db = new KindergartenDbContext();

            // Якщо раніше була стара БД без таблиць Teachers/Children,
            // то при спробі доступу буде "no such table".
            // Для навчального проєкту найпростіше — пересоздати БД.
            bool needsRecreate = false;
            try
            {
                // перевірка наявності нових таблиць
                _ = db.Teachers.Any();
                _ = db.Children.Any();
                _ = db.Children.Select(c => c.NotesForParents).Any();
                _ = db.Users.Any();
            }
            catch
            {
                needsRecreate = true;
            }

            if (needsRecreate)
            {
                db.Database.EnsureDeleted();
            }

            db.Database.EnsureCreated();

            // ===== ГРУПИ =====
            if (!db.Groups.Any())
            {
                db.Groups.AddRange(
                    new GroupData
                    {
                        Name = "Сонечка",
                        AgeCategory = "3–4 роки",
                        MaxChildren = 18,
                        CurrentChildren = 0,
                        Teacher = "",
                        Room = "Каб. 12"
                    },
                    new GroupData
                    {
                        Name = "Зірочки",
                        AgeCategory = "4–5 років",
                        MaxChildren = 15,
                        CurrentChildren = 0,
                        Teacher = "",
                        Room = "Каб. 9"
                    },
                    new GroupData
                    {
                        Name = "Бджілки",
                        AgeCategory = "5–6 років",
                        MaxChildren = 20,
                        CurrentChildren = 0,
                        Teacher = "",
                        Room = "Каб. 7"
                    }
                );
                db.SaveChanges();
            }

            // ===== ВИХОВАТЕЛІ =====
            if (!db.Teachers.Any())
            {
                var groups = db.Groups.ToList();
                int g1 = groups.First(g => g.Name == "Сонечка").Id;
                int g2 = groups.First(g => g.Name == "Зірочки").Id;
                int g3 = groups.First(g => g.Name == "Бджілки").Id;

                db.Teachers.AddRange(
                    new TeacherData { FullName = "Іваненко О.О.", Phone = "0981234567", Email = "ivan@kind.com", Position = "Вихователь", IsPrimary = true, GroupId = g1 },
                    new TeacherData { FullName = "Коваль Н.М.", Phone = "0972223344", Email = "koval@kind.com", Position = "Помічник вихователя", IsPrimary = false, GroupId = g1 },

                    new TeacherData { FullName = "Петренко І.П.", Phone = "0979876543", Email = "petro@kind.com", Position = "Вихователь", IsPrimary = true, GroupId = g2 },
                    new TeacherData { FullName = "Шевчук Л.О.", Phone = "0931112233", Email = "shevchuk@kind.com", Position = "Помічник вихователя", IsPrimary = false, GroupId = g2 },

                    new TeacherData { FullName = "Мельник А.А.", Phone = "0955556677", Email = "melnyk@kind.com", Position = "Вихователь", IsPrimary = true, GroupId = g3 }
                );
                db.SaveChanges();
            }

            // ===== ДІТИ =====
            if (!db.Children.Any())
            {
                var g = db.Groups.ToList();
                int g1 = g.First(x => x.Name == "Сонечка").Id;
                int g2 = g.First(x => x.Name == "Зірочки").Id;
                int g3 = g.First(x => x.Name == "Бджілки").Id;

                db.Children.AddRange(
                    new ChildData { FullName = "Савченко Марія", BirthDate = new DateTime(2021, 5, 12), ParentFullName = "Савченко Олена", ParentPhone = "0501112233", Address = "Львів", MedicalNotes = "Алергія на цитрусові", NotesForParents = "Будь ласка, принесіть змінний одяг.", GroupId = g1 },
                    new ChildData { FullName = "Бойко Андрій", BirthDate = new DateTime(2021, 11, 2), ParentFullName = "Бойко Ігор", ParentPhone = "0673334455", Address = "Львів", MedicalNotes = "", NotesForParents = "Потрібно оновити довідку від лікаря.", GroupId = g1 },

                    new ChildData { FullName = "Кравчук Софія", BirthDate = new DateTime(2020, 8, 25), ParentFullName = "Кравчук Оксана", ParentPhone = "0937778899", Address = "Львів", MedicalNotes = "", GroupId = g2 },

                    new ChildData { FullName = "Мороз Денис", BirthDate = new DateTime(2019, 3, 14), ParentFullName = "Мороз Наталія", ParentPhone = "0970001122", Address = "Львів", MedicalNotes = "Потрібен інгалятор при застуді", GroupId = g3 }
                );
                db.SaveChanges();
            }

            
            // ===== КОРИСТУВАЧІ (логін/пароль) =====
            SeedUsers(db);

// Оновлюємо кеш-поля в Groups: Teacher (імена вихователів) і CurrentChildren
            RefreshGroupCache(db);
            db.SaveChanges();
        }

        
        private static void SeedUsers(KindergartenDbContext db)
        {
            // Щоб не плодити дублікати
            if (db.Users.Any()) return;

            // ADMIN
            var (adminHash, adminSalt) = PasswordHasher.HashPassword("12345");
            db.Users.Add(new UserAccount
            {
                Username = "admin",
                Role = UserRole.Admin,
                PasswordHash = adminHash,
                PasswordSalt = adminSalt
            });

            // TEACHERS → teacher1/12345, teacher2/12345...
            var teachers = db.Teachers.OrderBy(t => t.Id).ToList();
            int tIndex = 1;
            foreach (var t in teachers)
            {
                var (h, s) = PasswordHasher.HashPassword("12345");
                db.Users.Add(new UserAccount
                {
                    Username = $"teacher{tIndex}",
                    Role = UserRole.Teacher,
                    TeacherId = t.Id,
                    PasswordHash = h,
                    PasswordSalt = s
                });
                tIndex++;
            }

            // PARENTS → parent1/12345, parent2/12345... (прив'язка до конкретної дитини)
            var kids = db.Children.OrderBy(c => c.Id).ToList();
            int pIndex = 1;
            foreach (var c in kids)
            {
                var (h, s) = PasswordHasher.HashPassword("12345");
                db.Users.Add(new UserAccount
                {
                    Username = $"parent{pIndex}",
                    Role = UserRole.Parent,
                    ChildId = c.Id,
                    PasswordHash = h,
                    PasswordSalt = s
                });
                pIndex++;
            }

            db.SaveChanges();
        }

public static void RefreshGroupCache(KindergartenDbContext db)
        {
            var groups = db.Groups
                .Include(x => x.Teachers)
                .Include(x => x.Children)
                .ToList();

            foreach (var group in groups)
            {
                // Якщо Children таблиця реально використовується — синхронізуємо.
                // Якщо записів дітей нема — не затираємо вручну введене значення.
                if (group.Children.Count > 0)
                    group.CurrentChildren = group.Children.Count;

                // Якщо Teachers таблиця реально використовується — синхронізуємо.
                // Якщо вихователів в таблиці нема — залишаємо те, що введено у формі "Групи".
                if (group.Teachers.Count > 0)
                {
                    var orderedTeachers = group.Teachers
                        .OrderByDescending(t => t.IsPrimary)
                        .ThenBy(t => t.FullName)
                        .Select(t => t.FullName)
                        .ToList();

                    group.Teacher = string.Join(", ", orderedTeachers);
                }
                else if (string.IsNullOrWhiteSpace(group.Teacher))
                {
                    group.Teacher = "—";
                }
            }
        }
    }
}
