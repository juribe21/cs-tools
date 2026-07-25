/* **** 13875 **** */

/// <summary>
/// Get TagBundle On HandCost
/// </summary>
/// <param name="session"></param>
/// <param name="tagBundleId"></param>
/// <returns></returns>
[WebMethod(Description = "Get TagBundle On HandCost.")]
public TagBundleOnHandCost GetTagBundleOnHandCost(SoapUserSession session, int tagBundleId)
{
    int methodDurationId = 0;
    try
    {
        // make sure session is valid and log activity if valid
        ValidateAndLogSoapUserSession(session, MethodName);

        // validate required parameter(s)                
        ThrowIfNull(typeof(int), tagBundleId, "tagBundleId");

        TagBundleEntityAccessor accessor = new TagBundleEntityAccessor(this.ConnectionString);
        TagBundleOnHandCost traceability = new TagBundleOnHandCost()
            ;
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            MethodDuration obj = new MethodDuration { LoginSessionId = session.SessionId, MethodName = MethodName, StartUTCDateTime = DateTime.UtcNow };
            methodAccessor.Insert(obj);
            methodDurationId = obj.MethodDurationId;
            traceability = accessor.GetTagBundleOnHandCost(tagBundleId);
            methodAccessor.Update(methodDurationId, null, null);
        }
        else
        {
            traceability = accessor.GetTagBundleOnHandCost(tagBundleId);
        }

        return traceability;
    }
    catch (CapstoneException ex)
    {
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetTagBundleOnHandCostFailed.ToString());
        }

        log_.Error(ex.ToString());
        throw SoapExceptionHelper.ToSoapException(ex);
    }
    catch (Exception ex)
    {
        if (logMethodDuration)
        {
            MethodDurationEntityAccessor methodAccessor = new MethodDurationEntityAccessor(this.ConnectionString);
            methodAccessor.Update(methodDurationId, ex.Message, BusinessLogicException.GetTagBundleOnHandCostFailed.ToString());
        }

        log_.Error(ExceptionFormatter.FormatMessage(ex));
        throw SoapExceptionHelper.ToSoapException(new CapstoneException(BusinessLogicException.GetTagBundleOnHandCostFailed, "Get TagBundle On HandCost Failed."));
    }
}

public const int GetTagBundleOnHandCostFailed = 10116;

public class TagBundleOnHandCost
{
    public string CostUOM { get; set; }
    public decimal? TotalMaterialCost { get; set; }
    public decimal? MaterialCostPer { get; set; }
    public decimal? TotalInboundFreightCost { get; set; }
    public decimal? InboundFreightCostPer { get; set; }
    public decimal? TotalCost3 { get; set; }
    public decimal? Cost3Per { get; set; }
    public decimal? TotalCost4 { get; set; }
    public decimal? Cost4Per { get; set; }
    public decimal? TotalCost5 { get; set; }
    public decimal? Cost5Per { get; set; }
    public decimal? TotalCost6 { get; set; }
    public decimal? Cost6Per { get; set; }
    public decimal? TotalCost7 { get; set; }
    public decimal? Cost7Per { get; set; }
    public decimal? TotalCost8 { get; set; }
    public decimal? Cost8Per { get; set; }
    public decimal? TotalCost9 { get; set; }
    public decimal? Cost9Per { get; set; }
    public decimal? TotalCost10 { get; set; }
    public decimal? Cost10Per { get; set; }
    public decimal? TotalCost { get; set; }
    public decimal? TotalCostPer { get; set; }
}


[TestMethod]
public void TestGetTagBundleOnHandCost()
{
    try
    {
        TagBundleEntityAccessor accessorTag = new TagBundleEntityAccessor(Helper.ConnectionString);
        int tagBundleId = 13372; // 11526;

        var result = accessorTag.GetTagBundleOnHandCost(tagBundleId);
        if (result != null)
        {
        }
    }
    catch (Exception e)
    {
        Assert.Fail(e.Message);
    }
}

/// ************************************************************ ///
public TagBundleOnHandCost GetTagBundleOnHandCost(int tagBundleId)
{
    TagBundleOnHandCost handCost = new TagBundleOnHandCost();

    using (CapstoneModelDataContext context = new CapstoneModelDataContext(this.ConnectionString))
    {
        /// Step 1 - Get some Info About the Bundle, The Category Cost and the Warehouse Dimensions
        var bundleInfo = (from tb in context.TagBundleEntities
                          join itm in context.ItemEntities on tb.ItemId equals itm.ItemId
                          join cat in context.CategoryEntities on itm.CategoryId equals cat.CategoryId
                          join wh in context.WarehouseEntities on tb.WarehouseId equals wh.WarehouseId
                          where tb.TagBundleId == tagBundleId
                          select new
                          {
                              tb.TagBundleId,
                              cat.MetricUnitCostUOM,
                              cat.ImperialUnitCostUOM,
                              wh.DimensionsType,
                              tb.WarehouseId
                          }).FirstOrDefault();

        /// Step 2 - Get the Totals from the Tag table
        var tagTotals = (from tag in context.TagEntities
                         where tag.TagBundleId == tagBundleId && tag.StatusType == 1 && tag.WarehouseId == tag.WarehouseId // ←
                         group tag by new { tag.TagBundleId } into taag
                         select new
                         {
                             TotalQuantityInLength = taag.Sum(s => s.MeasuredLength),
                             TotalQuantityInWeight = taag.Sum(s => s.QuantityInStockedByUnitOfMeasure),
                             totalMaterialCost = taag.Sum(s => s.TotalMaterialCost),
                             totalInboundFreightCost = taag.Sum(s => s.TotalInboundFreightCost),
                             totalCost3 = taag.Sum(s => s.TotalCost3),
                             totalCost4 = taag.Sum(s => s.TotalCost4),
                             totalCost5 = taag.Sum(s => s.TotalCost5),
                             totalCost6 = taag.Sum(s => s.TotalCost6),
                             totalCost7 = taag.Sum(s => s.TotalCost7),
                             totalCost8 = taag.Sum(s => s.TotalCost8),
                             totalCost9 = taag.Sum(s => s.TotalCost9),
                             totalCost10 = taag.Sum(s => s.TotalCost10)
                         }
                         ).FirstOrDefault();

        /// Step 4 - Convert the Quantity into the Unit Cost UOM
        decimal? quantityInCostedByUOM = 0;

        if (bundleInfo.DimensionsType == 2)
        {
            if (bundleInfo.MetricUnitCostUOM == "cm")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInLength;
            }
            if (bundleInfo.MetricUnitCostUOM == "kg")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInWeight;
            }
            if (bundleInfo.MetricUnitCostUOM == "m")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInLength / 100;
            }
            if (bundleInfo.MetricUnitCostUOM == "mt")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInWeight / 100;
            }
        }
        else
        {
            if (bundleInfo.ImperialUnitCostUOM == "c ft")
            {
                quantityInCostedByUOM = (tagTotals.TotalQuantityInLength / 12) / 100;
            }
            if (bundleInfo.ImperialUnitCostUOM == "cwt")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInWeight / 100;
            }
            if (bundleInfo.ImperialUnitCostUOM == "ft")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInLength / 12;
            }
            if (bundleInfo.ImperialUnitCostUOM == "in")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInLength;
            }
            if (bundleInfo.ImperialUnitCostUOM == "lb")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInWeight;
            }
            if (bundleInfo.ImperialUnitCostUOM == "t")
            {
                quantityInCostedByUOM = tagTotals.TotalQuantityInWeight / 200;
            }
        }


        /// Return One Record
        if (bundleInfo.DimensionsType == 2)
        {
            handCost.CostUOM = bundleInfo.MetricUnitCostUOM;
        }
        else
        {
            handCost.CostUOM = bundleInfo.ImperialUnitCostUOM;
        }
        handCost.TotalMaterialCost = tagTotals.totalMaterialCost;
        handCost.MaterialCostPer = (tagTotals.totalMaterialCost / quantityInCostedByUOM);
        handCost.TotalInboundFreightCost = tagTotals.totalInboundFreightCost;
        handCost.InboundFreightCostPer = tagTotals.totalInboundFreightCost / quantityInCostedByUOM;
        handCost.TotalCost3 = tagTotals.totalCost3;
        handCost.Cost3Per = tagTotals.totalCost3 / quantityInCostedByUOM;
        handCost.TotalCost4 = tagTotals.totalCost4;
        handCost.Cost4Per = tagTotals.totalCost4 / quantityInCostedByUOM;
        handCost.TotalCost5 = tagTotals.totalCost5;
        handCost.Cost5Per = tagTotals.totalCost5 / quantityInCostedByUOM;
        handCost.TotalCost6 = tagTotals.totalCost6;
        handCost.Cost6Per = tagTotals.totalCost6 / quantityInCostedByUOM;
        handCost.TotalCost7 = tagTotals.totalCost7;
        handCost.Cost7Per = tagTotals.totalCost7 / quantityInCostedByUOM;
        handCost.TotalCost8 = tagTotals.totalCost8;
        handCost.Cost8Per = tagTotals.totalCost8 / quantityInCostedByUOM;
        handCost.TotalCost9 = tagTotals.totalCost9;
        handCost.Cost9Per = tagTotals.totalCost9 / quantityInCostedByUOM;
        handCost.TotalCost10 = tagTotals.totalCost10;
        handCost.Cost10Per = tagTotals.totalCost10 / quantityInCostedByUOM;

        handCost.TotalCost = tagTotals.totalMaterialCost + tagTotals.totalInboundFreightCost + handCost.TotalCost3 + handCost.TotalCost4 + handCost.TotalCost5
            + handCost.TotalCost6 + handCost.TotalCost7 + handCost.TotalCost8 + handCost.TotalCost9 + handCost.TotalCost10;
        handCost.TotalCostPer = handCost.TotalCost / quantityInCostedByUOM;

        return handCost;
    }
}


/*
SELECT TBA.TagBundle_Id, ITM.ItemCode, ITM.BriefDescription, ISS.ImperialDescription, ISS.MetricDescription,
TBA.NumberOfPiecesToPull, TBA.Note, WH.Dimensions_Type	
	 	 FROM TagBundleAllocation TBA	
	 	 	 JOIN TagBundle TB ON TB.TagBundle_Id = TBA.TagBundle_Id	
	 	 	 JOIN Item ITM ON ITM.Item_Id = TB.Item_Id	
	 	 	 JOIN Warehouse WH ON WH.Warehouse_Id = TB.Warehouse_Id	
	 	 	 LEFT JOIN ItemStandardSize ISS ON TB.ItemStandardSize_Id = ISS.StandardSize_Id	
	 	 WHERE TBA.SalesOrderDetail_Id = 737759	-- TagBundleId
	 	 ORDER BY TB.TagBundle_Id	


Select DISTINCT (TagBundle_Id), PieceCount From Tag Where TagBundle_Id IS NOT NULL aND PieceCount > 0

Select SUM (Tag.PieceCount) FROM Tag WHERE TagBundle_Id = 737906 AND Status_Type = 1

SELECT SUM(Tag.MeasuredLength) AS TotalQuantityInLength, SUM(Tag.QuantityInStockedByUnitOfMeasure) AS TotalQuantityInWeight,
SUM(Tag.TotalMaterialCost), SUM(Tag.TotalInboundFreightCost), SUM(Tag.TotalCost3), SUM(Tag.TotalCost4), SUM(Tag.TotalCost5),
SUM(Tag.TotalCost6), SUM(Tag.TotalCost7), SUM(Tag.TotalCost8), SUM(Tag.TotalCost9), SUM(Tag.TotalCost10)	
	 	 FROM Tag	
	 	 	 WHERE TagBundle_Id = 737906 --TB.TagBundle_Id	
	 	 	 	 AND Tag.Status_Type = 1	
	 	 	 	 AND Warehouse_Id = 7 --TB.Warehouse_Id


*/