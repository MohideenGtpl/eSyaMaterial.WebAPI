using HCP.Material.DL.Entities;
using HCP.Material.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HCP.Material.IF;
using HCP.Material.DL.Entities;

namespace HCP.Material.DL.Repository
{
    public class CommonRepository : ICommonRepository
    {
        public static string GetValidationMessageFromException(DbUpdateException ex)
        {
            string msg = ex.InnerException == null ? ex.ToString() : ex.InnerException.Message;

            if (msg.LastIndexOf(',') == msg.Length - 1)
                msg = msg.Remove(msg.LastIndexOf(','));
            return msg;
        }

        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcapcd
                        .Where(w => w.CodeType == codeType && w.ActiveStatus)
                        .Select(r => new DO_ApplicationCodes
                        {
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcapcd
                        .Where(w => w.ActiveStatus
                        && l_codeType.Contains(w.CodeType))
                        .Select(r => new DO_ApplicationCodes
                        {
                            CodeType = r.CodeType,
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ItemGroup>> GetItemGroup()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEiitgr
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_ItemGroup
                        {
                            ItemGroupId = r.ItemGroup,
                            ItemGroupDesc = r.ItemGroupDesc
                        }).OrderBy(o => o.ItemGroupDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ItemCategory>> GetItemCategory(int ItemGroup)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEiitgc
                        .Where(w => w.ItemGroup == ItemGroup && w.ActiveStatus)
                        .Select(r => new DO_ItemCategory
                        {
                            ItemCategory = r.ItemCategory,
                            ItemCategoryDesc = r.ItemCategoryNavigation.ItemCategoryDesc,
                        }).OrderBy(o => o.ItemCategoryDesc).Distinct().ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ItemSubCategory>> GetItemSubCategory(int ItemCategory)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEiitsc
                        .Where(w => w.ItemCategory == ItemCategory && w.ActiveStatus)
                        .Select(r => new DO_ItemSubCategory
                        {
                            ItemSubCategory = r.ItemSubCategory,
                            ItemSubCategoryDesc = r.ItemSubCategoryDesc,
                        }).OrderBy(o => o.ItemSubCategoryDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ItemGroupCategoryLink>> GetItemCategoryForItemGroup(int ItemGroup)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEiitgc
                        .Where(w => w.ItemGroup == ItemGroup && w.ActiveStatus)
                        .Select(r => new DO_ItemGroupCategoryLink
                        {
                            ItemCategory = r.ItemCategory,
                            ItemSubCategory = r.ItemSubCategory
                        }).OrderBy(o => o.ItemCategory).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_UnitsofMeasure>> GetUnitofMeasure()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEciuom
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_UnitsofMeasure
                        {
                            UnitOfMeasure = r.UnitOfMeasure,
                            Uompdesc = r.Uompdesc
                        }).OrderBy(o => o.Uompdesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_BusinessLocation>> GetBusinessKey()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbsln
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.BusinessKey,
                            LocationDescription = r.BusinessName + " - " + r.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_StoreMaster>> GetAccountingStore()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEcstrm.Where(x => x.StoreType == "M" && x.ActiveStatus == true)

                                  .Select(s => new DO_StoreMaster
                                  {
                                      StoreCode = s.StoreCode,
                                      StoreDesc = s.StoreDesc
                                  }).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_StoreMaster>> GetCustodianStore()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEcstrm.Where(x=> x.ActiveStatus == true)

                                  .Select(s => new DO_StoreMaster
                                  {
                                      CustodianStore = s.StoreCode,
                                      StoreDesc = s.StoreDesc,
                                      ActiveStatus = false
                                  }).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_StoreMaster>> GetStoreListByBusinessKey(int BusinessKey)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEastbl.Where(x => x.BusinessKey == BusinessKey && x.ActiveStatus == true)
                        .GroupJoin(db.GtEcstrm,
                         x => x.StoreCode,
                         y => y.StoreCode,
                         (x, y) => new { x, y = y.FirstOrDefault() }).DefaultIfEmpty()
                        .Select(r => new DO_StoreMaster
                        {
                            StoreCode = r.x.StoreCode,
                            StoreDesc = r.y.StoreDesc,
                        }).ToListAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
