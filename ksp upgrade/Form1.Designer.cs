namespace ksp_upgrade
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
            btnStart = new Button();
            scoreboardListView = new ListView();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(160, 30);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += button1_Click;
            // 
            // scoreboardListView
            // 
            scoreboardListView.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            scoreboardListView.Location = new Point(100, 80);
            scoreboardListView.Name = "scoreboardListView";
            scoreboardListView.Size = new Size(200, 300);
            scoreboardListView.TabIndex = 1;
            scoreboardListView.UseCompatibleStateImageBehavior = false;
            scoreboardListView.View = View.Details;
            scoreboardListView.SelectedIndexChanged += scoreboardListView_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Yellow;
            ClientSize = new Size(384, 411);
            Controls.Add(scoreboardListView);
            Controls.Add(btnStart);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ksp upgrade";
            ResumeLayout(false);
        }

        #endregion

        private Button btnStart;
        private ListView scoreboardListView;
    }
}