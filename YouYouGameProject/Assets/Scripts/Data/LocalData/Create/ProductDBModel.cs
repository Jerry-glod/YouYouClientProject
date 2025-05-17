//===================================================
//备注：代码为工具自动生成，请勿手动修改
//===================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ProductDBModel:AbstractDBModel<ProductDBModel,ProductEntity>
{
    protected override string FileName { get { return "Product.data"; } }
    protected override ProductEntity MakeEntity(GameDataTableParser parser)
    {
        ProductEntity entity = new ProductEntity();
        entity.Id = parser.GetFieldValue("Id").ToInt();
        entity.Name = parser.GetFieldValue("Name");
        entity.Price = parser.GetFieldValue("Price").ToFloat();
        entity.PicName = parser.GetFieldValue("PicName");
        entity.Desc = parser.GetFieldValue("Desc");

        return entity;
    }
}
