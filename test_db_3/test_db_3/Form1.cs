using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace test_db_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_display_all_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=test_db;User Id=postgres;Password=123321;");
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT m.songname, a.artist_name, m.duration_seconds, m.explicit FROM music_tracks AS m JOIN artists AS a ON m.artist_id = a.artist_id";
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            cmd.Dispose();
            connection.Close();
        }

        private void btn_display_non_explicit_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=test_db;User Id=postgres;Password=123321;");
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT m.songname, a.artist_name, m.duration_seconds FROM music_tracks AS m JOIN artists AS a ON m.artist_id = a.artist_id WHERE m.explicit = FALSE";
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            cmd.Dispose();
            connection.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=test_db;User Id=postgres;Password=123321;");
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT m.songname FROM music_tracks AS m JOIN artists AS a ON m.artist_id = a.artist_id WHERE a.artist_name = '" + textBox1.Text + "'";
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView2.DataSource = dt;
                }
                cmd.Dispose();
                connection.Close();
                textBox1.Text = "";
            }
        }
    }
}
