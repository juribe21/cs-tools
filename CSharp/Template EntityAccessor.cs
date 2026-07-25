using Bayern.CapstoneService.BusinessLogic;
using Bayern.CapstoneService.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayern.CapstoneService.DAL
{
    public class TagBundleHistoryEntityAccessor : AccessorBase<TagBundleHistoryEntity, CapstoneModelDataContext>
    {
        // NLog logger instance for this class
        private readonly Logger log_ = LogManager.GetCurrentClassLogger();

        public TagBundleHistoryEntityAccessor(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public TagBundleHistoryEntityAccessor(CapstoneModelDataContext context)
        {
            if (context != null)
            {
                this.Context = context;
                this.ConnectionString = context.Connection.ConnectionString;
            }
        }

        public List<TagBundleHistory> GetAllTagBundleHistory()
        {
            List<TagBundleHistoryEntity> tagBundleEntities = Context.TagBundleHistoryEntities.OrderBy(TagBundleHistory.SortByColumn).ToList();
            List<TagBundleHistory> list = new List<TagBundleHistory>();
            foreach (TagBundleHistoryEntity itmrsEntity in tagBundleEntities)
            {
                TagBundleHistory obj = DTOConversion.ConvertTo<TagBundleHistory>(itmrsEntity);
                list.Add(obj);
            }
            return list;
        }

        public TagBundleHistory GetTagBundleHistoryById(int tagBundleHistoryId)
        {
            TagBundleHistoryEntity entity = Context.TagBundleHistoryEntities.Where(e => e.TagBundleHistoryId == tagBundleHistoryId).FirstOrDefault();
            if (entity == null)
                return null;
            return DTOConversion.ConvertTo<TagBundleHistory>(entity);
        }

        public TagBundleHistory InsertTagBundleHistory(TagBundleHistory tagBundleHistory)
        {
            try
            {
                TagBundleHistoryEntity obj = DTOConversion.ConvertTo<TagBundleHistoryEntity>(tagBundleHistory);
                this.Add(obj);
                tagBundleHistory.TagBundleHistoryId = obj.TagBundleHistoryId;
                return tagBundleHistory;
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2601 || sqlEx.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    throw new CapstoneException(BusinessLogicException.InsertAlreadyExists, "Insert object already exists.");
                }
                else throw new CapstoneException(BusinessLogicException.InsertFailed, "Record cannot be inserted.");
            }
            catch
            {
                throw new CapstoneException(BusinessLogicException.InsertFailed, "Record cannot be inserted.");
            }
        }

        public TagBundleHistory UpdateTagBundleHistory(TagBundleHistory tagBundleHistory)
        {
            try
            {
                TagBundleHistoryEntity entity = Context.TagBundleHistoryEntities.Where(e => e.TagBundleHistoryId == tagBundleHistory.TagBundleHistoryId).FirstOrDefault();
                if (entity == null)
                {
                    string message = string.Format("Object not found TagBundleHistory [TagBundleHistoryId: {0}]", tagBundleHistory.TagBundleHistoryId);
                    throw new BusinessLogicException(BusinessLogicException.InvalidObjectUpdateRequest, message);
                }
                TagBundleHistoryEntity obj = DTOConversion.ConvertTo<TagBundleHistoryEntity>(tagBundleHistory);
                this.UpdateEntity(obj, e => e.TagBundleHistoryId == tagBundleHistory.TagBundleHistoryId);
                return tagBundleHistory;
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2601 || sqlEx.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    throw new CapstoneException(BusinessLogicException.UpdateAlreadyExists, "Update object already exists.");
                }
                else throw new CapstoneException(BusinessLogicException.UpdateFailed, "Record cannot be updated.");
            }
            catch
            {
                throw new CapstoneException(BusinessLogicException.UpdateFailed, "Record cannot be updated.");
            }
        }

        public bool DeleteTagBundleHistory(int tagBundleHistoryId)
        {
            try
            {
                TagBundleHistoryEntity entity = Context.TagBundleHistoryEntities.Where(e => e.TagBundleHistoryId == tagBundleHistoryId).FirstOrDefault();
                if (entity == null)
                {
                    string message = string.Format("Object not found TagBundleHistory [TagBundleHistoryId: {0}]", tagBundleHistoryId);
                    return false;
                }

                this.Delete(entity, true);
                return true;

            }
            catch (SqlException sqlEx)
            {

                if (sqlEx.Number == 547)
                {
                    string message = "Record cannot be deleted because it is referenced elsewhere.";
                    throw new CapstoneException(BusinessLogicException.DeleteFailedDuetoForeignkeyReference, message);
                }
                else
                {
                    throw new CapstoneException(BusinessLogicException.DeleteFailed, sqlEx.Message);
                }
            }
            catch (CapstoneException ex)
            {
                throw new CapstoneException(ex.Code, ex.Message);
            }
        }


    }

}
