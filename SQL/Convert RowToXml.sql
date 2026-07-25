/* Convert row to XML */

-- Set row as xml format
DECLARE @xml XML
SET @XML = (
	SELECT TOP(1) *
	FROM VcaOrderRequestData
	WHERE VcaRequestIndex = 1739
	FOR XML AUTO
	);
Select @XML

-- Create [Value, Pair] for each colum to be selected
SELECT DISTINCT
		CAST(Attribute.Name.query('local-name(.)') AS VARCHAR(100)) Attribute,
		Attribute.Name.value('.','VARCHAR(100)') Value
		FROM @XML.nodes('//@*') Attribute(Name)