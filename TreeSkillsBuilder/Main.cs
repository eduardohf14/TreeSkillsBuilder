using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SQLite;
using System.IO;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Collections.Generic;

namespace TreeSkillsBuilder
{
    public partial class Main : Form
    {
        // Import necessary Windows API functions
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        private bool _isCapturingMousePos = false;
        private IntPtr _targetWindowHandle;
        private IntPtr _myWindowHandle;
        private string _dbPath = "skills.db";
        private bool isBotRunning = false;
        // Constants for mouse events
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        // Constants for key presses
        private const int VK_RETURN = 0x0D;
        private const uint KEYEVENTF_KEYDOWN = 0x0000;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        // Add this field for the bot thread
        private Thread _botThread;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public Main()
        {
            InitializeComponent();
            _myWindowHandle = this.Handle;

            // Initialize database
            InitializeDatabase();

            // Load existing skills
            LoadSkills();
        }

        private void btnCaptureMousePos_Click(object sender, EventArgs e)
        {
            // Find the target window
            _targetWindowHandle = FindWindow(null, "Mu Arena Season 6");

            if (_targetWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("Could not find 'Mu Arena Season 6' window.");
                return;
            }

            // Set focus to the target window
            SetForegroundWindow(_targetWindowHandle);

            // Start capturing mouse position
            _isCapturingMousePos = true;

            // Set up a timer to check for mouse click in the target window
            Timer timer = new Timer();
            timer.Interval = 100; // Check every 100ms
            timer.Tick += (s, args) =>
            {
                if (!_isCapturingMousePos)
                {
                    timer.Stop();
                    timer.Dispose();
                    return;
                }

                if (GetCursorPos(out POINT point))
                {
                    // Check if mouse button is pressed (left button in this case)
                    if ((Control.MouseButtons & MouseButtons.Left) != 0)
                    {
                        _isCapturingMousePos = false;

                        // Return focus to our application
                        SetForegroundWindow(_myWindowHandle);

                        positionTxt.Text = $"{point.X} {point.Y}";

                        timer.Stop();
                        timer.Dispose();
                    }
                }
            };
            timer.Start();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);

                using (var connection = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
                {
                    connection.Open();

                    string createTableQuery = @"
                    CREATE TABLE Skills (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        skill TEXT NOT NULL,
                        points INTEGER NOT NULL,
                        position TEXT NOT NULL
                    )";

                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadSkills()
        {
            skillsDataGrid.Rows.Clear();

            using (var connection = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                connection.Open();

                string selectQuery = "SELECT skill, points, position FROM Skills";

                using (var command = new SQLiteCommand(selectQuery, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int rowIndex = skillsDataGrid.Rows.Add(
                            reader["skill"],
                            reader["points"],
                            reader["position"]
                        );

                        // Store the original data in the row's Tag property
                        skillsDataGrid.Rows[rowIndex].Tag = new SkillData
                        {
                            skill = reader["skill"].ToString(),
                            points = Convert.ToInt32(reader["points"]),
                            position = reader["position"].ToString()
                        };
                    }
                }
            }
        }

        private void SaveSkillToDatabase(string name, int points, string position)
        {
            string[] coordinates = position.Split(' ');
            if (coordinates.Length != 2)
            {
                MessageBox.Show("Invalid position format");
                return;
            }

            int positionX = int.Parse(coordinates[0]);
            int positionY = int.Parse(coordinates[1]);

            using (var connection = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                connection.Open();

                string insertQuery = @"
                INSERT INTO Skills (skill, points, position)
                VALUES (@skill, @points, @position)";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@skill", name);
                    command.Parameters.AddWithValue("@points", points);
                    command.Parameters.AddWithValue("@position", position);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void btnSaveSkill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(skillNameTxt.Text))
            {
                MessageBox.Show("Escolha um nome pro skill.");
                return;
            }

            if (string.IsNullOrEmpty(positionTxt.Text))
            {
                MessageBox.Show("Informe a posição X Y da skill");
                return;
            }

            // Add to DataGridView
            skillsDataGrid.Rows.Add(
                skillNameTxt.Text,
                pointsTxt.Value,
                positionTxt.Text
            );

            // Save to database
            SaveSkillToDatabase(skillNameTxt.Text, (int)pointsTxt.Value, positionTxt.Text);

            // Clear inputs
            skillNameTxt.Clear();
            pointsTxt.Value = 0;
            positionTxt.Clear();

            MessageBox.Show("Skill saved successfully!");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Initialize DataGridView columns if not already set in designer
            if (skillsDataGrid.Columns.Count == 0)
            {
                skillsDataGrid.Columns.Add("skill", "Skill Name");
                skillsDataGrid.Columns.Add("points", "Points");
                skillsDataGrid.Columns.Add("position", "Position (X Y)");
            }
        }

        private void UpdateSkillInDatabase(string originalName, string name, int points, string position)
        {
            string[] coordinates = position.Split(' ');
            if (coordinates.Length != 2)
            {
                MessageBox.Show("Invalid position format");
                return;
            }

            int positionX = int.Parse(coordinates[0]);
            int positionY = int.Parse(coordinates[1]);

            using (var connection = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                connection.Open();

                string updateQuery = @"
        UPDATE Skills 
        SET skill = @skill, 
            points = @points, 
            position = @position
        WHERE skill = @OriginalName";

                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@skill", name);
                    command.Parameters.AddWithValue("@points", points);
                    command.Parameters.AddWithValue("@position", $"{positionX} {positionY}");
                    command.Parameters.AddWithValue("@OriginalName", originalName);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Failed to update skill. It may have been deleted.");
                    }
                }
            }
        }

        private void DeleteSkillFromDatabase(string name)
        {
            using (var connection = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Skills WHERE skill = @skill";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@skill", name);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void skillsDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewRow row = skillsDataGrid.Rows[e.RowIndex];

            // Get the original values (stored in Tag property)
            if (row.Tag == null)
            {
                // First edit - store original values
                row.Tag = new SkillData
                {
                    skill = row.Cells["skill"].Value?.ToString(),
                    points = Convert.ToInt32(row.Cells["points"].Value),
                    position = row.Cells["position"].Value?.ToString()
                };
                return;
            }

            // Get current and original values
            SkillData originalData = (SkillData)row.Tag;
            string currentName = row.Cells["skill"].Value?.ToString();
            int currentPoints = Convert.ToInt32(row.Cells["points"].Value);
            string currentPosition = row.Cells["position"].Value?.ToString();

            // Check if any value actually changed
            if (originalData.skill == currentName &&
                originalData.points == currentPoints &&
                originalData.position == currentPosition)
            {
                return;
            }

            // Validate the new data
            if (string.IsNullOrEmpty(currentName))
            {
                MessageBox.Show("Skill name cannot be empty");
                row.Cells["skill"].Value = originalData.skill;
                return;
            }

            if (string.IsNullOrEmpty(currentPosition) || currentPosition.Split(' ').Length != 2)
            {
                MessageBox.Show("Position must be in format 'X Y'");
                row.Cells["position"].Value = originalData.position;
                return;
            }

            // Update in database
            try
            {
                // If name changed, we need to delete the old record and insert a new one
                if (originalData.skill != currentName)
                {
                    DeleteSkillFromDatabase(originalData.skill);
                    SaveSkillToDatabase(currentName, currentPoints, currentPosition);
                }
                else
                {
                    UpdateSkillInDatabase(originalData.skill, currentName, currentPoints, currentPosition);
                }

                // Update the stored original values
                row.Tag = new SkillData
                {
                    skill = currentName,
                    points = currentPoints,
                    position = currentPosition
                };

                MessageBox.Show("Changes saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}");
                // Revert to original values
                row.Cells["skill"].Value = originalData.skill;
                row.Cells["points"].Value = originalData.points;
                row.Cells["position"].Value = originalData.position;
            }
        }

        private void skillsDataGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Cells["skill"].Value == null) return;

            string skillName = e.Row.Cells["skill"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{skillName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeleteSkillFromDatabase(skillName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting skill: {ex.Message}");
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnDistribute_Click(object sender, EventArgs e)
        {
            if (isBotRunning)
            {
                stopBot();
                return;
            }

            startBot();
        }

        private void startBot()
        {
            if (skillsDataGrid.Rows.Count == 0)
            {
                MessageBox.Show("Você não tem skills pra distribuir!");
                return;
            }

            isBotRunning = true;
            btnDistribute.Text = "Parar";

            // Create and start the bot thread
            _botThread = new Thread(BotWorker);
            _botThread.IsBackground = true;
            _botThread.Start();
        }

        private void stopBot()
        {
            isBotRunning = false;
            btnDistribute.Text = "Distribuir pontos";

            // Wait for the thread to finish (with timeout)
            if (_botThread != null && _botThread.IsAlive)
            {
                if (!_botThread.Join(1000)) // Wait 1 second for clean shutdown
                {
                    _botThread.Abort(); // Force stop if not responding
                }
            }
        }

        private void BotWorker()
        {
            try
            {
                // Find the target window
                _targetWindowHandle = FindWindow(null, muWindowNameTxt.Text);
                if (_targetWindowHandle == IntPtr.Zero)
                {
                    MessageBox.Show($"Could not find '{muWindowNameTxt.Text}' window.");
                    return;
                }

                // Force window to foreground
                SetForegroundWindow(_targetWindowHandle);
                ShowWindow(_targetWindowHandle, 9);  // SW_RESTORE
                Thread.Sleep(3000);

                // Execute the initial key sequence
                ExecuteInitialKeySequence();

                // Get current mouse position
                GetCursorPos(out POINT currentPos);
                int currentX = currentPos.X;
                int currentY = currentPos.Y;

                var skills = GetSkillsFromDatabase();
                foreach (var skill in skills)
                {
                    if (!isBotRunning) break;
                    if (skill.points == 0) continue;

                    var posParts = skill.position.Split(' ');
                    if (posParts.Length != 2) continue;

                    int x = int.Parse(posParts[0]);
                    int y = int.Parse(posParts[1]);

                    // Visual movement (optional)
                    for (int s = 1; s <= 3; s++)
                    {
                        int stepX = currentX + (x - currentX) * s / 3;
                        int stepY = currentY + (y - currentY) * s / 3;
                        SetCursorPos(stepX, stepY);
                        Thread.Sleep(30);
                    }
                    SetCursorPos(x, y);
                    Thread.Sleep(100);

                    for (int i = 0; i < skill.points && isBotRunning; i++)
                    {
                        // Left click
                        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, 0);
                        Thread.Sleep(40);
                        mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);

                        Thread.Sleep(700);

                        // Press Enter
                        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0);
                        Thread.Sleep(40);
                        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);

                        Thread.Sleep(1000);
                        if (!isBotRunning) break;
                    }

                    if (isBotRunning) Thread.Sleep(555);
                }
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate {
                    MessageBox.Show($"Bot error: {ex.Message}");
                });
            }
            finally
            {
                this.Invoke((MethodInvoker)delegate {
                    isBotRunning = false;
                    btnDistribute.Text = "Distribuir pontos";
                });
            }
        }

        private void ExecuteInitialKeySequence()
        {
            // 1. Press Enter
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0);
            Thread.Sleep(20);
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);
            Thread.Sleep(500);

            // 2. Type '/bar'
            SendKeys.SendWait("/bar");
            Thread.Sleep(500);

            // 3. Press Enter
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0);
            Thread.Sleep(20);
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);
            Thread.Sleep(2000); // Wait 1 second

            // 4. Press 'c' then 'a'
            keybd_event(0x43, 0, KEYEVENTF_KEYDOWN, 0); // 'c' down
            Thread.Sleep(20);
            keybd_event(0x43, 0, KEYEVENTF_KEYUP, 0);   // 'c' up
            Thread.Sleep(500);

            keybd_event(0x41, 0, KEYEVENTF_KEYDOWN, 0); // 'a' down
            Thread.Sleep(20);
            keybd_event(0x41, 0, KEYEVENTF_KEYUP, 0);   // 'a' up
            Thread.Sleep(500);
        }

        private List<SkillData> GetSkillsFromDatabase()
        {
            var skills = new List<SkillData>();

            using (var connection = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                connection.Open();

                string selectQuery = "SELECT skill, points, position FROM Skills";

                using (var command = new SQLiteCommand(selectQuery, connection))

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        skills.Add(new SkillData
                        {
                            skill = reader["skill"].ToString(),
                            points = Convert.ToInt32(reader["points"]),
                            position = reader["position"].ToString()
                        });
                    }
                }
            }

            return skills;
        }
    }
}
