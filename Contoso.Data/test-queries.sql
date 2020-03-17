
SELECT  o.Id as [OrderId],
        o.OrderTimeStamp as [OrderedOn],
        p.Name as [Product],
        p.InventoryCount as [InStock],
        c.Quantity as [QuantityOrdered]
FROM    [Order] o
JOIN    [CartItem] c on o.Id = c.OrderId
JOIN    [Products] p on c.ProductId = p.Name

GO;
RETURN;

SELECT  * FROM [Order]
SELECT  * FROM [CartItem]
SELECT  * FROM [Products]
