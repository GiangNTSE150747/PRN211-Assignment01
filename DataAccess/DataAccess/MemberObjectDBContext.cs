
using BusinessObject;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.DataAccess
{
    public class MemberDBContext : BaseADL
    {
        //Singleton Design Pattern
        private static MemberDBContext instance = null;
        private static readonly object InstanceLock = new object();

        private MemberDBContext() { }
        public static MemberDBContext Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDBContext();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MemberObject> GetMemberList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberID, MemberName, Email, Password, City, Country from Members";
            var members = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    members.Add(new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return members;
        }

        public MemberObject GetMemberByID(int memberID)
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberID, MemberName, Email, Password, City, Country from Members Where MemberID = @MemberID";
            MemberObject member = null;
            try
            {
                var param = dataProvider.CreateParameter("@CarId", 4, memberID, DbType.Int32);
                System.Console.WriteLine("aaaaaaaaaaa");
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    member = new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public void AddNew(MemberObject member)
        {
            try
            {
                MemberObject mem = GetMemberByID(member.MemberID);
                if (mem != null)
                {
                    string SQLInsert = "Insert Members Values @MemberID, @MenberName, @Email, @Password, @City, @Country";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, mem.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@MenberName", 100, mem.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, mem.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 100, mem.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 100, mem.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 100, mem.Country, DbType.String));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("This Member is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(MemberObject member)
        {
            try
            {
                MemberObject mem = GetMemberByID(member.MemberID);
                if (mem != null)
                {
                    string SQLUpdate = "Update Cars set MemberName = @MemberName, Email = @Email, Password = @Password" +
                        ", City = @City, Country = @Country Where MemberId = @MemberId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, mem.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@MenberName", 100, mem.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, mem.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 100, mem.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 100, mem.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 100, mem.Country, DbType.String));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("This car does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }


        //Note: Có thể câu Query này sai
        public void Remove(int memberID)
        {
            try
            {
                MemberObject mem = GetMemberByID(memberID);
                if (mem != null)
                {
                    string SQLDelete = "Delete Members where MemberID = @MemberID";
                    var param = dataProvider.CreateParameter("@MemberID", 4, memberID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("This car does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
