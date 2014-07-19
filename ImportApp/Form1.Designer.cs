namespace ImportApp
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ClearCompany = new System.Windows.Forms.Button();
            this.CompanyList1 = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SelectTextBox1 = new System.Windows.Forms.TextBox();
            this.SelectCompany = new System.Windows.Forms.Button();
            this.ImportCompany = new System.Windows.Forms.Button();
            this.Msg1 = new System.Windows.Forms.TextBox();
            this.Msg2 = new System.Windows.Forms.TextBox();
            this.ImportProduct = new System.Windows.Forms.Button();
            this.SelectProduct = new System.Windows.Forms.Button();
            this.SelectTextBox2 = new System.Windows.Forms.TextBox();
            this.CompanyList2 = new System.Windows.Forms.ComboBox();
            this.ClearProduct = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(608, 429);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Msg1);
            this.tabPage1.Controls.Add(this.ImportCompany);
            this.tabPage1.Controls.Add(this.SelectCompany);
            this.tabPage1.Controls.Add(this.SelectTextBox1);
            this.tabPage1.Controls.Add(this.CompanyList1);
            this.tabPage1.Controls.Add(this.ClearCompany);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(600, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "公司信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Msg2);
            this.tabPage2.Controls.Add(this.ImportProduct);
            this.tabPage2.Controls.Add(this.SelectProduct);
            this.tabPage2.Controls.Add(this.SelectTextBox2);
            this.tabPage2.Controls.Add(this.CompanyList2);
            this.tabPage2.Controls.Add(this.ClearProduct);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(600, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "作品信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ClearCompany
            // 
            this.ClearCompany.Location = new System.Drawing.Point(494, 6);
            this.ClearCompany.Name = "ClearCompany";
            this.ClearCompany.Size = new System.Drawing.Size(100, 20);
            this.ClearCompany.TabIndex = 0;
            this.ClearCompany.Text = "清除公司信息";
            this.ClearCompany.UseVisualStyleBackColor = true;
            // 
            // CompanyList1
            // 
            this.CompanyList1.FormattingEnabled = true;
            this.CompanyList1.Location = new System.Drawing.Point(6, 6);
            this.CompanyList1.Name = "CompanyList1";
            this.CompanyList1.Size = new System.Drawing.Size(482, 20);
            this.CompanyList1.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SelectTextBox1
            // 
            this.SelectTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SelectTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectTextBox1.Location = new System.Drawing.Point(6, 32);
            this.SelectTextBox1.Name = "SelectTextBox1";
            this.SelectTextBox1.ReadOnly = true;
            this.SelectTextBox1.Size = new System.Drawing.Size(356, 21);
            this.SelectTextBox1.TabIndex = 2;
            // 
            // SelectCompany
            // 
            this.SelectCompany.Location = new System.Drawing.Point(368, 32);
            this.SelectCompany.Name = "SelectCompany";
            this.SelectCompany.Size = new System.Drawing.Size(110, 21);
            this.SelectCompany.TabIndex = 3;
            this.SelectCompany.Text = "选择公司信息文件";
            this.SelectCompany.UseVisualStyleBackColor = true;
            // 
            // ImportCompany
            // 
            this.ImportCompany.Location = new System.Drawing.Point(484, 32);
            this.ImportCompany.Name = "ImportCompany";
            this.ImportCompany.Size = new System.Drawing.Size(110, 21);
            this.ImportCompany.TabIndex = 4;
            this.ImportCompany.Text = "导入公司信息文件";
            this.ImportCompany.UseVisualStyleBackColor = true;
            // 
            // Msg1
            // 
            this.Msg1.BackColor = System.Drawing.SystemColors.Info;
            this.Msg1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Msg1.Location = new System.Drawing.Point(7, 59);
            this.Msg1.Multiline = true;
            this.Msg1.Name = "Msg1";
            this.Msg1.ReadOnly = true;
            this.Msg1.Size = new System.Drawing.Size(587, 339);
            this.Msg1.TabIndex = 5;
            // 
            // Msg2
            // 
            this.Msg2.BackColor = System.Drawing.SystemColors.Info;
            this.Msg2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Msg2.Location = new System.Drawing.Point(7, 59);
            this.Msg2.Multiline = true;
            this.Msg2.Name = "Msg2";
            this.Msg2.ReadOnly = true;
            this.Msg2.Size = new System.Drawing.Size(587, 339);
            this.Msg2.TabIndex = 11;
            // 
            // ImportProduct
            // 
            this.ImportProduct.Location = new System.Drawing.Point(484, 32);
            this.ImportProduct.Name = "ImportProduct";
            this.ImportProduct.Size = new System.Drawing.Size(110, 21);
            this.ImportProduct.TabIndex = 10;
            this.ImportProduct.Text = "导入产品信息文件";
            this.ImportProduct.UseVisualStyleBackColor = true;
            // 
            // SelectProduct
            // 
            this.SelectProduct.Location = new System.Drawing.Point(368, 32);
            this.SelectProduct.Name = "SelectProduct";
            this.SelectProduct.Size = new System.Drawing.Size(110, 21);
            this.SelectProduct.TabIndex = 9;
            this.SelectProduct.Text = "选择产品信息文件";
            this.SelectProduct.UseVisualStyleBackColor = true;
            // 
            // SelectTextBox2
            // 
            this.SelectTextBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SelectTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectTextBox2.Location = new System.Drawing.Point(6, 32);
            this.SelectTextBox2.Name = "SelectTextBox2";
            this.SelectTextBox2.ReadOnly = true;
            this.SelectTextBox2.Size = new System.Drawing.Size(356, 21);
            this.SelectTextBox2.TabIndex = 8;
            // 
            // CompanyList2
            // 
            this.CompanyList2.FormattingEnabled = true;
            this.CompanyList2.Location = new System.Drawing.Point(6, 6);
            this.CompanyList2.Name = "CompanyList2";
            this.CompanyList2.Size = new System.Drawing.Size(482, 20);
            this.CompanyList2.TabIndex = 7;
            // 
            // ClearProduct
            // 
            this.ClearProduct.Location = new System.Drawing.Point(494, 6);
            this.ClearProduct.Name = "ClearProduct";
            this.ClearProduct.Size = new System.Drawing.Size(100, 20);
            this.ClearProduct.TabIndex = 6;
            this.ClearProduct.Text = "清除产品信息";
            this.ClearProduct.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(600, 404);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "作品页面";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(600, 404);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "公司页面";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 21);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(600, 404);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "资讯页面";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 21);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(600, 404);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "流程页面";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 21);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(600, 404);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "设计师页面";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "网站信息导入操作";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button SelectCompany;
        private System.Windows.Forms.TextBox SelectTextBox1;
        private System.Windows.Forms.ComboBox CompanyList1;
        private System.Windows.Forms.Button ClearCompany;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox Msg1;
        private System.Windows.Forms.Button ImportCompany;
        private System.Windows.Forms.TextBox Msg2;
        private System.Windows.Forms.Button ImportProduct;
        private System.Windows.Forms.Button SelectProduct;
        private System.Windows.Forms.TextBox SelectTextBox2;
        private System.Windows.Forms.ComboBox CompanyList2;
        private System.Windows.Forms.Button ClearProduct;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;

    }
}

