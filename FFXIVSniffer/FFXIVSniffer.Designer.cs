namespace FFXIVSniffer
{
    partial class FFXIVSnifferForm
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
            this.components = new System.ComponentModel.Container();
            this.PacketsView = new System.Windows.Forms.DataGridView();
            this.PacketContents = new System.Windows.Forms.RichTextBox();
            this.leftSide = new System.Windows.Forms.Panel();
            this.packetWrapperBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PacketsView)).BeginInit();
            this.leftSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetWrapperBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PacketsView
            // 
            this.PacketsView.AllowUserToAddRows = false;
            this.PacketsView.AllowUserToDeleteRows = false;
            this.PacketsView.AllowUserToResizeRows = false;
            this.PacketsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.PacketsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PacketsView.Dock = System.Windows.Forms.DockStyle.Top;
            this.PacketsView.Location = new System.Drawing.Point(0, 0);
            this.PacketsView.MultiSelect = false;
            this.PacketsView.Name = "PacketsView";
            this.PacketsView.ReadOnly = true;
            this.PacketsView.RowHeadersVisible = false;
            this.PacketsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PacketsView.Size = new System.Drawing.Size(505, 294);
            this.PacketsView.TabIndex = 0;
            this.PacketsView.SelectionChanged += new System.EventHandler(this.PacketsView_SelectionChanged);
            // 
            // PacketContents
            // 
            this.PacketContents.Dock = System.Windows.Forms.DockStyle.Right;
            this.PacketContents.Location = new System.Drawing.Point(505, 0);
            this.PacketContents.Name = "PacketContents";
            this.PacketContents.Size = new System.Drawing.Size(232, 573);
            this.PacketContents.TabIndex = 1;
            this.PacketContents.Text = "";
            // 
            // leftSide
            // 
            this.leftSide.Controls.Add(this.PacketsView);
            this.leftSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSide.Location = new System.Drawing.Point(0, 0);
            this.leftSide.Name = "leftSide";
            this.leftSide.Size = new System.Drawing.Size(505, 573);
            this.leftSide.TabIndex = 2;
            // 
            // packetWrapperBindingSource
            // 
            this.packetWrapperBindingSource.DataSource = typeof(FFXIVSniffer.PacketWrapper);
            // 
            // FFXIVSnifferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 573);
            this.Controls.Add(this.leftSide);
            this.Controls.Add(this.PacketContents);
            this.Name = "FFXIVSnifferForm";
            this.Text = "FFXIV Sniffer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FFXIVSnifferForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PacketsView)).EndInit();
            this.leftSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.packetWrapperBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PacketsView;
        private System.Windows.Forms.BindingSource packetWrapperBindingSource;
        private System.Windows.Forms.RichTextBox PacketContents;
        private System.Windows.Forms.Panel leftSide;
    }
}

