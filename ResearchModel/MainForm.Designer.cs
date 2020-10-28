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
            this.trueSource = new SatelliteResearch.RadioStation();
            this.newSource = new SatelliteResearch.RadioStation();
            this.label1 = new System.Windows.Forms.Label();
            this.stepTextBox = new System.Windows.Forms.TextBox();
            this.minstep = new System.Windows.Forms.Label();
            this.minStepTextBox = new System.Windows.Forms.TextBox();
            this.Denomirator = new System.Windows.Forms.Label();
            this.denominatorTextBox = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.dCoordinatesGraphButton = new System.Windows.Forms.Button();
            this.dtGraphButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searcherStation1
            // 
            this.searcherStation1.Location = new System.Drawing.Point(1346, 12);
            this.searcherStation1.Name = "searcherStation1";
            this.searcherStation1.Size = new System.Drawing.Size(226, 78);
            this.searcherStation1.TabIndex = 0;
            // 
            // searcherStation2
            // 
            this.searcherStation2.Location = new System.Drawing.Point(1346, 96);
            this.searcherStation2.Name = "searcherStation2";
            this.searcherStation2.Size = new System.Drawing.Size(226, 78);
            this.searcherStation2.TabIndex = 1;
            // 
            // searcherStation3
            // 
            this.searcherStation3.Location = new System.Drawing.Point(1346, 180);
            this.searcherStation3.Name = "searcherStation3";
            this.searcherStation3.Size = new System.Drawing.Size(226, 78);
            this.searcherStation3.TabIndex = 2;
            // 
            // searcherStation4
            // 
            this.searcherStation4.Location = new System.Drawing.Point(1346, 264);
            this.searcherStation4.Name = "searcherStation4";
            this.searcherStation4.Size = new System.Drawing.Size(226, 78);
            this.searcherStation4.TabIndex = 3;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(1473, 772);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(83, 25);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButtonClick);
            // 
            // trueSource
            // 
            this.trueSource.Location = new System.Drawing.Point(1346, 348);
            this.trueSource.Name = "trueSource";
            this.trueSource.Size = new System.Drawing.Size(226, 78);
            this.trueSource.TabIndex = 3;
            // 
            // newSource
            // 
            this.newSource.Location = new System.Drawing.Point(1346, 432);
            this.newSource.Name = "newSource";
            this.newSource.Size = new System.Drawing.Size(226, 78);
            this.newSource.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1502, 572);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Step";
            // 
            // stepTextBox
            // 
            this.stepTextBox.Location = new System.Drawing.Point(1502, 592);
            this.stepTextBox.Name = "stepTextBox";
            this.stepTextBox.Size = new System.Drawing.Size(43, 25);
            this.stepTextBox.TabIndex = 6;
            // 
            // minstep
            // 
            this.minstep.AutoSize = true;
            this.minstep.Location = new System.Drawing.Point(1502, 628);
            this.minstep.Name = "minstep";
            this.minstep.Size = new System.Drawing.Size(63, 17);
            this.minstep.TabIndex = 5;
            this.minstep.Text = "Step(min)";
            // 
            // minStepTextBox
            // 
            this.minStepTextBox.Location = new System.Drawing.Point(1502, 648);
            this.minStepTextBox.Name = "minStepTextBox";
            this.minStepTextBox.Size = new System.Drawing.Size(43, 25);
            this.minStepTextBox.TabIndex = 6;
            // 
            // Denomirator
            // 
            this.Denomirator.AutoSize = true;
            this.Denomirator.Location = new System.Drawing.Point(1502, 687);
            this.Denomirator.Name = "Denomirator";
            this.Denomirator.Size = new System.Drawing.Size(82, 17);
            this.Denomirator.TabIndex = 5;
            this.Denomirator.Text = "Denomirator";
            // 
            // denominatorTextBox
            // 
            this.denominatorTextBox.Location = new System.Drawing.Point(1502, 707);
            this.denominatorTextBox.Name = "denominatorTextBox";
            this.denominatorTextBox.Size = new System.Drawing.Size(43, 25);
            this.denominatorTextBox.TabIndex = 6;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(1362, 772);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(83, 25);
            this.refreshButton.TabIndex = 7;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButtonClick);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(1502, 745);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(46, 17);
            this.timeLabel.TabIndex = 8;
            this.timeLabel.Text = "Время";
            // 
            // dCoordinatesGraphButton
            // 
            this.dCoordinatesGraphButton.Location = new System.Drawing.Point(1362, 803);
            this.dCoordinatesGraphButton.Name = "dCoordinatesGraphButton";
            this.dCoordinatesGraphButton.Size = new System.Drawing.Size(83, 44);
            this.dCoordinatesGraphButton.TabIndex = 7;
            this.dCoordinatesGraphButton.Text = "Разница координат";
            this.dCoordinatesGraphButton.UseVisualStyleBackColor = true;
            this.dCoordinatesGraphButton.Click += new System.EventHandler(this.DCoordinatesGraphButtonClick);
            // 
            // dtGraphButton
            // 
            this.dtGraphButton.Location = new System.Drawing.Point(1473, 803);
            this.dtGraphButton.Name = "dtGraphButton";
            this.dtGraphButton.Size = new System.Drawing.Size(83, 44);
            this.dtGraphButton.TabIndex = 4;
            this.dtGraphButton.Text = "Разница времени";
            this.dtGraphButton.UseVisualStyleBackColor = true;
            this.dtGraphButton.Click += new System.EventHandler(this.DtGraphButton);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 859);
            this.Controls.Add(this.dtGraphButton);
            this.Controls.Add(this.dCoordinatesGraphButton);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.denominatorTextBox);
            this.Controls.Add(this.Denomirator);
            this.Controls.Add(this.minStepTextBox);
            this.Controls.Add(this.minstep);
            this.Controls.Add(this.stepTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newSource);
            this.Controls.Add(this.trueSource);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.searcherStation4);
            this.Controls.Add(this.searcherStation3);
            this.Controls.Add(this.searcherStation2);
            this.Controls.Add(this.searcherStation1);
            this.Controls.Add(this.refreshButton);
            this.Name = "MainForm";
            this.Text = "Refresh";
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private RadioStation trueSource;
        private RadioStation newSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox stepTextBox;
        private System.Windows.Forms.Label minstep;
        private System.Windows.Forms.TextBox minStepTextBox;
        private System.Windows.Forms.Label Denomirator;
        private System.Windows.Forms.TextBox denominatorTextBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button dCoordinatesGraphButton;
        private System.Windows.Forms.Button dtGraphButton;
    }
}