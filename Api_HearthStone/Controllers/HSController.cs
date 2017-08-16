using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Web.Http;
using Api_HearthStone.Unity;
using IBatisService;
using Newtonsoft.Json;
using SolrService;
using System.Data;
using Newtonsoft.Json.Linq;
using HS_Model;
using System.Collections;

namespace Api_HearthStone.Controllers
{
    //Install-Package Microsoft.AspNet.WebApi.WebHost  添加Route引用的dll
    //http://www.cnblogs.com/TiestoRay/p/5755454.html -- 路由的相关知识
    [RoutePrefix("HearthStone/HS")]// --这里是路由前缀，下面的方法的RouteAttribute只写后面的部分就可以了
    public class HSController : ApiController
    {
        private IBatisDepository repository = null;
        public HSController() 
        {
            try
            {
                if (repository == null)
                    repository = new IBatisDepository();
            }
            catch (Exception)
            {
 
            }
        }

        /// <summary>
        /// 获取各职业卡牌数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CorsAttribute]
        [Route("GetOccupationCardCount/{key}")]
        public String HS_GetOccupationCardCount()
        {
            DataTable table = repository.HS_GetOccupationCardCount();
            return DataTableToJson(table);
        }

        /// <summary>
        /// 获取各稀有度牌数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CorsAttribute]
        [Route("GetRarityCardCount/{key}")]
        public String HS_GetRarityCardCount()
        {
            DataTable table = repository.HS_GetRarityCardCount();
            return DataTableToJson(table);
        }

        /// <summary>
        /// 获取各职业代表橙卡
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CorsAttribute]
        [Route("GetIsShowCard/{key}")]
        public String HS_GetIsShowCard()
        {
            DataTable table = repository.HS_GetIsShowCard();
            return DataTableToJson(table);
        }

        /// <summary>
        /// 获取每个职业展示的6张橙卡
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CorsAttribute]
        [Route("GetOccupationShowCard/{key}")]
        public String HS_GetOccupationShowCard()
        {
            try
            {
                DataTable table = repository.HS_GetOccupationShowCard();
                return DataTableToJson(table);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取退环境的经典橙卡
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CorsAttribute]
        [Route("GetClassicCard/{key}")]
        public String HS_GetClassicCard()
        {
            try
            {
                DataTable table = repository.HS_GetClassicCard();
                return DataTableToJson(table);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取冰封王座卡牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CorsAttribute]
        [Route("GetFrostThroneCard/{key}")]
        public String HS_GetFrostThroneCard()
        {
            try
            {
                DataTable table = repository.HS_GetFrostThroneCard();
                return DataTableToJson(table);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string DataTableToJson(DataTable table)
        {
            JArray array = new JArray();
            foreach (DataRow row in table.Rows)
            {
                JObject jobject = new JObject();
                foreach (DataColumn item in row.Table.Columns)
                {
                    jobject[item.ColumnName] = row[item.ColumnName].ToString();
                }
                array.Add(jobject);
            }
            return array.ToString();
        }
        /*-------------------------------------------*/

        //[HttpGet]
        //[CorsAttribute]
        //[Route("GetPageData/{pageNum:int}")] 
        //public String GetPageData([FromUri]int pageNum)
        //{
        //    ObjectCache cache = MemoryCache.Default;
        //    IList<Card> result = null;
        //    if (cache[pageNum.ToString()] == null)
        //    {
        //        result = repository.GetListPage<Card>(pageNum, pageNum + 1, 14);
        //        cache.Add(new CacheItem(pageNum.ToString(), result), new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddHours(1) });
        //    }
        //    else
        //        result = cache[pageNum.ToString()] as IList<Card>;
        //    return JsonConvert.SerializeObject(result);
        //}    

        //[HttpGet]
        //[CorsAttribute]
        //[Route("GetSingelById/{id:int}")] 
        //public String GetSingelById([FromUri]string id)
        //{
        //    Card result = repository.GetSingelModel<Card>(id);
        //    return JsonConvert.SerializeObject(result);
        //}

        //[HttpGet]
        //[CorsAttribute]
        //[Route("GetDataByQuery/{key}")] 
        //public string GetDataByQuery([FromUri]string key)
        //{
        //    SolrDepository solr = new SolrDepository();
        //    return solr.Search(key);
        //}

        //// PUT api/values/5
        //[Route("~api/otherbooks")]//这里是不希望使用路由前缀的方法
        //public void GetDataByQuery(int id, [FromBody]string value)
        //{
        //}

        //[Route("date/{*date:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")] // -- /api/books/date/2013/06/17
        //public void Delete(int id)
        //{
        //}

        //[Route("api/books/locale/{lcid:int?}")]
        //public void GetBooksByLocale(int lcid = 1033) 
        //{

        //}

        ////[Route("api/books/{id}", Name = "GetBookById")] // -- 1.这里说的是每个路由都有一个名字，是路由
        //public HttpResponseMessage Test()
        //{
        //    var response = Request.CreateResponse(HttpStatusCode.Created);
        //    string uri = Url.Link("GetBookById", new { id = "5" });//2.然后通过这里来产生url
        //    response.Headers.Location = new Uri(uri);
        //    return response;
        //}
    }
}