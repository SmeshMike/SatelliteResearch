using System.Windows.Forms.DataVisualization.Charting;

namespace ResearchModel
{
    partial class ChartsForm
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
            this.SuspendLayout();
            // 
            // ChartsForm
            // 
            ChartArea chartArea = new ChartArea();
            Legend legend = new Legend();
            this.dtDifference = new Chart();
            chartArea.Name = "ChartArea";
            this.dtDifference.ChartAreas.Add(chartArea);
            this.dtDifference.Dock = System.Windows.Forms.DockStyle.Fill;
            legend.Name = "Legend";
            this.dtDifference.Legends.Add(legend);
            this.dtDifference.Location = new System.Drawing.Point(0, 50);
            this.dtDifference.Name = "chart";
            this.dtDifference.TabIndex = 0;
            this.dtDifference.Text = "chart";

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 459);
            this.Name = "ChartsForm";
            this.Text = "ChartsForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}