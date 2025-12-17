PRAGMA foreign_keys = ON;

-- ===== GROUPS =====
CREATE TABLE IF NOT EXISTS Groups (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    AgeCategory TEXT,
    MaxChildren INTEGER,
    CurrentChildren INTEGER,
    Teacher TEXT,
    Room TEXT
);

-- ===== TEACHERS =====
CREATE TABLE IF NOT EXISTS Teachers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Phone TEXT,
    Email TEXT,
    Position TEXT,
    IsPrimary INTEGER NOT NULL DEFAULT 0,
    GroupId INTEGER,
    FOREIGN KEY (GroupId) REFERENCES Groups(Id) ON DELETE SET NULL
);

-- ===== CHILDREN =====
CREATE TABLE IF NOT EXISTS Children (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    BirthDate TEXT NOT NULL,
    ParentFullName TEXT,
    ParentPhone TEXT,
    Address TEXT,
    MedicalNotes TEXT,
    NotesForParents TEXT,
    GroupId INTEGER,
    FOREIGN KEY (GroupId) REFERENCES Groups(Id) ON DELETE SET NULL
);

-- ===== USERS =====
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT NOT NULL UNIQUE,
    Role INTEGER NOT NULL,
    PasswordHash BLOB NOT NULL,
    PasswordSalt BLOB NOT NULL,
    TeacherId INTEGER,
    ChildId INTEGER,
    FOREIGN KEY (TeacherId) REFERENCES Teachers(Id),
    FOREIGN KEY (ChildId) REFERENCES Children(Id)
);
