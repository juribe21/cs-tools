
#region TagBundle

[TestMethod]
public void TestGetAll()
{
    try
    {
        var accessor = new TagBundleEntityAccessor(Helper.ConnectionString);

        var result = accessor.GetAll();
        if (result != null)
        {

        }
    }
    catch (Exception e)
    {
        Assert.Fail(e.Message);
    }
}


[TestMethod]
public void TestGetById()
{
    try
    {
        var accessor = new TagBundleEntityAccessor(Helper.ConnectionString);
        int tagBundleId = 1;

        TagBundle result = accessor.GetById(tagBundleId);
        if (result != null)
        {

        }
    }
    catch (Exception e)
    {
        Assert.Fail(e.Message);
    }
}


[TestMethod]
public void TestInsertTagBundle()
{
    try
    {
        var accessor = new TagBundleEntityAccessor(Helper.ConnectionString);

        TagBundle tagBundle = new TagBundle()
        {

        };

        TagBundle result = accessor.InsertTagBundle(tagBundle);
        if (result != null)
        {

        }
    }
    catch (Exception e)
    {
        Assert.Fail(e.Message);
    }
}


[TestMethod]
public void TestUpdateTagBundle()
{
    try
    {
        var accessor = new TagBundleEntityAccessor(Helper.ConnectionString);
        TagBundle tagBundle = new TagBundle()
        {

        };

        TagBundle result = accessor.UpdateTagBundle(tagBundle);
        if (result != null)
        {

        }
    }
    catch (Exception e)
    {
        Assert.Fail(e.Message);
    }
}


[TestMethod]
public void TestDeleteTagBundle()
{
    try
    {
        var accessor = new TagBundleEntityAccessor(Helper.ConnectionString);
        int tagBundleId = 1;

        bool result = accessor.DeleteTagBundle(tagBundleId);
        if (result)
        {

        }
    }
    catch (Exception e)
    {
        Assert.Fail(e.Message);
    }
}

#endregion
