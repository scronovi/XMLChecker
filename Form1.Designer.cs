namespace XMLChecker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            check_button = new Button();
            path1_textbox = new TextBox();
            path2_textbox = new TextBox();
            tablist = new TabControl();
            tabPage1 = new TabPage();
            klarText = new Label();
            progressBar1 = new ProgressBar();
            tabPage2 = new TabPage();
            logTextBox = new RichTextBox();
            logClear = new Button();
            tablist.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // check_button
            // 
            check_button.BackColor = Color.CornflowerBlue;
            check_button.Location = new Point(337, 296);
            check_button.Name = "check_button";
            check_button.Size = new Size(128, 72);
            check_button.TabIndex = 0;
            check_button.Text = "Kör";
            check_button.UseVisualStyleBackColor = false;
            check_button.Click += check_button_Click;
            // 
            // path1_textbox
            // 
            path1_textbox.Location = new Point(0, 108);
            path1_textbox.Name = "path1_textbox";
            path1_textbox.PlaceholderText = "Sökväg till mapp 1";
            path1_textbox.Size = new Size(393, 23);
            path1_textbox.TabIndex = 1;
            // 
            // path2_textbox
            // 
            path2_textbox.Location = new Point(0, 185);
            path2_textbox.Name = "path2_textbox";
            path2_textbox.PlaceholderText = "Sökväg till mapp 2";
            path2_textbox.Size = new Size(393, 23);
            path2_textbox.TabIndex = 2;
            // 
            // tablist
            // 
            tablist.Controls.Add(tabPage1);
            tablist.Controls.Add(tabPage2);
            tablist.ItemSize = new Size(58, 40);
            tablist.Location = new Point(-2, 0);
            tablist.Name = "tablist";
            tablist.SelectedIndex = 0;
            tablist.Size = new Size(806, 454);
            tablist.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.AliceBlue;
            tabPage1.Controls.Add(logClear);
            tabPage1.Controls.Add(klarText);
            tabPage1.Controls.Add(progressBar1);
            tabPage1.Controls.Add(check_button);
            tabPage1.Controls.Add(path2_textbox);
            tabPage1.Controls.Add(path1_textbox);
            tabPage1.Location = new Point(4, 44);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(798, 406);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Program";
            // 
            // klarText
            // 
            klarText.BackColor = Color.Transparent;
            klarText.Font = new Font("Verdana", 17.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            klarText.ForeColor = Color.Green;
            klarText.Location = new Point(337, 243);
            klarText.Name = "klarText";
            klarText.Size = new Size(128, 50);
            klarText.TabIndex = 7;
            klarText.Text = "KLAR";
            klarText.TextAlign = ContentAlignment.MiddleCenter;
            klarText.Visible = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(0, 413);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(796, 10);
            progressBar1.TabIndex = 6;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.Gainsboro;
            tabPage2.Controls.Add(logTextBox);
            tabPage2.Location = new Point(4, 44);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(798, 406);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Log";
            // 
            // logTextBox
            // 
            logTextBox.Location = new Point(0, 0);
            logTextBox.Name = "logTextBox";
            logTextBox.Size = new Size(798, 426);
            logTextBox.TabIndex = 0;
            logTextBox.Text = "";
            // 
            // logClear
            // 
            logClear.Location = new Point(605, 321);
            logClear.Name = "logClear";
            logClear.Size = new Size(75, 23);
            logClear.TabIndex = 8;
            logClear.Text = "Rensa log";
            logClear.UseVisualStyleBackColor = true;
            logClear.Click += logClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(tablist);
            Name = "Form1";
            Text = "Form1";
            tablist.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button check_button;
        private TextBox path1_textbox;
        private TextBox path2_textbox;
        private TabControl tablist;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ProgressBar progressBar1;
        private RichTextBox logTextBox;
        private Label klarText;
        private Button logClear;
    }
}
