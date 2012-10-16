namespace TickSell
{
    partial class TS
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CellMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SpeedInitialer = new System.Windows.Forms.ToolStripMenuItem();
            this.保存修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清理房间爱你ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MouseMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tipDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.指定座位号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RecoverSeat = new System.Windows.Forms.ToolStripMenuItem();
            this.tvCells = new System.Windows.Forms.TreeView();
            this.TreeViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddNewCell = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteCell = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCellName = new System.Windows.Forms.ToolStripMenuItem();
            this.添加根房间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SellModeSeatMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.出售此座位 = new System.Windows.Forms.ToolStripMenuItem();
            this.取消出售此座位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印小票ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeCellMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblState = new System.Windows.Forms.Label();
            this.btnSwitchMode = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTickManagement = new System.Windows.Forms.Button();
            this.SeatTypeManagement = new System.Windows.Forms.Button();
            this.btnStatic = new System.Windows.Forms.Button();
            this.btnUserManage = new System.Windows.Forms.Button();
            this.timeFreshSeat = new System.Windows.Forms.Timer(this.components);
            this.tvTimeCell = new TickSell.TimeCellTreeView();
            this.flSeats = new TickSell.SeatBtnPanel();
            this.CellMenu.SuspendLayout();
            this.MouseMenu.SuspendLayout();
            this.TreeViewMenu.SuspendLayout();
            this.SellModeSeatMenu.SuspendLayout();
            this.TimeCellMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CellMenu
            // 
            this.CellMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SpeedInitialer,
            this.保存修改ToolStripMenuItem,
            this.清理房间爱你ToolStripMenuItem});
            this.CellMenu.Name = "CellMenu";
            this.CellMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // SpeedInitialer
            // 
            this.SpeedInitialer.Name = "SpeedInitialer";
            this.SpeedInitialer.Size = new System.Drawing.Size(148, 22);
            this.SpeedInitialer.Text = "快速生成房间";
            this.SpeedInitialer.Click += new System.EventHandler(this.SpeedInitialer_Click);
            // 
            // 保存修改ToolStripMenuItem
            // 
            this.保存修改ToolStripMenuItem.Name = "保存修改ToolStripMenuItem";
            this.保存修改ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存修改ToolStripMenuItem.Text = "保存修改";
            this.保存修改ToolStripMenuItem.Click += new System.EventHandler(this.保存修改ToolStripMenuItem_Click);
            // 
            // 清理房间爱你ToolStripMenuItem
            // 
            this.清理房间爱你ToolStripMenuItem.Name = "清理房间爱你ToolStripMenuItem";
            this.清理房间爱你ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清理房间爱你ToolStripMenuItem.Text = "清理房间";
            this.清理房间爱你ToolStripMenuItem.Click += new System.EventHandler(this.清理房间爱你ToolStripMenuItem_Click);
            // 
            // MouseMenu
            // 
            this.MouseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tipDelete,
            this.指定座位号ToolStripMenuItem,
            this.RecoverSeat});
            this.MouseMenu.Name = "MouseMenu";
            this.MouseMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // tipDelete
            // 
            this.tipDelete.Name = "tipDelete";
            this.tipDelete.Size = new System.Drawing.Size(148, 22);
            this.tipDelete.Text = "删除座位";
            this.tipDelete.Click += new System.EventHandler(this.删除座位ToolStripMenuItem_Click);
            // 
            // 指定座位号ToolStripMenuItem
            // 
            this.指定座位号ToolStripMenuItem.Name = "指定座位号ToolStripMenuItem";
            this.指定座位号ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.指定座位号ToolStripMenuItem.Text = "修改座位信息";
            this.指定座位号ToolStripMenuItem.Click += new System.EventHandler(this.指定座位号ToolStripMenuItem_Click);
            // 
            // RecoverSeat
            // 
            this.RecoverSeat.Name = "RecoverSeat";
            this.RecoverSeat.Size = new System.Drawing.Size(148, 22);
            this.RecoverSeat.Text = "重置座位";
            this.RecoverSeat.Click += new System.EventHandler(this.RecoverSeat_Click);
            // 
            // tvCells
            // 
            this.tvCells.Location = new System.Drawing.Point(0, 47);
            this.tvCells.Name = "tvCells";
            this.tvCells.Size = new System.Drawing.Size(136, 508);
            this.tvCells.TabIndex = 5;
            this.tvCells.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCells_AfterSelect);
            this.tvCells.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCells_NodeMouseDoubleClick);
            this.tvCells.Click += new System.EventHandler(this.tvCells_Click);
            // 
            // TreeViewMenu
            // 
            this.TreeViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddNewCell,
            this.DeleteCell,
            this.EditCellName,
            this.添加根房间ToolStripMenuItem});
            this.TreeViewMenu.Name = "TreeViewMenu";
            this.TreeViewMenu.Size = new System.Drawing.Size(149, 92);
            // 
            // AddNewCell
            // 
            this.AddNewCell.Name = "AddNewCell";
            this.AddNewCell.Size = new System.Drawing.Size(148, 22);
            this.AddNewCell.Text = "添加子房间";
            this.AddNewCell.Click += new System.EventHandler(this.AddNewCell_Click);
            // 
            // DeleteCell
            // 
            this.DeleteCell.Name = "DeleteCell";
            this.DeleteCell.Size = new System.Drawing.Size(148, 22);
            this.DeleteCell.Text = "删除房间";
            this.DeleteCell.Click += new System.EventHandler(this.DeleteCell_Click);
            // 
            // EditCellName
            // 
            this.EditCellName.Name = "EditCellName";
            this.EditCellName.Size = new System.Drawing.Size(148, 22);
            this.EditCellName.Text = "修改房间名称";
            this.EditCellName.Click += new System.EventHandler(this.EditCellName_Click);
            // 
            // 添加根房间ToolStripMenuItem
            // 
            this.添加根房间ToolStripMenuItem.Name = "添加根房间ToolStripMenuItem";
            this.添加根房间ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加根房间ToolStripMenuItem.Text = "添加根房间";
            this.添加根房间ToolStripMenuItem.Click += new System.EventHandler(this.添加根房间ToolStripMenuItem_Click);
            // 
            // SellModeSeatMenu
            // 
            this.SellModeSeatMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.出售此座位,
            this.取消出售此座位ToolStripMenuItem,
            this.打印小票ToolStripMenuItem});
            this.SellModeSeatMenu.Name = "SellModeMenu";
            this.SellModeSeatMenu.Size = new System.Drawing.Size(161, 70);
            // 
            // 出售此座位
            // 
            this.出售此座位.Name = "出售此座位";
            this.出售此座位.Size = new System.Drawing.Size(160, 22);
            this.出售此座位.Text = "出售此座位";
            this.出售此座位.Click += new System.EventHandler(this.出售此座位_Click);
            // 
            // 取消出售此座位ToolStripMenuItem
            // 
            this.取消出售此座位ToolStripMenuItem.Name = "取消出售此座位ToolStripMenuItem";
            this.取消出售此座位ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.取消出售此座位ToolStripMenuItem.Text = "取消出售此座位";
            this.取消出售此座位ToolStripMenuItem.Click += new System.EventHandler(this.取消出售此座位ToolStripMenuItem_Click);
            // 
            // 打印小票ToolStripMenuItem
            // 
            this.打印小票ToolStripMenuItem.Name = "打印小票ToolStripMenuItem";
            this.打印小票ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.打印小票ToolStripMenuItem.Text = "打印小票";
            this.打印小票ToolStripMenuItem.Click += new System.EventHandler(this.打印小票ToolStripMenuItem_Click);
            // 
            // TimeCellMenu
            // 
            this.TimeCellMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.TimeCellMenu.Name = "MouseMenu";
            this.TimeCellMenu.Size = new System.Drawing.Size(173, 70);
            this.TimeCellMenu.Opening += new System.ComponentModel.CancelEventHandler(this.TimeCellMenu_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem1.Text = "增加影片播放信息";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.增加影片播放信息_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem2.Text = "修改影片播放信息";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem3.Text = "删除影片播放信息";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(33, 18);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(0, 12);
            this.lblState.TabIndex = 0;
            // 
            // btnSwitchMode
            // 
            this.btnSwitchMode.Location = new System.Drawing.Point(693, 12);
            this.btnSwitchMode.Name = "btnSwitchMode";
            this.btnSwitchMode.Size = new System.Drawing.Size(108, 23);
            this.btnSwitchMode.TabIndex = 1;
            this.btnSwitchMode.Text = "切换至售票模式";
            this.btnSwitchMode.UseVisualStyleBackColor = true;
            this.btnSwitchMode.Visible = false;
            this.btnSwitchMode.Click += new System.EventHandler(this.btnSwitchMode_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTickManagement);
            this.panel1.Controls.Add(this.SeatTypeManagement);
            this.panel1.Controls.Add(this.btnStatic);
            this.panel1.Controls.Add(this.btnUserManage);
            this.panel1.Controls.Add(this.btnSwitchMode);
            this.panel1.Controls.Add(this.lblState);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 41);
            this.panel1.TabIndex = 10;
            // 
            // btnTickManagement
            // 
            this.btnTickManagement.Location = new System.Drawing.Point(336, 12);
            this.btnTickManagement.Name = "btnTickManagement";
            this.btnTickManagement.Size = new System.Drawing.Size(97, 23);
            this.btnTickManagement.TabIndex = 5;
            this.btnTickManagement.Text = "管理票类型";
            this.btnTickManagement.UseVisualStyleBackColor = true;
            this.btnTickManagement.Click += new System.EventHandler(this.btnTickManagement_Click);
            // 
            // SeatTypeManagement
            // 
            this.SeatTypeManagement.Location = new System.Drawing.Point(223, 12);
            this.SeatTypeManagement.Name = "SeatTypeManagement";
            this.SeatTypeManagement.Size = new System.Drawing.Size(97, 23);
            this.SeatTypeManagement.TabIndex = 4;
            this.SeatTypeManagement.Text = "管理座位类型";
            this.SeatTypeManagement.UseVisualStyleBackColor = true;
            this.SeatTypeManagement.Click += new System.EventHandler(this.SeatTypeManagement_Click);
            // 
            // btnStatic
            // 
            this.btnStatic.Location = new System.Drawing.Point(128, 12);
            this.btnStatic.Name = "btnStatic";
            this.btnStatic.Size = new System.Drawing.Size(75, 23);
            this.btnStatic.TabIndex = 3;
            this.btnStatic.Text = "统计信息";
            this.btnStatic.UseVisualStyleBackColor = true;
            this.btnStatic.Click += new System.EventHandler(this.btnStatic_Click);
            // 
            // btnUserManage
            // 
            this.btnUserManage.Location = new System.Drawing.Point(35, 13);
            this.btnUserManage.Name = "btnUserManage";
            this.btnUserManage.Size = new System.Drawing.Size(75, 23);
            this.btnUserManage.TabIndex = 2;
            this.btnUserManage.Text = "用户管理";
            this.btnUserManage.UseVisualStyleBackColor = true;
            this.btnUserManage.Click += new System.EventHandler(this.btnUserManage_Click);
            // 
            // timeFreshSeat
            // 
            this.timeFreshSeat.Interval = 60000;
            // 
            // tvTimeCell
            // 
            this.tvTimeCell.Context = null;
            this.tvTimeCell.ContextMenuStrip = this.TimeCellMenu;
            this.tvTimeCell.CurrentCell = null;
            this.tvTimeCell.CurrentTimeCell = null;
            this.tvTimeCell.CurrentUser = null;
            this.tvTimeCell.EditModeBtnMenuStrip = null;
            this.tvTimeCell.FlSeats = null;
            this.tvTimeCell.Location = new System.Drawing.Point(142, 47);
            this.tvTimeCell.Name = "tvTimeCell";
            this.tvTimeCell.SellModeBtnMenuStrip = null;
            this.tvTimeCell.Size = new System.Drawing.Size(280, 508);
            this.tvTimeCell.TabIndex = 11;
            this.tvTimeCell.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTimeCell_AfterSelect);
            // 
            // flSeats
            // 
            this.flSeats.BtnEditModeMenu = null;
            this.flSeats.BtnSellModeMenu = null;
            this.flSeats.ContextMenuStrip = this.CellMenu;
            this.flSeats.Location = new System.Drawing.Point(428, 47);
            this.flSeats.Name = "flSeats";
            this.flSeats.PanelEditModeMenu = null;
            this.flSeats.PanelSellModeMenu = null;
            this.flSeats.Size = new System.Drawing.Size(771, 479);
            this.flSeats.TabIndex = 1;
            this.flSeats.Paint += new System.Windows.Forms.PaintEventHandler(this.flSeats_Paint);
            this.flSeats.MouseEnter += new System.EventHandler(this.flSeats_MouseEnter);
            this.flSeats.MouseLeave += new System.EventHandler(this.flSeats_MouseLeave);
            // 
            // TS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 526);
            this.Controls.Add(this.tvTimeCell);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvCells);
            this.Controls.Add(this.flSeats);
            this.Name = "TS";
            this.Text = "售票系统";
            this.Load += new System.EventHandler(this.TS_Load);
            this.Shown += new System.EventHandler(this.TS_Shown);
            this.Resize += new System.EventHandler(this.TS_Resize);
            this.CellMenu.ResumeLayout(false);
            this.MouseMenu.ResumeLayout(false);
            this.TreeViewMenu.ResumeLayout(false);
            this.SellModeSeatMenu.ResumeLayout(false);
            this.TimeCellMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SeatBtnPanel flSeats;
        private System.Windows.Forms.ContextMenuStrip MouseMenu;
        private System.Windows.Forms.ToolStripMenuItem tipDelete;
        private System.Windows.Forms.ToolStripMenuItem 指定座位号ToolStripMenuItem;
        private System.Windows.Forms.TreeView tvCells;
        private System.Windows.Forms.ContextMenuStrip TreeViewMenu;
        private System.Windows.Forms.ToolStripMenuItem AddNewCell;
        private System.Windows.Forms.ToolStripMenuItem DeleteCell;
        private System.Windows.Forms.ToolStripMenuItem EditCellName;
        private System.Windows.Forms.ContextMenuStrip CellMenu;
        private System.Windows.Forms.ToolStripMenuItem SpeedInitialer;
        private System.Windows.Forms.ToolStripMenuItem RecoverSeat;
        private System.Windows.Forms.ToolStripMenuItem 保存修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清理房间爱你ToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip SellModeSeatMenu;
        private System.Windows.Forms.ToolStripMenuItem 出售此座位;
        private System.Windows.Forms.ContextMenuStrip TimeCellMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private TimeCellTreeView tvTimeCell;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnSwitchMode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 取消出售此座位ToolStripMenuItem;
        private System.Windows.Forms.Button btnUserManage;
        private System.Windows.Forms.ToolStripMenuItem 打印小票ToolStripMenuItem;
        private System.Windows.Forms.Timer timeFreshSeat;
        private System.Windows.Forms.ToolStripMenuItem 添加根房间ToolStripMenuItem;
        private System.Windows.Forms.Button btnStatic;
        private System.Windows.Forms.Button SeatTypeManagement;
        private System.Windows.Forms.Button btnTickManagement;
    }
}

