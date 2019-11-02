namespace NissanCoupon
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lbTotalCoupon = new System.Windows.Forms.Label();
            this.lbCurrentOtpCount = new System.Windows.Forms.Label();
            this.lbServicesState = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.SystemInfoTick = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFindSMSResult = new System.Windows.Forms.TextBox();
            this.btnResendSMS = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtSMSToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtSMSFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnLoadSMS = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSendCancelCouponInfo = new System.Windows.Forms.Button();
            this.btnChangeCustomerName = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTotalCoupon
            // 
            this.lbTotalCoupon.AutoSize = true;
            this.lbTotalCoupon.Location = new System.Drawing.Point(11, 34);
            this.lbTotalCoupon.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbTotalCoupon.Name = "lbTotalCoupon";
            this.lbTotalCoupon.Size = new System.Drawing.Size(323, 31);
            this.lbTotalCoupon.TabIndex = 1;
            this.lbTotalCoupon.Text = "Số khuyến mãi hiện tại : 0";
            // 
            // lbCurrentOtpCount
            // 
            this.lbCurrentOtpCount.AutoSize = true;
            this.lbCurrentOtpCount.Location = new System.Drawing.Point(11, 65);
            this.lbCurrentOtpCount.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbCurrentOtpCount.Name = "lbCurrentOtpCount";
            this.lbCurrentOtpCount.Size = new System.Drawing.Size(258, 31);
            this.lbCurrentOtpCount.TabIndex = 2;
            this.lbCurrentOtpCount.Text = "Số OTP đang mở : 0";
            // 
            // lbServicesState
            // 
            this.lbServicesState.AutoSize = true;
            this.lbServicesState.Location = new System.Drawing.Point(11, 96);
            this.lbServicesState.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbServicesState.Name = "lbServicesState";
            this.lbServicesState.Size = new System.Drawing.Size(305, 31);
            this.lbServicesState.TabIndex = 3;
            this.lbServicesState.Text = "Trạng thái services : OK";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(11, 127);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(351, 31);
            this.lbVersion.TabIndex = 4;
            this.lbVersion.Text = "Phiên bản phần mềm : 1.0.0";
            // 
            // SystemInfoTick
            // 
            this.SystemInfoTick.Enabled = true;
            this.SystemInfoTick.Interval = 1000;
            this.SystemInfoTick.Tick += new System.EventHandler(this.SystemInfoTick_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbVersion);
            this.groupBox1.Controls.Add(this.lbTotalCoupon);
            this.groupBox1.Controls.Add(this.lbServicesState);
            this.groupBox1.Controls.Add(this.lbCurrentOtpCount);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(948, 173);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin hệ thống";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFindSMSResult);
            this.groupBox2.Controls.Add(this.btnResendSMS);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtSMSToDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtSMSFromDate);
            this.groupBox2.Controls.Add(this.btnLoadSMS);
            this.groupBox2.Location = new System.Drawing.Point(12, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(947, 213);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gửi lại SMS fail";
            // 
            // txtFindSMSResult
            // 
            this.txtFindSMSResult.Location = new System.Drawing.Point(23, 92);
            this.txtFindSMSResult.Multiline = true;
            this.txtFindSMSResult.Name = "txtFindSMSResult";
            this.txtFindSMSResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFindSMSResult.Size = new System.Drawing.Size(643, 105);
            this.txtFindSMSResult.TabIndex = 10;
            this.txtFindSMSResult.Text = "Tổng số 0 SMS đã tìm thấy";
            // 
            // btnResendSMS
            // 
            this.btnResendSMS.Location = new System.Drawing.Point(678, 92);
            this.btnResendSMS.Name = "btnResendSMS";
            this.btnResendSMS.Size = new System.Drawing.Size(269, 51);
            this.btnResendSMS.TabIndex = 8;
            this.btnResendSMS.Text = "Gửi lại";
            this.btnResendSMS.UseVisualStyleBackColor = true;
            this.btnResendSMS.Click += new System.EventHandler(this.btnResendSMS_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "Đến ngày";
            // 
            // dtSMSToDate
            // 
            this.dtSMSToDate.CustomFormat = "dd/MM/yyyy";
            this.dtSMSToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSMSToDate.Location = new System.Drawing.Point(484, 38);
            this.dtSMSToDate.Name = "dtSMSToDate";
            this.dtSMSToDate.Size = new System.Drawing.Size(182, 38);
            this.dtSMSToDate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "Từ ngày";
            // 
            // dtSMSFromDate
            // 
            this.dtSMSFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtSMSFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSMSFromDate.Location = new System.Drawing.Point(134, 37);
            this.dtSMSFromDate.Name = "dtSMSFromDate";
            this.dtSMSFromDate.Size = new System.Drawing.Size(198, 38);
            this.dtSMSFromDate.TabIndex = 1;
            this.dtSMSFromDate.Value = new System.DateTime(2019, 8, 1, 11, 6, 32, 0);
            // 
            // btnLoadSMS
            // 
            this.btnLoadSMS.Location = new System.Drawing.Point(678, 35);
            this.btnLoadSMS.Name = "btnLoadSMS";
            this.btnLoadSMS.Size = new System.Drawing.Size(269, 51);
            this.btnLoadSMS.TabIndex = 0;
            this.btnLoadSMS.Text = "Lấy danh sách";
            this.btnLoadSMS.UseVisualStyleBackColor = true;
            this.btnLoadSMS.Click += new System.EventHandler(this.btnLoadSMS_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnChangeCustomerName);
            this.groupBox3.Controls.Add(this.btnSendCancelCouponInfo);
            this.groupBox3.Location = new System.Drawing.Point(13, 411);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(946, 133);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chức năng khác";
            // 
            // btnSendCancelCouponInfo
            // 
            this.btnSendCancelCouponInfo.Enabled = false;
            this.btnSendCancelCouponInfo.Location = new System.Drawing.Point(6, 37);
            this.btnSendCancelCouponInfo.Name = "btnSendCancelCouponInfo";
            this.btnSendCancelCouponInfo.Size = new System.Drawing.Size(355, 51);
            this.btnSendCancelCouponInfo.TabIndex = 1;
            this.btnSendCancelCouponInfo.Text = "Gửi thông báo hủy mã KM";
            this.btnSendCancelCouponInfo.UseVisualStyleBackColor = true;
            this.btnSendCancelCouponInfo.Click += new System.EventHandler(this.btnSendCancelCouponInfo_Click);
            // 
            // btnChangeCustomerName
            // 
            this.btnChangeCustomerName.Location = new System.Drawing.Point(367, 37);
            this.btnChangeCustomerName.Name = "btnChangeCustomerName";
            this.btnChangeCustomerName.Size = new System.Drawing.Size(467, 51);
            this.btnChangeCustomerName.TabIndex = 2;
            this.btnChangeCustomerName.Text = "Sửa tên khách hàng theo danh sách";
            this.btnChangeCustomerName.UseVisualStyleBackColor = true;
            this.btnChangeCustomerName.Click += new System.EventHandler(this.btnChangeCustomerName_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 556);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "frmMain";
            this.Text = "NissanCoupon";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbTotalCoupon;
        private System.Windows.Forms.Label lbCurrentOtpCount;
        private System.Windows.Forms.Label lbServicesState;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Timer SystemInfoTick;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtSMSFromDate;
        private System.Windows.Forms.Button btnLoadSMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtSMSToDate;
        private System.Windows.Forms.Button btnResendSMS;
        private System.Windows.Forms.TextBox txtFindSMSResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSendCancelCouponInfo;
        private System.Windows.Forms.Button btnChangeCustomerName;
    }
}

