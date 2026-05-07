namespace Pushover
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.ComboBox comboBoxService;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelService;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.comboBoxService = new System.Windows.Forms.ComboBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelService = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(12, 15);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(107, 20);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "Сообщение";
            this.textBoxMessage.Location = new System.Drawing.Point(16, 38);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(456, 80);
            this.textBoxMessage.TabIndex = 1;
            this.labelService.AutoSize = true;
            this.labelService.Location = new System.Drawing.Point(12, 132);
            this.labelService.Name = "labelService";
            this.labelService.Size = new System.Drawing.Size(152, 20);
            this.labelService.TabIndex = 2;
            this.labelService.Text = "Тип уведомления";
            this.comboBoxService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxService.FormattingEnabled = true;
            this.comboBoxService.Location = new System.Drawing.Point(16, 155);
            this.comboBoxService.Name = "comboBoxService";
            this.comboBoxService.Size = new System.Drawing.Size(222, 28);
            this.comboBoxService.TabIndex = 3;
            this.buttonSend.Location = new System.Drawing.Point(262, 152);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(210, 34);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Отправить уведомление";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            this.richTextBoxLog.Location = new System.Drawing.Point(16, 202);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(456, 211);
            this.richTextBoxLog.TabIndex = 5;
            this.richTextBoxLog.Text = "";
            this.ClientSize = new System.Drawing.Size(492, 430);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.comboBoxService);
            this.Controls.Add(this.labelService);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.labelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Pushover";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
