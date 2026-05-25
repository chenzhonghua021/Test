using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskManager.Models;
using TaskManager.Data;

namespace TaskManager
{
    public partial class MainForm : Form
    {
        private int? editingTaskId = null;

        public MainForm()
        {
            InitializeComponent();
            Database.Initialize();
            InitializeComboBoxes();
            LoadTasks();
        }

        private void InitializeComboBoxes()
        {
            cmbPriority.Items.AddRange(new string[] { "高", "中", "低" });
            cmbPriority.SelectedIndex = 0;
            
            cmbStatus.Items.AddRange(new string[] { "待办", "进行中", "已完成" });
            cmbStatus.SelectedIndex = 0;

            cmbFilterStatus.Items.AddRange(new string[] { "全部", "待办", "进行中", "已完成" });
            cmbFilterStatus.SelectedIndex = 0;
        }

        private void LoadTasks()
        {
            List<TaskItem> tasks;
            string status = cmbFilterStatus.SelectedItem?.ToString();
            if (status == "全部")
                tasks = Database.GetTasks();
            else
            {
                string dbStatus = status == "待办" ? "Todo"
                                : status == "进行中" ? "InProgress"
                                : "Done";
                tasks = Database.GetTasksByStatus(dbStatus);
            }
            dataGridViewTasks.DataSource = tasks;
            dataGridViewTasks.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TaskItem task = new TaskItem
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                Priority = cmbPriority.SelectedIndex + 1,
                DueDate = dtpDueDate.Value.Date,
                Status = cmbStatus.SelectedIndex == 0 ? "Todo" :
                         cmbStatus.SelectedIndex == 1 ? "InProgress" : "Done"
            };
            Database.AddTask(task);
            LoadTasks();
            ClearInputs();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (editingTaskId == null)
            {
                MessageBox.Show("请选择要编辑的任务！");
                return;
            }
            TaskItem task = new TaskItem
            {
                Id = editingTaskId.Value,
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                Priority = cmbPriority.SelectedIndex + 1,
                DueDate = dtpDueDate.Value.Date,
                Status = cmbStatus.SelectedIndex == 0 ? "Todo" :
                         cmbStatus.SelectedIndex == 1 ? "InProgress" : "Done"
            };
            Database.UpdateTask(task);
            LoadTasks();
            ClearInputs();
            editingTaskId = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (editingTaskId == null)
            {
                MessageBox.Show("请选择要删除的任务！");
                return;
            }
            Database.DeleteTask(editingTaskId.Value);
            LoadTasks();
            ClearInputs();
            editingTaskId = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void dataGridViewTasks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewTasks.Rows[e.RowIndex].DataBoundItem as TaskItem;
                if (row != null)
                {
                    editingTaskId = row.Id;
                    txtTitle.Text = row.Title;
                    txtDescription.Text = row.Description;
                    cmbPriority.SelectedIndex = row.Priority - 1;
                    cmbStatus.SelectedIndex = row.Status == "Todo" ? 0 : row.Status == "InProgress" ? 1 : 2;
                    dtpDueDate.Value = row.DueDate;
                }
            }
        }

        private void ClearInputs()
        {
            editingTaskId = null;
            txtTitle.Text = "";
            txtDescription.Text = "";
            cmbPriority.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            dtpDueDate.Value = DateTime.Now;
        }
    }
}
