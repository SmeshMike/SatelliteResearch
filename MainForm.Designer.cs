namespace SatelliteResearch
{
    partial class MainForm
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
            this.searcherStation1 = new SatelliteResearch.RadioStation();
            this.searcherStation2 = new SatelliteResearch.RadioStation();
            this.searcherStation3 = new SatelliteResearch.RadioStation();
            this.searcherStation4 = new SatelliteResearch.RadioStation();
            this.runButton = new System.Windows.Forms.Button();
            this.source = new SatelliteResearch.RadioStation();
            this.newSource = new SatelliteResearch.RadioStation();
            this.label1 = new System.Windows.Forms.Label();
            this.stepTextBox = new System.Windows.Forms.TextBox();
            this.minstep = new System.Windows.Forms.Label();
            this.minStepTextBox = new System.Windows.Forms.TextBox();
            this.Denomirator = new System.Windows.Forms.Label();
            this.denominatorTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // searcherStation1
            // 
            this.searcherStation1.Location = new System.Drawing.Point(1393, 12);
            this.searcherStation1.Name = "searcherStation1";
            this.searcherStation1.Size = new System.Drawing.Size(179, 78);
            this.searcherStation1.TabIndex = 0;
            // 
            // searcherStation2
            // 
            this.searcherStation2.Location = new System.Drawing.Point(1393, 96);
            this.searcherStation2.Name = "searcherStation2";
            this.searcherStation2.Size = new System.Drawing.Size(179, 78);
            this.searcherStation2.TabIndex = 1;
            // 
            // searcherStation3
            // 
            this.searcherStation3.Location = new System.Drawing.Point(1393, 180);
            this.searcherStation3.Name = "searcherStation3";
            this.searcherStation3.Size = new System.Drawing.Size(179, 78);
            this.searcherStation3.TabIndex = 2;
            // 
            // searcherStation4
            // 
            this.searcherStation4.Location = new System.Drawing.Point(1393, 264);
            this.searcherStation4.Name = "searcherStation4";
            this.searcherStation4.Size = new System.Drawing.Size(179, 78);
            this.searcherStation4.TabIndex = 3;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(1489, 822);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(83, 25);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButtonClick);
            // 
            // source
            // 
            this.source.Location = new System.Drawing.Point(1393, 348);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(179, 78);
            this.source.TabIndex = 3;
            // 
            // newSource
            // 
            this.newSource.Location = new System.Drawing.Point(1393, 432);
            this.newSource.Name = "newSource";
            this.newSource.Size = new System.Drawing.Size(179, 78);
            this.newSource.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1404, 575);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Step";
            // 
            // stepTextBox
            // 
            this.stepTextBox.Location = new System.Drawing.Point(1404, 595);
            this.stepTextBox.Name = "stepTextBox";
            this.stepTextBox.Size = new System.Drawing.Size(43, 25);
            this.stepTextBox.TabIndex = 6;
            // 
            // minstep
            // 
            this.minstep.AutoSize = true;
            this.minstep.Location = new System.Drawing.Point(1404, 631);
            this.minstep.Name = "minstep";
            this.minstep.Size = new System.Drawing.Size(63, 17);
            this.minstep.TabIndex = 5;
            this.minstep.Text = "Step(min)";
            // 
            // minStepTextBox
            // 
            this.minStepTextBox.Location = new System.Drawing.Point(1404, 651);
            this.minStepTextBox.Name = "minStepTextBox";
            this.minStepTextBox.Size = new System.Drawing.Size(43, 25);
            this.minStepTextBox.TabIndex = 6;
            // 
            // Denomirator
            // 
            this.Denomirator.AutoSize = true;
            this.Denomirator.Location = new System.Drawing.Point(1404, 688);
            this.Denomirator.Name = "Denomirator";
            this.Denomirator.Size = new System.Drawing.Size(82, 17);
            this.Denomirator.TabIndex = 5;
            this.Denomirator.Text = "Denomirator";
            // 
            // denominatorTextBox
            // 
            this.denominatorTextBox.Location = new System.Drawing.Point(1404, 708);
            this.denominatorTextBox.Name = "denominatorTextBox";
            this.denominatorTextBox.Size = new System.Drawing.Size(43, 25);
            this.denominatorTextBox.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 859);
            this.Controls.Add(this.denominatorTextBox);
            this.Controls.Add(this.Denomirator);
            this.Controls.Add(this.minStepTextBox);
            this.Controls.Add(this.minstep);
            this.Controls.Add(this.stepTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newSource);
            this.Controls.Add(this.source);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.searcherStation4);
            this.Controls.Add(this.searcherStation3);
            this.Controls.Add(this.searcherStation2);
            this.Controls.Add(this.searcherStation1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioStation searcherStation1;
        private RadioStation radioStation;
        private RadioStation searcherStation2;
        private RadioStation searcherStation3;
        private RadioStation searcherStation4;
        private System.Windows.Forms.Button runButton;
        private RadioStation source;
        private RadioStation newSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox stepTextBox;
        private System.Windows.Forms.Label minstep;
        private System.Windows.Forms.TextBox minStepTextBox;
        private System.Windows.Forms.Label Denomirator;
        private System.Windows.Forms.TextBox denominatorTextBox;
        private System.Windows.Forms.TextBox textBox;
    }
}