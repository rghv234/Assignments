create trigger update_inventory_after_sale
on sales.order_items
after insert
as
begin
update s
set s.quantity = s.quantity - i.quantity
from production.stocks s
inner join inserted i 
on s.product_id = i.product_id
and s.store_id = (
select store_id from sales.orders where order_id = i.order_id
)
end
---------------------------------------------------------------------------------
create trigger trg_prevent_invalid_sales_insert
on sales.order_items
instead of insert
as
begin
if exists (
select 1
from inserted i
join sales.orders o on i.order_id = o.order_id
join production.stocks s on s.store_id = o.store_id and s.product_id = i.product_id
where i.quantity > s.quantity
)
begin
raiserror('Cannot insert sale: quantity exceeds available stock', 16, 1)
return
end

insert into sales.order_items(order_id, item_id, product_id, quantity, list_price, discount)
select order_id, item_id, product_id, quantity, list_price, discount
from inserted
end
-----------------------------------------------------------------------------------
create trigger prevent_product_deletion_with_sales
on production.products
instead of delete
as
begin
if exists (
select 1
from deleted d
join sales.order_items oi on d.product_id = oi.product_id
)
begin 
raiserror('Cannot delete product: it has associated sales records.',16,1)
return
end

delete from production.products
where product_id in (select product_id from deleted)
end

