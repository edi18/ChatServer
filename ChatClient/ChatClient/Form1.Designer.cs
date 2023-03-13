namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sendMsgBtn = new System.Windows.Forms.Button();
            this.msgInputBox = new System.Windows.Forms.TextBox();
            this.msgOutputBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // sendMsgBtn
            // 
            this.sendMsgBtn.Location = new System.Drawing.Point(12, 423);
            this.sendMsgBtn.Name = "sendMsgBtn";
            this.sendMsgBtn.Size = new System.Drawing.Size(109, 52);
            this.sendMsgBtn.TabIndex = 0;
            this.sendMsgBtn.Text = "Send";
            this.sendMsgBtn.UseVisualStyleBackColor = true;
            this.sendMsgBtn.Click += new System.EventHandler(this.sendMsgBtn_Click);
            // 
            // msgInputBox
            // 
            this.msgInputBox.Location = new System.Drawing.Point(127, 423);
            this.msgInputBox.Multiline = true;
            this.msgInputBox.Name = "msgInputBox";
            this.msgInputBox.Size = new System.Drawing.Size(402, 52);
            this.msgInputBox.TabIndex = 1;
            // 
            // msgOutputBox
            // 
            this.msgOutputBox.Location = new System.Drawing.Point(13, 13);
            this.msgOutputBox.Name = "msgOutputBox";
            this.msgOutputBox.ReadOnly = true;
            this.msgOutputBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.msgOutputBox.Size = new System.Drawing.Size(516, 404);
            this.msgOutputBox.TabIndex = 2;
            this.msgOutputBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 487);
            this.Controls.Add(this.msgOutputBox);
            this.Controls.Add(this.msgInputBox);
            this.Controls.Add(this.sendMsgBtn);
            this.Name = "Form1";
            this.Text = "Chat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendMsgBtn;
        private System.Windows.Forms.TextBox msgInputBox;
        private System.Windows.Forms.RichTextBox msgOutputBox;
    }
}

