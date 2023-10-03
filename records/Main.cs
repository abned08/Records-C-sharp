using records.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
namespace records
{
    public partial class Main : Form
    {
        private static Main frm;
        static void frmFormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }
        public static Main getMainForm
        {
            get
            {
                if (frm == null)
                {
                    frm = new Main();
                    frm.FormClosed += new FormClosedEventHandler(frmFormClosed);
                }
                return frm;
            }
        }
        public Main()
        {

            InitializeComponent();
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            if (frm == null)
                frm = this;

        }



        private void UpdateTextPosition()
        {
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            Double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            String tmp = " ";
            Double tmpWidth = 50;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }

            this.Text = tmp + this.Text.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabDoc_Click(object sender, EventArgs e)
        {

        }

        private void ClearAndAuto()
        {

            txtCodeID.Text = "";
            txtrecordName.Text = "";
            txtSearch.Text = "";
            txtserviceID.Text = "";
            txtServiceName.Text = "";
            serDgv.ClearSelection();
            dgvRecordName.ClearSelection();
            txtQuantity.Text = "";
            rbOut.Checked = true;
            dtpRecord.Value = DateTime.Today;
            cbYear.SelectedIndex = cbYear.Items.Count - 1;
            cbFilterRecord.ResetText();
            cbFilterRecord.SelectedIndex = -1;
            cbFilterRecord.SelectedIndex = -1;
            cbService.ResetText();
            cbService.SelectedIndex = -1;
            dtpRecord.CustomFormat = " ";
            dgvRec.ClearSelection();
            dgvRpt.ClearSelection();



        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAndAuto();
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            UpdateTextPosition();
            ClearAndAuto();

        }

        private void tabDoc_Enter(object sender, EventArgs e)
        {
            ClearAndAuto();
            showInDGV();
            btnEdit.Enabled = false;
            dgvRecordName.ClearSelection();
        }

        public void showInDGV()
        {
            DataTable tbl = DB.GetData("select * from recordNameTbl ORDER BY codeID");
            dgvRecordName.DataSource = tbl;
            DataTable serTbl = DB.GetData("select * from serviceTbl ORDER BY serviceID");
            serDgv.DataSource = serTbl;
            DataTable recTbl = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID ORDER BY ID ASC,date");
            dgvRec.DataSource = recTbl;
            String year = dtpRpt.Value.ToString("yyyy/MM/dd");
            DataTable reportDgv = DB.GetData("select rn.recordName as 'التسمية',rn.codeID as 'الرمز',(ISNULL(sum(r.[in]), 0)) - (ISNULL(sum(r.[out]), 0)) as 'الكمية المتوفرة' from recordNameTbl rn left join recordTbl r on rn.codeID = r.codeID and r.date <= '" + year + "' group by rn.codeID, rn.recordName");
            dgvRpt.DataSource = reportDgv;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCodeID.Text.Trim() == "")
            {
                MessageBox.Show("رمز المطبوعة فارغ");
                txtCodeID.Select();
            }
            else if (txtrecordName.Text.Trim() == "")
            {
                MessageBox.Show("اسم المطبوعة فارغ");
                txtrecordName.Select();
            }
            else
            {
                String msg = "";
                try
                {
                    DB.Run("insert into recordNameTbl values ('" + txtCodeID.Text.Replace("'", "") + "','" + txtrecordName.Text.Replace("'", "") + "')");
                    msg += "تم إضافة المطبوعة بنجاح";
                    ClearAndAuto();

                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("هذا الرمز موجود مسبقا");
                        txtCodeID.Select();
                        txtCodeID.SelectAll();

                    }
                    else MessageBox.Show(ex.Message);
                }



                finally
                {
                    if (msg != "") MessageBox.Show(msg);
                    showInDGV();

                }
            }
        }

        private void dgvRecordName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = false;
            btnEdit.Enabled = true;
            txtCodeID.Text = dgvRecordName.CurrentRow.Cells[0].Value.ToString();
            txtrecordName.Text = dgvRecordName.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            String msg = "";
            try
            {
                DB.Run("update recordNameTbl set codeID ='" + txtCodeID.Text.Replace("'", "") + "', recordName='" + txtrecordName.Text.Replace("'", "") + "' where codeID='" + txtCodeID.Text.Replace("'", "") + "'");
                msg += "تم تعديل المطبوعة بنجاح";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("هذا الرمز موجود مسبقا");
                else MessageBox.Show(ex.Message);
            }



            finally
            {
                if (msg != "") MessageBox.Show(msg);
                ClearAndAuto();
                showInDGV();

            }
        }

        private void dgvRecordName_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dgvRecordName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && txtrecordName.Text != "")
            {
                if (MessageBox.Show("هل تريد فعلا حذف المطبوعة", "حذف المطبوعة", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DB.Run("delete from recordNameTbl where codeID='" + txtCodeID.Text.Replace("'", "") + "'");
                    showInDGV();
                    ClearAndAuto();
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("لم يتم تحديد أي مطبوعة");
            }
        }

        private void pbSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                DataTable tbl = DB.GetData("select * from recordNameTbl where codeID like '%" + txtSearch.Text.Replace("'", "") + "%' or recordName like '%" + txtSearch.Text.Replace("'", "") + "%'");
                dgvRecordName.DataSource = tbl;
            }
            else
            {
                showInDGV();
            }

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            pbSearch_Click(new object(), new EventArgs());
        }

        private void serBtnSave_Click(object sender, EventArgs e)
        {

            if (txtServiceName.Text.Trim() == "")
            {
                MessageBox.Show("اسم المصلحة فارغ");
                txtServiceName.Select();
            }

            else
            {
                String msg = "";
                try
                {
                    DB.Run("insert into serviceTbl values ('" + txtServiceName.Text.Replace("'", "") + "') ");
                    msg += "تم إضافة المصلحة بنجاح";
                    txtServiceName.Select();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) MessageBox.Show("هذه المصلحة موجودة مسبقا ");
                    else MessageBox.Show(ex.Message);
                }

                finally
                {
                    if (msg != "") MessageBox.Show(msg);
                    ClearAndAuto();
                    showInDGV();

                }
            }


        }

        private void serDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            serBtnSave.Enabled = false;
            txtserviceID.Text = serDgv.CurrentRow.Cells[0].Value.ToString();
            txtServiceName.Text = serDgv.CurrentRow.Cells[1].Value.ToString();
            serBtnEdit.Enabled = true;

        }

        private void serBtnEdit_Click(object sender, EventArgs e)
        {
            if (txtServiceName.Text.Trim() == "")
            {
                MessageBox.Show("اسم المصلحة فارغ");
                txtServiceName.Select();
            }

            else
            {
                String msg = "";
                try
                {
                    DB.Run("update serviceTbl set serviceName= '" + txtServiceName.Text.Replace("'", "") + "' where serviceID='" + txtserviceID.Text.Replace("'", "") + "'");
                    msg += "تم تعديل المصلحة بنجاح";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    if (msg != "") MessageBox.Show(msg);
                    txtserviceID.Text = "";
                    ClearAndAuto();
                    serBtnEdit.Enabled = false;
                    serBtnNew.Enabled = false;
                    serBtnSave.Enabled = true;
                    showInDGV();

                }
            }
        }

        private void serBtnNew_Click(object sender, EventArgs e)
        {
            serBtnSave.Enabled = true;
            serBtnEdit.Enabled = false;
            txtserviceID.Text = "";
            ClearAndAuto();
        }

        private void tabControl_Enter(object sender, EventArgs e)
        {
        }

        private void tabService_Enter(object sender, EventArgs e)
        {
            ClearAndAuto();
            showInDGV();
            serBtnEdit.Enabled = false;
            serDgv.ClearSelection();
        }

        private void serDgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && txtServiceName.Text != "")
            {

                if (MessageBox.Show("هل تريد فعلا حذف المصلحة", "حذف المصلحة", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DB.Run("delete from serviceTbl where serviceID='" + txtserviceID.Text.Replace("'", "") + "'");
                    showInDGV();
                    ClearAndAuto();
                    serBtnEdit.Enabled = false;
                    serBtnSave.Enabled = true;
                }


            }
            else
            {
                MessageBox.Show("لم يتم تحديد أي مصلحة");
            }
        }

        private void serPbSearch_Click(object sender, EventArgs e)
        {
            if (serTxtSearch.Text != "")
            {
                DataTable tbls = DB.GetData("select * from serviceTbl where serviceName like '%" + serTxtSearch.Text.Replace("'", "") + "%'");
                serDgv.DataSource = tbls;
            }
            else
            {
                showInDGV();
            }
        }

        private void serTxtSearch_TextChanged(object sender, EventArgs e)
        {
            serPbSearch_Click(new object(), new EventArgs());
        }

        public void tabRecord_Enter(object sender, EventArgs e)
        {
            DataTable years = DB.GetData("WITH cte AS (SELECT id, year(date) as years, ROW_NUMBER() OVER (PARTITION BY year(date) ORDER BY id DESC) AS rn FROM recordTbl) SELECT * FROM cte WHERE rn = 1");
            cbYear.DataSource = years;
            cbYear.DisplayMember = "years";
            cbYear.ValueMember = "id";

            //DataTable lastYearSelected = DB.GetData("select max(year(date)) from recordTbl");


            recBtnEdit.Enabled = false;

            DataTable tblr = DB.GetData("select recordName as 'rn' ,codeID from recordNameTbl");
            cbFilterRecord.DataSource = tblr;
            cbFilterRecord.DisplayMember = "rn";
            cbFilterRecord.ValueMember = "codeID";


            DataTable tblFilterRec = DB.GetData("select * from recordNameTbl");
            cbFilterRecord.DataSource = tblFilterRec;
            cbFilterRecord.DisplayMember = "recordName";
            cbFilterRecord.ValueMember = "codeID";

            DataTable tbl3 = DB.GetData("select * from serviceTbl");
            cbService.DataSource = tbl3;
            showInDGV();
            dgvRec.ClearSelection();
            ClearAndAuto();


        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void recBtnSave_Click(object sender, EventArgs e)
        {

            if (cbFilterRecord.SelectedItem == null)
            {
                MessageBox.Show("يجب تحديد المطبوعة");
                cbFilterRecord.Focus();
            }
            else if (txtQuantity.Text.Trim() == "")
            {
                MessageBox.Show("لم تحدد الكمية");
                txtQuantity.Select();
            }
            else if (cbService.SelectedItem == null)
            {
                MessageBox.Show("يجب تحديد المصلحة");
                cbService.Focus();
            }

            else
            {
                String msg = "";
                String inOut = "";
                if (rbIn.Checked == true)
                {
                    inOut += "in";
                }
                else
                {
                    inOut += "out";
                }
                String codeIDE = cbFilterRecord.SelectedValue.ToString();
                int serviceID = int.Parse(cbService.SelectedValue.ToString());
                String dt = dtpRecord.Value.ToString("yyyy/MM/dd");
                int quantity = int.Parse(txtQuantity.Text.Replace("'", "").ToString());
                try
                {
                    if (inOut == "in")
                    {
                        DB.Run("insert into recordTbl values ('" + codeIDE + "','" + dt + "'," + quantity + ", NULL," + serviceID + ")");
                    }
                    else
                    {
                        DB.Run("insert into recordTbl values ('" + codeIDE + "','" + dt + "',NULL ," + quantity + "," + serviceID + ")");
                    }
                    msg += "تم إضافة التسجيل بنجاح";
                    ClearAndAuto();

                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("هذا التسجيل موجود مسبقا");
                        txtCodeID.Select();
                        txtCodeID.SelectAll();

                    }
                    else MessageBox.Show(ex.Message);
                }



                finally
                {
                    if (msg != "") MessageBox.Show(msg);
                    showInDGV();

                }
            }
        }

        private void recBtnEdit_Click(object sender, EventArgs e)
        {

            String msg = "";
            String inOut = "";
            if (rbIn.Checked == true)
            {
                inOut += "in";
            }
            else
            {
                inOut += "out";
            }
            String codeID = cbFilterRecord.SelectedValue.ToString();
            int serviceID = int.Parse(cbService.SelectedValue.ToString());
            DateTime dt = dtpRecord.Value;
            int quantity = int.Parse(txtQuantity.Text.Replace("'", "").ToString());
            int IDD = Convert.ToInt32(dgvRec.CurrentRow.Cells[0].Value.ToString());
            try
            {
                if (inOut == "in")
                {
                    DB.Run("update recordTbl set codeID='" + codeID + "', date='" + dt + "',[in]=" + quantity + ", out= NULL, serviceID=" + serviceID + " where ID=" + IDD + " ");
                }
                else
                {
                    DB.Run("update recordTbl set codeID='" + codeID + "',date='" + dt + "', [in]= NULL, out= " + quantity + ", serviceID=" + serviceID + "  where ID=" + IDD + "");
                }
                msg += "تم تعديل التسجيل بنجاح";
                ClearAndAuto();

            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("هذا التسجيل موجود مسبقا");
                    txtCodeID.Select();
                    txtCodeID.SelectAll();

                }
                else MessageBox.Show(ex.Message);
            }



            finally
            {
                if (msg != "") MessageBox.Show(msg);
                showInDGV();

            }
        }

        private void dgvRec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbYear.SelectedItem = null;
            recBtnSave.Enabled = false;
            recBtnEdit.Enabled = true;
            //txttest.Text = dtpRecord.Value.Year.ToString().Trim();
            /*if (dgvRec.CurrentRow.Cells[2].Value.ToString() != "")
            {
                txtQuantity.Text = dgvRec.CurrentRow.Cells[2].Value.ToString();
                rbIn.Checked = true;
            }
            else if(dgvRec.CurrentRow.Cells[3].Value.ToString() != "")
            {
                txtQuantity.Text = dgvRec.CurrentRow.Cells[3].Value.ToString();
                rbOut.Checked = true;
            }
            */
            //dtpRecord.Value = Convert.ToDateTime(dgvRec.CurrentRow.Cells[1].Value.ToString());
            //String year = dtpRecord.Value.Year.ToString();
            //cbYear.SelectedIndex = cbYear.FindStringExact(year);


            String recordName = dgvRec.CurrentRow.Cells[7].Value.ToString();
            label3.Text = recordName;
            String service = dgvRec.CurrentRow.Cells[4].Value.ToString();
            label7.Text = service;
        }

        private void txtIndex_TextChanged(object sender, EventArgs e)
        {

        }

        private void recBtnNew_Click(object sender, EventArgs e)
        {
            recBtnEdit.Enabled = false;
            recBtnSave.Enabled = true;
            ClearAndAuto();
            AddEditForm addFrm = new AddEditForm();
            addFrm.recBtnSaveAE.Text = "حفظ";
            addFrm.ShowDialog();
        }

        private void dgvRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("هل تريد فعلا حذف التسجيل", "حذف التسجيل", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int IDD = Convert.ToInt32(dgvRec.CurrentRow.Cells[0].Value.ToString());
                    DB.Run("delete from recordTbl where ID=" + IDD + "");
                    showInDGV();
                    ClearAndAuto();
                    recBtnEdit.Enabled = false;
                    recBtnSave.Enabled = true;
                }
            }
            else
            {

            }
        }

        private void recTxtSearch_TextChanged(object sender, EventArgs e)
        {


            if (recTxtSearch.Text != "")
            {
                cbYear.SelectedIndex = -1;
                cbFilterRecord.SelectedIndex = -1;
                cbService.SelectedIndex = -1;
                if (recTxtSearch.Text == "")
                {
                    showInDGV();
                }
                String year = cbYear.GetItemText(cbYear.SelectedItem);
                String recFilter = cbFilterRecord.GetItemText(cbFilterRecord.SelectedItem);
                String serFilterr = cbService.GetItemText(cbService.SelectedItem);

                DataTable rectbl1 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%') ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl1;
            }
            else if (cbYear.SelectedItem != null && cbFilterRecord.SelectedItem != null && cbService.SelectedItem != null)
            {
                String year = cbYear.GetItemText(cbYear.SelectedItem);
                String recFilter = cbFilterRecord.GetItemText(cbFilterRecord.SelectedItem);
                String serFilterr = cbService.GetItemText(cbService.SelectedItem);

                DataTable rectbl1 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%') and year(r.date) like '%" + year + "%' and rn.recordName='" + recFilter + "' and s.serviceName like '%" + serFilterr + "%' ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl1;
            }
            else if (cbYear.SelectedItem != null && cbFilterRecord.SelectedItem == null && cbService.SelectedItem == null)
            {
                String year = cbYear.GetItemText(cbYear.SelectedItem);
                DataTable rectbl2 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%') and year(r.date) like '%" + year + "%' ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl2;
            }
            else if (cbYear.SelectedItem == null && cbFilterRecord.SelectedItem != null && cbService.SelectedItem == null)
            {
                String recFilter = cbFilterRecord.GetItemText(cbFilterRecord.SelectedItem);
                DataTable rectbl3 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%') and rn.recordName='" + recFilter + "' ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl3;
            }

            else if (cbYear.SelectedItem != null && cbFilterRecord.SelectedItem != null && cbService.SelectedItem == null)
            {
                String recFilter = cbFilterRecord.GetItemText(cbFilterRecord.SelectedItem);
                String year = cbYear.GetItemText(cbYear.SelectedItem);
                DataTable rectbl4 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%') and rn.recordName='" + recFilter + "' and year(r.date) like '%" + year + "%' ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl4;
            }



            else if (cbYear.SelectedItem == null && cbFilterRecord.SelectedItem == null && cbService.SelectedItem != null)
            {

                String serFilter = cbService.GetItemText(cbService.SelectedItem);

                DataTable rectbl5 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%') and s.serviceName like '%" + serFilter + "%' ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl5;
            }

            else if (cbYear.SelectedItem != null && cbFilterRecord.SelectedItem == null && cbService.SelectedItem != null)
            {
                String year = cbYear.GetItemText(cbYear.SelectedItem);
                String serFilter1 = cbService.GetItemText(cbService.SelectedItem);
                DataTable rectbl6 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%') and year(r.date) like '%" + year + "%' and s.serviceName like '%" + serFilter1 + "%' ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl6;
            }

            else if (cbYear.SelectedItem == null && cbFilterRecord.SelectedItem != null && cbService.SelectedItem != null)
            {
                String serFilter2 = cbService.GetItemText(cbService.SelectedItem);
                String recFilter = cbFilterRecord.GetItemText(cbFilterRecord.SelectedItem);
                DataTable rectbl7 = DB.GetData("SELECT r.ID, r.date,r.[in],r.out,s.serviceName,r.codeID,r.serviceID, rn.recordName FROM recordTbl r INNER JOIN serviceTbl s ON r.serviceID=s.serviceID INNER JOIN recordNameTbl rn ON r.codeID=rn.codeID where (r.[in] like '%" + recTxtSearch.Text.Replace("'", "") + "%' or r.out like '%" + recTxtSearch.Text.Replace("'", "") + "%' or s.serviceName like '%" + recTxtSearch.Text.Replace("'", "") + "%')  and s.serviceName like '%" + serFilter2 + "%' and rn.recordName='" + recFilter + "' ORDER BY ID ASC,date");
                dgvRec.DataSource = rectbl7;
            }
            else
            {
                return;
            }
        }

        private void cbYear_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            recTxtSearch_TextChanged(new object(), new EventArgs());
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ClearAndAuto();
            showInDGV();
            cbYear.SelectedIndex = -1;
        }

        private void dtpRecord_ValueChanged(object sender, EventArgs e)
        {
            dtpRecord.CustomFormat = "dd/MM/yyyy";
        }

        private void Main_Shown(object sender, EventArgs e)
        {
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            ClearAndAuto();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            String strQuery;
            DB.Run("delete from rptTable ");
            for (int i = 0; i < dgvRpt.Rows.Count; i++)
            {
                strQuery = @"insert into rptTable values('" + dgvRpt.Rows[i].Cells["name"].Value + "', '" + dgvRpt.Rows[i].Cells["code"].Value + "','" + dgvRpt.Rows[i].Cells["quantity"].Value + "','" + dgvRpt.Rows[i].Cells["quantityDemanded"].Value + "','" + dgvRpt.Rows[i].Cells["notes"].Value + "'); ";
                DB.Run(strQuery);
            }


            DataSet1 ds;
            DataSet1TableAdapters.doRptTableAdapter doTa;
            DataSet1TableAdapters.prRptTableAdapter prTa;
            DataSet1TableAdapters.cfRptTableAdapter cfTa;
            String dateLast;


            ReportYear myReport;
            myReport = new ReportYear();
            myReport.Load(Application.StartupPath + "\\ReportYear.rpt");
            try
            {
                dateLast = dtpRpt.Value.ToString("dd-MM-yyyy");
                ds = new DataSet1();
                doTa = new DataSet1TableAdapters.doRptTableAdapter();
                prTa = new DataSet1TableAdapters.prRptTableAdapter();
                cfTa = new DataSet1TableAdapters.cfRptTableAdapter();
                doTa.Fill(ds.doRpt, Convert.ToDateTime(dateLast));
                prTa.Fill(ds.prRpt, Convert.ToDateTime(dateLast));
                cfTa.Fill(ds.cfRpt, Convert.ToDateTime(dateLast));
                myReport.SetDataSource(ds);
                myReport.SetParameterValue("@date", Convert.ToDateTime(dateLast));
                RptViewer rptv = new RptViewer();
                rptv.crystalReportViewer1.ReportSource = myReport;
                rptv.ShowDialog();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cbFilterRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            recTxtSearch_TextChanged(new object(), new EventArgs());
        }

        private void tabReport_Click(object sender, EventArgs e)
        {
            ClearAndAuto();
        }

        private void tabReport_Enter(object sender, EventArgs e)
        {
            showInDGV();
        }

        private void dtpRpt_ValueChanged(object sender, EventArgs e)
        {
            String year = dtpRpt.Value.ToString("yyyy/MM/dd");
            DataTable reportDgv = DB.GetData("select rn.recordName as 'التسمية',rn.codeID as 'الرمز',(ISNULL(sum(r.[in]), 0)) - (ISNULL(sum(r.[out]), 0)) as 'الكمية المتوفرة' from recordNameTbl rn left join recordTbl r on rn.codeID = r.codeID and r.date <= '" + year + "' group by rn.codeID, rn.recordName");
            dgvRpt.DataSource = reportDgv;

        }

        private void dtpRpt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String year = dtpRpt.Value.ToString("yyyy/MM/dd");
                DataTable reportDgv = DB.GetData("select rn.recordName as 'التسمية',rn.codeID as 'الرمز',(ISNULL(sum(r.[in]), 0)) - (ISNULL(sum(r.[out]), 0)) as 'الكمية المتوفرة' from recordNameTbl rn left join recordTbl r on rn.codeID = r.codeID and r.date <= '" + year + "' group by rn.codeID, rn.recordName");
                dgvRpt.DataSource = reportDgv;
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtFileName.Text = "";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtFileName.Text != "")
            {
                String fileName = txtFileName.Text + "\\recordsDB" + " - " + DateTime.Now.ToShortDateString().Replace('/', '-') + " - " + DateTime.Now.ToShortTimeString().Replace(':', '-');
                DB.Run("Backup database recordsDB to disk= '" + fileName + ".bak'");
                MessageBox.Show("تم إنشاء النسخة الاحتياطية بنجاح");
                txtFileName.Text = "";
            }
            else
            {
                MessageBox.Show("حدد المسار أولا");
            }

        }

        private void btnBrowseRestor_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileNameRestore.Text = openFileDialog1.FileName;

            }
        }

        private void btnCloseRestore_Click(object sender, EventArgs e)
        {
            txtFileNameRestore.Text = "";
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (txtFileNameRestore.Text == "")
            {
                MessageBox.Show("حدد النسخة الاحتياطية أولا");
            }
            else
            {
                DB.Run("ALTER DATABASE recordsDB set offline with rollback immediate; Restore Database recordsDB from disk= '" + txtFileNameRestore.Text + "'");
                MessageBox.Show("تم استعادة النسخة الاحتياطية بنجاح");
                txtFileNameRestore.Text = "";


            }
        }



        private void tabRecord_Click(object sender, EventArgs e)
        {

        }

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            recTxtSearch_TextChanged(new object(), new EventArgs());
        }

        private void dgvRec_DoubleClick(object sender, EventArgs e)
        {
            AddEditForm aefrm = new AddEditForm();
            aefrm.Text = "تعديل";

            if (dgvRec.CurrentRow.Cells[2].Value.ToString() != "")
            {
                aefrm.txtQuantityAE.Text = dgvRec.CurrentRow.Cells[2].Value.ToString();
                aefrm.rbInAE.Checked = true;
            }
            else if (dgvRec.CurrentRow.Cells[3].Value.ToString() != "")
            {
                aefrm.txtQuantityAE.Text = dgvRec.CurrentRow.Cells[3].Value.ToString();
                aefrm.rbOutAE.Checked = true;
            }

            aefrm.dtpRecordAE.Value = Convert.ToDateTime(dgvRec.CurrentRow.Cells[1].Value.ToString());

            String serviceAE = dgvRec.CurrentRow.Cells[4].Value.ToString();
            aefrm.cbServiceAE.SelectedIndex = cbService.FindStringExact(serviceAE);
            String recordNameAE = dgvRec.CurrentRow.Cells[7].Value.ToString();
            aefrm.cbRecordAE.SelectedIndex = cbFilterRecord.FindString(recordNameAE);
            aefrm.txtIDAE.Text = dgvRec.CurrentRow.Cells[0].Value.ToString();
            aefrm.recBtnSaveAE.Text = "تعديل";
            aefrm.ShowDialog();


        }

        private void dgvRec_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
