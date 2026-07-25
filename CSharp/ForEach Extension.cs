/* *** ForEach Extension ***/

public void ForEachExtension()
{
    /// Get some List
    var handTags = context.TagEntities.Where(x => x.TagBundleId == input.TagBundleId && x.StatusType == 1).ToList();

    /// Update some fields
    handTags.ForEach(x => x.WarehouseLocationId = input.NewWarehouseLocationId);

    /// Update records
    handTags.ForEach(x => UpdateEntity(x, e => e.TagId == x.TagId));
}