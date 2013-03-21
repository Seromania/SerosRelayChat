namespace SerosRelayChatClient
{
    partial class Channel
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.list_Users = new System.Windows.Forms.ListBox();
            this.txt_chat = new System.Windows.Forms.TextBox();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_Users
            // 
            this.list_Users.FormattingEnabled = true;
            this.list_Users.Location = new System.Drawing.Point(489, 2);
            this.list_Users.Name = "list_Users";
            this.list_Users.Size = new System.Drawing.Size(167, 368);
            this.list_Users.TabIndex = 0;
            // 
            // txt_chat
            // 
            this.txt_chat.BackColor = System.Drawing.Color.White;
            this.txt_chat.Location = new System.Drawing.Point(2, 3);
            this.txt_chat.Multiline = true;
            this.txt_chat.Name = "txt_chat";
            this.txt_chat.ReadOnly = true;
            this.txt_chat.Size = new System.Drawing.Size(487, 367);
            this.txt_chat.TabIndex = 1;
            // 
            // txt_message
            // 
            this.txt_message.Location = new System.Drawing.Point(2, 375);
            this.txt_message.MaxLength = 510;
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(484, 20);
            this.txt_message.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 375);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 19);
            this.button1.TabIndex = 3;
            this.button1.Text = "Senden";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 398);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_message);
            this.Controls.Add(this.txt_chat);
            this.Controls.Add(this.list_Users);
            this.Name = "Channel";
            this.Text = "Channel";
            this.Load += new System.EventHandler(this.Channel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_Users;
        private System.Windows.Forms.TextBox txt_chat;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.Button button1;
    }
}