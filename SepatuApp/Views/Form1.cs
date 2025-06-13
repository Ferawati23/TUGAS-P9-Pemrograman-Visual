using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SepatuApp.Controllers;
using SepatuApp.Models;

namespace SepatuApp
{
    public partial class Form1 : Form
    {
        private readonly SepatuController controller = new SepatuController();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Tambahkan kolom ke DataGridView
            dgvSepatu.Columns.Add("Merk", "Merk");
            dgvSepatu.Columns.Add("Warna", "Warna");
            dgvSepatu.Columns.Add("Ukuran", "Ukuran");
            dgvSepatu.Columns.Add("Stok", "Stok");

            try
            {
                using (var conn = new Database().GetConnection())
                {
                    conn.Open();
                    MessageBox.Show("Koneksi berhasil ke database.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal terhubung ke database: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            dgvSepatu.Rows.Clear();
            List<Sepatu> sepatuList = controller.GetAll();

            foreach (var s in sepatuList)
            {
                dgvSepatu.Rows.Add(s.Merk, s.Warna, s.Ukuran, s.Stok);
            }
        }

        private void ClearForm()
        {
            txtMerk.Text = "";
            txtWarna.Text = "";
            txtUkuran.Text = "";
            txtStok.Text = "";
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMerk.Text) ||
                string.IsNullOrWhiteSpace(txtWarna.Text) ||
                string.IsNullOrWhiteSpace(txtUkuran.Text) ||
                string.IsNullOrWhiteSpace(txtStok.Text))
            {
                MessageBox.Show("Harap lengkapi semua data!");
                return;
            }

            try
            {
                var sepatu = new Sepatu
                {
                    Merk = txtMerk.Text,
                    Warna = txtWarna.Text,
                    Ukuran = txtUkuran.Text,
                    Stok = int.Parse(txtStok.Text)
                };

                controller.Add(sepatu);
                LoadData();
                ClearForm();
            }
            catch (FormatException)
            {
                MessageBox.Show("Stok harus berupa angka.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSepatu.CurrentRow == null || dgvSepatu.CurrentRow.Index < 0)
            {
                MessageBox.Show("Pilih baris yang ingin diedit.");
                return;
            }

            try
            {
                string oldMerk = dgvSepatu.CurrentRow.Cells[0].Value.ToString();

                var sepatu = new Sepatu
                {
                    Merk = txtMerk.Text,
                    Warna = txtWarna.Text,
                    Ukuran = txtUkuran.Text,
                    Stok = int.Parse(txtStok.Text)
                };

                controller.Update(oldMerk, sepatu);
                LoadData();
                ClearForm();
            }
            catch (FormatException)
            {
                MessageBox.Show("Stok harus berupa angka.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvSepatu.CurrentRow == null || dgvSepatu.CurrentRow.Index < 0)
            {
                MessageBox.Show("Pilih baris yang ingin dihapus.");
                return;
            }

            try
            {
                string merkToDelete = dgvSepatu.CurrentRow.Cells[0].Value.ToString();
                controller.Delete(merkToDelete);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void dgvSepatu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMerk.Text = dgvSepatu.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtWarna.Text = dgvSepatu.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtUkuran.Text = dgvSepatu.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtStok.Text = dgvSepatu.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
