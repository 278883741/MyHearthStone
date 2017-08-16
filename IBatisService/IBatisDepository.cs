using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;

namespace IBatisService
{
    public class IBatisDepository
    {
        private static ISqlMapper mapper = null;
        public IBatisDepository()
        {
            if(mapper == null)
                mapper = Mapper.Instance();
        }
        //public IList<T> GetList<T>() where T : class
        //{
        //    IList<T> result = mapper.QueryForList<T>("HearthStone.SelectAll", null);
        //    return result;
        //}
        //public IList<T> GetListPage<T>(int startPage,int endPage,int pageSize) where T : class
        //{
        //    int skipResults = (startPage-1) * pageSize;
        //    int maxResults = (endPage - startPage) * pageSize;
        //    IList<T> result = mapper.QueryForList<T>("HearthStone.SelectAllOrderByCreateDate", null, skipResults, maxResults);
        //    return result;
        //}
        //public IList<T> GetListPageByOccupation<T>(int startPage, int endPage, int pageSize, string Occupation) where T : class
        //{
        //    int skipResults = (startPage - 1) * pageSize;
        //    int maxResults = (endPage - startPage) * pageSize;
        //    IList<T> result = mapper.QueryForList<T>("HearthStone.SelectCardsByOccupation", Occupation, skipResults, maxResults);
        //    return result;
        //}
        //public T GetSingelModel<T>(string id) where T : class
        //{
        //    T result = mapper.QueryForObject<T>("HearthStone.SelectSingelById", id);
        //    return result;
        //}
        //public IList<T> GetHotCards<T>() where T : class
        //{
        //    IList<T> result = mapper.QueryForList<T>("HearthStone.SelectHotCards", null);
        //    return result;
        //}
        
        //public IList<T> GetList<T>(Hashtable dic) where T : class
        //{
        //    IList<T> result = mapper.QueryForList<T>("name", dic);
        //    return result;
        //}

        /*---------------------------------------*/
        /// <summary>
        /// 获取各职业卡牌数
        /// </summary>
        /// <returns></returns>
        public DataTable HS_GetOccupationCardCount()
        {
            DataTable table = QueryForDataTable("HearthStone1.SelectOccupationCardCount", null);
            return table;
        }
        /// <summary>
        /// 获取各稀有度牌数
        /// </summary>
        /// <returns></returns>
        public DataTable HS_GetRarityCardCount()
        {
            DataTable table = QueryForDataTable("HearthStone1.SelectRarityCardCount", null);
            return table;
        }
        /// <summary>
        /// 获取各职业代表橙卡
        /// </summary>
        /// <returns></returns>
        public DataTable HS_GetIsShowCard() 
        {
            DataTable table = QueryForDataTable("HearthStone1.SelectIsShowCard", null);
            return table;
        }
        /// <summary>
        /// 获取每个职业展示的6张橙卡
        /// </summary>
        /// <returns></returns>
        public DataTable HS_GetOccupationShowCard()
        {
            DataTable table = QueryForDataTable("HearthStone1.SelectOccupationShowCard", null);
            return table;
        }
        /// <summary>
        /// 获取退环境的经典橙卡
        /// </summary>
        /// <returns></returns>
        public DataTable HS_GetClassicCard()
        {
            DataTable table = QueryForDataTable("HearthStone1.SelectClassicCard", null);
            return table;
        }
        /// <summary>
        /// 获取冰封王座卡牌
        /// </summary>
        /// <returns></returns>
        public DataTable HS_GetFrostThroneCard()
        {
            DataTable table = QueryForDataTable("HearthStone1.SelectFrostThroneCard", null);
            return table;
        }
        //public IList<T> HS_SelectOccupationCard<T>(string occupation) where T : class
        //{
        //    IList<T> result = mapper.QueryForList<T>("HearthStone.SelectOccupationCard", occupation);
        //    return result;
        //}

        public DataTable QueryForDataTable(string statementName, object paramObject)
        {
            DataSet ds = new DataSet();
            bool isSessionLocal = false;
            IDalSession session = mapper.LocalSession;
            if (session == null)
            {
                session = new SqlMapSession(mapper);
                session.OpenConnection();
                isSessionLocal = true;
            }
            IDbCommand cmd = new SqlCommand(GetSql(mapper, statementName, paramObject));
            try
            {
                cmd.Connection = session.Connection;
                IDbDataAdapter adapter = session.CreateDataAdapter(cmd);
                adapter.Fill(ds);
            }
            finally
            {
                if (isSessionLocal)
                    session.CloseConnection();
            }
            return ds.Tables[0];
        }
        public string GetSql(ISqlMapper mapper, string statementName, object paramObject)
        {
            IMappedStatement statement = mapper.GetMappedStatement(statementName);
            if (!mapper.IsSessionStarted)
                mapper.OpenConnection();
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);
            return scope.PreparedStatement.PreparedSql;
        }
    }
}
