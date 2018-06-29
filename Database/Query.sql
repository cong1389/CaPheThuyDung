use SmartStore
go

select * from ShippingMethod

use KuteShop
GO

select * from Setting where name like '%Shipping%'

select  * from MenuLink where MenuName like N'%Dịch vụ%'

select * from StaticContent where VirtualCategoryId like '%310f945c-a560-4eb0-b4d5-4aa1ef36dd18%'