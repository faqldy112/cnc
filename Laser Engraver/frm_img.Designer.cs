namespace Laser_Engraver
{
    partial class frm_img
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pic_image = new System.Windows.Forms.PictureBox();
            this.pic_rulerY = new System.Windows.Forms.PictureBox();
            this.pic_rulerX = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btn_engrave = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_lppo = new System.Windows.Forms.Button();
            this.btn_gray = new System.Windows.Forms.Button();
            this.btn_mirror = new System.Windows.Forms.Button();
            this.btn_reserve = new System.Windows.Forms.Button();
            this.radio_millimeter = new System.Windows.Forms.RadioButton();
            this.rb_realeye = new System.Windows.Forms.RadioButton();
            this.rb_pixel = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.track_zoom = new System.Windows.Forms.TrackBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刻度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.毫米ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.像素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rulerY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rulerX)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_zoom)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 28, 3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pic_image);
            this.splitContainer1.Panel1.Controls.Add(this.pic_rulerY);
            this.splitContainer1.Panel1.Controls.Add(this.pic_rulerX);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.btn_engrave);
            this.splitContainer1.Panel2.Controls.Add(this.btn_reset);
            this.splitContainer1.Panel2.Controls.Add(this.btn_lppo);
            this.splitContainer1.Panel2.Controls.Add(this.btn_gray);
            this.splitContainer1.Panel2.Controls.Add(this.btn_mirror);
            this.splitContainer1.Panel2.Controls.Add(this.btn_reserve);
            this.splitContainer1.Panel2.Controls.Add(this.radio_millimeter);
            this.splitContainer1.Panel2.Controls.Add(this.rb_realeye);
            this.splitContainer1.Panel2.Controls.Add(this.rb_pixel);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.track_zoom);
            this.splitContainer1.Size = new System.Drawing.Size(762, 525);
            this.splitContainer1.SplitterDistance = 521;
            this.splitContainer1.TabIndex = 0;
            // 
            // pic_image
            // 
            this.pic_image.BackColor = System.Drawing.Color.LightGray;
            this.pic_image.Location = new System.Drawing.Point(20, 21);
            this.pic_image.Name = "pic_image";
            this.pic_image.Size = new System.Drawing.Size(500, 500);
            this.pic_image.TabIndex = 2;
            this.pic_image.TabStop = false;
            // 
            // pic_rulerY
            // 
            this.pic_rulerY.Location = new System.Drawing.Point(0, 21);
            this.pic_rulerY.Name = "pic_rulerY";
            this.pic_rulerY.Size = new System.Drawing.Size(20, 500);
            this.pic_rulerY.TabIndex = 1;
            this.pic_rulerY.TabStop = false;
            // 
            // pic_rulerX
            // 
            this.pic_rulerX.Location = new System.Drawing.Point(20, 0);
            this.pic_rulerX.Name = "pic_rulerX";
            this.pic_rulerX.Size = new System.Drawing.Size(500, 20);
            this.pic_rulerX.TabIndex = 0;
            this.pic_rulerX.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 503);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(237, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // btn_engrave
            // 
            this.btn_engrave.Location = new System.Drawing.Point(118, 155);
            this.btn_engrave.Name = "btn_engrave";
            this.btn_engrave.Size = new System.Drawing.Size(75, 23);
            this.btn_engrave.TabIndex = 10;
            this.btn_engrave.Text = "雕刻";
            this.btn_engrave.UseVisualStyleBackColor = true;
            this.btn_engrave.Click += new System.EventHandler(this.btn_engrave_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(36, 155);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 9;
            this.btn_reset.Text = "还原";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_lppo
            // 
            this.btn_lppo.Location = new System.Drawing.Point(118, 125);
            this.btn_lppo.Name = "btn_lppo";
            this.btn_lppo.Size = new System.Drawing.Size(75, 23);
            this.btn_lppo.TabIndex = 8;
            this.btn_lppo.Text = "二值化";
            this.btn_lppo.UseVisualStyleBackColor = true;
            this.btn_lppo.Click += new System.EventHandler(this.btn_lppo_Click);
            // 
            // btn_gray
            // 
            this.btn_gray.Location = new System.Drawing.Point(36, 125);
            this.btn_gray.Name = "btn_gray";
            this.btn_gray.Size = new System.Drawing.Size(75, 23);
            this.btn_gray.TabIndex = 7;
            this.btn_gray.Text = "灰度";
            this.btn_gray.UseVisualStyleBackColor = true;
            this.btn_gray.Click += new System.EventHandler(this.btn_gray_Click);
            // 
            // btn_mirror
            // 
            this.btn_mirror.Location = new System.Drawing.Point(117, 95);
            this.btn_mirror.Name = "btn_mirror";
            this.btn_mirror.Size = new System.Drawing.Size(75, 23);
            this.btn_mirror.TabIndex = 6;
            this.btn_mirror.Text = "刻章";
            this.btn_mirror.UseVisualStyleBackColor = true;
            this.btn_mirror.Click += new System.EventHandler(this.btn_mirror_Click);
            // 
            // btn_reserve
            // 
            this.btn_reserve.Location = new System.Drawing.Point(36, 95);
            this.btn_reserve.Name = "btn_reserve";
            this.btn_reserve.Size = new System.Drawing.Size(75, 23);
            this.btn_reserve.TabIndex = 5;
            this.btn_reserve.Text = "反向";
            this.btn_reserve.UseVisualStyleBackColor = true;
            this.btn_reserve.Click += new System.EventHandler(this.btn_reserve_Click);
            // 
            // radio_millimeter
            // 
            this.radio_millimeter.AutoSize = true;
            this.radio_millimeter.Location = new System.Drawing.Point(77, 60);
            this.radio_millimeter.Name = "radio_millimeter";
            this.radio_millimeter.Size = new System.Drawing.Size(47, 16);
            this.radio_millimeter.TabIndex = 4;
            this.radio_millimeter.TabStop = true;
            this.radio_millimeter.Text = "毫米";
            this.radio_millimeter.UseVisualStyleBackColor = true;
            this.radio_millimeter.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_realeye
            // 
            this.rb_realeye.AutoSize = true;
            this.rb_realeye.Location = new System.Drawing.Point(130, 60);
            this.rb_realeye.Name = "rb_realeye";
            this.rb_realeye.Size = new System.Drawing.Size(71, 16);
            this.rb_realeye.TabIndex = 3;
            this.rb_realeye.Text = "雕刻预览";
            this.rb_realeye.UseVisualStyleBackColor = true;
            this.rb_realeye.CheckedChanged += new System.EventHandler(this.rb_realeye_CheckedChanged);
            // 
            // rb_pixel
            // 
            this.rb_pixel.AutoSize = true;
            this.rb_pixel.Checked = true;
            this.rb_pixel.Location = new System.Drawing.Point(16, 60);
            this.rb_pixel.Name = "rb_pixel";
            this.rb_pixel.Size = new System.Drawing.Size(47, 16);
            this.rb_pixel.TabIndex = 2;
            this.rb_pixel.TabStop = true;
            this.rb_pixel.Text = "像素";
            this.rb_pixel.UseVisualStyleBackColor = true;
            this.rb_pixel.CheckedChanged += new System.EventHandler(this.rb_pixel_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "缩放：";
            // 
            // track_zoom
            // 
            this.track_zoom.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            this.track_zoom.Location = new System.Drawing.Point(51, 21);
            this.track_zoom.Minimum = 1;
            this.track_zoom.Name = "track_zoom";
            this.track_zoom.Size = new System.Drawing.Size(146, 45);
            this.track_zoom.TabIndex = 0;
            this.track_zoom.Value = 1;
            this.track_zoom.Scroll += new System.EventHandler(this.track_zoom_Scroll);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            this.contextMenuStrip1.Text = "文件";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem1,
            this.显示ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(762, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem1
            // 
            this.文件ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem1.Name = "文件ToolStripMenuItem1";
            this.文件ToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem1.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刻度ToolStripMenuItem});
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.显示ToolStripMenuItem.Text = "视图";
            // 
            // 刻度ToolStripMenuItem
            // 
            this.刻度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.毫米ToolStripMenuItem,
            this.像素ToolStripMenuItem});
            this.刻度ToolStripMenuItem.Name = "刻度ToolStripMenuItem";
            this.刻度ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.刻度ToolStripMenuItem.Text = "刻度";
            // 
            // 毫米ToolStripMenuItem
            // 
            this.毫米ToolStripMenuItem.Checked = true;
            this.毫米ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.毫米ToolStripMenuItem.Name = "毫米ToolStripMenuItem";
            this.毫米ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.毫米ToolStripMenuItem.Text = "毫米";
            // 
            // 像素ToolStripMenuItem
            // 
            this.像素ToolStripMenuItem.Name = "像素ToolStripMenuItem";
            this.像素ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.像素ToolStripMenuItem.Text = "像素";
            // 
            // frm_img
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 550);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frm_img";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rulerY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_rulerX)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_zoom)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pic_image;
        private System.Windows.Forms.PictureBox pic_rulerY;
        private System.Windows.Forms.PictureBox pic_rulerX;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刻度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 毫米ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 像素ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar track_zoom;
        private System.Windows.Forms.RadioButton rb_realeye;
        private System.Windows.Forms.RadioButton rb_pixel;
        private System.Windows.Forms.RadioButton radio_millimeter;
        private System.Windows.Forms.Button btn_gray;
        private System.Windows.Forms.Button btn_mirror;
        private System.Windows.Forms.Button btn_reserve;
        private System.Windows.Forms.Button btn_lppo;
        private System.Windows.Forms.Button btn_engrave;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}