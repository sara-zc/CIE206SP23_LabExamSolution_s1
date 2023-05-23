using System.Data;
using System.Data.SqlClient;

namespace CIE206LabExam_trial1.Models
{
    public class DB
    {
        string con = "Data Source=YourServerName;Initial Catalog=LabExam_StudentName_PetDB;Integrated Security=True";

        private object readTablePets()
        {
            SqlConnection conn = new SqlConnection(con);
            //string query = "Select * from Pet";
            string query = "select PetID as PetID, Name as PetName, (2023 - BirthYear) as [age], (Owner.Fname + ' ' + Owner.Lname) as OwnerName, (Vet.Fname + ' ' + Vet.Lname) as VetName \r\nfrom Pet left join Vet on Pet.VetID = Vet.VetId left join Owner on Pet.OwnerID = Owner.OwnerID";
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable rd = new DataTable();

            try
            {
                conn.Open();
                rd.Load(cmd.ExecuteReader());
                return rd;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Pet> getAllPets()
        {
            DataTable dataTable = dataTable = (DataTable)readTablePets();
            List<Pet> pets = new List<Pet>();
            foreach (DataRow dr in dataTable.Rows)
            {
                pets.Add(new Pet
                {
                    id = (int)dr["PetID"],
                    name = dr["PetName"].ToString(),
                    year = dr["age"].ToString(),
                    ownername = dr["OwnerName"] != DBNull.Value ? (string)dr["OwnerName"] : null,
                    vetname = dr["VetName"] != DBNull.Value ? (string)dr["VetName"] : null
                }) ;
            }

            return pets;

        }

        public void AddPet(Pet newpet)
        {
            SqlConnection conn = new SqlConnection(con);
            string proc = "addNewPet";
            SqlCommand cmd = new SqlCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                // determine ID of newly added Pet
                newpet.id = MaxId() + 1;

                // execute stored procedure previously created to add the new pet info
                cmd.Parameters.Add(new SqlParameter("@id", newpet.id));
                cmd.Parameters.Add(new SqlParameter("@name", newpet.name));
                cmd.Parameters.Add(new SqlParameter("@year", newpet.year));
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Pet added successfully!");
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public int MaxId()
        {
            SqlConnection conn = new SqlConnection(con);
            string q = "select max(PetID) from Pet ";
            SqlCommand cmd = new SqlCommand(q, conn);
            int max;
            try
            {
                conn.Open();
                max = (int)cmd.ExecuteScalar();
                return max;
            } catch(SqlException e)

            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
