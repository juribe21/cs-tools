// Enum Helper

using System.ComponentModel;

// Create enum with dataanotation "Description"
public enum CreditHoldStatusEnum
{
    [Description("Always")]
    Always = 1,
    [Description("As Required")]
    AsRequired = 2,
    [Description("Never")]
    Never = 3
}


// Create class
public static class EnumHelper
{
    /// <summary>
    /// Retrieve the description on the enum, e.g.
    /// [Description("Bright Pink")]
    /// BrightPink = 2,
    /// Then when you pass in the enum, it will retrieve the description
    /// </summary>
    /// <param name="en">The Enumeration</param>
    /// <returns>A string representing the friendly name</returns>
    public static string GetDescription(Enum en)
    {
        Type type = en.GetType();

        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return en.ToString();
    }
}
EnumHelper.GetDescription((CreditHoldStatusEnum)line.CreditHoldType)


/* ********************************************************************* */
/* 1 */
public static class ExtensionUtil
{
    public static string GetPropertyDescription<T>(string fieldName)
    {
        var property = typeof(T).GetProperty(fieldName);
        if (property != null)
        {
            var attribute = property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
            var descriptionAttribute = (DescriptionAttribute)attribute;
            return descriptionAttribute.Description;
        }
        else
        {
            return string.Empty;
        }
    }
}

/* 2 Pass class to method GetPropertyDescription*/
relatedDocuments.Add(ExtensionUtil.GetPropertyDescription<DetermineDocumentTypesForSalesOrderResult>(nameof(relatedDocumentFlagObject.FabricationOrderLinesExistFlag)));

/* 3 Class with Notation Description*/
public class DetermineDocumentTypesForSalesOrderResult
{
    [Description("Picking Ticket")]
    public bool PickingLinesExistFlag { get; set; }
    [Description("Purchasing Ticket")]
    public bool PurchasingLinesExistFlag { get; set; }
    [Description("Production Ticket")]
    public bool ProductionLinesExistFlag { get; set; }
    [Description("Quick Cut Ticket")]
    public bool QuickCutLinesExistFlag { get; set; }
    [Description("Fabrication Order")]
    public bool FabricationOrderLinesExistFlag { get; set; }
    [Description("Fulfillment Ticket")]
    public bool FulfillmentLinesExistFlag { get; set; }
}