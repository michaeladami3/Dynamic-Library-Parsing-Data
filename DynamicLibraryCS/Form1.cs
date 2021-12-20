﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using HW5P2Lib;
using Microsoft.FSharp.Collections;
using Microsoft.VisualBasic.FileIO;
using MySql.Data;
using MySql.Data.MySqlClient;
using Type = Google.Protobuf.WellKnownTypes.Type;

namespace Project5
{
    public partial class Form1 : Form
    {
        Microsoft.FSharp.Collections.FSharpList<HW5P2Lib.HW5P2.Article> alldata;
        MySqlConnection conn;
        void establishConnection()
        {
            string connStr = "server=" + textBox3.Text + ";user=" + textBox5.Text + ";database=" + textBox7.Text +
                             ";port=" + textBox4.Text + ";password=" + textBox6.Text;
            conn = new MySqlConnection(connStr); // must be these values when submitting to gradescope
            conn.Open();
        }
        public Form1()
        {
            InitializeComponent();
            // Establish Connection to MySQL Server
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBox2.Text, out _) && textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    string stringInput = textBox2.Text;
                    int input = Int32.Parse(stringInput);
                    textBox8.Text = "Title: ";
                    textBox8.Text += HW5P2Lib.HW5P2.getTitle(input, alldata);
                    textBox8.Text += Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "Either Input Incorrect or Article Not Found";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBox2.Text, out _) && textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    string stringInput = textBox2.Text;
                    int input = Int32.Parse(stringInput);
                    textBox8.Text = "Number of Words in The Article: ";
                    textBox8.Text += HW5P2Lib.HW5P2.wordCount(input, alldata);
                    textBox8.Text += Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                textBox8.Text ="Either input incorrect or Article Not Found";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBox2.Text, out _) && textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    string stringInput = textBox2.Text;
                    int input = Int32.Parse(stringInput);
                    textBox8.Text = "Month of Chosen Article: ";
                    textBox8.Text += HW5P2Lib.HW5P2.getMonthName(input, alldata);
                    textBox8.Text += Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "Either input incorrect or Article Not Found";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    Microsoft.FSharp.Collections.FSharpList<string> publisherNames = HW5P2Lib.HW5P2.publishers(alldata);
                    textBox8.Text = "Unique Publishers: " + Environment.NewLine;
                    textBox8.Text += String.Join(Environment.NewLine, publisherNames);
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "CSV not found";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    Microsoft.FSharp.Collections.FSharpList<string> countryNames = HW5P2Lib.HW5P2.countries(alldata);
                    textBox8.Text = "Unique Countries: " + Environment.NewLine;
                    textBox8.Text += String.Join(Environment.NewLine, countryNames);
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "CSV not found";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    double overallguard = HW5P2Lib.HW5P2.avgNewsguardscoreForArticles(alldata);
                    textBox8.Text = "Average News Guard Score for All Articles: ";
                    textBox8.Text += overallguard;
                    textBox8.Text += Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "CSV not found";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    Microsoft.FSharp.Collections.FSharpList<Tuple<string, int>> nArticles =
                        HW5P2Lib.HW5P2.numberOfArticlesEachMonth(alldata);
                    textBox8.Text = "Number of Articles for Each Month:" + Environment.NewLine;
                    string output = HW5P2Lib.HW5P2.buildHistogram(nArticles, alldata.Length, "");
                    output = output.Replace("/n", Environment.NewLine);
                    textBox8.Text += output;
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "CSV not found";
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> reliablepct =
                        HW5P2Lib.HW5P2.reliableArticlePercentEachPublisher(alldata);
                    Microsoft.FSharp.Collections.FSharpList<string> lines1 =
                        HW5P2Lib.HW5P2.printNamesAndPercentages(reliablepct);
                    textBox8.Text = "Percentage of Articles That Are Reliable for Each Publisher: " +
                                    Environment.NewLine;
                    foreach (string line in lines1)
                    {
                        textBox8.Text += line;
                        textBox8.Text += Environment.NewLine;
                    }

                    textBox8.Text += Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "CSV not found";
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    textBox8.Text = "Average News Guard Score for Each Country: " + Environment.NewLine;
                    Microsoft.FSharp.Collections.FSharpList<string> lines1 =
                        HW5P2Lib.HW5P2.printNamesAndPercentages(HW5P2Lib.HW5P2.averageguardscore(alldata));
                    foreach (var line in HW5P2Lib.HW5P2.avgNewsguardscoreEachCountry(alldata, lines1))
                    {
                        textBox8.Text += line.Item1;
                        textBox8.Text += ": ";
                        textBox8.Text += line.Item2.ToString("#.000");
                        textBox8.Text += Environment.NewLine;

                    }
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "CSV not found";
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.EndsWith(".csv"))
                {
                    string filename = textBox1.Text;
                    alldata = HW5P2Lib.HW5P2.readfile(filename);
                    textBox8.Text = "The Average News Guard Score for Each Political Bias Category: " +
                                    Environment.NewLine;
                    string output =
                        HW5P2Lib.HW5P2.buildHistogramFloat(HW5P2Lib.HW5P2.avgNewsguardscoreEachBias(alldata), "");
                    output = output.Replace("/n", Environment.NewLine);
                    textBox8.Text += output;
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = "CSV not found";
                return;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                textBox8.Text = "No Connection Available or Article not found";
                return;
            }
            try
            {
                if (Regex.IsMatch(textBox2.Text, @"\d"))
                {
                    string stringInput = textBox2.Text;
                    int input = Int32.Parse(stringInput);
                    string query = String.Format(@"
                    SELECT title
                    FROM news
                    WHERE news_id = {0};", input);
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    textBox8.Text = String.Format("{0}", reader.GetName(0));
                    textBox8.Text += Environment.NewLine;
                    while (reader.Read())
                    {
                        textBox8.Text += reader[0];
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                textBox8.Text = ex.ToString();
            }


        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                textBox8.Text = "No Connection Available";
                return;
            }
            try
            {
                string query = String.Format(@" 
                        SELECT news_id, LENGTH(body_text) AS length
                        FROM news
                        WHERE LENGTH(body_text)>100
                        ORDER BY news_id;
                    ");
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}\t{1}", reader.GetString(0), reader.GetInt32(1));
                    textBox8.Text += Environment.NewLine;

                }

                reader.Close();
            
            }
            catch (Exception ex)
            {
                textBox8.Text = ex.ToString();
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                textBox8.Text = "No Connection Available";
                return;
            }

            try
            {
                string query = String.Format(@"
                SELECT title, DATE_FORMAT(STR_TO_DATE(publish_date, '%c/%d/%y'), '%M') AS Month
                FROM news
                ORDER BY STR_TO_DATE(publish_date, '%m/%d/%y')");
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}\t{1}", reader.GetString(0), reader.GetString(1));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();
            
            }
            catch (Exception ex)
            {
                textBox8.Text = ex.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                textBox8.Text = "No Connection Available";
                return;
            }
            try
            {
                string query = String.Format(@"SELECT publisher
                FROM publisher_table
                JOIN news
                USING (publisher_id)
                GROUP BY publisher
                ORDER BY publisher;");
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}", reader.GetName(0));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}", reader.GetString(0));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();
            
            }
            catch (Exception ex)
            {
                textBox8.Text=ex.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                textBox8.Text = "No Connection Available";
                return;
            }
            try
            {
                string query = String.Format(@"SELECT country, COUNT(news_id) AS articleCount
                FROM country_table
                LEFT JOIN news
                USING (country_id)
                GROUP BY country
                ORDER BY articleCount DESC;
                ");

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}\t{1}", reader.GetString(0), reader.GetInt32(1));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();
                // Write (copy from queries folder) the query
                // Build a Command which holds the query and the location of the target server
                // Retrieve the results into a DataReader
                // Output the header from the DataReader
                // Loop through the rows of the DataReader to output the values from the DataReader
                // Close the DataReader
            }
            catch (Exception ex)
            {
                textBox8.Text=ex.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                textBox8.Text = "No Connection Available";
                return;
            }

            try
            {
                string query = String.Format(@"
                SELECT ROUND(AVG(news_guard_score),3) AS `Average Score`
                FROM news;"
                );

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}", reader.GetName(0));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}", String.Format("{0:F3}", reader.GetDouble(0)));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();

                // Write (copy from queries folder) the query
                // Build a Command which holds the query and the location of the target server
                // Retrieve the results into a DataReader
                // Output the header from the DataReader
                // Loop through the rows of the DataReader to output the values from the DataReader
                // Close the DataReader
            
            }
            catch (Exception ex)
            {
                textBox8.Text = ex.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                return;
            }

            try
            {
                string query = String.Format(@"
                    SELECT month, numArticles, overall, ROUND(100*numArticles/overall,3) AS percentage
                    FROM
                    (
                    SELECT month, monthnum, COUNT(publish_date) AS numArticles, overallCount AS overall
                    FROM
                    (
                    SELECT DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%M') AS month, 
                           DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%m') AS monthnum,
	                       publish_date
                    FROM news
                    ) AS T1
                    JOIN
                    (
                    SELECT COUNT(*) overallCount FROM news
                    ) AS T2
                    GROUP BY month, monthnum, overallCount
                    ) AS T3
                    ORDER BY monthnum;");

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1),
                    reader.GetName(2), reader.GetName(3));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}\t{1}\t{2}\t{3}", reader.GetString(0), reader.GetInt32(1),
                        reader.GetInt32(2), String.Format("{0:F3}", reader.GetDouble(3)));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();
                // Write (copy from queries folder) the query
                // Build a Command which holds the query and the location of the target server
                // Retrieve the results into a DataReader
                // Output the header from the DataReader
                // Loop through the rows of the DataReader to output the values from the DataReader
                // Close the DataReader
            
            }
            catch (Exception ex)
            {
                textBox8.Text = ex.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                return;
            }
            try
            {
                string query = String.Format(@"SELECT publisher, ROUND(AVG(reliability)*100, 3) AS percentage
                FROM news
                JOIN publisher_table
                USING (publisher_id)
                GROUP BY publisher
                ORDER BY percentage DESC, publisher;");
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}\t{1}", reader.GetString(0),
                        String.Format("{0:F3}", reader.GetDouble(1)));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();
                // Write (copy from queries folder) the query
                // Build a Command which holds the query and the location of the target server
                // Retrieve the results into a DataReader
                // Output the header from the DataReader
                // Loop through the rows of the DataReader to output the values from the DataReader
                // Close the DataReader
            
            }
            catch (Exception ex)
            {
                textBox8.Text=ex.ToString();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                return;
            }

            try
            {
                string query = String.Format(@"
                SELECT country, ROUND(AVG(news_guard_score),3) AS avg_news_score
                FROM news
                JOIN country_table
                USING (country_id)
                GROUP BY country
                ORDER BY AVG(news_guard_score) DESC, country ASC;
                ");
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}\t{1}", reader.GetName(0), reader.GetName(1));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}\t{1}", reader.GetString(0), reader.GetInt32(1));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();
                // Write (copy from queries folder) the query
                // Build a Command which holds the query and the location of the target server
                // Retrieve the results into a DataReader
                // Output the header from the DataReader
                // Loop through the rows of the DataReader to output the values from the DataReader
                // Close the DataReader
            
            }
            catch (Exception ex)
            {
                textBox8.Text = ex.ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish Connection to MySQL Server
                establishConnection();
            }
            catch (Exception ex)
            {
                return;
            }
            try
            {
                string query = String.Format(@"
                SELECT author, political_bias, COUNT(*) AS numArticles
                FROM news
                JOIN news_authors
                USING (news_id)
                JOIN author_table
                USING (author_id)
                JOIN political_bias_table
                USING (political_bias_id)
                GROUP BY author, political_bias
                ORDER BY author, COUNT(*) DESC, political_bias;
                ");
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                textBox8.Text = String.Format("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1),
                    reader.GetName(2));
                textBox8.Text += Environment.NewLine;
                while (reader.Read())
                {
                    textBox8.Text += String.Format("{0}\t{1}\t{2}", reader.GetString(0), reader.GetString(1),
                        reader.GetInt32(2));
                    textBox8.Text += Environment.NewLine;
                }

                reader.Close();
            

                // Write (copy from queries folder) the query
                // Build a Command which holds the query and the location of the target server
                // Retrieve the results into a DataReader
                // Output the header from the DataReader
                // Loop through the rows of the DataReader to output the values from the DataReader
                // Close the DataReader
            }
            catch (Exception ex)
            {
                textBox8.Text=ex.ToString();
            }
        }
    }
}
