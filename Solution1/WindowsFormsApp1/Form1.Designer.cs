
namespace WindowsFormsApp1
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
            this.numericUpDownLight1 = new ClassLibrary1.NumericUpDownLight();
            this.SuspendLayout();
            // 
            // numericUpDownLight1
            // 
            this.numericUpDownLight1.Location = new System.Drawing.Point(296, 167);
            this.numericUpDownLight1.Name = "numericUpDownLight1";
            this.numericUpDownLight1.Size = new System.Drawing.Size(200, 30);
            this.numericUpDownLight1.TabIndex = 0;
            this.numericUpDownLight1.Text = "numericUpDownLight1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numericUpDownLight1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ClassLibrary1.NumericUpDownLight numericUpDownLight1;
    }
}

