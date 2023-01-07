namespace EventHandling {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.pbMain = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
			this.SuspendLayout();
			// 
			// pbMain
			// 
			this.pbMain.Location = new System.Drawing.Point(12, 12);
			this.pbMain.Name = "pbMain";
			this.pbMain.Size = new System.Drawing.Size(760, 475);
			this.pbMain.TabIndex = 0;
			this.pbMain.TabStop = false;
			this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
			this.pbMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseClick);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 16;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(778, 37);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(508, 393);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(778, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 30);
			this.label1.TabIndex = 2;
			this.label1.Text = "Очки: 0";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1298, 498);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.pbMain);
			this.KeyPreview = true;
			this.Name = "Form1";
			this.Text = "Form1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PictureBox pbMain;
		private System.Windows.Forms.Timer timer1;
		private RichTextBox richTextBox1;
		private Label label1;
	}
}