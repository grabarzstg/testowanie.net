using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Ticket
    {
        private SqlConnection con;
        private SqlDataReader reader;
        private SqlDataAdapter da;
        SqlCommandBuilder cmdBuilder;
        DataSet TicketDataSet = new DataSet();

        public String from, destination;
        public int id , fromZone, destinationZone;
        public double price;

        public Ticket() { }

        public Ticket(int id)
        {
            this.id = id;
        }

        public Ticket(String from, String destination, int fromZone, int destinationZone, double price)
        {
            this.from = from;
            this.destination = destination;
            this.fromZone = fromZone;
            this.destinationZone = destinationZone;
            this.price = price;
        }

        public void Get(int id)
        {
            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            reader = new SqlCommand("select * from Ticket where ID=" + id, con).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this.id = reader.GetInt32(0); 
                    from = reader.GetString(1);
                    destination = reader.GetString(2);
                    fromZone = reader.GetInt32(3);
                    destinationZone = reader.GetInt32(4);
                    price = reader.GetDouble(5);
                    Console.WriteLine("ID | From (zone): | To:(zone) | Price \n {0}  |   {1} ({3})  |   {2}  ({4})  |  {5}",
                    this.id, from, destination, fromZone, destinationZone, price);
                }
                reader.Close(); con.Close();  
            }
            else
            {
                Console.WriteLine("No rows found.");
                reader.Close(); con.Close();  
                throw new NullReferenceException();
            }
            
        }

        public List<Ticket> GetAll()
        {
            List<Ticket> result = new List<Ticket>();
            Ticket temp;

            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            reader = new SqlCommand("select * from Ticket", con).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    temp = new Ticket(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetDouble(5));
                    temp.id = reader.GetInt32(0);
                    result.Add(temp);
                }
            }
            reader.Close();  con.Close(); 

            return result;
        }

        public void Insert()
        {
            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            da = new SqlDataAdapter("select * from Ticket where ID=" + id, con);
            cmdBuilder = new SqlCommandBuilder(da);
            da.Fill(TicketDataSet, "Ticket");
            DataRow newRow = TicketDataSet.Tables["Ticket"].NewRow();

            newRow[1] = this.from;
            newRow[2] = this.destination;
            newRow[3] = this.fromZone;
            newRow[4] = this.destinationZone;
            newRow[5] = this.price;

            TicketDataSet.Tables["Ticket"].Rows.Add(newRow);
            da.Update(TicketDataSet, "Ticket");
            reader = new SqlCommand("SELECT * FROM Ticket WHERE ID = (SELECT MAX(ID) FROM Ticket)", con).ExecuteReader();
            reader.Read();
            this.id = reader.GetInt32(0); 
            reader.Close();  con.Close();
        }

        public void Remove()
        {
            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            reader = new SqlCommand("delete from Ticket where ID=" + id, con).ExecuteReader();
            reader.Close();  con.Close(); 
        }

        public void RemoveAll()
        {
            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            reader = new SqlCommand("delete from Ticket", con).ExecuteReader();
            reader.Close();  con.Close(); 
        }

        public void Update(Ticket data)
        {
            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            reader = new SqlCommand("UPDATE Ticket SET [From] = '"+ data.from +"',Destination ='"+ data.destination +"',FromZone = '" + data.fromZone +"',DestinationZone = '" + data.destinationZone +"',Price = '" + data.price +"' WHERE ID=" + id, con).ExecuteReader();
            reader.Close();  con.Close(); 
            this.Update(data); 
            
        }
        
        

    }
}
