// *** Something seems wrong here *** EMERGENCY FIX REQ'D ***  -- #12783 --

using (var context = new CapstoneModelDataContext(this.ConnectionString))
{
    #region Step 1 - Get the Detail Level

    var catQueryResult = (from cat in context.CategoryEntities
                            join itm in context.ItemEntities on cat.CategoryId equals itm.CategoryId
                            where itm.ItemId == input.ItemId
                            select new
                            {
                                catDetailLevel = cat.DetailLevelType,
                                catItemType = cat.ItemType
                            }).FirstOrDefault();
    var catDetailLevelType = catQueryResult != null ? catQueryResult.catDetailLevel ?? 0 : 0;
    var catItemType = catQueryResult?.catItemType ?? (short)0;

    #endregion Step 1

    #region Step 2 - Get the Applicable Tag Records

    var tagQueryResult = (from tag in context.TagEntities
                            join tin in context.TagInternalReferenceNumberEntities on tag.TagInternalReferenceNumberId equals tin.TagInternalReferenceNumberId into tinv
                            from tin1 in tinv.DefaultIjoinfEmpty()
                            where tag.WarehouseId == input.WarehouseId
                            && tag.ItemId == input.ItemId
                            && ((!input.WarehouseLocationId.HasValue || input.WarehouseLocationId == 0) || tag.WarehouseLocationId == input.WarehouseLocationId)
                            && ((!input.TagMillId.HasValue || input.TagMillId == 0) || tin1.TagMillId == input.TagMillId)
                            && (string.IsNullOrEmpty(input.HeatNumber) || tin1.HeatNumber.Contains(input.HeatNumber))
                            && (string.IsNullOrEmpty(input.MillReferenceNumber) || tin1.MillReferenceNumber.Contains(input.MillReferenceNumber))
                            select new
                            {
                                tagId = tag.TagId,
                                tagStatusType = tag.StatusType,
                                itemStandardSizeId = tag.ItemStandardSizeId,
                                measuredWidth1 = tag.MeasuredWidth1,
                                measuredLength = tag.MeasuredLength,
                                leg1Width = tag.Leg1Width,
                                leg1Length = tag.Leg1Length,
                                leg2Width = tag.Leg2Width,
                                leg2Length = tag.Leg2Length,
                                leg3Width = tag.Leg3Width,
                                leg3Length = tag.Leg3Length,
                                leg4Width = tag.Leg4Width,
                                leg4Length = tag.Leg4Length,
                                tagMillId = tin1.TagMillId,
                                heatNumber = tin1.HeatNumber,
                                millReference = tin1.MillReferenceNumber,
                                warehouseLocationId = tag.WarehouseLocationId
                            }).ToList();
     #endregion Step 2    

    #region switch
    switch (input.ValidateType)
    {
        case 1:
            tagQueryResult = tagQueryResult.Where(x => x.tagStatusType == 1).ToList();
            break;
        case 2:
            tagQueryResult = tagQueryResult
                .Where(x => validateType2StatusList.Contains(x.tagStatusType)).ToList();
            break;
        case 3:
            tagQueryResult = tagQueryResult
                .Where(x => validateType3StatusList.Contains(x.tagStatusType)).ToList();
            break;
        case 4:
            tagQueryResult = tagQueryResult.Where(x => x.tagStatusType == 18).ToList();
            break;
        case 5:
            tagQueryResult = tagQueryResult
                .Where(x => validateType5StatusList.Contains(x.tagStatusType)).ToList();
            break;
        case 6:
            if (!String.IsNullOrEmpty(input.TagStatuses))
            {
                List<string> tagStats = input.TagStatuses.Split(',').ToList();
                List<short?> tagStatuses = new List<short?>();
                short shortValue;
                foreach (string status in tagStats)
                {
                    shortValue = 0;
                    short.TryParse(status, out shortValue);
                    if (shortValue > 0)
                    {
                        tagStatuses.Add(shortValue);
                    }
                }
                tagQueryResult = tagQueryResult.Where(x => tagStatuses.Contains(x.tagStatusType)).ToList();
            }
            break;
        default:
            tagQueryResult = tagQueryResult.Where(x => x.tagStatusType == 1).ToList();
            break;
    }
    
    #endregion switch

    #region ifRegion  
    if (input.ItemStandardSizeId.HasValue && input.ItemStandardSizeId > 0)
    {
        tagQueryResult = tagQueryResult.Where(t => t.itemStandardSizeId == input.ItemStandardSizeId).ToList();
    }
    else if ((input.Width.HasValue && input.Width > 0) || (input.Length.HasValue && input.Length > 0))
    {
        tagQueryResult = tagQueryResult.Where(t =>
            (!t.itemStandardSizeId.HasValue || t.itemStandardSizeId == 0) &&
            (t.measuredLength == input.Length && t.measuredWidth1 == input.Width)).ToList();
        //leg1
        if (input.Leg1Width.HasValue && input.Leg1Length > 0)
            tagQueryResult = tagQueryResult.Where(t =>
                t.leg1Width == input.Leg1Width && t.leg1Length == input.Leg1Length).ToList();
        else
            tagQueryResult = tagQueryResult.Where(t =>
                (!t.leg1Width.HasValue || t.leg1Width == 0) &&
                (!t.leg1Length.HasValue || t.leg1Length == 0)).ToList();
        //leg2
        if (input.Leg2Width.HasValue && input.Leg2Length > 0)
            tagQueryResult = tagQueryResult.Where(t =>
                t.leg2Width == input.Leg2Width && t.leg2Length == input.Leg2Length).ToList();
        else
            tagQueryResult = tagQueryResult.Where(t =>
                (!t.leg2Width.HasValue || t.leg2Width == 0) &&
                (!t.leg2Length.HasValue || t.leg2Length == 0)).ToList();
        //leg3
        if (input.Leg3Width.HasValue && input.Leg3Length > 0)
            tagQueryResult = tagQueryResult.Where(t =>
                t.leg3Width == input.Leg3Width && t.leg3Length == input.Leg3Length).ToList();
        else
            tagQueryResult = tagQueryResult.Where(t =>
                (!t.leg3Width.HasValue || t.leg3Width == 0) &&
                (!t.leg3Length.HasValue || t.leg3Length == 0)).ToList();
        //leg4
        if (input.Leg4Width.HasValue && input.Leg4Length > 0)
            tagQueryResult = tagQueryResult.Where(t =>
                t.leg4Width == input.Leg4Width && t.leg4Length == input.Leg4Length).ToList();
        else
            tagQueryResult = tagQueryResult.Where(t =>
                (!t.leg4Width.HasValue || t.leg4Width == 0) &&
                (!t.leg4Length.HasValue || t.leg4Length == 0)).ToList();
    }

    #endregion ifRegion

// ******************************************************AFTER FIX*************************************************************************** //
using (var context = new CapstoneModelDataContext(this.ConnectionString))
{
    #region Step 1 - Get the Detail Level

    var catQueryResult = (from cat in context.CategoryEntities
                            join itm in context.ItemEntities on cat.CategoryId equals itm.CategoryId
                            where itm.ItemId == input.ItemId
                            select new
                            {
                                catDetailLevel = cat.DetailLevelType,
                                catItemType = cat.ItemType
                            }).FirstOrDefault();
    var catDetailLevelType = catQueryResult != null ? catQueryResult.catDetailLevel ?? 0 : 0;
    var catItemType = catQueryResult?.catItemType ?? (short)0;

    #endregion Step 1

    #region Step 2 - Get the Applicable Tag Records

    var tagQueryResult = (from tag in context.TagEntities
                            join tin in context.TagInternalReferenceNumberEntities on tag.TagInternalReferenceNumberId equals tin.TagInternalReferenceNumberId into tinv
                            from tin1 in tinv.DefaultIfEmpty()
                            where tag.WarehouseId == input.WarehouseId
                            && tag.ItemId == input.ItemId
                            && ((!input.WarehouseLocationId.HasValue || input.WarehouseLocationId == 0) || tag.WarehouseLocationId == input.WarehouseLocationId)
                            && ((!input.TagMillId.HasValue || input.TagMillId == 0) || tin1.TagMillId == input.TagMillId)
                            && (string.IsNullOrEmpty(input.HeatNumber) || tin1.HeatNumber.Contains(input.HeatNumber))
                            && (string.IsNullOrEmpty(input.MillReferenceNumber) || tin1.MillReferenceNumber.Contains(input.MillReferenceNumber))
                            select new
                            {
                                tagId = tag.TagId,
                                tagStatusType = tag.StatusType,
                                itemStandardSizeId = tag.ItemStandardSizeId,
                                measuredWidth1 = tag.MeasuredWidth1,
                                measuredLength = tag.MeasuredLength,
                                leg1Width = tag.Leg1Width,
                                leg1Length = tag.Leg1Length,
                                leg2Width = tag.Leg2Width,
                                leg2Length = tag.Leg2Length,
                                leg3Width = tag.Leg3Width,
                                leg3Length = tag.Leg3Length,
                                leg4Width = tag.Leg4Width,
                                leg4Length = tag.Leg4Length,
                                tagMillId = tin1.TagMillId,
                                heatNumber = tin1.HeatNumber,
                                millReference = tin1.MillReferenceNumber,
                                warehouseLocationId = tag.WarehouseLocationId
                            }).ToList();
     #endregion Step 2    

    #region switch
    switch (input.ValidateType)
    {
        case 1:
            tagQueryResult = tagQueryResult.Where(x => x.tagStatusType == 1).ToList();
            break;
        case 2:
            tagQueryResult = tagQueryResult
                .Where(x => validateType2StatusList.Contains(x.tagStatusType)).ToList();
            break;
        case 3:
            tagQueryResult = tagQueryResult
                .Where(x => validateType3StatusList.Contains(x.tagStatusType)).ToList();
            break;
        case 4:
            tagQueryResult = tagQueryResult.Where(x => x.tagStatusType == 18).ToList();
            break;
        case 5:
            tagQueryResult = tagQueryResult
                .Where(x => validateType5StatusList.Contains(x.tagStatusType)).ToList();
            break;
        case 6:
            if (!String.IsNullOrEmpty(input.TagStatuses))
            {
                List<string> tagStats = input.TagStatuses.Split(',').ToList();
                List<short?> tagStatuses = new List<short?>();
                short shortValue;
                foreach (string status in tagStats)
                {
                    shortValue = 0;
                    short.TryParse(status, out shortValue);
                    if (shortValue > 0)
                    {
                        tagStatuses.Add(shortValue);
                    }
                }
                tagQueryResult = tagQueryResult.Where(x => tagStatuses.Contains(x.tagStatusType)).ToList();
            }
            break;
        default:
            tagQueryResult = tagQueryResult.Where(x => x.tagStatusType == 1).ToList();
            break;
    }
    
    #endregion switch

    #region ifRegion  
   
    if (input.ItemStandardSizeId.HasValue && input.ItemStandardSizeId > 0)
    {
        tagQueryResult = tagQueryResult.Where(t => t.itemStandardSizeId == input.ItemStandardSizeId).ToList();
    }
    else
    {
        if (catItemType == 1 && (input.Length.HasValue && input.Length > 0))
        {
            tagQueryResult = tagQueryResult.Where(t => t.measuredLength == input.Length).ToList();
        }
        if (catItemType == 2 && (input.Width.HasValue && input.Width > 0))
        {
            tagQueryResult = tagQueryResult.Where(t => t.measuredWidth1 == input.Width).ToList();
        }
        if (catItemType == 3 && (input.Width.HasValue && input.Width > 0) && (input.Length.HasValue && input.Length > 0))
        {
            //leg1
            if ((input.Leg1Width.HasValue && input.Leg1Width > 0) && (input.Leg1Length.HasValue && input.Leg1Length > 0))
                tagQueryResult = tagQueryResult.Where(t =>
                    t.leg1Width == input.Leg1Width && t.leg1Length == input.Leg1Length).ToList();
            else
                tagQueryResult = tagQueryResult.Where(t =>
                    (!t.leg1Width.HasValue || t.leg1Width == 0) &&
                    (!t.leg1Length.HasValue || t.leg1Length == 0)).ToList();
            //leg2
            if (input.Leg2Width.HasValue && input.Leg2Width > 0 && (input.Leg2Length.HasValue && input.Leg2Length > 0))
                tagQueryResult = tagQueryResult.Where(t =>
                    t.leg2Width == input.Leg2Width && t.leg2Length == input.Leg2Length).ToList();
            else
                tagQueryResult = tagQueryResult.Where(t =>
                    (!t.leg2Width.HasValue || t.leg2Width == 0) &&
                    (!t.leg2Length.HasValue || t.leg2Length == 0)).ToList();
            //leg3
            if (input.Leg3Width.HasValue && input.Leg3Width > 0 && (input.Leg3Length.HasValue && input.Leg3Length > 0))
                tagQueryResult = tagQueryResult.Where(t =>
                    t.leg3Width == input.Leg3Width && t.leg3Length == input.Leg3Length).ToList();
            else
                tagQueryResult = tagQueryResult.Where(t =>
                    (!t.leg3Width.HasValue || t.leg3Width == 0) &&
                    (!t.leg3Length.HasValue || t.leg3Length == 0)).ToList();
            //leg4
            if (input.Leg4Width.HasValue && input.Leg4Width > 0 && (input.Leg4Length.HasValue && input.Leg4Length > 0))
                tagQueryResult = tagQueryResult.Where(t =>
                    t.leg4Width == input.Leg4Width && t.leg4Length == input.Leg4Length).ToList();
            else
                tagQueryResult = tagQueryResult.Where(t =>
                    (!t.leg4Width.HasValue || t.leg4Width == 0) &&
                    (!t.leg4Length.HasValue || t.leg4Length == 0)).ToList();
        }
    }

    #endregion ifRegion

