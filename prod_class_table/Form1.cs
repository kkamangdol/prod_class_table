using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 데이터 조회시 끊김 줄이기
using System.Reflection;

using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web;

namespace prod_class_table
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


     // ORACLEL DB TABLE 연결
        private DataTable GetData()
        {
            string strConn = "Data Source = DEV_DDF; User ID = DDDB; Password = dddb";
            OracleConnection conn = new OracleConnection(strConn);
            OracleDataAdapter adapter = new OracleDataAdapter("select prod_code, prod_name, prod_abbr, prod_eng, prod_type, prod_class3, prod_class2, prod_str_dt, prod_end_dt, unit_price from smcode01", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


         // DataTable dt2 = dt.DefaultView.ToTable(false, new string[] { "PROD_CODE", "PROD_NAME", "PROD_ABBR", "PROD_ENG", "PROD_TYPE", "PROD_CLASS3", "PROD_CLASS2", "PROD_STR_DT", "PROD_END_DT", "UNIT_PRICE" });

            return dt;
        }


        // 조회버튼
        private void button1_Click(object sender, EventArgs e)
        {

         //Waitting
            this.Cursor = Cursors.WaitCursor;

            DataTable dt = GetData();

            dataGridView1.DataSource = dt;

         // 컬럼명 변경
           this.dataGridView1.Columns[0].HeaderText = "제품코드";
            this.dataGridView1.Columns[1].HeaderText = "제품명";
            this.dataGridView1.Columns[2].HeaderText = "제품약어명";
            this.dataGridView1.Columns[3].HeaderText = "제품영문명";
            this.dataGridView1.Columns[4].HeaderText = "제품구분";
            this.dataGridView1.Columns[5].HeaderText = "제품소분류";
            this.dataGridView1.Columns[6].HeaderText = "제품중분류";
            this.dataGridView1.Columns[7].HeaderText = "제품생산개시일";
            this.dataGridView1.Columns[8].HeaderText = "제품생산종료일";
            this.dataGridView1.Columns[9].HeaderText = "기준단가";

         // 테이블 컬럼숨기기
         // this.dataGridView1.Columns[1].Visible = false;

         // 데이터 조회시 끊김 줄이기
            dataGridView1.DoubleBuffered(true);

         // 행 색변경
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;

         // 여러 행 선택 방지
            dataGridView1.MultiSelect = false;

         // 전체 컬럼의 Sorting 기능 차단
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

         // 자동으로 선택되는 셀 해제
            dataGridView1.CurrentCell = null;

         //원래대로
            this.Cursor = Cursors.Default;



            // 메세지박스
            // MessageBox.Show("조회가 완료되었습니다!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 검색하기
        private DataTable SearchData()
        {
            string strConn = "Data Source = DEV_DDF; User ID = DDDB; Password = dddb";
            OracleConnection conn = new OracleConnection(strConn);
            OracleDataAdapter adapter = new OracleDataAdapter("select PROD_CODE, PROD_NAME, PROD_ABBR, PROD_ENG, PROD_TYPE, PROD_CLASS3, PROD_CLASS2, PROD_STR_DT, PROD_END_DT, UNIT_PRICE  from SMCODE01 where prod_name like '%" + textBox1.Text + "%' ", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }


        // 1. 버튼으로 검색하기
        private void search_button_Click(object sender, EventArgs e)
        {
            // 마우스 커서 Waitting
            this.Cursor = Cursors.WaitCursor;


            DataTable dt = SearchData();

            dataGridView1.DataSource = dt;

            // 컬럼명 변경
            this.dataGridView1.Columns[0].HeaderText = "제품코드";
            this.dataGridView1.Columns[1].HeaderText = "제품명";
            this.dataGridView1.Columns[2].HeaderText = "제품약어명";
            this.dataGridView1.Columns[3].HeaderText = "제품영문명";
            this.dataGridView1.Columns[4].HeaderText = "제품구분";
            this.dataGridView1.Columns[5].HeaderText = "제품소분류";
            this.dataGridView1.Columns[6].HeaderText = "제품중분류";
            this.dataGridView1.Columns[7].HeaderText = "제품생산개시일";
            this.dataGridView1.Columns[8].HeaderText = "제품생산종료일";
            this.dataGridView1.Columns[9].HeaderText = "기준단가";

            // 데이터 조회시 끊김 줄이기
            dataGridView1.DoubleBuffered(true);

            // 행 색변경
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;

            // 여러 행 선택 방지
            dataGridView1.MultiSelect = false;

            // 전체 컬럼의 Sorting 기능 차단
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // 자동으로 선택되는 셀 해제
            dataGridView1.CurrentCell = null;

            //  조회한 내용에서 버튼으로 검색하기(기존)
            //   (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            //       String.Format("PROD_NAME like '%" + textBox1.Text + "%'");

            // 메세지박스
            //  MessageBox.Show("검색이 완료되었습니다!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // 마우스 커서 원래대로
            this.Cursor = Cursors.Default;
        }


     // 2. 엔터로 검색하기
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          // 마우스 커서 Waitting
            this.Cursor = Cursors.WaitCursor;

            DataTable dt = SearchData();

            dataGridView1.DataSource = dt;

            // 컬럼명 변경
            this.dataGridView1.Columns[0].HeaderText = "제품코드";
            this.dataGridView1.Columns[1].HeaderText = "제품명";
            this.dataGridView1.Columns[2].HeaderText = "제품약어명";
            this.dataGridView1.Columns[3].HeaderText = "제품영문명";
            this.dataGridView1.Columns[4].HeaderText = "제품구분";
            this.dataGridView1.Columns[5].HeaderText = "제품소분류";
            this.dataGridView1.Columns[6].HeaderText = "제품중분류";
            this.dataGridView1.Columns[7].HeaderText = "제품생산개시일";
            this.dataGridView1.Columns[8].HeaderText = "제품생산종료일";
            this.dataGridView1.Columns[9].HeaderText = "기준단가";

            // 데이터 조회시 끊김 줄이기
            dataGridView1.DoubleBuffered(true);

            // 행 색변경
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;

            // 여러 행 선택 방지
            dataGridView1.MultiSelect = false;

            // 전체 컬럼의 Sorting 기능 차단
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // 자동으로 선택되는 셀 해제
            dataGridView1.CurrentCell = null;

            // 조회한 내용에서 엔터로 검색하기(기존)
            /*            if (e.KeyChar == (char)13)
                        {
                            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                            String.Format("PROD_NAME like '%" + textBox1.Text + "%'");

                         // 메세지박스
                         // MessageBox.Show("검색이 완료되었습니다!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }*/

            // 마우스 커서 원래대로
            this.Cursor = Cursors.Default;
        }

        // 데이터 뽑아내기      
        // 1. 셀 방향키 선택
        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            textprodcode.Text = row.Cells[0].Value.ToString();
            textprodname.Text = row.Cells[1].Value.ToString();
            textprodstrdt.Text = row.Cells[7].Value.ToString();
            textprodenddt.Text = row.Cells[8].Value.ToString();
            textunitprice.Text = row.Cells[9].Value.ToString();
        }

        // 2. 셀 마우스 선택
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            textprodcode.Text = row.Cells[0].Value.ToString();
            textprodname.Text = row.Cells[1].Value.ToString();
            textprodstrdt.Text = row.Cells[7].Value.ToString();
            textprodenddt.Text = row.Cells[8].Value.ToString();
            textunitprice.Text = row.Cells[9].Value.ToString();
        }
    }


    // 데이터 조회시 끊김 줄이기
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
