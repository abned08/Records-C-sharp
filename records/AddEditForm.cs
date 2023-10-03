using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace records
{
    public partial class AddEditForm : Form
    {
        public AddEditForm()
        {
            InitializeComponent();
            cbRecordAE.DataSource = DB.GetData("select * from recordNameTbl");
            cbServiceAE.DataSource = DB.GetData("select * from serviceTbl");
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
            if (recBtnSaveAE.Text == "حفظ")
                ClearAndAuto();
        }

        private void AddOrEdit()
        {
            if (recBtnSaveAE.Text == "حفظ")
            {
                if (cbRecordAE.SelectedItem == null)
                {
                    MessageBox.Show("يجب تحديد المطبوعة");
                    cbRecordAE.Focus();
                }
                else if (txtQuantityAE.Text.Trim() == "")
                {
                    MessageBox.Show("لم تحدد الكمية");
                    txtQuantityAE.Select();
                }
                else if (cbServiceAE.SelectedItem == null)
                {
                    MessageBox.Show("يجب تحديد المصلحة");
                    cbServiceAE.Focus();
                }

                else
                {
                    String msg = "";
                    String inOut = "";
                    if (rbInAE.Checked == true)
                    {
                        inOut += "in";
                    }
                    else
                    {
                        inOut += "out";
                    }
                    String codeIDE = cbRecordAE.SelectedValue.ToString();
                    int serviceID = int.Parse(cbServiceAE.SelectedValue.ToString());
                    String dt = dtpRecordAE.Value.ToString("yyyy/MM/dd");
                    int quantity = int.Parse(txtQuantityAE.Text.Replace("'", "").ToString());
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
                            cbRecordAE.Focus();
                        }
                        else MessageBox.Show(ex.Message);
                    }



                    finally
                    {
                        if (msg != "") MessageBox.Show(msg);

                    }
                }

            }
            else if (recBtnSaveAE.Text == "تعديل")
            {
                if (txtQuantityAE.Text.Trim() == "")
                {
                    MessageBox.Show("لم تحدد الكمية");
                    txtQuantityAE.Select();
                }
                else
                {
                    String msg = "";
                    String inOut = "";
                    if (rbInAE.Checked == true)
                    {
                        inOut += "in";
                    }
                    else
                    {
                        inOut += "out";
                    }
                    String codeID = cbRecordAE.SelectedValue.ToString();
                    int serviceID = int.Parse(cbServiceAE.SelectedValue.ToString());
                    String dt = dtpRecordAE.Value.ToString("yyyy/MM/dd");
                    int quantity = int.Parse(txtQuantityAE.Text.Replace("'", "").ToString());
                    int IDD = Convert.ToInt32(txtIDAE.Text);
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


                        }
                        else MessageBox.Show(ex.Message);
                    }



                    finally
                    {
                        if (msg != "") MessageBox.Show(msg);

                        this.Close();
                    }


                }


            }
        }
        private void ClearAndAuto()
        {

            txtQuantityAE.Text = "";
            rbOutAE.Checked = true;
            dtpRecordAE.Value = DateTime.Today;
            cbRecordAE.SelectedIndex = -1;
            cbServiceAE.SelectedIndex = -1;
            txtIDAE.Text = "";

        }

        private void recBtnSaveAE_Click(object sender, EventArgs e)
        {
            AddOrEdit();
        }

        private void AddEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            Main.getMainForm.tabRecord_Enter(new object(), new EventArgs());
        }

        private void txtQuantityAE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
