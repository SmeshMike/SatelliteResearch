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
            this.errorLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // searcherStation1
            // 
            this.searcherStation1.Location = new System.Drawing.Point(1346, 11);
            this.searcherStation1.Name = "searcherStation1";
            this.searcherStation1.Size = new System.Drawing.Size(226, 69);
            this.searcherStation1.TabIndex = 0;
            this.searcherStation1.x = 0D;
            this.searcherStation1.y = 0D;
            this.searcherStation1.z = 0D;
            // 
            // searcherStation2
            // 
            this.searcherStation2.Location = new System.Drawing.Point(1346, 85);
            this.searcherStation2.Name = "searcherStation2";
            this.searcherStation2.Size = new System.Drawing.Size(226, 69);
            this.searcherStation2.TabIndex = 1;
            this.searcherStation2.x = 0D;
            this.searcherStation2.y = 0D;
            this.searcherStation2.z = 0D;
            // 
            // searcherStation3
            // 
            this.searcherStation3.Location = new System.Drawing.Point(1346, 159);
            this.searcherStation3.Name = "searcherStation3";
            this.searcherStation3.Size = new System.Drawing.Size(226, 69);
            this.searcherStation3.TabIndex = 2;
            this.searcherStation3.x = 0D;
            this.searcherStation3.y = 0D;
            this.searcherStation3.z = 0D;
            // 
            // searcherStation4
            // 
            this.searcherStation4.Location = new System.Drawing.Point(1346, 233);
            this.searcherStation4.Name = "searcherStation4";
            this.searcherStation4.Size = new System.Drawing.Size(226, 69);
            this.searcherStation4.TabIndex = 3;
            this.searcherStation4.x = 0D;
            this.searcherStation4.y = 0D;
            this.searcherStation4.z = 0D;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(1473, 681);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(83, 22);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButtonClick);
            // 
            // trueSource
            // 
            this.trueSource.Location = new System.Drawing.Point(1346, 307);
            this.trueSource.Name = "trueSource";
            this.trueSource.Size = new System.Drawing.Size(226, 69);
            this.trueSource.TabIndex = 3;
            this.trueSource.x = 0D;
            this.trueSource.y = 0D;
            this.trueSource.z = 0D;
            // 
            // newSource
            // 
            this.newSource.Location = new System.Drawing.Point(1346, 381);
            this.newSource.Name = "newSource";
            this.newSource.Size = new System.Drawing.Size(226, 69);
            this.newSource.TabIndex = 3;
            this.newSource.x = 0D;
            this.newSource.y = 0D;
            this.newSource.z = 0D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1502, 505);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Step";
            // 
            // stepTextBox
            // 
            this.stepTextBox.Location = new System.Drawing.Point(1502, 522);
            this.stepTextBox.Name = "stepTextBox";
            this.stepTextBox.Size = new System.Drawing.Size(54, 23);
            this.stepTextBox.TabIndex = 6;
            // 
            // minstep
            // 
            this.minstep.AutoSize = true;
            this.minstep.Location = new System.Drawing.Point(1502, 554);
            this.minstep.Name = "minstep";
            this.minstep.Size = new System.Drawing.Size(59, 15);
            this.minstep.TabIndex = 5;
            this.minstep.Text = "Step(min)";
            // 
            // minStepTextBox
            // 
            this.minStepTextBox.Location = new System.Drawing.Point(1502, 572);
            this.minStepTextBox.Name = "minStepTextBox";
            this.minStepTextBox.Size = new System.Drawing.Size(54, 23);
            this.minStepTextBox.TabIndex = 6;
            // 
            // Denomirator
            // 
            this.Denomirator.AutoSize = true;
            this.Denomirator.Location = new System.Drawing.Point(1502, 606);
            this.Denomirator.Name = "Denomirator";
            this.Denomirator.Size = new System.Drawing.Size(74, 15);
            this.Denomirator.TabIndex = 5;
            this.Denomirator.Text = "Denomirator";
            // 
            // denominatorTextBox
            // 
            this.denominatorTextBox.Location = new System.Drawing.Point(1502, 624);
            this.denominatorTextBox.Name = "denominatorTextBox";
            this.denominatorTextBox.Size = new System.Drawing.Size(54, 23);
            this.denominatorTextBox.TabIndex = 6;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(1362, 681);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(83, 22);
            this.refreshButton.TabIndex = 7;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButtonClick);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(1473, 657);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(42, 15);
            this.timeLabel.TabIndex = 8;
            this.timeLabel.Text = "Время";
            // 
            // dCoordinatesGraphButton
            // 
            this.dCoordinatesGraphButton.Location = new System.Drawing.Point(1362, 709);
            this.dCoordinatesGraphButton.Name = "dCoordinatesGraphButton";
            this.dCoordinatesGraphButton.Size = new System.Drawing.Size(83, 39);
            this.dCoordinatesGraphButton.TabIndex = 7;
            this.dCoordinatesGraphButton.Text = "Разница координат";
            this.dCoordinatesGraphButton.UseVisualStyleBackColor = true;
            this.dCoordinatesGraphButton.Click += new System.EventHandler(this.DCoordinatesGraphButtonClick);
            // 
            // dtGraphButton
            // 
            this.dtGraphButton.Location = new System.Drawing.Point(1473, 709);
            this.dtGraphButton.Name = "dtGraphButton";
            this.dtGraphButton.Size = new System.Drawing.Size(83, 39);
            this.dtGraphButton.TabIndex = 4;
            this.dtGraphButton.Text = "Разница времени";
            this.dtGraphButton.UseVisualStyleBackColor = true;
            this.dtGraphButton.Click += new System.EventHandler(this.DtGraphButton);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(1362, 657);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(53, 15);
            this.errorLabel.TabIndex = 9;
            this.errorLabel.Text = "Ошибка";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(1346, 457);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(226, 23);
            this.progressBar.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 758);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.errorLabel);
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

        public RadioStation searcherStation1;
        public RadioStation radioStation;
        public RadioStation searcherStation2;
        public RadioStation searcherStation3;
        public RadioStation searcherStation4;
        private System.Windows.Forms.Button runButton;
        public RadioStation trueSource;
        public RadioStation newSource;
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
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}