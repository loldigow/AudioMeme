using SonsDeMemes.Custons;

namespace SonsDeMemes
{
    partial class AplicacaoSons
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AplicacaoSons));
            button1 = new MeuBotao();
            button2 = new MeuBotao();
            button3 = new MeuBotao();
            button4 = new MeuBotao();
            button5 = new MeuBotao();
            button6 = new MeuBotao();
            button7 = new MeuBotao();
            button8 = new MeuBotao();
            button9 = new MeuBotao();
            iconMinimizado = new NotifyIcon(components);
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 38);
            button1.Name = "button1";
            button1.Size = new Size(94, 86);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(112, 38);
            button2.Name = "button2";
            button2.Size = new Size(94, 86);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(212, 38);
            button3.Name = "button3";
            button3.Size = new Size(94, 86);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(12, 130);
            button4.Name = "button4";
            button4.Size = new Size(94, 86);
            button4.TabIndex = 5;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(112, 130);
            button5.Name = "button5";
            button5.Size = new Size(94, 86);
            button5.TabIndex = 4;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(211, 130);
            button6.Name = "button6";
            button6.Size = new Size(94, 86);
            button6.TabIndex = 3;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(12, 222);
            button7.Name = "button7";
            button7.Size = new Size(94, 86);
            button7.TabIndex = 8;
            button7.Text = "button7";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(112, 222);
            button8.Name = "button8";
            button8.Size = new Size(94, 86);
            button8.TabIndex = 7;
            button8.Text = "button8";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(211, 222);
            button9.Name = "button9";
            button9.Size = new Size(94, 86);
            button9.TabIndex = 6;
            button9.Text = "button9";
            button9.UseVisualStyleBackColor = true;
            // 
            // iconMinimizado
            // 
            iconMinimizado.Icon = (Icon)resources.GetObject("$this.Icon");
            iconMinimizado.Text = "Meme sons";
            iconMinimizado.Visible = true;
            // 
            // AplicacaoSons
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(317, 338);
            Controls.Add(button7);
            Controls.Add(button8);
            Controls.Add(button9);
            Controls.Add(button4);
            Controls.Add(button5);
            Controls.Add(button6);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AplicacaoSons";
            Text = "Meme icons";
            ResumeLayout(false);
        }

        #endregion

        private MeuBotao button1;
        private MeuBotao button2;
        private MeuBotao button3;
        private MeuBotao button4;
        private MeuBotao button5;
        private MeuBotao button6;
        private MeuBotao button7;
        private MeuBotao button8;
        private MeuBotao button9;
        private NotifyIcon iconMinimizado;
    }
}