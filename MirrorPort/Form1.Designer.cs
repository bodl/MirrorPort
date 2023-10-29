namespace MirrorPort
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
            btn_start = new Button();
            btn_stop = new Button();
            lb_status = new Label();
            tb_print = new RichTextBox();
            check_logshow = new CheckBox();
            n_limit = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)n_limit).BeginInit();
            SuspendLayout();
            // 
            // btn_start
            // 
            btn_start.Location = new Point(12, 15);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(75, 23);
            btn_start.TabIndex = 0;
            btn_start.Text = "start";
            btn_start.UseVisualStyleBackColor = true;
            btn_start.Click += btn_start_Click;
            // 
            // btn_stop
            // 
            btn_stop.Location = new Point(105, 15);
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(75, 23);
            btn_stop.TabIndex = 1;
            btn_stop.Text = "stop";
            btn_stop.UseVisualStyleBackColor = true;
            btn_stop.Click += btn_stop_Click;
            // 
            // lb_status
            // 
            lb_status.AutoSize = true;
            lb_status.Location = new Point(203, 18);
            lb_status.Name = "lb_status";
            lb_status.Size = new Size(106, 17);
            lb_status.TabIndex = 2;
            lb_status.Text = "running/stopped";
            // 
            // tb_print
            // 
            tb_print.BulletIndent = 2;
            tb_print.Dock = DockStyle.Bottom;
            tb_print.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tb_print.ForeColor = SystemColors.WindowFrame;
            tb_print.Location = new Point(0, 45);
            tb_print.Name = "tb_print";
            tb_print.ReadOnly = true;
            tb_print.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            tb_print.Size = new Size(800, 405);
            tb_print.TabIndex = 3;
            tb_print.Text = "";
            // 
            // check_logshow
            // 
            check_logshow.AutoSize = true;
            check_logshow.Location = new Point(333, 18);
            check_logshow.Name = "check_logshow";
            check_logshow.Size = new Size(80, 21);
            check_logshow.TabIndex = 4;
            check_logshow.Text = "LogShow";
            check_logshow.UseVisualStyleBackColor = true;
            check_logshow.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // n_limit
            // 
            n_limit.Location = new Point(655, 15);
            n_limit.Name = "n_limit";
            n_limit.Size = new Size(84, 23);
            n_limit.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(748, 21);
            label1.Name = "label1";
            label1.Size = new Size(20, 17);
            label1.TabIndex = 6;
            label1.Text = "M";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(586, 20);
            label2.Name = "label2";
            label2.Size = new Size(63, 17);
            label2.TabIndex = 7;
            label2.Text = "限制速度: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(n_limit);
            Controls.Add(check_logshow);
            Controls.Add(tb_print);
            Controls.Add(lb_status);
            Controls.Add(btn_stop);
            Controls.Add(btn_start);
            MinimumSize = new Size(816, 489);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)n_limit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_start;
        private Button btn_stop;
        private Label lb_status;
        private RichTextBox tb_print;
        private CheckBox check_logshow;
        private NumericUpDown n_limit;
        private Label label1;
        private Label label2;
    }
}