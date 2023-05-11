﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HCP.Material.IF;
using HCP.Material.DL.Entities;
using HCP.Material.DO;
using System.Collections.Generic;

namespace HCP.Material.DL.Repository
{
    public class ItemCodesRepository : IItemCodesRepository
    {
        public async Task<List<DO_ItemCodes>> GetItemList()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEiitcd
                        .Select(r => new DO_ItemCodes
                        {
                            ItemCode = r.ItemCode,
                            ItemDescription = r.ItemDescription
                        }).OrderBy(o => o.ItemDescription).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<DO_ItemCodes>> GetItemDetails(int ItemCode)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    //var result = await db.GtEskucd.Join(db.GtEiitcd,
                    //     x => x.Skuid,
                    //     y => y.ItemCode,
                    //     (x, y) => new { x, y }).Join(db.GtEcapcd,
                    //     a => a.y.PackUnit,
                    //     p => p.ApplicationCode, (a, p) => new { a, p })
                    //     .Select(r => new DO_ItemCodes
                    //     {
                    //         Skuid = r.a.x.Skuid,
                    //         ItemGroup = r.a.x.Skugroup,
                    //         ItemCategory = r.a.x.Skucategory,
                    //         ItemSubCategory = r.a.x.SkusubCategory,
                    //         Skutype = r.a.x.Skutype,
                    //         ItemCode = r.a.y.ItemCode,
                    //         ItemDescription = r.a.y.ItemDescription,
                    //         UnitOfMeasure = r.a.y.UnitOfMeasure,
                    //         PackUnit = r.a.y.PackUnit,
                    //         PackUnitDesc = r.p.CodeDesc,
                    //         PackSize = r.a.y.PackSize,
                    //         InventoryClass = r.a.y.InventoryClass,
                    //         ItemClass = r.a.y.ItemClass,
                    //         ItemSource = r.a.y.ItemSource,
                    //         ItemCriticality = r.a.y.ItemCriticality,
                    //         BarCodeID = r.a.y.BarcodeCodeId,
                    //         UsageStatus = r.a.y.UsageStatus,
                    //         ActiveStatus = r.a.x.ActiveStatus,

                    //     }).ToListAsync();

                    var result = await db.GtEiitcd.Join(db.GtEskucd,
                         x => x.ItemCode,
                         y => y.Skucode,
                         (x, y) => new { x, y }).Join(db.GtEcapcd,
                         a => a.x.PackUnit,
                         p => p.ApplicationCode, (a, p) => new { a, p })
                         .Select(r => new DO_ItemCodes
                         {
                             Skuid = r.a.y.Skuid,
                             ItemGroup = r.a.x.ItemGroup,
                             ItemCategory = r.a.x.ItemCategory,
                             ItemSubCategory = r.a.x.ItemSubCategory,
                             Skutype = r.a.y.Skutype,
                             ItemCode = r.a.x.ItemCode,
                             ItemDescription = r.a.x.ItemDescription,
                             UnitOfMeasure = r.a.x.UnitOfMeasure,
                             PackUnit = r.a.x.PackUnit,
                             PackUnitDesc = r.p.CodeDesc,
                             PackSize = r.a.x.PackSize,
                             InventoryClass = r.a.x.InventoryClass,
                             ItemClass = r.a.x.ItemClass,
                             ItemSource = r.a.x.ItemSource,
                             ItemCriticality = r.a.x.ItemCriticality,
                             BarCodeID = r.a.x.BarcodeCodeId,
                             UsageStatus = r.a.x.UsageStatus,
                             ActiveStatus = r.a.x.ActiveStatus,
                             Skucode=r.a.y.Skucode

                         }).ToListAsync();
                    foreach (var obj in result)
                    {
                        if (obj.InventoryClass == "S")
                        {
                            obj.InventoryClass = "Stockable";
                        }
                        else
                        {
                            obj.InventoryClass = "Non-Stockable";
                        }

                        if (obj.ItemClass == "B")
                        {
                            obj.ItemClass = "Bought Out";
                        }
                        else if (obj.ItemClass == "C")
                        {
                            obj.ItemClass = "Consignment";
                        }
                        else if (obj.ItemClass == "I")
                        {
                            obj.ItemClass = "In-House";
                        }
                        else
                        {
                            obj.ItemClass = "Sub Contract";
                        }

                        if (obj.ItemSource == "L")
                        {
                            obj.ItemSource = "Local";
                        }
                        else if (obj.ItemSource == "I")
                        {
                            obj.ItemSource = "Imported";
                        }
                        else
                        {
                            obj.ItemSource = "Out Station";
                        }

                        if (obj.ItemCriticality == "D")
                        {
                            obj.ItemCriticality = "Desirable";
                        }
                        else if (obj.ItemCriticality == "E")
                        {
                            obj.ItemCriticality = "Essential";
                        }
                        else
                        {
                            obj.ItemCriticality = "Vital";
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ItemCodes>> GetItemMaster(int ItemGroup, int ItemCategory, int ItemSubCategory)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    //var result = await db.GtEiitcd
                    //    .Where(x =>
                    //          x.ItemGroup == ItemGroup && x.ItemCategory == ItemCategory && x.ItemSubCategory == ItemSubCategory)
                    //    .Join(db.GtEcapcd,
                    //     r => r.PackUnit,
                    //     y => y.ApplicationCode,
                    //    (r, y) => new DO_ItemCodes
                    //    {
                    //        ItemCode = r.ItemCode,
                    //        ItemGroup = r.ItemGroup,
                    //        ItemCategory = r.ItemCategory,
                    //        ItemSubCategory = r.ItemSubCategory,
                    //        ItemDescription = r.ItemDescription,
                    //        UnitOfMeasure = r.UnitOfMeasure,
                    //        PackUnit = r.PackUnit,
                    //        PackUnitDesc = y.CodeDesc,
                    //        PackSize = r.PackSize,
                    //        InventoryClass = r.InventoryClass,
                    //        ItemClass = r.ItemClass,
                    //        ItemSource = r.ItemSource,
                    //        ItemCriticality = r.ItemCriticality,
                    //        BarCodeID = r.BarcodeCodeId,
                    //        UsageStatus = r.UsageStatus,
                    //        ActiveStatus = r.ActiveStatus
                    //    }).ToListAsync();

                    //var result = await db.GtEskucd.Join(db.GtEiitcd,
                    //     x => x.Skuid,
                    //     y => y.ItemCode,
                    //     (x, y) => new { x, y }).Join(db.GtEcapcd,
                    //     a => a.y.PackUnit,
                    //     p => p.ApplicationCode, (a, p) => new { a, p })
                    //     .Select(r => new DO_ItemCodes
                    //     {
                    //         Skuid = r.a.x.Skuid,
                    //         //ItemGroup = r.a.x.Skugroup,
                    //         //ItemCategory = r.a.x.Skucategory,
                    //         //ItemSubCategory = r.a.x.SkusubCategory,
                    //         Skutype = r.a.x.Skutype,
                    //         ItemCode = r.a.y.ItemCode,
                    //         ItemDescription = r.a.y.ItemDescription,
                    //         UnitOfMeasure = r.a.y.UnitOfMeasure,
                    //         PackUnit = r.a.y.PackUnit,
                    //         PackUnitDesc = r.p.CodeDesc,
                    //         PackSize = r.a.y.PackSize,
                    //         InventoryClass = r.a.y.InventoryClass,
                    //         ItemClass = r.a.y.ItemClass,
                    //         ItemSource = r.a.y.ItemSource,
                    //         ItemCriticality = r.a.y.ItemCriticality,
                    //         BarCodeID = r.a.y.BarcodeCodeId,
                    //         UsageStatus = r.a.y.UsageStatus,
                    //         ActiveStatus = r.a.x.ActiveStatus,

                    //     }).ToListAsync();
                    var result = await db.GtEiitcd.Join(db.GtEskucd,
                        x => x.ItemCode,
                        y => y.Skucode,
                        (x, y) => new { x, y }).Join(db.GtEcapcd,
                        a => a.x.PackUnit,
                        p => p.ApplicationCode, (a, p) => new { a, p })
                        .Select(r => new DO_ItemCodes
                        {
                            Skuid = r.a.y.Skuid,
                            ItemGroup = r.a.x.ItemGroup,
                            ItemCategory = r.a.x.ItemCategory,
                            ItemSubCategory = r.a.x.ItemSubCategory,
                            Skutype = r.a.y.Skutype,
                            ItemCode = r.a.x.ItemCode,
                            ItemDescription = r.a.x.ItemDescription,
                            UnitOfMeasure = r.a.x.UnitOfMeasure,
                            PackUnit = r.a.x.PackUnit,
                            PackUnitDesc = r.p.CodeDesc,
                            PackSize = r.a.x.PackSize,
                            InventoryClass = r.a.x.InventoryClass,
                            ItemClass = r.a.x.ItemClass,
                            ItemSource = r.a.x.ItemSource,
                            ItemCriticality = r.a.x.ItemCriticality,
                            BarCodeID = r.a.x.BarcodeCodeId,
                            UsageStatus = r.a.x.UsageStatus,
                            ActiveStatus = r.a.x.ActiveStatus,
                            Skucode=r.a.y.Skucode

                        }).ToListAsync();
                    foreach (var obj in result)
                    {
                        if (obj.InventoryClass == "S")
                        {
                            obj.InventoryClass = "Stockable";
                        }
                        else
                        {
                            obj.InventoryClass = "Non-Stockable";
                        }

                        if (obj.ItemClass == "B")
                        {
                            obj.ItemClass = "Bought Out";
                        }
                        else if (obj.ItemClass == "C")
                        {
                            obj.ItemClass = "Consignment";
                        }
                        else if (obj.ItemClass == "I")
                        {
                            obj.ItemClass = "In-House";
                        }
                        else
                        {
                            obj.ItemClass = "Sub Contract";
                        }

                        if (obj.ItemSource == "L")
                        {
                            obj.ItemSource = "Local";
                        }
                        else if (obj.ItemSource == "I")
                        {
                            obj.ItemSource = "Imported";
                        }
                        else
                        {
                            obj.ItemSource = "Out Station";
                        }

                        if (obj.ItemCriticality == "D")
                        {
                            obj.ItemCriticality = "Desirable";
                        }
                        else if (obj.ItemCriticality == "E")
                        {
                            obj.ItemCriticality = "Essential";
                        }
                        else
                        {
                            obj.ItemCriticality = "Vital";
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ItemCodes> GetItemParameterList(int ItemCode)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var ds = db.GtEiitcd
                        .Where(w => w.ItemCode == ItemCode)
                        .Select(r => new DO_ItemCodes
                        {
                            l_FormParameter = r.GtEipait.Select(p => new DO_eSyaParameter
                            {
                                ParameterID = p.ParameterId,
                                ParmAction = p.ParmAction,
                                ParmPerct = p.ParmPerc,
                                ParmDesc = p.ParmDesc,
                                ParmValue = p.ParmValue
                            }).ToList()
                        }).FirstOrDefaultAsync();

                    return await ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertItemCodes(DO_ItemCodes itemCodes)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEiitcd is_itemDescExists = db.GtEiitcd.FirstOrDefault(u => u.ItemDescription.ToUpper().Replace(" ", "") == itemCodes.ItemDescription.ToUpper().Replace(" ", ""));
                        if (is_itemDescExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Item Description already Exists." };

                        }

                        int _itemCode = db.GtEiitcd.Select(c => c.ItemCode).DefaultIfEmpty().Max();
                        _itemCode = _itemCode + 1;

                        var b_Entity = new GtEiitcd
                        {
                            ItemCode = _itemCode,
                            ItemGroup = itemCodes.ItemGroup,
                            ItemCategory = itemCodes.ItemCategory,
                            ItemSubCategory = itemCodes.ItemSubCategory,
                            ItemDescription = itemCodes.ItemDescription,
                            UnitOfMeasure = itemCodes.UnitOfMeasure,
                            PackUnit = itemCodes.PackUnit,
                            PackSize = itemCodes.PackSize,
                            InventoryClass = itemCodes.InventoryClass,
                            ItemClass = itemCodes.ItemClass,
                            ItemSource = itemCodes.ItemSource,
                            ItemCriticality = itemCodes.ItemCriticality,
                            IsBusinessLinked = false,
                            IsHsnlinked = false,
                            BarcodeCodeId = itemCodes.BarCodeID,
                            UsageStatus = false,
                            ActiveStatus = itemCodes.ActiveStatus,
                            FormId = itemCodes.FormID,
                            CreatedBy = itemCodes.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = itemCodes.TerminalID
                        };
                        db.GtEiitcd.Add(b_Entity);

                        foreach (DO_eSyaParameter ip in itemCodes.l_FormParameter)
                        {
                            var pMaster = new GtEipait
                            {
                                ItemCode = _itemCode,
                                ParameterId = ip.ParameterID,
                                ParmPerc = ip.ParmPerct,
                                ParmAction = ip.ParmAction,
                                ParmDesc = ip.ParmDesc,
                                ParmValue = ip.ParmValue,
                                ActiveStatus = ip.ActiveStatus,
                                FormId = itemCodes.FormID,
                                CreatedBy = itemCodes.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = itemCodes.TerminalID
                            };
                            db.GtEipait.Add(pMaster);
                        }
                        await db.SaveChangesAsync();

                        int _skuid = db.GtEskucd.Select(c => c.Skuid).DefaultIfEmpty().Max();
                        _skuid = _skuid + 1;

                        var sku_Entity = new GtEskucd
                        {
                            Skuid = _skuid,
                            Skutype = itemCodes.Skutype,
                            Skucode = _itemCode,
                            ActiveStatus = itemCodes.ActiveStatus,
                            FormId = itemCodes.FormID,
                            CreatedBy = itemCodes.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = itemCodes.TerminalID
                        };
                        db.GtEskucd.Add(sku_Entity);
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = "Item Created Successfully." };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonRepository.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateItemCodes(DO_ItemCodes itemCodes)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                       GtEiitcd is_ItemExists = db.GtEiitcd.FirstOrDefault(be => be.ItemDescription.ToUpper().Replace(" ", "") == itemCodes.ItemDescription.ToUpper().Replace(" ", "") && be.ItemCode != itemCodes.ItemCode);
                        if (is_ItemExists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Item Description already Exists." };

                        }

                        GtEiitcd b_Entity = db.GtEiitcd.Where(be => be.ItemCode == itemCodes.ItemCode).FirstOrDefault();
                        if (b_Entity != null)
                        {
                            b_Entity.ItemGroup = itemCodes.ItemGroup;
                            b_Entity.ItemCategory = itemCodes.ItemCategory;
                            b_Entity.ItemSubCategory = itemCodes.ItemSubCategory;
                            b_Entity.ItemDescription = itemCodes.ItemDescription;
                            b_Entity.UnitOfMeasure = itemCodes.UnitOfMeasure;
                            b_Entity.PackUnit = itemCodes.PackUnit;
                            b_Entity.PackSize = itemCodes.PackSize;
                            b_Entity.InventoryClass = itemCodes.InventoryClass;
                            b_Entity.ItemClass = itemCodes.ItemClass;
                            b_Entity.ItemSource = itemCodes.ItemSource;
                            b_Entity.ItemCriticality = itemCodes.ItemCriticality;
                            b_Entity.BarcodeCodeId = itemCodes.BarCodeID;
                            b_Entity.ActiveStatus = itemCodes.ActiveStatus;
                            b_Entity.ModifiedBy = itemCodes.UserID;
                            b_Entity.ModifiedOn = System.DateTime.Now;
                            b_Entity.ModifiedTerminal = itemCodes.TerminalID;

                            await db.SaveChangesAsync();
                           
                            foreach (DO_eSyaParameter ip in itemCodes.l_FormParameter)
                            {
                                GtEipait sPar = db.GtEipait.Where(x => x.ItemCode == itemCodes.ItemCode && x.ParameterId == ip.ParameterID).FirstOrDefault();
                                if (sPar != null)
                                {
                                    sPar.ParmAction = ip.ParmAction;
                                    sPar.ParmDesc = ip.ParmDesc;
                                    sPar.ParmPerc = ip.ParmPerct;
                                    sPar.ParmValue = ip.ParmValue;
                                    sPar.ActiveStatus = itemCodes.ActiveStatus;
                                    sPar.ModifiedBy = itemCodes.UserID;
                                    sPar.ModifiedOn = System.DateTime.Now;
                                    sPar.ModifiedTerminal = itemCodes.TerminalID;
                                }
                                else
                                {
                                    var pMaster = new GtEipait
                                    {
                                        ItemCode = itemCodes.ItemCode,
                                        ParameterId = ip.ParameterID,
                                        ParmPerc = ip.ParmPerct,
                                        ParmAction = ip.ParmAction,
                                        ParmDesc = ip.ParmDesc,
                                        ParmValue = ip.ParmValue,
                                        ActiveStatus = ip.ActiveStatus,
                                        FormId = itemCodes.FormID,
                                        CreatedBy = itemCodes.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = itemCodes.TerminalID
                                    };
                                    db.GtEipait.Add(pMaster);
                                }
                                await db.SaveChangesAsync();
                            }
                            GtEskucd _sku = db.GtEskucd.FirstOrDefault(x => x.Skuid == itemCodes.Skuid && x.Skucode== itemCodes.Skucode);
                            if (_sku != null)
                            {
                                _sku.Skutype = itemCodes.Skutype;
                                _sku.Skucode = itemCodes.Skucode;
                                _sku.ActiveStatus = itemCodes.ActiveStatus;
                                _sku.ModifiedBy = itemCodes.UserID;
                                _sku.ModifiedOn = System.DateTime.Now;
                                _sku.ModifiedTerminal = itemCodes.TerminalID;
                            }
                            await db.SaveChangesAsync();
                            
                            dbContext.Commit();

                            return new DO_ReturnParameter() { Status = true, Message = "Item Updated Successfully." };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Item not found." };

                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonRepository.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        //public async Task<DO_ReturnParameter> InsertItemCodes(DO_ItemCodes itemCodes)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                int _itemCode = db.GtEskucd.Select(c => c.Skuid).DefaultIfEmpty().Max();
        //                _itemCode = _itemCode + 1;

        //                var sku_Entity = new GtEskucd
        //                {
        //                    Skuid=_itemCode,
        //                    Skugroup=itemCodes.ItemGroup,
        //                    Skucategory=itemCodes.ItemCategory,
        //                    SkusubCategory=itemCodes.ItemSubCategory,
        //                    Skutype=itemCodes.Skutype,
        //                    ActiveStatus=itemCodes.ActiveStatus,
        //                    FormId=itemCodes.FormID,
        //                    CreatedBy=itemCodes.UserID,
        //                    CreatedOn=System.DateTime.Now,
        //                    CreatedTerminal=itemCodes.TerminalID
        //                };
        //                db.GtEskucd.Add(sku_Entity);

        //                GtEiitcd is_itemDescExists = db.GtEiitcd.FirstOrDefault(u => u.ItemDescription.ToUpper().Replace(" ", "") == itemCodes.ItemDescription.ToUpper().Replace(" ", ""));
        //                if (is_itemDescExists != null)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, Message = "Item Description already Exists." };

        //                }

        //                //int _itemCode = db.GtEiitcd.Select(c => c.ItemCode).DefaultIfEmpty().Max();
        //                //_itemCode = _itemCode + 1;

        //                var b_Entity = new GtEiitcd
        //                {
        //                    ItemCode = _itemCode,
        //                    //ItemGroup = itemCodes.ItemGroup,
        //                    //ItemCategory = itemCodes.ItemCategory,
        //                    //ItemSubCategory = itemCodes.ItemSubCategory,
        //                    ItemDescription = itemCodes.ItemDescription,
        //                    UnitOfMeasure = itemCodes.UnitOfMeasure,
        //                    PackUnit = itemCodes.PackUnit,
        //                    PackSize = itemCodes.PackSize,
        //                    InventoryClass = itemCodes.InventoryClass,
        //                    ItemClass = itemCodes.ItemClass,
        //                    ItemSource = itemCodes.ItemSource,
        //                    ItemCriticality = itemCodes.ItemCriticality,
        //                    IsBusinessLinked = false,
        //                    IsHsnlinked = false,
        //                    BarcodeCodeId = itemCodes.BarCodeID,
        //                    UsageStatus = false,
        //                    ActiveStatus = itemCodes.ActiveStatus,
        //                    FormId = itemCodes.FormID,
        //                    CreatedBy = itemCodes.UserID,
        //                    CreatedOn = System.DateTime.Now,
        //                    CreatedTerminal = itemCodes.TerminalID
        //                };
        //                db.GtEiitcd.Add(b_Entity);

        //                foreach (DO_eSyaParameter ip in itemCodes.l_FormParameter)
        //                {
        //                    var pMaster = new GtEipait
        //                    {
        //                        ItemCode = _itemCode,
        //                        ParameterId = ip.ParameterID,
        //                        ParmPerc = ip.ParmPerct,
        //                        ParmAction = ip.ParmAction,
        //                        ParmDesc = ip.ParmDesc,
        //                        ParmValue = ip.ParmValue,
        //                        ActiveStatus = ip.ActiveStatus,
        //                        FormId = itemCodes.FormID,
        //                        CreatedBy = itemCodes.UserID,
        //                        CreatedOn = System.DateTime.Now,
        //                        CreatedTerminal = itemCodes.TerminalID
        //                    };
        //                    db.GtEipait.Add(pMaster);
        //                }
        //                await db.SaveChangesAsync();
        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, Message = "Item Created Successfully." };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonRepository.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<DO_ReturnParameter> UpdateItemCodes(DO_ItemCodes itemCodes)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                GtEskucd _sku = db.GtEskucd.FirstOrDefault(x => x.Skuid == itemCodes.ItemCode);
        //                if (_sku != null)
        //                {
        //                    _sku.Skugroup = itemCodes.ItemGroup;
        //                    _sku.Skucategory = itemCodes.ItemCategory;
        //                    _sku.SkusubCategory = itemCodes.ItemSubCategory;
        //                    _sku.Skutype = itemCodes.Skutype;
        //                    _sku.ActiveStatus = itemCodes.ActiveStatus;
        //                    _sku.ModifiedBy = itemCodes.UserID;
        //                    _sku.ModifiedOn = System.DateTime.Now;
        //                    _sku.ModifiedTerminal = itemCodes.TerminalID;
        //                }
        //                await db.SaveChangesAsync();

        //                GtEiitcd is_ItemExists = db.GtEiitcd.FirstOrDefault(be => be.ItemDescription.ToUpper().Replace(" ", "") == itemCodes.ItemDescription.ToUpper().Replace(" ", "") && be.ItemCode != itemCodes.ItemCode);
        //                if (is_ItemExists != null)
        //                {
        //                    return new DO_ReturnParameter() { Status = false, Message = "Item Description already Exists." };

        //                }

        //                GtEiitcd b_Entity = db.GtEiitcd.Where(be => be.ItemCode == itemCodes.ItemCode).FirstOrDefault();
        //                if (b_Entity != null)
        //                {
        //                    //b_Entity.ItemGroup = itemCodes.ItemGroup;
        //                    //b_Entity.ItemCategory = itemCodes.ItemCategory;
        //                    //b_Entity.ItemSubCategory = itemCodes.ItemSubCategory;
        //                    b_Entity.ItemDescription = itemCodes.ItemDescription;
        //                    b_Entity.UnitOfMeasure = itemCodes.UnitOfMeasure;
        //                    b_Entity.PackUnit = itemCodes.PackUnit;
        //                    b_Entity.PackSize = itemCodes.PackSize;
        //                    b_Entity.InventoryClass = itemCodes.InventoryClass;
        //                    b_Entity.ItemClass = itemCodes.ItemClass;
        //                    b_Entity.ItemSource = itemCodes.ItemSource;
        //                    b_Entity.ItemCriticality = itemCodes.ItemCriticality;
        //                    b_Entity.BarcodeCodeId = itemCodes.BarCodeID;
        //                    b_Entity.ActiveStatus = itemCodes.ActiveStatus;
        //                    b_Entity.ModifiedBy = itemCodes.UserID;
        //                    b_Entity.ModifiedOn = System.DateTime.Now;
        //                    b_Entity.ModifiedTerminal = itemCodes.TerminalID;

        //                    await db.SaveChangesAsync();
        //                    //if (itemCodes.ActiveStatus)
        //                    //{
        //                    foreach (DO_eSyaParameter ip in itemCodes.l_FormParameter)
        //                    {
        //                        GtEipait sPar = db.GtEipait.Where(x => x.ItemCode == itemCodes.ItemCode && x.ParameterId == ip.ParameterID).FirstOrDefault();
        //                        if (sPar != null)
        //                        {
        //                            sPar.ParmAction = ip.ParmAction;
        //                            sPar.ParmDesc = ip.ParmDesc;
        //                            sPar.ParmPerc = ip.ParmPerct;
        //                            sPar.ParmValue = ip.ParmValue;
        //                            sPar.ActiveStatus = itemCodes.ActiveStatus;
        //                            sPar.ModifiedBy = itemCodes.UserID;
        //                            sPar.ModifiedOn = System.DateTime.Now;
        //                            sPar.ModifiedTerminal = itemCodes.TerminalID;
        //                        }
        //                        else
        //                        {
        //                            var pMaster = new GtEipait
        //                            {
        //                                ItemCode = itemCodes.ItemCode,
        //                                ParameterId = ip.ParameterID,
        //                                ParmPerc = ip.ParmPerct,
        //                                ParmAction = ip.ParmAction,
        //                                ParmDesc = ip.ParmDesc,
        //                                ParmValue = ip.ParmValue,
        //                                ActiveStatus = ip.ActiveStatus,
        //                                FormId = itemCodes.FormID,
        //                                CreatedBy = itemCodes.UserID,
        //                                CreatedOn = System.DateTime.Now,
        //                                CreatedTerminal = itemCodes.TerminalID
        //                            };
        //                            db.GtEipait.Add(pMaster);
        //                        }
        //                        await db.SaveChangesAsync();
        //                    }
        //                    //}
        //                    //else
        //                    //{
        //                    //    var fa = await db.GtEipait.Where(w => w.ItemCode == itemCodes.ItemCode).ToListAsync();

        //                    //    foreach (GtEipait f in fa)
        //                    //    {
        //                    //        f.ParmAction = false;
        //                    //        f.ActiveStatus = false;
        //                    //        f.ModifiedBy = itemCodes.UserID;
        //                    //        f.ModifiedOn = System.DateTime.Now;
        //                    //        f.ModifiedTerminal = itemCodes.TerminalID;
        //                    //    }
        //                    //    await db.SaveChangesAsync();
        //                    //}

        //                    dbContext.Commit();

        //                    return new DO_ReturnParameter() { Status = true, Message = "Item Updated Successfully." };
        //                }
        //                else
        //                {
        //                    return new DO_ReturnParameter() { Status = false, Message = "Item not found." };

        //                }
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonRepository.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}


        #region Item Business Link

        public async Task<List<DO_ItemBusinessLink>> GetCustodianStore(int BusinessKey, int ItemCode)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = await db.GtEcstrm.Where(x=> x.ActiveStatus == true)

                                  .Select(s => new DO_ItemBusinessLink
                                  {
                                      CustodianStore = s.StoreCode,
                                      StoreDesc = s.StoreDesc,
                                      ActiveStatus = false
                                  }).ToListAsync();

                    foreach (var obj in result)
                    {
                        GtEiitbl getCustodianStore = db.GtEiitbl.Where(c => c.BusinessKey == BusinessKey && c.ItemCode == ItemCode && c.CustodianStore == obj.CustodianStore).FirstOrDefault();
                        if (getCustodianStore != null)
                        {
                            obj.ActiveStatus = getCustodianStore.ActiveStatus;
                        }
                        else
                        {
                            obj.ActiveStatus = false;
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<DO_ItemConfigurartion> GetItemTree()
        //{
        //    try
        //    {
        //        using (eSyaEnterprise db = new eSyaEnterprise())
        //        {
        //            DO_ItemConfigurartion ic = new DO_ItemConfigurartion();

        //            ic.l_ItemGroup = await db.GtEskucd.Where(w => w.ActiveStatus == true)
        //                                .Join(db.GtEiitgr,
        //                                 x => x.Skugroup,
        //                                 y => y.ItemGroup,
        //                                (x, y) => new { x, y })
        //                            .Select(m => new DO_ItemGroup()
        //                            {
        //                                ItemGroupId = m.x.Skugroup,
        //                                ItemGroupDesc = m.y.ItemGroupDesc,
        //                                ActiveStatus = m.x.ActiveStatus
        //                            }).Distinct().ToListAsync();

        //            ic.l_ItemCategory = await db.GtEskucd.Where(w => w.ActiveStatus == true)
        //                               .Join(db.GtEiitct,
        //                                h => h.Skucategory,
        //                                k => k.ItemCategory,
        //                               (h, k) => new { h, k })
        //                           .Select(s => new DO_ItemCategory()
        //                            {
        //                                ItemGroupId = s.h.Skugroup,
        //                                ItemCategory = s.h.Skucategory,
        //                                ItemCategoryDesc = s.k.ItemCategoryDesc,
        //                                ActiveStatus = s.h.ActiveStatus
        //                            }).Distinct().ToListAsync();

        //            ic.l_ItemSubCategory = await db.GtEskucd.Where(w => w.SkusubCategory != 0 &&  w.ActiveStatus == true)
        //                .Join(db.GtEiitsc,
        //                 o => o.SkusubCategory,
        //                 p => p.ItemSubCategory,
        //                (o, p) =>new {o,p })
        //                .Select(r=> new DO_ItemSubCategory
        //                {
        //                    ItemGroupId =r.o.Skugroup,
        //                    ItemCategory =r.o.Skucategory,
        //                    ItemSubCategory =r.o.SkusubCategory,
        //                    ItemSubCategoryDesc =r.p.ItemSubCategoryDesc,
        //                    ActiveStatus =r.o.ActiveStatus
        //                }).Distinct().ToListAsync();

        //            ic.l_ItemCodes = await db.GtEskucd.Where(w => w.ActiveStatus == true)
        //                 .Join(db.GtEiitcd,
        //                 t => t.Skuid,
        //                 u => u.ItemCode,
        //                (t, u) => new { t, u })
        //                            .Select(s => new DO_ItemCodes()
        //                            {
        //                                ItemGroup = s.t.Skugroup,
        //                                ItemCategory = s.t.Skucategory,
        //                                ItemSubCategory = s.t.SkusubCategory,
        //                                ItemCode = s.t.Skuid,
        //                                ItemDescription = s.u.ItemDescription,
        //                                ActiveStatus = s.t.ActiveStatus
        //                            }).ToListAsync();

        //            return ic;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<DO_ItemConfigurartion> GetItemTree()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    DO_ItemConfigurartion ic = new DO_ItemConfigurartion();

                    ic.l_ItemGroup = await db.GtEiitcd.Where(w => w.ActiveStatus == true)
                                        .Join(db.GtEiitgr,
                                         x => x.ItemGroup,
                                         y => y.ItemGroup,
                                        (x, y) => new { x, y })
                                    .Select(m => new DO_ItemGroup()
                                    {
                                        ItemGroupId = m.x.ItemGroup,
                                        ItemGroupDesc = m.y.ItemGroupDesc,
                                        ActiveStatus = m.x.ActiveStatus
                                    }).Distinct().ToListAsync();

                    ic.l_ItemCategory = await db.GtEiitcd.Where(w => w.ActiveStatus == true)
                                       .Join(db.GtEiitct,
                                        h => h.ItemCategory,
                                        k => k.ItemCategory,
                                       (h, k) => new { h, k })
                                   .Select(s => new DO_ItemCategory()
                                   {
                                       ItemGroupId = s.h.ItemGroup,
                                       ItemCategory = s.h.ItemCategory,
                                       ItemCategoryDesc = s.k.ItemCategoryDesc,
                                       ActiveStatus = s.h.ActiveStatus
                                   }).Distinct().ToListAsync();

                    ic.l_ItemSubCategory = await db.GtEiitcd.Where(w => w.ItemSubCategory != 0 && w.ActiveStatus == true)
                        .Join(db.GtEiitsc,
                         o => o.ItemSubCategory,
                         p => p.ItemSubCategory,
                        (o, p) => new { o, p })
                        .Select(r => new DO_ItemSubCategory
                        {
                            ItemGroupId = r.o.ItemGroup,
                            ItemCategory = r.o.ItemCategory,
                            ItemSubCategory = r.o.ItemSubCategory,
                            ItemSubCategoryDesc = r.p.ItemSubCategoryDesc,
                            ActiveStatus = r.o.ActiveStatus
                        }).Distinct().ToListAsync();

                    ic.l_ItemCodes = await db.GtEiitcd.Where(w => w.ActiveStatus == true)
                         .Join(db.GtEskucd,
                         t => t.ItemCode,
                         u => u.Skucode,
                        (t, u) => new { t, u })
                                    .Select(s => new DO_ItemCodes()
                                    {
                                        ItemGroup = s.t.ItemGroup,
                                        ItemCategory = s.t.ItemCategory,
                                        ItemSubCategory = s.t.ItemSubCategory,
                                        //ItemCode = s.t.Skuid,
                                        ItemCode = s.u.Skucode,
                                        ItemDescription = s.t.ItemDescription,
                                        ActiveStatus = s.t.ActiveStatus
                                    }).ToListAsync();

                    return ic;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ItemAccountingStore>> GetAccountingStoreForItem(int businesskey, int itemcode, string accountingType)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = await db.GtEcstrm
                        .Join(db.GtEastbl.Where(w => w.BusinessKey == businesskey
                            && w.ActiveStatus),
                            s => s.StoreCode,
                            b => b.StoreCode,
                            (s, b) => new { s, b })
                        .GroupJoin(db.GtEiitas.Where(w => w.ItemCode == itemcode && w.AccountingType == accountingType),
                            sb => new { sb.b.BusinessKey, sb.b.StoreCode },
                            a => new { a.BusinessKey, a.StoreCode },
                            (sb, a) => new { sb, a = a.FirstOrDefault() })
                        .Where(x => x.sb.b.BusinessKey == businesskey && x.sb.s.ActiveStatus == true)
                        .Select(r => new DO_ItemAccountingStore
                        {
                            AccountingType = accountingType,
                            StoreCode = r.sb.s.StoreCode,
                            StoreDesc = r.sb.s.StoreDesc,
                            ActiveStatus = r.a != null ? r.a.ActiveStatus : false
                        }).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertAccountingStoreForItem(List<DO_ItemAccountingStore> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        foreach (var cs in obj)
                        {
                            GtEiitas s = db.GtEiitas.Where(x => x.BusinessKey == cs.BusinessKey && x.ItemCode == cs.ItemCode && x.StoreCode==cs.StoreCode && x.AccountingType.Trim() == cs.AccountingType.Trim()).FirstOrDefault();
                            if (s != null)
                            {
                                db.GtEiitas.Remove(s);
                                await db.SaveChangesAsync();
                            }
                        }

                        if (obj.Count > 0)
                        {
                            foreach (var i in obj.Where(x=>x.ActiveStatus==true))
                            {
                                GtEiitas item = new GtEiitas
                                {
                                    BusinessKey = i.BusinessKey,
                                    ItemCode = i.ItemCode,
                                    AccountingType = i.AccountingType,
                                    StoreCode = i.StoreCode,
                                    ActiveStatus = i.ActiveStatus,
                                    FormId = i.FormId,
                                    CreatedBy = i.UserID,
                                    CreatedOn = DateTime.Now,
                                    CreatedTerminal = i.TerminalID
                                };
                                    db.GtEiitas.Add(item);                                
                                await db.SaveChangesAsync();
                            }
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, Message = "Save/Updated Successfully." };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "No Custodian Store Seleted to Save/Update." };

                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonRepository.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }


        public async Task<List<DO_StoreMaster>> GetCustodianStoreListbyAccountingStore(int Businesskey, int Itemcode, int accountingcode)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = await db.GtEiitbl.Where(x => x.BusinessKey==Businesskey && x.ItemCode==Itemcode && x.AccountingStore==accountingcode && x.ActiveStatus==true)

                                  .Select(s => new DO_StoreMaster
                                  {
                                      CustodianStore = s.CustodianStore,
                                      ActiveStatus = s.ActiveStatus
                                  }).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateItemCodetoCustodianStore(List<DO_ItemCodeLinkCustodianStore> obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        foreach(var cs in obj)
                        {
                            GtEiitbl s = db.GtEiitbl.Where(x => x.BusinessKey == cs.BusinessKey && x.ItemCode == cs.ItemCode && x.AccountingStore == cs.AccountingStore).FirstOrDefault();
                            if (s != null)
                            {
                                db.GtEiitbl.Remove(s);
                                await db.SaveChangesAsync();
                            }
                        }

                        if (obj.Count>0)
                        {
                            foreach (var store in obj)
                            {
                                GtEiitbl item = new GtEiitbl
                                {
                                    BusinessKey = store.BusinessKey,
                                    ItemCode = store.ItemCode,
                                    AccountingStore = store.AccountingStore,
                                    CustodianStore= store.CustodianStore,
                                    ActiveStatus = store.ActiveStatus,
                                    FormId = store.FormId,
                                    CreatedBy = store.UserID,
                                    CreatedOn = DateTime.Now,
                                    CreatedTerminal = store.TerminalID
                                };
                                db.GtEiitbl.Add(item);
                                await db.SaveChangesAsync();
                            }
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, Message = "Save/Updated Successfully." };
                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "No Custodian Store Seleted to Save/Update." };

                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonRepository.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<List<DO_ItemAccountingStore>> GetStoreClassStorebyBusinessKey(int businesskey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = await db.GtEastbl.Where(w => w.BusinessKey == businesskey && w.ActiveStatus)
                        .Join(db.GtEcstrm,
                            s => s.StoreCode,
                            b => b.StoreCode,
                            (s, b) => new { s, b })
                            .Select(r => new DO_ItemAccountingStore
                         {
                            AccountingType = r.s.StoreClass,
                            StoreCode = r.s.StoreCode,
                            StoreDesc = r.b.StoreDesc,
                            ActiveStatus =false
                        }).ToListAsync();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ItemAccountingStore>> GetCustodianStoreListbyAccountType(int Businesskey, int Itemcode, string actype)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = await db.GtEiitas.Where(x => x.BusinessKey == Businesskey && x.ItemCode == Itemcode && x.AccountingType.Trim() == actype.Trim())

                                  .Select(s => new DO_ItemAccountingStore
                                  {
                                      BusinessKey = s.BusinessKey,
                                      ItemCode=s.ItemCode,
                                      StoreCode=s.StoreCode,
                                      AccountingType=s.AccountingType,
                                      ActiveStatus = s.ActiveStatus
                                  }).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Item Business Link

        #region Item Reorder Level

        public async Task<List<DO_ItemReorderLevel>> GetItemReorderLevel(int BusinessKey, int StoreCode)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var query = await db.GtEiitro.Where(x => x.BusinessKey == BusinessKey && x.StoreCode == StoreCode && x.ActiveStatus == true)
                        .Select(r => new DO_ItemReorderLevel
                        {
                            ItemCode = r.ItemCode,
                        }).ToListAsync();

                    var result1 = await db.GtEiitro.Where(x => x.BusinessKey == BusinessKey && x.StoreCode == StoreCode && x.ActiveStatus == true)
                        .GroupJoin(db.GtEiitcd,
                         x => x.ItemCode,
                         y => y.ItemCode,
                         (x, y) => new { x, y = y.FirstOrDefault() }).DefaultIfEmpty()
                        .Select(r => new DO_ItemReorderLevel
                        {
                            BusinessKey = r.x.BusinessKey,
                            ItemCode = r.x.ItemCode,
                            ItemDescription = r.y.ItemDescription,
                            StoreCode = r.x.StoreCode,
                            MaximumStockLevel = r.x.MaximumStockLevel,
                            ReorderLevel = r.x.ReorderLevel,
                            SafetyStockLevel = r.x.SafetyStockLevel,
                            MinimumStockLevel = r.x.MinimumStockLevel,
                            ActiveStatus = r.x.ActiveStatus
                        }).ToListAsync();

                    var result = await db.GtEiitcd.Where(x => x.ActiveStatus == true)
                        .Select(r => new DO_ItemReorderLevel
                        {
                            BusinessKey = BusinessKey,
                            ItemCode = r.ItemCode,
                            ItemDescription = r.ItemDescription,
                            StoreCode = StoreCode,
                            MaximumStockLevel = 0,
                            ReorderLevel = 0,
                            SafetyStockLevel = 0,
                            MinimumStockLevel = 0,
                            ActiveStatus = r.ActiveStatus
                        }).ToListAsync();//.Except(query).ToListAsync();
                                         //.Union(result1).ToListAsync();

                    var customers = result.Where(c => !query.Select(fc => fc.ItemCode).Contains(c.ItemCode)).ToList();

                    var final = customers.Union(result1).OrderBy(x => x.ItemCode).ToList();

                    return final;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateItemReorderLevel(List<DO_ItemReorderLevel> obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_ItemReorderLevel = obj.Where(w => !String.IsNullOrEmpty(w.ItemDescription)).Count();
                        if (is_ItemReorderLevel <= 0)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Please Enter Item Reorder Level Details." };
                        }

                        foreach (var sd in obj.Where(w => !String.IsNullOrEmpty(w.ItemDescription)))
                        {
                            GtEiitro cs_sd = db.GtEiitro.Where(x => x.BusinessKey == sd.BusinessKey
                                            && x.StoreCode == sd.StoreCode && x.ItemCode == sd.ItemCode).FirstOrDefault();
                            if (cs_sd == null)
                            {
                                var o_cssd = new GtEiitro
                                {
                                    BusinessKey = sd.BusinessKey,
                                    StoreCode = sd.StoreCode,
                                    ItemCode = sd.ItemCode,
                                    MaximumStockLevel = sd.MaximumStockLevel,
                                    ReorderLevel = sd.ReorderLevel,
                                    SafetyStockLevel = sd.SafetyStockLevel,
                                    MinimumStockLevel = sd.MinimumStockLevel,
                                    ActiveStatus = sd.ActiveStatus,
                                    FormId = sd.FormID,
                                    CreatedBy = sd.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = sd.TerminalID
                                };
                                db.GtEiitro.Add(o_cssd);
                            }
                            else
                            {
                                cs_sd.MaximumStockLevel = sd.MaximumStockLevel;
                                cs_sd.ReorderLevel = sd.ReorderLevel;
                                cs_sd.SafetyStockLevel = sd.SafetyStockLevel;
                                cs_sd.MinimumStockLevel = sd.MinimumStockLevel;
                                cs_sd.ActiveStatus = sd.ActiveStatus;
                                cs_sd.ModifiedBy = sd.UserID;
                                cs_sd.ModifiedOn = System.DateTime.Now;
                                cs_sd.ModifiedTerminal = sd.TerminalID;
                            }
                            await db.SaveChangesAsync();
                        }

                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = "Item Reorder Level Created Successfully." };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonRepository.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        #endregion Item Reorder Level
    }
}
