using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TaskManager.Models;

namespace TaskManager.Data
{
    public static class Database
    {
        private static string connStr = "Data Source=tasks.db;Version=3;";
        
        public static void Initialize()
        {
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    Priority INTEGER,
                    DueDate TEXT,
                    Status TEXT
                );";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 新增任务
        public static void AddTask(TaskItem task)
        {
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = @"INSERT INTO Tasks (Title, Description, Priority, DueDate, Status)
                               VALUES (@title, @desc, @priority, @duedate, @status)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", task.Title);
                    cmd.Parameters.AddWithValue("@desc", task.Description);
                    cmd.Parameters.AddWithValue("@priority", task.Priority);
                    cmd.Parameters.AddWithValue("@duedate", task.DueDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@status", task.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 查询所有任务
        public static List<TaskItem> GetTasks()
        {
            var list = new List<TaskItem>();
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM Tasks";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaskItem
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Priority = Convert.ToInt32(reader["Priority"]),
                            DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        // 删除任务
        public static void DeleteTask(int id)
        {
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "DELETE FROM Tasks WHERE Id=@id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 更新任务
        public static void UpdateTask(TaskItem task)
        {
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = @"UPDATE Tasks SET Title=@title, Description=@desc, Priority=@priority, DueDate=@duedate, Status=@status WHERE Id=@id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", task.Title);
                    cmd.Parameters.AddWithValue("@desc", task.Description);
                    cmd.Parameters.AddWithValue("@priority", task.Priority);
                    cmd.Parameters.AddWithValue("@duedate", task.DueDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@status", task.Status);
                    cmd.Parameters.AddWithValue("@id", task.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 按状态筛选
        public static List<TaskItem> GetTasksByStatus(string status)
        {
            var list = new List<TaskItem>();
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM Tasks WHERE Status=@status";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TaskItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Priority = Convert.ToInt32(reader["Priority"]),
                                DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
